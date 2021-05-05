# 入门指南

本指南逐步介绍了在Unity中打开[example environments](Learning-Environment-Examples.md)其中一个例子的端到端过程，训练智能体、将经过训练的模型嵌入到Unity环境中。
看完后应该能够训练任何示例环境。如果不熟悉[Unity Engine](https://unity3d.com/unity)，查看[Background: Unity](Background-Unity.md)页面。
此外，如果不熟悉机器学习，查看[Background: Machine Learning](Background-Machine-Learning.md)页面简要了解概述和有用的指示。

![3D Balance Ball](images/balance.png)

在本指南中，将使用**3D Balance Ball**环境，其中包含多个立方体和球，每个立方体智能体试图通过水平旋转或垂直旋转来防止其上方的球掉落。在这个环境中，立方体智能体是一个**Agent**，它接收
平衡球的每一步都会获得回报，丢球就会受负面奖励的处罚。训练过程的目标是让智能体学会平衡顶上的球。


## 安装

如果尚未安装，请按照[installation instructions](Installation.md)进行。
然后，打开包含所有示例环境的Unity项目：

1.导航至`Window -> Package Manager`，打开“包管理器”窗口。
1.导航到ML-Agents软件包后单击它。
1.找到“ 3D Ball”示例，然后单击`Import`。
1.在**Project**窗口中，转到`Assets/ML-Agents/Examples/3DBall/Scenes`文件夹并打开`3DBall`场景文件。

## 了解Unity环境

智能体是观察_环境_并与之交互的自主actor，在Unity上下文中，环境是一个包含多个Agent对象的场景，以及与Agent交互的其他实体。

![Unity Editor](images/mlagents-3DBallHierarchy.png)

**注意：**在Unity中，场景中所有事物的基础对象是_GameObject_。 GameObject本质上是其他所有内容的容器，包括行为，图形，物理behaviors, graphics, physics等。要查看组成GameObject的组件，在Scene窗口中选择GameObject，然后打开Inspector窗，会显示GameObject上的每个组件。

打开3D平衡球场景后，可能会注意到的第一件事是它包含多个立方体，场景中的每个立方体都是一个独立agent，但它们都具有相同的Behavior。这样3D平衡球场景可以加速训练，因为所有十二个agents都在
并训练。

### 智能体Agent

Agent是在环境中观察并采取行动的actor，在在3D平衡球环境中，Agent组件放置在十二个“Agent”游戏对象GameObjects。基本Agent对象具有一些会影响behavior的属性：

- **行为参数Behavior Parameters**-每个智能体都必须有Behavior。行为决定了智能体如何做出决定。
- **Max Step（最大步长**）-定义在Agent的操作结束之前可以进行多少次模拟，在3D平衡球中，智能体经过5000步后重新启动。

#### 行为参数：矢量观测空间 Behavior Parameters : Vector Observation Space

智能体根据其其状态的观察结果做出决定，向量观测是浮点数向量，其中包含智能体做决策的相关信息。

3D平衡球示例的“行为参数Behavior Parameters”的“空间大小”`Space Size`为8，即包含Agent的观察的特征向量有八个元素：立方体旋转的`x`和`z`分量，以及球的相对位置和速度的`x`, `y`, `z`分量。

#### 行为参数：动作 Behavior Parameters : Actions

智能体Agent以动作actions形式接收，ML-Agents工具包将动作分为连续continuous和离散discrete两种类型。3D平衡球示例的编程中使用连续动作，是一个浮点数的向量，可以连续变化。
进一步来说，它使用2的“空间大小”`Space Size`来控制要应用于的`x` 和 `z`轴的旋转量使球保持平衡。

## 运行预训练模型

这里为智能体已预训练了模型（`.onnx`文件），并且使用[Unity Inference Engine](Unity-Inference-Engine.md) 在Unity内运行这些模型。
这里使用3D球示例的预训练模型。

1.在**Project**窗口中，转到`Assets/ML-Agents/Examples/3DBall/Prefabs`文件夹。展开`3DBall`并单击`Agent` prefab。应该在**Inspector**中看到`Agent` prefab预制件。

   **注意**：3`3DBall`场景中的平台都是使用`3DBall`预制件创建的，改动时无需单独更新所有12个平台，只需要更新`3DBall`prefab即可。


   ![Platform Prefab](images/platform_prefab.png)

1. 在**Project**窗口中，拖动位于`Assets/ML-Agents/Examples/3DBall/TFModels`的**3DBall**模型到Agent GameObject **Inspector**窗口的`Behavior Parameters (Script)`组件的`Model`属性。

   ![3dball learning brain](images/3dball_learning_brain.png)

1. 此时**Hierarchy**窗口中每个`3DBall`下的每个`Agent`现在在`Behavior Parameters`上包含**3DBall**作为`Model`。
   **注意**：可以使用Scene Hierarchy中的搜索栏一次选择场景中的多个游戏对象来修改它们。
1. 设置用于该模型的**Inference Device**为`CPU`。
1. 单击Unity编辑器中的**Play**按钮，就能看到平台使用预训练模型平衡球。

## 通过强化学习训练新模型

尽管在此环境中提供了预训练模型，但自己创造的环境将需要从零开始的训练智能体生成新的模型文件。这里演示如何使用ML-Agents Python软件包中的强化学习算法做到这一点，提供了一个方便的命令`mlagents-learn`，接受用于配置训练和推理阶段的参数。

### 训练环境

1. 打开命令行或终端窗口。
1. 导航到克隆`ml-agents`存储库的文件夹。 
**Note**: 如果遵循默认的[installation](Installation.md)，则应能够从任何目录运行`mlagents-learn`。
1. 运行`mlagents-learn config/ppo/3DBall.yaml --run-id=first3DBallRun`。
   - `config/ppo/3DBall.yaml`是默认训练配置文件的路径，`config/ppo`文件夹包括所有示例环境（包括3DBall）训练配置文件。
   - `run-id`是此训练任务session的唯一名称。
1. 当屏幕上显示 _"Start training by pressing the Play button in the Unity Editor"_ 时，点击 **Play** 按钮开始在Unity编辑器中训练。

如果`mlagents-learn`正确运行并开始训练，应该会看到如下提示：

```console
INFO:mlagents_envs:
'Ball3DAcademy' started successfully!
Unity Academy name: Ball3DAcademy

INFO:mlagents_envs:Connected new brain:
Unity brain name: 3DBallLearning
        Number of Visual Observations (per agent): 0
        Vector Observation space size (per agent): 8
        Number of stacked Vector Observation: 1
INFO:mlagents_envs:Hyperparameters for the PPO Trainer of brain 3DBallLearning:
        batch_size:          64
        beta:                0.001
        buffer_size:         12000
        epsilon:             0.2
        gamma:               0.995
        hidden_units:        128
        lambd:               0.99
        learning_rate:       0.0003
        max_steps:           5.0e4
        normalize:           True
        num_epoch:           3
        num_layers:          2
        time_horizon:        1000
        sequence_length:     64
        summary_freq:        1000
        use_recurrent:       False
        memory_size:         256
        use_curiosity:       False
        curiosity_strength:  0.01
        curiosity_enc_size:  128
        output_path: ./results/first3DBallRun/3DBallLearning
INFO:mlagents.trainers: first3DBallRun: 3DBallLearning: Step: 1000. Mean Reward: 1.242. Std of Reward: 0.746. Training.
INFO:mlagents.trainers: first3DBallRun: 3DBallLearning: Step: 2000. Mean Reward: 1.319. Std of Reward: 0.693. Training.
INFO:mlagents.trainers: first3DBallRun: 3DBallLearning: Step: 3000. Mean Reward: 1.804. Std of Reward: 1.056. Training.
INFO:mlagents.trainers: first3DBallRun: 3DBallLearning: Step: 4000. Mean Reward: 2.151. Std of Reward: 1.432. Training.
INFO:mlagents.trainers: first3DBallRun: 3DBallLearning: Step: 5000. Mean Reward: 3.175. Std of Reward: 2.250. Training.
INFO:mlagents.trainers: first3DBallRun: 3DBallLearning: Step: 6000. Mean Reward: 4.898. Std of Reward: 4.019. Training.
INFO:mlagents.trainers: first3DBallRun: 3DBallLearning: Step: 7000. Mean Reward: 6.716. Std of Reward: 5.125. Training.
INFO:mlagents.trainers: first3DBallRun: 3DBallLearning: Step: 8000. Mean Reward: 12.124. Std of Reward: 11.929. Training.
INFO:mlagents.trainers: first3DBallRun: 3DBallLearning: Step: 9000. Mean Reward: 18.151. Std of Reward: 16.871. Training.
INFO:mlagents.trainers: first3DBallRun: 3DBallLearning: Step: 10000. Mean Reward: 27.284. Std of Reward: 28.667. Training.
```

注意显示的`Mean Reward`值是如何随着训练而增加的，这是训练成功的积极信号。

**Note**: 可以使用可执行文件而不是编辑器进行训练，遵循[Using an Executable](Learning-Environment-Executable.md)中的指示。

### 观察训练进度

开始使用`mlagents-learn`进行训练后，`ml-agents`目录将包含`results`目录。为了更详细地观察训练过程，可以使用TensorBoard从命令行运行：

```sh
tensorboard --logdir results
```

然后在浏览器中导航到`localhost:6006`查看TensorBoard汇总统计信息如下所示。就本节而言，重要的统计数据是`Environment/Cumulative Reward`，应该在整个训练过程中不断增加，最终收敛到接近`100`的最大值，这是智能体可以积累的最大奖励。

![Example TensorBoard Run](images/mlagents-TensorBoard.png)

## 将模型嵌入到Unity环境中

训练过程完成后，保存模型（由`Saved Model`消息提示）后可将其添加到Unity项目中，与生成模型的智能体一起使用。
**Note:** `Saved Model`消息出现时不要只关闭Unity窗口，等待训练进程关闭窗口，或在命令行提示符下点击`Ctrl+C`。
手动关闭窗口的话，`.onnx`文件（训练模型文件）不会导出到ml-agents文件夹中。

如果已使用`Ctrl+C`提前退出了训练，想恢复训练的话再次运行相同的命令，并附加`--resume`标志：

```sh
mlagents-learn config/ppo/3DBall.yaml --run-id=first3DBallRun --resume
```

训练的模型是`results/<run-identifier>/<behavior_name>.onnx`，其中`<behavior_name>`是相应智能体对应模型的`Behavior Name`的名称。
该文件对应于模型的最新检查点，可以按照以下步骤将此经过训练的模型嵌入Agents中，
与[上述步骤](#running-a-pre-trained-model)类似。

1. 将模型文件移到`Project/Assets/ML-Agents/Examples/3DBall/TFModels/`。
1. 打开Unity编辑器，如上所述选择**3DBall**场景。
1. 选择**3DBall** prefab Agent对象。
1. 将`<behavior_name>.onnx`文件从编辑器的Project窗口拖到**Ball3DAgent** inspector窗口的**Model**占位符。
1. 按下编辑器顶部的**Play**按钮。

## 下一步

- 有关ML-Agents工具包的更多信息，查看[ML-Agents Toolkit Overview](ML-Agents-Overview.md)。
- 有关创建自己的学习环境的“ Hello World”简介，查看[Making a New Learning Environment](Learning-Environment-Create-New.md)。
- 有关在工具包中提供的更复杂的示例环境的概述，查看[Example Environments](Learning-Environment-Examples.md)。
- 有关可用的各种训练选项的详细信息，查看[Training ML-Agents](Training-ML-Agents.md)。 
