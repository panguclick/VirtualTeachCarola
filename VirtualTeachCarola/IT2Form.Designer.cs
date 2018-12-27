namespace VirtualTeachCarola
{
    partial class IT2Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IT2Form));
            this.axShockwaveFlash1 = new AxShockwaveFlashObjects.AxShockwaveFlash();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.listViewDATA = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listViewDTC = new System.Windows.Forms.ListView();
            this.columnHeaderDTC1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderDTC2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            ((System.ComponentModel.ISupportInitialize)(this.axShockwaveFlash1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // axShockwaveFlash1
            // 
            this.axShockwaveFlash1.Enabled = true;
            this.axShockwaveFlash1.Location = new System.Drawing.Point(0, 33);
            this.axShockwaveFlash1.Name = "axShockwaveFlash1";
            this.axShockwaveFlash1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axShockwaveFlash1.OcxState")));
            this.axShockwaveFlash1.Size = new System.Drawing.Size(510, 757);
            this.axShockwaveFlash1.TabIndex = 0;
            this.axShockwaveFlash1.Enter += new System.EventHandler(this.Flash_Enter);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(510, 40);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form_MouseMove);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form_MouseUp);
            // 
            // listViewDATA
            // 
            this.listViewDATA.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.listViewDATA.FullRowSelect = true;
            this.listViewDATA.GridLines = true;
            this.listViewDATA.Location = new System.Drawing.Point(36, 140);
            this.listViewDATA.MultiSelect = false;
            this.listViewDATA.Name = "listViewDATA";
            this.listViewDATA.Size = new System.Drawing.Size(432, 295);
            this.listViewDATA.TabIndex = 3;
            this.listViewDATA.UseCompatibleStateImageBehavior = false;
            this.listViewDATA.View = System.Windows.Forms.View.Details;
            this.listViewDATA.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ListView_DoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "√";
            this.columnHeader1.Width = 32;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "测量项目";
            this.columnHeader2.Width = 150;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "值";
            this.columnHeader3.Width = 65;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "单位";
            this.columnHeader4.Width = 38;
            // 
            // listViewDTC
            // 
            this.listViewDTC.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderDTC1,
            this.columnHeaderDTC2});
            this.listViewDTC.FullRowSelect = true;
            this.listViewDTC.GridLines = true;
            this.listViewDTC.Location = new System.Drawing.Point(36, 134);
            this.listViewDTC.MultiSelect = false;
            this.listViewDTC.Name = "listViewDTC";
            this.listViewDTC.Size = new System.Drawing.Size(432, 295);
            this.listViewDTC.TabIndex = 4;
            this.listViewDTC.UseCompatibleStateImageBehavior = false;
            this.listViewDTC.View = System.Windows.Forms.View.Details;
            this.listViewDTC.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ListView_DoubleClick);
            // 
            // columnHeaderDTC1
            // 
            this.columnHeaderDTC1.Text = "";
            this.columnHeaderDTC1.Width = 71;
            // 
            // columnHeaderDTC2
            // 
            this.columnHeaderDTC2.Text = "";
            this.columnHeaderDTC2.Width = 209;
            // 
            // IT2Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(510, 790);
            this.Controls.Add(this.listViewDTC);
            this.Controls.Add(this.listViewDATA);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.axShockwaveFlash1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "IT2Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "IT2Form";
            ((System.ComponentModel.ISupportInitialize)(this.axShockwaveFlash1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AxShockwaveFlashObjects.AxShockwaveFlash axShockwaveFlash1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ListView listViewDATA;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;

        private System.Windows.Forms.ListView listViewDTC;
        private System.Windows.Forms.ColumnHeader columnHeaderDTC1;
        private System.Windows.Forms.ColumnHeader columnHeaderDTC2;
    }
}