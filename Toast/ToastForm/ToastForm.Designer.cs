using System.Drawing;
using System.Security.Permissions;
using System.Windows.Forms;

namespace Toast
{
    partial class ToastForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Name = "Toast";
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ShowInTaskbar = false;
            this.TopMost = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.TransparencyKey = this.BackColor;
        }

        #endregion
    }
}

