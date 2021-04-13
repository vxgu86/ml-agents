using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using Unity.Barracuda;
using Unity.Barracuda.ONNX;
using UnityEngine;

namespace Unity.Barracuda.Debug
{
    [DataContract]
    public class SnapshotTensor
    {
        [DataMember]
        public int[] Shape;

        [DataMember]
        public List<byte> FloatData;

        public Tensor ToTensor()
        {
            var floats = new List<float>();
            var floatAsBytes = FloatData.ToArray();
            for (var i = 0; i < floatAsBytes.Length; i += 4)
            {
                floats.Add(BitConverter.ToSingle(floatAsBytes, i));
            }
            var t = new Tensor(Shape, floats.ToArray());
            t.name = "temp";
            return t;
        }
    }

    [DataContract]
    public class BarracudaSnapshot
    {
        [DataMember]
        public Dictionary<string, SnapshotTensor> TensorsByName;

        public static SnapshotTensor FromTensor(Tensor t)
        {
            var shape = t.shape;
            var snapTensor = new SnapshotTensor();
            snapTensor.Shape = shape.ToArray();

            var floats = t.data.Download(shape);
            var tempBytes = new List<byte>(4 * floats.Length);
            for (var i = 0; i < floats.Length; i++)
            {
                tempBytes.AddRange(BitConverter.GetBytes(floats[i]));
            }
            snapTensor.FloatData = tempBytes;
            return snapTensor;
        }

        public static BarracudaSnapshot FromTensors(Dictionary<string, Tensor> tensorsByName)
        {
            var snap = new BarracudaSnapshot();
            snap.TensorsByName = new Dictionary<string, SnapshotTensor>();

            foreach (var item in tensorsByName)
            {
                var name = item.Key;
                var tensor = item.Value;
                var snapTensor = FromTensor(tensor);
                snap.TensorsByName[name] = snapTensor;
            }

            return snap;
        }

        public static void SaveToFile(Dictionary<string, Tensor> tensorsByName, string filename)
        {
            var snapshot = FromTensors(tensorsByName);
            var fs = new FileStream(filename, FileMode.Create, FileAccess.Write);
            var jsonSettings = new DataContractJsonSerializerSettings();
            jsonSettings.UseSimpleDictionaryFormat = true;
            var ser = new DataContractJsonSerializer(typeof(BarracudaSnapshot), jsonSettings);
            ser.WriteObject(fs, snapshot);
            fs.Close();
        }

        public static Dictionary<string, Tensor> LoadFromFile(string filename)
        {
            var fs = new FileStream(filename, FileMode.Open, FileAccess.Read);

            var jsonSettings = new DataContractJsonSerializerSettings();
            jsonSettings.UseSimpleDictionaryFormat = true;

            var ser = new DataContractJsonSerializer(typeof(BarracudaSnapshot), jsonSettings);
            var snapOut = (BarracudaSnapshot)ser.ReadObject(fs);
            fs.Close();

            var tensorsOut = new Dictionary<string, Tensor>();
            foreach (var item in snapOut.TensorsByName)
            {
                var name = item.Key;
                var snapTensor = item.Value;
                var tensor = snapTensor.ToTensor();
                tensor.name = name;
                tensorsOut[name] = tensor;
            }

            return tensorsOut;
        }

        public void ExecuteRepro(string onnxFilename, string snapShotFilename)
        {
            var rawModel = File.ReadAllBytes(onnxFilename);
            var converter = new ONNXModelConverter(true);
            var onnxModel = converter.Convert(rawModel);

            // from ModelOverrider.LoadOnnxModel
            NNModelData assetData = ScriptableObject.CreateInstance<NNModelData>();
            using (var memoryStream = new MemoryStream())
            using (var writer = new BinaryWriter(memoryStream))
            {
                ModelWriter.Save(writer, onnxModel);
                assetData.Value = memoryStream.ToArray();
            }
            assetData.name = "Data";
            assetData.hideFlags = HideFlags.HideInHierarchy;

            var nnModel = ScriptableObject.CreateInstance<NNModel>();
            nnModel.modelData = assetData;

            // from ModelRunner
            var barracudaModel = ModelLoader.Load(nnModel);
            var workerConfiguration = new WorkerFactory.WorkerConfiguration(compareAgainstType: WorkerFactory.Type.CSharpRef, verbose: false, compareLogLevel: CompareOpsUtils.LogLevel.Error, compareEpsilon: 1e-3f);
            var engine = WorkerFactory.CreateWorker(WorkerFactory.Type.CSharpBurst, barracudaModel, workerConfiguration);

            var inputTensors = LoadFromFile(snapShotFilename);
            engine.Execute(inputTensors);

        }

    }
}
