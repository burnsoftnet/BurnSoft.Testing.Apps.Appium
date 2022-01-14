using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurnSoft.Testing.Apps.Appium
{
    /// <summary>
    /// The General Actions Class is the main class that will found to common functions used to communicate with appium.  Just like I did
    /// in the selenium helper library, this will be the main class in case there are other special classes that need to be created to have it work with
    /// other OS's or application types, etc.
    /// </summary>
    public class GeneralActions
    {
        #region "Private Variables"
        private string _windowsApplicationDriverUrl;
        private string _WinAppDriverPath;
        #endregion
        #region "Public Variables"
        /// <summary>
        /// Gets or sets the windows application driver URL. If not set, it will default to http://127.0.0.1:4723
        /// </summary>
        /// <value>The windows application driver URL.</value>
        public string WindowsApplicationDriverUrl
        {
            get
            {
                if (_windowsApplicationDriverUrl == null) return "http://127.0.0.1:4723";
                if (_windowsApplicationDriverUrl.Length == 0)
                {
                    return "http://127.0.0.1:4723";
                }
                else
                {
                    return _windowsApplicationDriverUrl;
                }
            }
            set => _windowsApplicationDriverUrl = value;
        }

        public string WinAppDriverPath
        {
            get {
                if (_WinAppDriverPath == null) return @"C:\Program Files (x86)\Windows Application Driver\WinAppDriver.exe";
                if (_WinAppDriverPath.Length == 0)
                {
                    return @"C:\Program Files (x86)\Windows Application Driver\WinAppDriver.exe";
                }
                else
                {
                    return _WinAppDriverPath;
                }
            }
            set => _WinAppDriverPath = value;
        }

        #endregion

    }
}
