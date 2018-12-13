
using System.Drawing;
using System.Windows.Forms;

namespace VirtualTeachCarola
{
    partial class SettingForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private AxShockwaveFlashObjects.AxShockwaveFlash flashContrl;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingForm));
            this.flashContrl = new IForcePlayer();
            ((System.ComponentModel.ISupportInitialize)(this.flashContrl)).BeginInit();
            this.SuspendLayout();

            this.flashContrl.Enabled = true;
            this.flashContrl.Location = new System.Drawing.Point(0, 0);
            this.flashContrl.Name = "flashContrl";
            this.flashContrl.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("flashContrl.OcxState")));
            this.flashContrl.TabIndex = 0;
            // 
            // SettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SettingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SettingForm";
            this.Controls.Add(this.flashContrl);
            ((System.ComponentModel.ISupportInitialize)(this.flashContrl)).EndInit();
            Rectangle ScreenArea = System.Windows.Forms.Screen.GetBounds(this);
            this.ClientSize = new System.Drawing.Size(ScreenArea.Height*3/2, ScreenArea.Height);

            this.flashContrl.Size = this.ClientSize;
            this.ResumeLayout(false);
            this.Load += new System.EventHandler(this.Main_Load);

        }

        #endregion
    }
}