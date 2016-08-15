using System;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.UIElements;
using Microsoft.Practices.CompositeUI.WinForms;
using Microsoft.Practices.CompositeUI.Commands;
using Microsoft.Practices.CompositeUI.Services;

namespace SimpleCABApp
{
    public class SimpleCABShellApp : FormShellApplication<WorkItem, SimpleCABShellForm>
    {
        [STAThread]
        public static void Main()
        {
            new SimpleCABShellApp().Run();
        }

        protected override void AfterShellCreated()
        {
            base.AfterShellCreated();

            // 菜单
            ToolStripMenuItem fileItem = (ToolStripMenuItem)Shell.MainMenuStrip.Items["fileToolStripMenuItem"];
            // UIExtensionSite添加到WorkItem中
            RootWorkItem.UIExtensionSites.RegisterSite("FileDropDown", fileItem.DropDownItems);

            StatusStrip statusStrip = (StatusStrip)Shell.statusStrip;
            RootWorkItem.UIExtensionSites.RegisterSite("StatusStrip", statusStrip);

            AddMenuStripButton(RootWorkItem, "FileNewContact", "New");
            AddMenuStripButton(RootWorkItem, "FileExit", "Exit");
        }

        private void AddMenuStripButton(WorkItem workItem, string commandName, string text)
        {
            // 支持文本和图像的工具栏按钮
            ToolStripButton button = new ToolStripButton();
            button.Text = text;

            // Add the button to the MainToolBar
            workItem.UIExtensionSites["FileDropDown"].Add(button);

            // Associate the Click event of the button to a command
            // 将一个Command绑定到一个UIElement事件
            workItem.Commands[commandName].AddInvoker(button, "Click");
        }
    }
}