using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace BurnSoft.Testing.Apps.Appium.helpers
{
    /// <summary>
    /// Class XmlHandling helped handle saving xml and other xml data handling.
    /// </summary>
    public class XmlHandling
    {
        public static bool SaveXmlToFile(string data, string fileNamePath, out string errOut)
        {
            bool bAns = false;
            errOut = "";
            try
            {

                File.WriteAllText(fileNamePath, data);
                bAns = true;
            }
            catch (Exception e) 
            { 
                errOut = e.Message;
            }
            return bAns;
        }

        public static bool SaveXmlAsJsonToFile(string data, string fileNamePath, out string errOut)
        {
            bool bAns = false;
            errOut = "";
            try
            {
                data = ConvertXmlToJson(data);
                File.WriteAllText(fileNamePath, data);
                bAns = true;
            }
            catch (Exception e)
            {
                errOut = e.Message;
            }
            return bAns;
        }

        public static string ConvertXmlToJson(string xmlContent)
        {
            // 1. Load the XML string into an XmlDocument
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlContent);

            // 2. Use JsonConvert.SerializeXmlNode to convert the XmlDocument to a JSON string
            string jsonText = JsonConvert.SerializeXmlNode(doc, Newtonsoft.Json.Formatting.Indented);

            return jsonText;
        }
    }
}
