using System;
using System.Windows.Forms;

namespace SampleUITestApp
{
    /// <summary>
    /// Class frmMain.
    /// Implements the <see cref="Form" />
    /// </summary>
    /// <seealso cref="Form" />
    public partial class frmMain : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="frmMain"/> class.
        /// </summary>
        public frmMain()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Handles the Click event of the btnClickTest control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnClickTest_Click(object sender, EventArgs e)
        {
            lblClickStatus.Text = "Clicked";
            txtClickStatus.Text = "Clicked";
        }
        /// <summary>
        /// Handles the Click event of the mnuExit control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void mnuExit_Click(object sender, EventArgs e)
        {
            Close();
        }
        /// <summary>
        /// Handles the Load event of the frmMain control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void frmMain_Load(object sender, EventArgs e)
        {
            cmdDebugMode.DropDownStyle = ComboBoxStyle.DropDownList;
        }
    }
}
