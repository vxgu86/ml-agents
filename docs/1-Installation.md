# 安装

ML-Agents工具包包含几个组件：

-Unity软件包 ([`com.unity.ml-agents`](../com.unity.ml-agents/)) 包含
  将被集成到您的Unity项目中的Unity C＃SDK。此包装包含
  帮助您开始使用ML-Agents的样本。
-Unity包
  （[[.com.unity.ml-agents.extensions`]（../ com.unity.ml-agents.extensions /））
  包含尚未准备好加入的实验性C＃/ Unity组件
  基本的“ com.unity.ml-agents”包的名称。 `com.unity.ml-agents.extensions`
  直接依赖于com.unity.ml-agents。
-三个Python软件包：
  -[`mlagents`]（../ ml-agents /）包含用于以下方面的机器学习算法：
    使您能够训练Unity场景中的行为。 ML-Agent的大多数用户
    只需要直接安装`mlagents`。
  -[`mlagents_envs`]（../ ml-agents-envs /）包含与之交互的Python API
    一个Unity场景。它是促进数据消息传递的基础层
    在Unity场景和Python机器学习算法之间进行切换。
    因此，`mlagents`取决于`mlagents_envs`。
  -[`gym_unity`]（../ gym-unity /）为您的Unity场景提供了Python包装器
    支持OpenAI Gym界面。
-Unity [Project]（../ Project /）包含多个
  [示例环境]（Learning-Environment-Examples.md）突出显示了
  该工具包的各种功能可帮助您入门。

因此，要安装和使用ML-Agents工具包，您将需要：

-安装Unity（2019.4或更高版本）
-安装Python（3.6.1或更高版本）
-克隆此存储库（可选）
  -__注意：__如果不克隆存储库，则不会
  能够访问示例环境和培训配置，或者
  `com.unity.ml-agents.extensions`软件包。此外，
  [入门指南]（Getting-Started.md）假定您已克隆了
  资料库。
-安装`com.unity.ml-agents` Unity软件包
-安装`com.unity.ml-agents.extensions` Unity软件包（可选）
-安装`mlagents` Python软件包

###安装** Unity 2019.4 **或更高版本

[下载]（https://unity3d.com/get-unity/download）并安装Unity。我们
强烈建议您通过Unity Hub安装Unity，因为它将
使您能够管理多个Unity版本。

###安装** Python 3.6.1 **或更高版本

我们建议[安装]（https://www.python.org/downloads/）Python 3.6或3.7。
如果您使用的是Windows，请安装x86-64版本而不是x86。
如果您的Python环境不包含`pip3`，请参阅以下内容
[说明]（https://packaging.python.org/guides/installing-using-linux-tools/#installing-pip-setuptools-wheel-with-linux-package-managers）
在安装它。

###克隆ML-Agents工具包存储库（可选）

既然您已经安装了Unity和Python，现在就可以安装Unity和
Python软件包。您无需克隆存储库即可安装这些存储库
软件包，但如果您想下载我们的库，则可以选择克隆存储库
示例环境和训练配置以进行实验（某些
我们的教程/指南中的假设您可以访问我们的示例环境）。

**注意：** Unity软件包随附一些示例。您只需要克隆
如果您想探索更多示例，请访问存储库。

```sh
git clone --branch release_17 https://github.com/Unity-Technologies/ml-agents.git
```

`--branch release_17`选项将切换到最新版本的标签。
释放。省略它会得到可能不稳定的`main`分支。

####高级：用于开发的本地安装

如果您计划修改或扩展存储库，则需要克隆存储库。
ML-Agents工具包可满足您的需求。如果您打算做出这些改变
返回，请确保克隆`main`分支（通过省略`--branch release_17`
从上面的命令）。看到我们的
[贡献准则]（../ com.unity.ml-agents / CONTRIBUTING.md）了解更多
有关为ML-Agents Toolkit做出贡献的信息。

###安装`com.unity.ml-agents` Unity软件包

Unity ML-Agents C＃SDK是Unity软件包。您可以安装
`com.unity.ml-agents`包
[直接从Package Manager注册表中]（https://docs.unity3d.com/Manual/upm-ui-install.html）。
请确保在“高级”下拉列表中启用“预览包”
为了找到该软件包的最新预览版。

**注意：**如果您没有在“包管理器”中看到ML-Agents包
请按照下面的[高级安装说明]（＃advanced-local-installation-for-development）。

####高级：用于开发的本地安装

您可以[添加本地]（https://docs.unity3d.com/Manual/upm-ui-local.html）
`com.unity.ml-agents`软件包（从您刚刚克隆的存储库中）到您的
项目：

1.导航至菜单“窗口”->“包管理器”。
1.在软件包管理器窗口中，单击软件包列表左上方的“ +”按钮。
1.选择“从磁盘添加软件包...”。
1.导航到t 
