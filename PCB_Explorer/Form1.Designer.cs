namespace PCB_Explorer
{
    partial class Form1
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
            this.nuScale = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.nuScaleX = new System.Windows.Forms.NumericUpDown();
            this.btnSave = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.nuScaleY = new System.Windows.Forms.NumericUpDown();
            this.nuOffsetX = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.nuOffsetY = new System.Windows.Forms.NumericUpDown();
            this.lbItems = new System.Windows.Forms.ListBox();
            this.tbItem = new System.Windows.Forms.TextBox();
            this.btnItemAdd = new System.Windows.Forms.Button();
            this.lbContact = new System.Windows.Forms.ListBox();
            this.btnAddContactLeft = new System.Windows.Forms.Button();
            this.btnAddContactBoth = new System.Windows.Forms.Button();
            this.btnAddContactRight = new System.Windows.Forms.Button();
            this.btnItemDel = new System.Windows.Forms.Button();
            this.btnDelContact = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nuScale)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nuScaleX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nuScaleY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nuOffsetX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nuOffsetY)).BeginInit();
            this.SuspendLayout();
            // 
            // nuScale
            // 
            this.nuScale.DecimalPlaces = 3;
            this.nuScale.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.nuScale.Location = new System.Drawing.Point(12, 25);
            this.nuScale.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nuScale.Name = "nuScale";
            this.nuScale.Size = new System.Drawing.Size(57, 20);
            this.nuScale.TabIndex = 0;
            this.nuScale.Value = new decimal(new int[] {
            2,
            0,
            0,
            65536});
            this.nuScale.ValueChanged += new System.EventHandler(this.nuScale_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Scale";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 459);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Scale X";
            // 
            // nuScaleX
            // 
            this.nuScaleX.DecimalPlaces = 3;
            this.nuScaleX.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.nuScaleX.Location = new System.Drawing.Point(13, 475);
            this.nuScaleX.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nuScaleX.Name = "nuScaleX";
            this.nuScaleX.Size = new System.Drawing.Size(72, 20);
            this.nuScaleX.TabIndex = 0;
            this.nuScaleX.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nuScaleX.ValueChanged += new System.EventHandler(this.nuScaleX_ValueChanged);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(75, 25);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(42, 23);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 498);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Scale Y";
            // 
            // nuScaleY
            // 
            this.nuScaleY.DecimalPlaces = 3;
            this.nuScaleY.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.nuScaleY.Location = new System.Drawing.Point(13, 514);
            this.nuScaleY.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nuScaleY.Name = "nuScaleY";
            this.nuScaleY.Size = new System.Drawing.Size(72, 20);
            this.nuScaleY.TabIndex = 0;
            this.nuScaleY.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nuScaleY.ValueChanged += new System.EventHandler(this.nuScaleY_ValueChanged);
            // 
            // nuOffsetX
            // 
            this.nuOffsetX.Location = new System.Drawing.Point(12, 397);
            this.nuOffsetX.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nuOffsetX.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.nuOffsetX.Name = "nuOffsetX";
            this.nuOffsetX.Size = new System.Drawing.Size(72, 20);
            this.nuOffsetX.TabIndex = 0;
            this.nuOffsetX.ValueChanged += new System.EventHandler(this.nuOffsetX_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 420);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(45, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Offset Y";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 381);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Offset X";
            // 
            // nuOffsetY
            // 
            this.nuOffsetY.Location = new System.Drawing.Point(12, 436);
            this.nuOffsetY.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nuOffsetY.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.nuOffsetY.Name = "nuOffsetY";
            this.nuOffsetY.Size = new System.Drawing.Size(72, 20);
            this.nuOffsetY.TabIndex = 0;
            this.nuOffsetY.ValueChanged += new System.EventHandler(this.nuOffsetY_ValueChanged);
            // 
            // lbItems
            // 
            this.lbItems.FormattingEnabled = true;
            this.lbItems.Location = new System.Drawing.Point(12, 54);
            this.lbItems.Name = "lbItems";
            this.lbItems.Size = new System.Drawing.Size(113, 173);
            this.lbItems.TabIndex = 3;
            this.lbItems.SelectedIndexChanged += new System.EventHandler(this.lbItems_SelectedIndexChanged);
            // 
            // tbItem
            // 
            this.tbItem.Location = new System.Drawing.Point(31, 236);
            this.tbItem.Name = "tbItem";
            this.tbItem.Size = new System.Drawing.Size(66, 20);
            this.tbItem.TabIndex = 4;
            // 
            // btnItemAdd
            // 
            this.btnItemAdd.Location = new System.Drawing.Point(103, 236);
            this.btnItemAdd.Name = "btnItemAdd";
            this.btnItemAdd.Size = new System.Drawing.Size(22, 23);
            this.btnItemAdd.TabIndex = 5;
            this.btnItemAdd.Text = "+";
            this.btnItemAdd.UseVisualStyleBackColor = true;
            this.btnItemAdd.Click += new System.EventHandler(this.btnItemAdd_Click);
            // 
            // lbContact
            // 
            this.lbContact.FormattingEnabled = true;
            this.lbContact.Location = new System.Drawing.Point(12, 262);
            this.lbContact.Name = "lbContact";
            this.lbContact.Size = new System.Drawing.Size(113, 82);
            this.lbContact.TabIndex = 3;
            this.lbContact.SelectedIndexChanged += new System.EventHandler(this.lbContact_SelectedIndexChanged);
            // 
            // btnAddContactLeft
            // 
            this.btnAddContactLeft.Location = new System.Drawing.Point(47, 350);
            this.btnAddContactLeft.Name = "btnAddContactLeft";
            this.btnAddContactLeft.Size = new System.Drawing.Size(22, 23);
            this.btnAddContactLeft.TabIndex = 5;
            this.btnAddContactLeft.Text = "L";
            this.btnAddContactLeft.UseVisualStyleBackColor = true;
            this.btnAddContactLeft.Click += new System.EventHandler(this.btnAddContactLeft_Click);
            // 
            // btnAddContactBoth
            // 
            this.btnAddContactBoth.Location = new System.Drawing.Point(75, 350);
            this.btnAddContactBoth.Name = "btnAddContactBoth";
            this.btnAddContactBoth.Size = new System.Drawing.Size(22, 23);
            this.btnAddContactBoth.TabIndex = 5;
            this.btnAddContactBoth.Text = "B";
            this.btnAddContactBoth.UseVisualStyleBackColor = true;
            this.btnAddContactBoth.Click += new System.EventHandler(this.btnAddContactBoth_Click);
            // 
            // btnAddContactRight
            // 
            this.btnAddContactRight.Location = new System.Drawing.Point(103, 350);
            this.btnAddContactRight.Name = "btnAddContactRight";
            this.btnAddContactRight.Size = new System.Drawing.Size(22, 23);
            this.btnAddContactRight.TabIndex = 5;
            this.btnAddContactRight.Text = "R";
            this.btnAddContactRight.UseVisualStyleBackColor = true;
            this.btnAddContactRight.Click += new System.EventHandler(this.btnAddContactRight_Click);
            // 
            // btnItemDel
            // 
            this.btnItemDel.Location = new System.Drawing.Point(12, 234);
            this.btnItemDel.Name = "btnItemDel";
            this.btnItemDel.Size = new System.Drawing.Size(13, 23);
            this.btnItemDel.TabIndex = 5;
            this.btnItemDel.Text = "-";
            this.btnItemDel.UseVisualStyleBackColor = true;
            this.btnItemDel.Click += new System.EventHandler(this.btnItemDel_Click);
            // 
            // btnDelContact
            // 
            this.btnDelContact.Location = new System.Drawing.Point(15, 350);
            this.btnDelContact.Name = "btnDelContact";
            this.btnDelContact.Size = new System.Drawing.Size(13, 23);
            this.btnDelContact.TabIndex = 5;
            this.btnDelContact.Text = "-";
            this.btnDelContact.UseVisualStyleBackColor = true;
            this.btnDelContact.Click += new System.EventHandler(this.btnDelContact_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(959, 754);
            this.Controls.Add(this.btnAddContactRight);
            this.Controls.Add(this.btnAddContactBoth);
            this.Controls.Add(this.btnAddContactLeft);
            this.Controls.Add(this.btnDelContact);
            this.Controls.Add(this.btnItemDel);
            this.Controls.Add(this.btnItemAdd);
            this.Controls.Add(this.tbItem);
            this.Controls.Add(this.lbContact);
            this.Controls.Add(this.lbItems);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nuScaleY);
            this.Controls.Add(this.nuOffsetY);
            this.Controls.Add(this.nuOffsetX);
            this.Controls.Add(this.nuScaleX);
            this.Controls.Add(this.nuScale);
            this.Name = "Form1";
            this.Text = "PCB Explorer";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            ((System.ComponentModel.ISupportInitialize)(this.nuScale)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nuScaleX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nuScaleY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nuOffsetX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nuOffsetY)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown nuScale;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nuScaleX;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown nuScaleY;
        private System.Windows.Forms.NumericUpDown nuOffsetX;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown nuOffsetY;
        private System.Windows.Forms.ListBox lbItems;
        private System.Windows.Forms.TextBox tbItem;
        private System.Windows.Forms.Button btnItemAdd;
        private System.Windows.Forms.ListBox lbContact;
        private System.Windows.Forms.Button btnAddContactLeft;
        private System.Windows.Forms.Button btnAddContactBoth;
        private System.Windows.Forms.Button btnAddContactRight;
        private System.Windows.Forms.Button btnItemDel;
        private System.Windows.Forms.Button btnDelContact;
    }
}

