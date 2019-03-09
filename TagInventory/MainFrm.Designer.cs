using System.IO;
using System.Linq;
using System.Xml.Serialization;

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
            
            XmlSerializer serializer = new XmlSerializer(typeof(UidDictClass[]), new XmlRootAttribute() { ElementName = "Uids" });
            using (Stream product_data = new FileStream("uids.dat", FileMode.Create))
            {
                serializer.Serialize(product_data, prodUIDDict.Select(kv => new UidDictClass() { uid = kv.Key, product_name = kv.Value }).ToArray());
            }

            serializer = new XmlSerializer(typeof(PriceDictClass[]), new XmlRootAttribute() { ElementName = "price" });
            using (Stream product_data = new FileStream("prices.dat", FileMode.Create))
            {
                serializer.Serialize(product_data, prodPriceDict.Select(kv => new PriceDictClass() { product_name = kv.Key, price = kv.Value }).ToArray());
            }

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
            this.delete_button = new System.Windows.Forms.Button();
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
            this.apply_button = new System.Windows.Forms.Button();
            this.nametxt = new System.Windows.Forms.TextBox();
            this.pricetxt = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.prodlist = new System.Windows.Forms.ComboBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.apply_new_button = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTag)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comboBoxFrame);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.comboBoxBaud);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.comboBoxCOM);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.buttonOpen);
            this.groupBox1.Controls.Add(this.buttonClose);
            this.groupBox1.Location = new System.Drawing.Point(33, 24);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.groupBox1.Size = new System.Drawing.Size(285, 212);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "세팅";
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
            this.buttonOpen.Location = new System.Drawing.Point(17, 169);
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
            this.buttonClose.Location = new System.Drawing.Point(147, 169);
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
            this.groupBox3.Controls.Add(this.delete_button);
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
            // delete_button
            // 
            this.delete_button.AutoSize = true;
            this.delete_button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.delete_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.delete_button.Location = new System.Drawing.Point(492, 171);
            this.delete_button.Margin = new System.Windows.Forms.Padding(4);
            this.delete_button.Name = "delete_button";
            this.delete_button.Size = new System.Drawing.Size(132, 42);
            this.delete_button.TabIndex = 12;
            this.delete_button.Text = "delete";
            this.delete_button.UseVisualStyleBackColor = false;
            this.delete_button.Click += new System.EventHandler(this.delete_button_Click);
            // 
            // labelTagNumber
            // 
            this.labelTagNumber.Font = new System.Drawing.Font("SimSun", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelTagNumber.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.labelTagNumber.Location = new System.Drawing.Point(484, 217);
            this.labelTagNumber.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelTagNumber.Name = "labelTagNumber";
            this.labelTagNumber.Size = new System.Drawing.Size(167, 58);
            this.labelTagNumber.TabIndex = 3;
            this.labelTagNumber.Text = "0";
            this.labelTagNumber.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonStop
            // 
            this.buttonStop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.buttonStop.Location = new System.Drawing.Point(492, 106);
            this.buttonStop.Margin = new System.Windows.Forms.Padding(4);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(132, 42);
            this.buttonStop.TabIndex = 2;
            this.buttonStop.Text = "Stop";
            this.buttonStop.UseVisualStyleBackColor = false;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // buttonStart
            // 
            this.buttonStart.BackColor = System.Drawing.SystemColors.Control;
            this.buttonStart.Location = new System.Drawing.Point(492, 39);
            this.buttonStart.Margin = new System.Windows.Forms.Padding(4);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(132, 42);
            this.buttonStart.TabIndex = 1;
            this.buttonStart.Text = "Start";
            this.buttonStart.UseVisualStyleBackColor = false;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // dataGridViewTag
            // 
            this.dataGridViewTag.AllowUserToAddRows = false;
            this.dataGridViewTag.AllowUserToDeleteRows = false;
            this.dataGridViewTag.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewTag.BackgroundColor = System.Drawing.Color.Azure;
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
            // apply_button
            // 
            this.apply_button.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.apply_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.apply_button.FlatAppearance.BorderSize = 0;
            this.apply_button.Location = new System.Drawing.Point(232, 24);
            this.apply_button.Name = "apply_button";
            this.apply_button.Size = new System.Drawing.Size(102, 44);
            this.apply_button.TabIndex = 0;
            this.apply_button.Text = "등록";
            this.apply_button.UseVisualStyleBackColor = false;
            this.apply_button.Click += new System.EventHandler(this.apply_button_Click);
            // 
            // nametxt
            // 
            this.nametxt.Location = new System.Drawing.Point(411, 57);
            this.nametxt.Name = "nametxt";
            this.nametxt.Size = new System.Drawing.Size(100, 25);
            this.nametxt.TabIndex = 11;
            // 
            // pricetxt
            // 
            this.pricetxt.Location = new System.Drawing.Point(410, 94);
            this.pricetxt.Name = "pricetxt";
            this.pricetxt.Size = new System.Drawing.Size(100, 25);
            this.pricetxt.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(337, 59);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "상품이름:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(341, 98);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 15);
            this.label5.TabIndex = 5;
            this.label5.Text = "가격     :";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.groupBox4);
            this.groupBox2.Controls.Add(this.apply_new_button);
            this.groupBox2.Location = new System.Drawing.Point(325, 27);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(372, 112);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "신규등록";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.button1);
            this.groupBox4.Location = new System.Drawing.Point(0, 133);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(376, 62);
            this.groupBox4.TabIndex = 14;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "신규등록";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.Location = new System.Drawing.Point(225, 31);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(102, 44);
            this.button1.TabIndex = 0;
            this.button1.Text = "등록";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.prodlist);
            this.groupBox5.Controls.Add(this.apply_button);
            this.groupBox5.Controls.Add(this.groupBox6);
            this.groupBox5.Location = new System.Drawing.Point(323, 146);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(372, 85);
            this.groupBox5.TabIndex = 15;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "기존물품등록";
            // 
            // prodlist
            // 
            this.prodlist.FormattingEnabled = true;
            this.prodlist.Location = new System.Drawing.Point(31, 36);
            this.prodlist.Name = "prodlist";
            this.prodlist.Size = new System.Drawing.Size(157, 23);
            this.prodlist.TabIndex = 15;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.button2);
            this.groupBox6.Location = new System.Drawing.Point(0, 133);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(376, 62);
            this.groupBox6.TabIndex = 14;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "신규등록";
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.Location = new System.Drawing.Point(225, 31);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(102, 44);
            this.button2.TabIndex = 0;
            this.button2.Text = "등록";
            this.button2.UseVisualStyleBackColor = false;
            // 
            // apply_new_button
            // 
            this.apply_new_button.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.apply_new_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.apply_new_button.FlatAppearance.BorderSize = 0;
            this.apply_new_button.Location = new System.Drawing.Point(230, 32);
            this.apply_new_button.Name = "apply_new_button";
            this.apply_new_button.Size = new System.Drawing.Size(102, 44);
            this.apply_new_button.TabIndex = 0;
            this.apply_new_button.Text = "등록";
            this.apply_new_button.UseVisualStyleBackColor = false;
            this.apply_new_button.Click += new System.EventHandler(this.apply_new_button_Click);
            // 
            // MainFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(713, 578);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.pricetxt);
            this.Controls.Add(this.nametxt);
            this.Controls.Add(this.total_prod_tb);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.total_price_tb);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "관리자창";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTag)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
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
        private System.Windows.Forms.Button apply_button;
        private System.Windows.Forms.Button delete_button;
        private System.Windows.Forms.TextBox nametxt;
        private System.Windows.Forms.TextBox pricetxt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button apply_new_button;
        private System.Windows.Forms.ComboBox prodlist;
    }
}

