using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;

namespace TagInventory
{
    public partial class SubFrm : Form
    {
        public SubFrm()
        {
            InitializeComponent();

            //prodlist에 데이터 추가
            DirectoryInfo di = new DirectoryInfo("./");
            FileInfo[] prod_files = di.GetFiles("*.dat");
            if (prod_files.Length != 0)
            {
                for (int index = 0; index < prod_files.Length; index++)
                {
                    string fname = prod_files[index].ToString();
                    if (fname != "uids.dat" && fname != "prices.dat")
                    {
                        //prodlist.Items.Add(prod_files[index].ToString());
                        load_data(prod_files[index].ToString());
                    }
                    

                }
            }

            this.buy_start.Enabled = true;
            this.finish_button.Enabled = false;
        }

        private void load_data(string file_name)
        {
            using (Stream prod_data = new FileStream(file_name, FileMode.Open))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                //file load
                MainFrm.Product prod = binaryFormatter.Deserialize(prod_data) as MainFrm.Product;
                string str_prod = prod.ToString();
                상품.Items.Add(str_prod);
            }
        }

        private void buy_start_Click(object sender, EventArgs e)
        {
            this.Invoke((EventHandler)(delegate
            {
                MainFrm.drawSubFlag = true;
            }));

            this.buy_start.Enabled = false;
            this.finish_button.Enabled = true;
        }

        private void finish_button_Click(object sender, EventArgs e)
        {
            this.Invoke((EventHandler)(delegate
            {
                MainFrm.drawSubFlag = false;
                //MainFrm.finishSubFlag = true;
                
            }));


            MessageBox.Show(total_price.Text + "원 구매완료");
            total_price.Text = "0";
            dataGridView1.Rows.Clear();
            this.buy_start.Enabled = true;
            this.finish_button.Enabled = false;
        }
    }
}
