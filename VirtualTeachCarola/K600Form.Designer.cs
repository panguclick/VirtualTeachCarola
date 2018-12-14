namespace VirtualTeachCarola
{
    partial class K600Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(K600Form));
            this.flashControl = new AxShockwaveFlashObjects.AxShockwaveFlash();
            this.titlePicture = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.flashControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.titlePicture)).BeginInit();
            this.SuspendLayout();
            // 
            // flashControl
            // 
            this.flashControl.CausesValidation = false;
            this.flashControl.Enabled = true;
            this.flashControl.Location = new System.Drawing.Point(0, 45);
            this.flashControl.Name = "flashControl";
            this.flashControl.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("flashControl.OcxState")));
            this.flashControl.Size = new System.Drawing.Size(730, 520);
            this.flashControl.TabIndex = 0;
            this.flashControl.Enter += new System.EventHandler(this.flashControl_Enter);
            // 
            // titlePicture
            // 
            this.titlePicture.InitialImage = null;
            this.titlePicture.Location = new System.Drawing.Point(0, 0);
            this.titlePicture.Name = "titlePicture";
            this.titlePicture.Size = new System.Drawing.Size(730, 45);
            this.titlePicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.titlePicture.TabIndex = 1;
            this.titlePicture.TabStop = false;
            this.titlePicture.Click += new System.EventHandler(this.titlePicture_Click);
            this.titlePicture.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form_MouseDown);
            this.titlePicture.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form_MouseMove);
            this.titlePicture.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form_MouseUp);
            // 
            // K600Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(730, 520);
            this.Controls.Add(this.titlePicture);
            this.Controls.Add(this.flashControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "K600Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "K600Form";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form_MouseUp);
            ((System.ComponentModel.ISupportInitialize)(this.flashControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.titlePicture)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AxShockwaveFlashObjects.AxShockwaveFlash flashControl;
        private System.Windows.Forms.PictureBox titlePicture;
    }
}