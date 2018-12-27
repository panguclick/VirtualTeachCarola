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
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listBox1 = new System.Windows.Forms.ListBox();
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
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.Location = new System.Drawing.Point(40, 123);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(642, 278);
            this.listView1.TabIndex = 3;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.DoubleClick += new System.EventHandler(this.ListView_DoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "√";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "测量项目";
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "值";
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "单位";
            // 
            // listBox1
            // 
            this.listBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 23;
            this.listBox1.Location = new System.Drawing.Point(40, 124);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(642, 257);
            this.listBox1.TabIndex = 4;
            this.listBox1.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.ListBox1_DrawItem);
            // 
            // K600Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(730, 520);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.listView1);
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
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ListBox listBox1;
    }
}