using System;
using System.Drawing;

namespace VirtualTeachCarola
{
    partial class ReferenceForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReferenceForm));
            this.flashContrl = new VirtualTeachCarola.IForcePlayer();
            this.BooklistView = new System.Windows.Forms.ListView();
            ((System.ComponentModel.ISupportInitialize)(this.flashContrl)).BeginInit();
            this.SuspendLayout();

            Rectangle ScreenArea = System.Windows.Forms.Screen.GetBounds(this);

            // 
            // ReferenceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(ScreenArea.Height * 2, ScreenArea.Height);
            this.Controls.Add(this.BooklistView);
            this.Controls.Add(this.flashContrl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ReferenceForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ReferenceForm";
            // 
            // flashContrl
            // 
            this.flashContrl.Enabled = true;
            this.flashContrl.Location = new System.Drawing.Point(0, 0);
            this.flashContrl.Name = "flashContrl";
            this.flashContrl.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("flashContrl.OcxState")));
            this.flashContrl.Size = this.ClientSize;
            this.flashContrl.TabIndex = 0;

            // 
            // BooklistView
            // 
            this.BooklistView.Location = new System.Drawing.Point(this.ClientSize.Width * 1 / 5, this.ClientSize.Height * 2 / 15);
            this.BooklistView.Name = "BooklistView";
            this.BooklistView.Size = new System.Drawing.Size(this.ClientSize.Width * 1 /2, this.ClientSize.Height * 7 / 9);
            this.BooklistView.TabIndex = 1;
            this.BooklistView.UseCompatibleStateImageBehavior = false;
            this.BooklistView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ReferenceForm_DoubleClick);
            this.BooklistView.Font = new Font("宋体", 16);
            this.Load += new System.EventHandler(this.ReferenceForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.flashContrl)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private IForcePlayer flashContrl;
        private System.Windows.Forms.ListView BooklistView;
    }
}