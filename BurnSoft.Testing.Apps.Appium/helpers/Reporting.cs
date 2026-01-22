using BurnSoft.Testing.Apps.Appium.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurnSoft.Testing.Apps.Appium.helpers
{
    /// <summary>
    /// Class containing Reporting Functions for the test Runs.
    /// </summary>
    public class Reporting
    {
        /// <summary>
        /// Generates the results from the Batch Command List to display the step number, testname, any returnedvalue results and
        /// if it failed, to return the element name that it failed at.
        /// </summary>
        /// <param name="cmdResults">The command results.</param>
        /// <param name="errOut">The error out.</param>
        /// <returns>System.String.</returns>
        public static string GenerateResults(List<BatchCommandList> cmdResults, out string errOut)
        {
            string sAns = "";
            errOut = "";
            try
            {
                int stepNumber = 1;
                foreach (BatchCommandList c in cmdResults)
                {
                    if (c.TestName != null)
                    {
                        if (c.TestName?.Length > 0)
                        {
                            string passFailed = c.PassedFailed ? "PASSED!" : "FAILED!";
                            sAns += $"{Environment.NewLine}{stepNumber}.)  {passFailed} {c.TestName}";
                            if (c.ReturnedValue.Length > 0) sAns += $"  {c.ReturnedValue}";
                            if (!c.PassedFailed) sAns += $"{Environment.NewLine} Failed at line: {c.ElementName}";
                            stepNumber++;
                        }
                    }
                }

                if (sAns.Length > 0) sAns += $"{Environment.NewLine}";
            }
            catch (Exception e)
            {
                errOut = e.Message;
            }
            return sAns;
        }

        /// <summary>
        /// Works through the results of the Batch Command list and looks to see if any of the tests where marked as failed,
        /// if some show up as failed then it will return false, else everything passed and it is true.
        /// </summary>
        /// <param name="results"></param>
        /// <returns></returns>
        public static bool AllTestsPassed(List<BatchCommandList> results)
        {
            return results.All(r => r.PassedFailed);
        }
    }
}
