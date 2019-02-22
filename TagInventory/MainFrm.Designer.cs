namespace TagInventory
{
    partial class MainFrm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxCommunication = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comboBoxFrame = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBoxBaud = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxCOM = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonOpen = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.labelTagNumber = new System.Windows.Forms.Label();
            this.buttonStop = new System.Windows.Forms.Button();
            this.buttonStart = new System.Windows.Forms.Button();
            this.dataGridViewTag = new System.Windows.Forms.DataGridView();
            this.UID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.product_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.count = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.total_price_tb = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.total_prod_tb = new System.Windows.Forms.TextBox();
            this.coke_button = new System.Windows.Forms.Button();
            this.soda_button = new System.Windows.Forms.Button();
            this.coffee_button = new System.Windows.Forms.Button();
            this.milk_button = new System.Windows.Forms.Button();
            this.salad_button = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTag)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 30);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Communication:";
            // 
            // comboBoxCommunication
            // 
            this.comboBoxCommunication.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCommunication.FormattingEnabled = true;
            this.comboBoxCommunication.Location = new System.Drawing.Point(155, 26);
            this.comboBoxCommunication.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxCommunication.Name = "comboBoxCommunication";
            this.comboBoxCommunication.Size = new System.Drawing.Size(160, 23);
            this.comboBoxCommunication.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comboBoxFrame);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.comboBoxBaud);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.comboBoxCOM);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(31, 72);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(285, 161);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Serial Port";
            // 
            // comboBoxFrame
            // 
            this.comboBoxFrame.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFrame.FormattingEnabled = true;
            this.comboBoxFrame.Location = new System.Drawing.Point(77, 119);
            this.comboBoxFrame.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxFrame.Name = "comboBoxFrame";
            this.comboBoxFrame.Size = new System.Drawing.Size(160, 23);
            this.comboBoxFrame.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 122);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 15);
            this.label4.TabIndex = 2;
            this.label4.Text = "Frame:";
            // 
            // comboBoxBaud
            // 
            this.comboBoxBaud.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxBaud.FormattingEnabled = true;
            this.comboBoxBaud.Location = new System.Drawing.Point(77, 76);
            this.comboBoxBaud.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxBaud.Name = "comboBoxBaud";
            this.comboBoxBaud.Size = new System.Drawing.Size(160, 23);
            this.comboBoxBaud.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 80);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "Baud:";
            // 
            // comboBoxCOM
            // 
            this.comboBoxCOM.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCOM.FormattingEnabled = true;
            this.comboBoxCOM.Location = new System.Drawing.Point(77, 29);
            this.comboBoxCOM.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxCOM.Name = "comboBoxCOM";
            this.comboBoxCOM.Size = new System.Drawing.Size(160, 23);
            this.comboBoxCOM.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 32);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 15);
            this.label2.TabIndex = 0;
            this.label2.Text = "COM:";
            // 
            // buttonOpen
            // 
            this.buttonOpen.Location = new System.Drawing.Point(376, 24);
            this.buttonOpen.Margin = new System.Windows.Forms.Padding(4);
            this.buttonOpen.Name = "buttonOpen";
            this.buttonOpen.Size = new System.Drawing.Size(100, 29);
            this.buttonOpen.TabIndex = 4;
            this.buttonOpen.Text = "Open";
            this.buttonOpen.UseVisualStyleBackColor = true;
            this.buttonOpen.Click += new System.EventHandler(this.buttonOpen_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(525, 24);
            this.buttonClose.Margin = new System.Windows.Forms.Padding(4);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(100, 29);
            this.buttonClose.TabIndex = 5;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.labelTagNumber);
            this.groupBox3.Controls.Add(this.buttonStop);
            this.groupBox3.Controls.Add(this.buttonStart);
            this.groupBox3.Controls.Add(this.dataGridViewTag);
            this.groupBox3.Location = new System.Drawing.Point(33, 259);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox3.Size = new System.Drawing.Size(664, 304);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            // 
            // labelTagNumber
            // 
            this.labelTagNumber.Font = new System.Drawing.Font("SimSun", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelTagNumber.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.labelTagNumber.Location = new System.Drawing.Point(489, 184);
            this.labelTagNumber.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelTagNumber.Name = "labelTagNumber";
            this.labelTagNumber.Size = new System.Drawing.Size(167, 58);
            this.labelTagNumber.TabIndex = 3;
            this.labelTagNumber.Text = "0";
            this.labelTagNumber.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonStop
            // 
            this.buttonStop.Location = new System.Drawing.Point(492, 106);
            this.buttonStop.Margin = new System.Windows.Forms.Padding(4);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(132, 42);
            this.buttonStop.TabIndex = 2;
            this.buttonStop.Text = "Stop";
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(492, 39);
            this.buttonStart.Margin = new System.Windows.Forms.Padding(4);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(132, 42);
            this.buttonStart.TabIndex = 1;
            this.buttonStart.Text = "Start";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // dataGridViewTag
            // 
            this.dataGridViewTag.AllowUserToAddRows = false;
            this.dataGridViewTag.AllowUserToDeleteRows = false;
            this.dataGridViewTag.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewTag.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewTag.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.UID,
            this.product_name,
            this.price,
            this.count});
            this.dataGridViewTag.Location = new System.Drawing.Point(17, 21);
            this.dataGridViewTag.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridViewTag.Name = "dataGridViewTag";
            this.dataGridViewTag.ReadOnly = true;
            this.dataGridViewTag.RowTemplate.Height = 23;
            this.dataGridViewTag.Size = new System.Drawing.Size(440, 271);
            this.dataGridViewTag.TabIndex = 0;
            // 
            // UID
            // 
            this.UID.HeaderText = "UID";
            this.UID.Name = "UID";
            this.UID.ReadOnly = true;
            // 
            // product_name
            // 
            this.product_name.HeaderText = "품명";
            this.product_name.Name = "product_name";
            this.product_name.ReadOnly = true;
            // 
            // price
            // 
            this.price.HeaderText = "단가";
            this.price.Name = "price";
            this.price.ReadOnly = true;
            // 
            // count
            // 
            this.count.HeaderText = "수량";
            this.count.Name = "count";
            this.count.ReadOnly = true;
            // 
            // total_price_tb
            // 
            this.total_price_tb.Location = new System.Drawing.Point(123, 237);
            this.total_price_tb.Name = "total_price_tb";
            this.total_price_tb.Size = new System.Drawing.Size(100, 25);
            this.total_price_tb.TabIndex = 7;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(30, 240);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(87, 15);
            this.label7.TabIndex = 8;
            this.label7.Text = "총판매 가격";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(252, 240);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(52, 15);
            this.label8.TabIndex = 9;
            this.label8.Text = "총품목";
            // 
            // total_prod_tb
            // 
            this.total_prod_tb.Location = new System.Drawing.Point(315, 237);
            this.total_prod_tb.Name = "total_prod_tb";
            this.total_prod_tb.Size = new System.Drawing.Size(374, 25);
            this.total_prod_tb.TabIndex = 10;
            // 
            // coke_button
            // 
            this.coke_button.Location = new System.Drawing.Point(332, 104);
            this.coke_button.Name = "coke_button";
            this.coke_button.Size = new System.Drawing.Size(75, 23);
            this.coke_button.TabIndex = 0;
            this.coke_button.Text = "콜라";
            this.coke_button.UseVisualStyleBackColor = true;
            this.coke_button.Click += new System.EventHandler(this.coke_button_Click);
            // 
            // soda_button
            // 
            this.soda_button.Location = new System.Drawing.Point(470, 104);
            this.soda_button.Name = "soda_button";
            this.soda_button.Size = new System.Drawing.Size(75, 23);
            this.soda_button.TabIndex = 0;
            this.soda_button.Text = "사이다";
            this.soda_button.UseVisualStyleBackColor = true;
            this.soda_button.Click += new System.EventHandler(this.soda_button_Click);
            // 
            // coffee_button
            // 
            this.coffee_button.Location = new System.Drawing.Point(605, 104);
            this.coffee_button.Name = "coffee_button";
            this.coffee_button.Size = new System.Drawing.Size(75, 23);
            this.coffee_button.TabIndex = 0;
            this.coffee_button.Text = "커피";
            this.coffee_button.UseVisualStyleBackColor = true;
            this.coffee_button.Click += new System.EventHandler(this.coffee_button_Click);
            // 
            // milk_button
            // 
            this.milk_button.Location = new System.Drawing.Point(401, 175);
            this.milk_button.Name = "milk_button";
            this.milk_button.Size = new System.Drawing.Size(75, 23);
            this.milk_button.TabIndex = 0;
            this.milk_button.Text = "우유";
            this.milk_button.UseVisualStyleBackColor = true;
            this.milk_button.Click += new System.EventHandler(this.milk_button_Click);
            // 
            // salad_button
            // 
            this.salad_button.Location = new System.Drawing.Point(530, 175);
            this.salad_button.Name = "salad_button";
            this.salad_button.Size = new System.Drawing.Size(75, 23);
            this.salad_button.TabIndex = 0;
            this.salad_button.Text = "샐러드";
            this.salad_button.UseVisualStyleBackColor = true;
            this.salad_button.Click += new System.EventHandler(this.salad_button_Click);
            // 
            // MainFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(713, 578);
            this.Controls.Add(this.salad_button);
            this.Controls.Add(this.milk_button);
            this.Controls.Add(this.coffee_button);
            this.Controls.Add(this.soda_button);
            this.Controls.Add(this.coke_button);
            this.Controls.Add(this.total_prod_tb);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.total_price_tb);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonOpen);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.comboBoxCommunication);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tag Inventory";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTag)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxCommunication;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox comboBoxCOM;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxFrame;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBoxBaud;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonOpen;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.DataGridView dataGridViewTag;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.Label labelTagNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn UID;
        private System.Windows.Forms.DataGridViewTextBoxColumn product_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn price;
        private System.Windows.Forms.DataGridViewTextBoxColumn count;
        private System.Windows.Forms.TextBox total_price_tb;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox total_prod_tb;
        private System.Windows.Forms.Button coke_button;
        private System.Windows.Forms.Button soda_button;
        private System.Windows.Forms.Button coffee_button;
        private System.Windows.Forms.Button milk_button;
        private System.Windows.Forms.Button salad_button;
    }
}

