namespace TagInventory
{
    partial class SubFrm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            상품 = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            total_price = new System.Windows.Forms.Label();
            dataGridView1 = new System.Windows.Forms.DataGridView();
            this.product_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.product_price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.finish_button = new System.Windows.Forms.Button();
            this.buy_start = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(dataGridView1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // 상품
            // 
            상품.Font = new System.Drawing.Font("굴림", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            상품.FormattingEnabled = true;
            상품.ItemHeight = 25;
            상품.Location = new System.Drawing.Point(5, 43);
            상품.Name = "상품";
            상품.Size = new System.Drawing.Size(341, 754);
            상품.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.groupBox1.Controls.Add(상품);
            this.groupBox1.Font = new System.Drawing.Font("굴림", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.groupBox1.Location = new System.Drawing.Point(12, 21);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(353, 807);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "가격표";
            // 
            // total_price
            // 
            total_price.Font = new System.Drawing.Font("SimSun", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            total_price.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            total_price.Location = new System.Drawing.Point(136, 739);
            total_price.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            total_price.Name = "total_price";
            total_price.Size = new System.Drawing.Size(167, 58);
            total_price.TabIndex = 4;
            total_price.Text = "0";
            total_price.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dataGridView1
            // 
            dataGridView1.BackgroundColor = System.Drawing.SystemColors.InactiveCaption;
            dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.product_name,
            this.product_price});
            dataGridView1.Location = new System.Drawing.Point(380, 64);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowTemplate.Height = 27;
            dataGridView1.Size = new System.Drawing.Size(305, 512);
            dataGridView1.TabIndex = 5;
            // 
            // product_name
            // 
            dataGridViewCellStyle5.Font = new System.Drawing.Font("굴림", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.product_name.DefaultCellStyle = dataGridViewCellStyle5;
            this.product_name.HeaderText = "상품명";
            this.product_name.Name = "product_name";
            this.product_name.ReadOnly = true;
            // 
            // product_price
            // 
            dataGridViewCellStyle6.Font = new System.Drawing.Font("굴림", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.product_price.DefaultCellStyle = dataGridViewCellStyle6;
            this.product_price.HeaderText = "상품 가격";
            this.product_price.Name = "product_price";
            this.product_price.ReadOnly = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.finish_button);
            this.groupBox2.Controls.Add(this.buy_start);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(total_price);
            this.groupBox2.Font = new System.Drawing.Font("굴림", 16F);
            this.groupBox2.Location = new System.Drawing.Point(371, 21);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(320, 807);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "구매목록";
            // 
            // finish_button
            // 
            this.finish_button.Location = new System.Drawing.Point(164, 573);
            this.finish_button.Name = "finish_button";
            this.finish_button.Size = new System.Drawing.Size(148, 62);
            this.finish_button.TabIndex = 7;
            this.finish_button.Text = "구매종료";
            this.finish_button.UseVisualStyleBackColor = true;
            this.finish_button.Click += new System.EventHandler(this.finish_button_Click);
            // 
            // buy_start
            // 
            this.buy_start.Location = new System.Drawing.Point(9, 573);
            this.buy_start.Name = "buy_start";
            this.buy_start.Size = new System.Drawing.Size(148, 62);
            this.buy_start.TabIndex = 6;
            this.buy_start.Text = "구매시작";
            this.buy_start.UseVisualStyleBackColor = true;
            this.buy_start.Click += new System.EventHandler(this.buy_start_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("굴림", 20F);
            this.label1.Location = new System.Drawing.Point(17, 709);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(155, 41);
            this.label1.TabIndex = 0;
            this.label1.Text = "총 금액";
            // 
            // SubFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(703, 840);
            this.Controls.Add(dataGridView1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Name = "SubFrm";
            this.Text = "소비자창";
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(dataGridView1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        public static System.Windows.Forms.Label total_price;
        private System.Windows.Forms.DataGridViewTextBoxColumn product_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn product_price;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buy_start;
        private System.Windows.Forms.Button finish_button;
        public static System.Windows.Forms.DataGridView dataGridView1;
        public static System.Windows.Forms.ListBox 상품;
    }
}