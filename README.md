# gframework

a simple common framework for unity


## RPC框架使用说明

* AChannel

  * 消息通道抽象类，作为TCP、UDP协议封装的基类。
  * 创建即初始化缓冲池，设置缓冲池大小，所有的Service通过Channel发送、接收消息。
  * 需要持有一个Packer和Dispatcher，用于打包、拆包和消息解析分发
* APacker

  * 提供消息打包、拆包接口
  * 需要自行定义怎么打包、怎么解包，并返回打包之后字节数组、拆包之后协议列表
* ADispatcher

  * 提供解析协议接口
  * 自行定义存储回调的数据结构，解析之后自行分发
* ICaller：定义C2S的接口规范
* ICallee：定义S2C的接口规范

## 代码规范

抽象类：ABase

接口：IBase

常量：全大写+下划线间隔：PACKET_SIZE_NUM
