using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SetExplorerOpenLocation
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public static IWshRuntimeLibrary.IWshShortcut CreateShortcut(string pathLink, string pathTarget)
        {
            var shell = new IWshRuntimeLibrary.WshShell();

            if (!pathLink.EndsWith(".lnk"))
            {
                throw new NotSupportedException("快捷方式只支持以「.lnk」作为结尾");
            }

            var shortcut = (IWshRuntimeLibrary.IWshShortcut)shell.CreateShortcut(pathLink);
            shortcut.TargetPath = pathTarget;
            return shortcut;
        }

        public static void CreateExplorerLink(string pathTarget)
        {
            // %USERPROFILE%\AppData\Roaming\Microsoft\Internet Explorer\Quick Launch\User Pinned\TaskBar

            var applicationData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var pathLink = System.IO.Path.Combine(applicationData, "Microsoft\\Internet Explorer\\Quick Launch\\User Pinned\\TaskBar\\File Explorer.lnk");

            if (!System.IO.File.Exists(pathLink))
            {
                throw new System.IO.FileNotFoundException("请先确保任务栏已固定「文件资源管理器」再进行操作！");
            }

            var shortcut = CreateShortcut(pathLink, pathTarget);
            shortcut.Save();
        }

        private string SelectFolder()
        {
            folderBrowserDialog1.SelectedPath = "";
            folderBrowserDialog1.ShowDialog();

            if (folderBrowserDialog1.SelectedPath == "")
            {
                throw new System.IO.DirectoryNotFoundException("请选择「目录」");
            }

            return folderBrowserDialog1.SelectedPath;
        }

        private void SetOpenFolder(object sender, EventArgs e)
        {
            try
            {
                var target = SelectFolder();
                CreateExplorerLink(target);

                MessageBox.Show($"设置成功，现在的打开位置为：\n「{target}」", "设置成功");
            }
            catch (System.IO.DirectoryNotFoundException error)
            {
                MessageBox.Show(error.Message, "目录不存在");
            }
            catch (System.IO.FileNotFoundException error)
            {
                MessageBox.Show(error.Message, "文件不存在");
            }
            catch (NotSupportedException error)
            {
                MessageBox.Show(error.Message, "失败");
            }
        }
    }
}
