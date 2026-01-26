using BurnSoft.Testing.Apps.Appium.Types;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BurnSoft.Testing.Apps.Appium.helpers
{
    /// <summary>
    /// The Json Handling Class contains functions to help convert the BatchCommandList that you generated/created
    /// into json to store to file to load later if you wish.
    /// summary>
    public class JsonHandling
    {
        #region "Exception Error Handling"        
        /// <summary>
        /// The class location
        /// </summary>
        private static string _classLocation = "BurnSoft.Testing.Apps.Appium.helpers.JsonHandling";

        /// <summary>
        /// Errors the message for regular Exceptions
        /// </summary>
        /// <param name="functionName">Name of the function.</param>
        /// <param name="e">The e.</param>
        /// <returns>System.String.</returns>
        private static string ErrorMessage(string functionName, Exception e) => $"{_classLocation}.{functionName} - {e.Message}";
        /// <summary>
        /// Errors the message for access violations
        /// </summary>
        /// <param name="functionName">Name of the function.</param>
        /// <param name="e">The e.</param>
        /// <returns>System.String.</returns>
        private static string ErrorMessage(string functionName, AccessViolationException e) => $"{_classLocation}.{functionName} - {e.Message}";
        /// <summary>
        /// Errors the message for invalid cast exception
        /// </summary>
        /// <param name="functionName">Name of the function.</param>
        /// <param name="e">The e.</param>
        /// <returns>System.String.</returns>
        private static string ErrorMessage(string functionName, InvalidCastException e) => $"{_classLocation}.{functionName} - {e.Message}";
        /// <summary>
        /// Errors the message argument exception
        /// </summary>
        /// <param name="functionName">Name of the function.</param>
        /// <param name="e">The e.</param>
        /// <returns>System.String.</returns>
        private static string ErrorMessage(string functionName, ArgumentException e) => $"{_classLocation}.{functionName} - {e.Message}";
        /// <summary>
        /// Errors the message for argument null exception.
        /// </summary>
        /// <param name="functionName">Name of the function.</param>
        /// <param name="e">The e.</param>
        /// <returns>System.String.</returns>
        private static string ErrorMessage(string functionName, ArgumentNullException e) => $"{_classLocation}.{functionName} - {e.Message}";
        #endregion        
        /// <summary>
        /// Converts the test sequence to json.
        /// </summary>
        /// <param name="lst">The BatchCommandList list test sequence that you created.</param>
        /// <param name="errOut">The error out, if error occurs.</param>
        /// <returns>System.String json format.</returns>
        public static string ConvertTestSequenceToJson(List<BatchCommandList> lst, out string errOut)
        {
            string sAns = "";
            errOut = "";
            try
            {
                sAns = System.Text.Json.JsonSerializer.Serialize(lst, new JsonSerializerOptions { WriteIndented = true });
            }
            catch (Exception ex)
            {
                errOut = ErrorMessage("ConvertTestSequenceToJson", ex);
            }
            return sAns;
        }

        /// <summary>
        /// Converts the test sequence to json file and save to the file that you want to
        /// store the information at.
        /// </summary>
        /// <param name="lst">The BatchCommandList list test sequence that you created.</param>
        /// <param name="saveToPath">The save to path.</param>
        /// <param name="errOut">The error out, if error occurs.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// <exception cref="System.Exception"></exception>
        public static bool ConvertTestSequenceToJsonFile(List<BatchCommandList> lst, string saveToPath, 
            out string errOut)
        {
            bool bAns = false;
            errOut = "";
            try
            {
                string json = ConvertTestSequenceToJson(lst, out errOut);
                if (errOut.Length > 0) throw new Exception(errOut);
                File.WriteAllText(saveToPath, json);
                bAns = true;
            }
            catch (Exception ex)
            {
                errOut = ErrorMessage("ConvertTestSequenceToJsonFile", ex);
            }
            return bAns;
        }

        /// <summary>
        /// Converts the json file  to batch command to use in the test sequence.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="errOut">The error out.</param>
        /// <returns>List&lt;BatchCommandList&gt;.</returns>
        public static List<BatchCommandList> ConvertJsonToBatchCommand(string filePath, out string errOut)
        {
            List<BatchCommandList > lst = new List<BatchCommandList>();
            errOut = "";
            try
            {
                using (StreamReader file = File.OpenText(filePath))
                using (JsonTextReader reader = new JsonTextReader(file))
                {
                    lst = Newtonsoft.Json.JsonSerializer.Create().Deserialize<List<BatchCommandList>>(reader);
                }
            }
            catch (Exception ex)
            {
                errOut = ErrorMessage("ConvertJsonToBatchCommand", ex);
            }
            return lst;
        }
    }
}
