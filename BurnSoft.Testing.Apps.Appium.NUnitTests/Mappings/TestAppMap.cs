

using System.Diagnostics;

namespace BurnSoft.Testing.Apps.Appium.NUnitTests.Mappings
{
    /// <summary>
    /// Class TestAppMap. General Class Mapping of the Test Ui to 
    /// use in testing, broken up into sections, AutomationID, Display Name
    /// </summary>
    public class TestAppMap
    {
        /// <summary>
        /// Class AutomationIds.
        /// </summary>
        public class AutomationIds
        {
            /// <summary>
            /// The exit button
            /// </summary>
            public static string ExitButton = "mnuExit";
            /// <summary>
            /// The menu file
            /// </summary>
            public static string MenuFile = "mnuFile";
            /// <summary>
            /// The click test button
            /// </summary>
            public static string ClickTestButton = "btnClickTest";

        }
        /// <summary>
        /// Class DisplayName.
        /// </summary>
        public class DisplayName
        {
            /// <summary>
            /// The exit button
            /// </summary>
            public static string ExitButton = "Exit";
            /// <summary>
            /// The menu file
            /// </summary>
            public static string MenuFile = "File";
            /// <summary>
            /// The click test button
            /// </summary>
            public static string ClickTestButton = "Click Test";
        }

    }
}
