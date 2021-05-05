# 安装

ML-Agents工具包包含几个组件：

- Unity软件包 ([`com.unity.ml-agents`](../com.unity.ml-agents/)) 包含将被集成到Unity项目中的Unity C# SDK，此包装包含帮助开始使用ML-Agents的样例。
- Unity软件包([`com.unity.ml-agents.extensions`](../com.unity.ml-agents.extensions/)) 包含尚未准备好加入基本的`com.unity.ml-agents`包的实验性C#/Unity组件`com.unity.ml-agents.extensions`直接依赖于`com.unity.ml-agents`。
- 三个Python软件包：
  - [`mlagents`](../ml-agents/) 包含用于在Unity场景中训练行为的机器学习算法，大多数用户只需要直接安装`mlagents`。
  - [`mlagents_envs`](../ml-agents-envs/) 包含一个与Unity场景交互的Python API，它是促进在Unity场景和Python机器学习算法之间进行数据消息传递的基础层，因此，`mlagents`依赖于 `mlagents_envs`。
  - [`gym_unity`](../gym-unity/) 为Unity场景提供Python包装器（Python-wrapper），支持OpenAI Gym接口。
- Unity [Project](../Project/) 包含多个[example environments](Learning-Environment-Examples.md)，分别突出演示该工具包的各种功能以帮助入门。

因此，要安装和使用ML-Agents工具包，将需要：
- 安装Unity（2019.4或更高版本）
- 安装Python（3.6.1或更高版本）
- 克隆此存储库（可选）
  - __注意:__ 如果不克隆存储库，则不能够访问示例环境和训练配置、`com.unity.ml-agents.extensions`软件包。[Getting Started Guide](Getting-Started.md)假定已克隆了本库。
- 安装`com.unity.ml-agents`Unity软件包
- 安装`com.unity.ml-agents.extensions` Unity软件包（可选）
- 安装`mlagents`Python软件包

### 安装 **Unity 2019.4** 或更高版本

[Download](https://unity3d.com/get-unity/download)并安装Unity。强烈建议通过Unity Hub安装Unity，能够管理多个Unity版本。

### 安装 **Python 3.6.1** 或更高版本

建议[installing](https://www.python.org/downloads/)Python 3.6或3.7。
如果使用的是Windows，安装x86-64版本而不是x86。
如果Python环境不包含`pip3`，参阅以下内容
[instructions](https://packaging.python.org/guides/installing-using-linux-tools/#installing-pip-setuptools-wheel-with-linux-package-managers)安装它。

### 克隆ML-Agents工具包存储库（可选）

安装Unity和Python软件包，无需克隆存储库即可安装这些存储库软件包。克隆存储库可同时下载示例环境和训练配置以进行实验。

**注意：** Unity软件包随附一些示例。

```sh
git clone --branch release_17 https://github.com/Unity-Technologies/ml-agents.git
```

`--branch release_17`选项将切换到最新版本的标签，省略它会下载到可能不稳定的`main`分支。

#### 高级：用于开发的本地安装

如果计划修改或扩展存储库，则需要克隆本存储库。如果打算将这些改变提交给主库，请克隆`main`分支（省略`--branch release_17`）。
可查看[Contributions Guidelines](../com.unity.ml-agents/CONTRIBUTING.md)了解更多有关为ML-Agents Toolkit做贡献的信息。

### 安装`com.unity.ml-agents` Unity软件包

Unity ML-Agents C# SDK是一个Unity软件包，可以[直接从Package Manager registry](https://docs.unity3d.com/Manual/upm-ui-install.html)安装`com.unity.ml-agents`包。
需确保在'Advanced'下拉列表中启用'Preview Packages'找到该软件包的最新预览版。

**注意：**如果没有在“包管理器”中看到ML-Agents包
请按照下面的步骤安装。

#### 高级：用于开发的本地安装

可以按照如下步骤[添加本地](https://docs.unity3d.com/Manual/upm-ui-local.html)将`com.unity.ml-agents`软件包（克隆的存储库中）添加到项目：

1. 导航至菜单`Window` -> `Package Manager`
1. 在软件包管理器窗口中，单击软件包列表左上方的“ +”按钮。
1. 选择“从磁盘添加软件包...”。
1. 导航到`com.unity.ml-agents`文件夹
1. 选择 `package.json` 文件。

<p align="center">
  <img src="images/unity_package_manager_window.png"
       alt="Unity Package Manager Window"
       height="300"
       border="10" />
  <img src="images/unity_package_json.png"
     alt="package.json"
     height="300"
     border="10" />
</p>

如果要按照文档中的示例进行学习，则可以打开Unity中的`Project`文件夹，立即开始查看。

### 安装`com.unity.ml-agents.extensions` Unity软件包（可选）

要安装`com.unity.ml-agents.extensions`软件包，需要先克隆存储库，然后类似于上面的步骤完成本地安装。
在前面概述
[Advanced: Local Installation for Development](#高级用于开发的本地安装-1)
部分。完整的安装步骤在
[package documentation](../com.unity.ml-agents.extensions/Documentation~/com.unity.ml-agents.extensions.md#installation)。

### 安装`mlagents` Python软件包

安装`mlagents` Python软件包涉及安装mlagents依赖的其他Python软件包。因此如果计算机具有已安装的依赖项的较旧版本，可能会遇到安装问题。
因此，支持的安装`mlagents`方式是利用Python Virtual Environments。虚拟环境可以隔离每个项目的依赖，并且在Mac / Windows / Linux上受支持。
可参见[虚拟环境指南]（Using-Virtual-Environment.md）。

#### （Windows）安装PyTorch

在Windows上，必须先单独安装PyTorch软件包，然后才能安装ML-Agents。激活虚拟环境并从命令行运行：

```sh
pip3 install torch~=1.7.1 -f https://download.pytorch.org/whl/torch_stable.html
```

请注意，在Windows上，可能还需要Microsoft的
[Visual C++ Redistributable](https://support.microsoft.com/en-us/help/2977003/the-latest-supported-visual-c-downloads)
更多有关安装选项和版本的信息参阅[PyTorch installation guide](https://pytorch.org/get-started/locally/)。

#### 安装`mlagents`

要安装`mlagents` Python软件包，激活虚拟环境并从命令行运行：

```sh
python -m pip install mlagents == 0.26.0
```

这将从PyPi安装`mlagents`，而不是从库中克隆安装。如果安装正确，可以运行`mlagents-learn --help`看到可以与`mlagents-learn`一起使用的命令行参数。

这种方式安装`mlagents`软件包，[setup.py file](../ml-agents/setup.py)中的依赖项也会一起安装，包括[PyTorch](Background-PyTorch.md)（需要具有AVX支持的CPU）。

#### 高级：用于开发的本地安装

如果打算对`mlagents`或`mlagents_envs`进行修改，则应从克隆的存储库而不是从PyPi安装软件包。为此，需要分别安装`mlagents`和`mlagents_envs`。从存储库的根目录，运行：

```sh
pip3 install torch -f https://download.pytorch.org/whl/torch_stable.html
pip3 install -e ./ml-agents-envs
pip3 install -e ./ml-agents
```

运行pip带有-e标志将可直接对Python文件进行更改，并在运行`mlagents-learn`时反映出来。
注意按照这个顺序安装这些软件包，`mlagents`软件包依赖`mlagents_envs`，按其他顺序安装将会自动下载来自PyPi的`mlagents_envs`。

## 下一步

[Getting Started](Getting-Started.md) 指南包含一些简短的教程，主要是在Unity中设置ML-Agents工具包，运行预先训练的模型，以及构建环境和训练智能体。

## 帮助

如果遇到有关ML-Agent的任何问题，请参阅[FAQ](FAQ.md)和[Limitations](Limitations.md)页面。如果找不到有帮助的东西，[submit an issue](https://github.com/Unity-Technologies/ml-agents/issues)，并尽可能提供有关OS，Python版本和确切错误的相关信息。
