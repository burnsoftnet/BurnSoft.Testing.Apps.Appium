using BurnSoft.Testing.Apps.Appium.Types;
using System;
using System.Collections.Generic;

namespace BurnSoft.Testing.Apps.Appium.helpers
{
    /// <summary>
    /// Class PrintLists helps print the lists to string or to console for debuggin purposes
    /// </summary>
    public class PrintLists
    {
        /// <summary>
        /// Prints the batch command to console
        /// </summary>
        /// <param name="lst">The LST.</param>
        public static void PrintBatchCommandToConsole(List<BatchCommandList> lst)
        {
            if (lst.Count > 0)
            {
                Console.WriteLine(PrintBatchCommand(lst));
            }
        }
        /// <summary>
        /// Prints the batch command to string format with return new line
        /// </summary>
        /// <param name="lst">The LST.</param>
        /// <returns>System.String.</returns>
        public static string PrintBatchCommand(List<BatchCommandList> lst)
        {
            string sAns = "";
            if (lst.Count > 0)
            {
                foreach (BatchCommandList l in lst)
                {
                    sAns += $"{Environment.NewLine}";
                    sAns += $"TestName: {l.TestName}{Environment.NewLine}";
                    sAns += $"CommandAction: {l.CommandAction}{Environment.NewLine}";
                    sAns += $"Actions: {l.Actions}{Environment.NewLine}";
                    sAns += $"ElementName: {l.ElementName}{Environment.NewLine}";
                    sAns += $"SendKeys: {l.SendKeys}{Environment.NewLine}";
                    sAns += $"PassedFailed: {l.PassedFailed}{Environment.NewLine}";
                    sAns += $"ReturnedValue: {l.ReturnedValue}{Environment.NewLine}";
                    sAns += $"ReturnedFoundValue: {l.ReturnedFoundValue}{Environment.NewLine}";
                    sAns += $"ReturnedValueBlankOk: {l.ReturnedValueBlankOk}{Environment.NewLine}";
                    sAns += $"SleepInterval: {l.SleepInterval}{Environment.NewLine}";
                    sAns += $"TabCount: {l.TabCount}{Environment.NewLine}";
                    sAns += $"TestNumber: {l.TestNumber}{Environment.NewLine}";
                    sAns += $"TestNameLookUp: {l.TestNameLookUp}{Environment.NewLine}";
                    sAns += $"RepeatXTimes: {l.RepeatXTimes}{Environment.NewLine}";
                    sAns += $"FilePath: {l.FilePath}{Environment.NewLine}";
                    sAns += $"{Environment.NewLine}";

                }
            }
            return sAns;
        }
    }
}
