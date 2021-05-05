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

如果您计划修改或扩展存储库，则需要克隆存储库。
ML-Agents工具包可满足您的需求。如果您打算做出这些改变
返回，请确保克隆`main`分支（通过省略`--branch release_17`
从上面的命令）。看到我们的
[贡献准则]（../ com.unity.ml-agents / CONTRIBUTING.md）了解更多
有关为ML-Agents Toolkit做出贡献的信息。

### 安装`com.unity.ml-agents` Unity软件包

Unity ML-Agents C＃SDK是Unity软件包。您可以安装
`com.unity.ml-agents`包
[直接从Package Manager注册表中]（https://docs.unity3d.com/Manual/upm-ui-install.html）。
请确保在“高级”下拉列表中启用“预览包”
为了找到该软件包的最新预览版。

**注意：**如果您没有在“包管理器”中看到ML-Agents包
请按照下面的[高级安装说明]（＃advanced-local-installation-for-development）。

#### 高级：用于开发的本地安装

您可以[添加本地]（https://docs.unity3d.com/Manual/upm-ui-local.html）
`com.unity.ml-agents`软件包（从您刚刚克隆的存储库中）到您的
项目：

1.导航至菜单“窗口”->“包管理器”。
1.在软件包管理器窗口中，单击软件包列表左上方的“ +”按钮。
1.选择“从磁盘添加软件包...”。
1.导航到t 
