本软件修改任务栏上文件资源管理器的打开位置的原理是：

如果快捷方式「%USERPROFILE%\AppData\Roaming\Microsoft\Internet Explorer\Quick Launch\User Pinned\TaskBar\File Explorer.lnk」
存在，则修改为选定的目标路径；不存在，则不修改。

创建快捷方式原理：通过调用 COM 组件「Windows Script Host Object Model」的「Interop.IWshRuntimeLibrary」接口来实现创建快捷方式。

在执行操作前，请务必确认「文件资源管理器」已经固定在任务栏上。如需手动修改教程，
可以参考 [修改任务栏上的文件资源管理器的打开位置]( https://668000.xyz/archives/21/)