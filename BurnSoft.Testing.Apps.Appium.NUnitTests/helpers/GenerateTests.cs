using BurnSoft.Testing.Apps.Appium.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurnSoft.Testing.Apps.Appium.NUnitTests.helpers
{
    internal class GenerateTests
    {
        /// <summary>
        /// Gets the commands.
        /// </summary>
        /// <returns>List&lt;BatchCommandList&gt;.</returns>
        internal static List<BatchCommandList> GetCommands()
        {
            string UseTab = "Other";
            string txt1 = "txtDatabaseServer";
            string txt2 = "txtUserName";
            string txt3 = "txtPassword";
            string saveBtn = "btnSave";
            string nextTab = "Main";

            List<BatchCommandList> cmd = new List<BatchCommandList>();
            cmd.Add(new BatchCommandList()
            {
                TestName = "Click On Tab",
                Actions = GeneralActions.MyAction.Click,
                CommandAction = GeneralActions.AppAction.FindElementByName,
                ElementName = UseTab
            });

            cmd.Add(new BatchCommandList()
            {
                TestName = "Verify Database Server Textbox exists",
                Actions = GeneralActions.MyAction.Nothing,
                CommandAction = GeneralActions.AppAction.FindElementByAccessibilityId,
                ElementName = txt1
            });
            cmd.Add(new BatchCommandList()
            {
                TestName = "Set Database Server",
                Actions = GeneralActions.MyAction.ClearAndSendKeys,
                CommandAction = GeneralActions.AppAction.FindElementByAccessibilityId,
                ElementName = txt1,
                SendKeys = "11.2.3.4"
            });

            cmd.Add(new BatchCommandList()
            {
                TestName = "Verify Username Textbox exists",
                Actions = GeneralActions.MyAction.Nothing,
                CommandAction = GeneralActions.AppAction.FindElementByAccessibilityId,
                ElementName = txt2
            });
            cmd.Add(new BatchCommandList()
            {
                TestName = "Set Username",
                Actions = GeneralActions.MyAction.ClearAndSendKeys,
                CommandAction = GeneralActions.AppAction.FindElementByAccessibilityId,
                ElementName = txt2,
                SendKeys = "superman"
            });

            cmd.Add(new BatchCommandList()
            {
                TestName = "Verify password Textbox exists",
                Actions = GeneralActions.MyAction.Nothing,
                CommandAction = GeneralActions.AppAction.FindElementByAccessibilityId,
                ElementName = txt3
            });
            cmd.Add(new BatchCommandList()
            {
                TestName = "Set Password",
                Actions = GeneralActions.MyAction.ClearAndSendKeys,
                CommandAction = GeneralActions.AppAction.FindElementByAccessibilityId,
                ElementName = txt3,
                SendKeys = "superangry"
            });


            cmd.Add(new BatchCommandList()
            {
                TestName = "Click Save Button",
                Actions = GeneralActions.MyAction.Click,
                CommandAction = GeneralActions.AppAction.FindElementByAccessibilityId,
                ElementName = saveBtn
            });

            cmd.Add(new BatchCommandList()
            {
                TestName = "Click on Main Tab",
                Actions = GeneralActions.MyAction.Click,
                CommandAction = GeneralActions.AppAction.FindElementByAccessibilityId,
                ElementName = nextTab
            });

            cmd.Add(new BatchCommandList()
            {
                TestName = "Click Button",
                Actions = GeneralActions.MyAction.Click,
                CommandAction = GeneralActions.AppAction.FindElementByAccessibilityId,
                ElementName = Mappings.TestAppMap.AutomationIds.ClickTestButton
            });

            cmd.Add(new BatchCommandList()
            {
                TestName = "Read Textbox in Main after Click",
                Actions = GeneralActions.MyAction.ReadValue,
                CommandAction = GeneralActions.AppAction.FindElementByAccessibilityId,
                ElementName = Mappings.TestAppMap.AutomationIds.ClickTestText
            });

            cmd.Add(new BatchCommandList()
            {
                TestName = "Read Label in Main after Click",
                Actions = GeneralActions.MyAction.ReadValue,
                CommandAction = GeneralActions.AppAction.FindElementByAccessibilityId,
                ElementName = Mappings.TestAppMap.AutomationIds.ClickTestLabel
            });

            cmd.Add(new BatchCommandList()
            {
                TestName = "Click File",
                Actions = GeneralActions.MyAction.Click,
                CommandAction = GeneralActions.AppAction.FindElementByAccessibilityId,
                ElementName = "mnuFile"
            });

            cmd.Add(new BatchCommandList()
            {
                TestName = "Click Exit",
                Actions = GeneralActions.MyAction.Click,
                CommandAction = GeneralActions.AppAction.FindElementByAccessibilityId,
                ElementName = "mnuExit"
            });

            return cmd;
        }
    }
}
