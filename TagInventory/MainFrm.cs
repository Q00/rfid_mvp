using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Linq;

namespace TagInventory
{
    
    public partial class MainFrm : Form
    {
        private UIntPtr hReader = UIntPtr.Zero;
        private UIntPtr InvenParamSpecList = UIntPtr.Zero;
        private int coke = 0;
        private int soda = 0;
        private int milk = 0;
        private int coffee = 0;
        private int salad = 0;

        private bool bInventoryFlg = false;
        private Thread inventoryThrd = null;
        static private Thread prodThrd = null;
        static private Dictionary<string, int> prodPriceDict = new Dictionary<string, int>();
        static private Dictionary<string, string> prodUIDDict = new Dictionary<string, string>();
        static private List<string> addUidList = new List<string>();
        //�Һ��ڴ� flag
        static public bool drowSubFlag = false;

        public MainFrm()
        {

            InitializeComponent();


            RFIDLIB.rfidlib_reader.RDR_LoadReaderDrivers(@"\Drivers");
            UInt32 nComCount = RFIDLIB.rfidlib_reader.COMPort_Enum();
            for (UInt32 j = 0; j < nComCount; j++)
            {
                StringBuilder nameBuf = new StringBuilder();
                nameBuf.Append('\0', 128);
                RFIDLIB.rfidlib_reader.COMPort_GetEnumItem(j, nameBuf, 128);
                comboBoxCOM.Items.Add(nameBuf.ToString());
            }
            if (nComCount > 0)
            {
                comboBoxCOM.SelectedIndex = 0;
            }

            comboBoxBaud.Items.Add("38400");
            comboBoxBaud.SelectedIndex = 0;

            comboBoxFrame.Items.Add("8E1");
            comboBoxFrame.Items.Add("8O1");
            comboBoxFrame.Items.Add("8N1");
            comboBoxFrame.SelectedIndex = 0;

            //prodlist�� ������ �߰�
            DirectoryInfo di = new DirectoryInfo("./");
            FileInfo[] prod_files = di.GetFiles("*.dat");
            if(prod_files.Length != 0)
            {
                for(int index=0; index < prod_files.Length; index++)
                {
                    //prodlist.Items.Add(prod_files[index].ToString());
                    load_data(prod_files[index].ToString());

                }
            }
            buttonOpen.Enabled = true;
            buttonClose.Enabled = false;
            comboBoxCOM.Enabled = true;
            comboBoxBaud.Enabled = true;
            comboBoxFrame.Enabled = true;
            buttonStart.Enabled = false;
            buttonStop.Enabled = false;
        }

        private void buttonOpen_Click(object sender, EventArgs e)
        {
            String connstr = "";

            connstr = RFIDLIB.rfidlib_def.CONNSTR_NAME_RDTYPE + "=" + "RD5100" + ";" +
                RFIDLIB.rfidlib_def.CONNSTR_NAME_COMMTYPE + "=" + RFIDLIB.rfidlib_def.CONNSTR_NAME_COMMTYPE_COM + ";" +
                RFIDLIB.rfidlib_def.CONNSTR_NAME_COMNAME + "=" + comboBoxCOM.Text + ";" +
                RFIDLIB.rfidlib_def.CONNSTR_NAME_COMBARUD + "=" + comboBoxBaud.Text + ";" +
                RFIDLIB.rfidlib_def.CONNSTR_NAME_COMFRAME + "=" + comboBoxFrame.Text + ";" +
                RFIDLIB.rfidlib_def.CONNSTR_NAME_BUSADDR + "=" + "255";



            //dataGridViewTag.Rows.Clear();
            //dataGridViewTag.Rows.Add(1);
            //dataGridViewTag[0, 0].Value = "123";
            //dataGridViewTag[1, 0].Value = "����ʿ�";
            //dataGridViewTag[0, 1].Value = "12345";
            //dataGridViewTag[1, 1].Value = "����ʿ�";
            //dataGridViewTag[0, 2].Value = "1236";
            //dataGridViewTag[1, 2].Value = "����ʿ�";
            //dataGridViewTag[0, 3].Value = "12345";
            //dataGridViewTag[1, 3].Value = "����ʿ�";
            //dataGridViewTag[0, 4].Value = "1236";
            //dataGridViewTag[1, 4].Value = "����ʿ�";

            //addUidList.Add("123");
            //addUidList.Add("12345");
            //addUidList.Add("1236");

            int iret = RFIDLIB.rfidlib_reader.RDR_Open(connstr, ref hReader);
            if (iret != 0)
            {
                return;
            }
            //var a = Enumerable.Except(prodUIDDict.Keys.ToArray(), dataGridViewTag.Rows.OfType<DataGridViewRow>().Select(r => r.Cells[0].Value.ToString()).ToArray());
            InvenParamSpecList = RFIDLIB.rfidlib_reader.RDR_CreateInvenParamSpecList();
            RFIDLIB.rfidlib_aip_iso15693.ISO15693_CreateInvenParam(InvenParamSpecList, 0, 0, 0, 0);
            buttonOpen.Enabled = false;
            buttonClose.Enabled = true;
            comboBoxCOM.Enabled = false;
            comboBoxBaud.Enabled = false;
            comboBoxFrame.Enabled = false;
            buttonStart.Enabled = true;
            buttonStop.Enabled = false;
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            datagrid_close();
        }

        private void datagrid_close()
        {
            if (inventoryThrd != null && inventoryThrd.IsAlive)
            {
                MessageBox.Show("Please stop the inventory thread first.");
                return;
            }
            if (InvenParamSpecList != UIntPtr.Zero)
            {
                RFIDLIB.rfidlib_reader.DNODE_Destroy(InvenParamSpecList);
                InvenParamSpecList = UIntPtr.Zero;
            }
            RFIDLIB.rfidlib_reader.RDR_Close(hReader);
            hReader = UIntPtr.Zero;
            buttonOpen.Enabled = true;
            buttonClose.Enabled = false;
            comboBoxCOM.Enabled = true;
            comboBoxBaud.Enabled = true;
            comboBoxFrame.Enabled = true;
            buttonStart.Enabled = false;
            buttonStop.Enabled = false;
        }

        private int Inventory(List<String> uids)
        {
            if (uids.Count > 0)
            {
                uids.Clear();
            }

            Byte[] antennaList = new Byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            int iret = RFIDLIB.rfidlib_reader.RDR_TagInventory(hReader, RFIDLIB.rfidlib_def.AI_TYPE_NEW, (byte)antennaList.Length, antennaList, InvenParamSpecList);

            if (iret != 0)
            {
                return iret;
            }

            UIntPtr TagDataReport = RFIDLIB.rfidlib_reader.RDR_GetTagDataReport(hReader, RFIDLIB.rfidlib_def.RFID_SEEK_FIRST); //first
            while (TagDataReport != UIntPtr.Zero)
            {
                UInt32 aip_id = 0;
                UInt32 tag_id = 0;
                UInt32 ant_id = 0;
                Byte dsfid = 0;
                Byte[] uid = new Byte[8];
                iret = RFIDLIB.rfidlib_aip_iso15693.ISO15693_ParseTagDataReport(TagDataReport, ref aip_id, ref tag_id, ref ant_id, ref dsfid, uid);
                if (iret == 0)
                {
                    string strUid = BitConverter.ToString(uid, 0, (int)8).Replace("-", string.Empty);
                    if (!uids.Contains(strUid))
                    {
                        uids.Add(strUid);
                    }

                }
                TagDataReport = RFIDLIB.rfidlib_reader.RDR_GetTagDataReport(hReader, RFIDLIB.rfidlib_def.RFID_SEEK_NEXT); //first
            }
            return 0;
        }
        private void buttonStart_Click(object sender, EventArgs e)
        {
            datagrid_start();
        }

        private void datagrid_start()
        {
            buttonStart.Enabled = false;
            buttonStop.Enabled = true;
            dataGridViewTag.Rows.Clear();
            labelTagNumber.Text = "0";
            inventoryThrd = new Thread(InventoryProc);
            inventoryThrd.Start();
        }

        void InventoryProc()
        {
            bInventoryFlg = true;
            coke = 0; coffee = 0; milk = 0; salad = 0; soda = 0;
            while (bInventoryFlg)
            {
                //uid init
                List<string> uids = new List<string>();
                int iret = Inventory(uids);
                if (iret != 0)
                {
                    continue;
                }
                RFIDLIB.rfidlib_reader.RDR_CloseRFTransmitter(hReader);
                //show the tags
                var first_check_data = dataGridViewTag.Rows.OfType<DataGridViewRow>().Select(r => r.Cells[0].Value.ToString()).ToArray();
                this.Invoke((EventHandler)(delegate
               {
                   if (dataGridViewTag.RowCount > uids.Count)
                   {
                       while (dataGridViewTag.RowCount > uids.Count)
                       {
                           dataGridViewTag.Rows.RemoveAt(dataGridViewTag.RowCount - 1);
                       }

                       int total_price = 0;
                       foreach (string prod in prodUIDDict.Values)
                       {
                           total_price += prodPriceDict[prod];
                       }

                       //prodUIDDict.Values.Distinct().ToList();


                       int present_price = 0;
                       for (int c = 0; c < dataGridViewTag.Rows.Count; c++)
                       {
                           present_price += Int32.Parse(dataGridViewTag[2, c].Value.ToString());
                       }

                       total_price_tb.Text = (total_price - present_price).ToString();
                   }
                   else if (dataGridViewTag.RowCount < uids.Count)
                   {
                       dataGridViewTag.Rows.Add(uids.Count - dataGridViewTag.RowCount);
                   }
                   for (int j = 0; j < uids.Count; j++)
                   {
                       if (prodUIDDict.ContainsKey(uids[j]))
                       {
                           //already exist
                           dataGridViewTag[0, j].Value = uids[j];
                           dataGridViewTag[1, j].Value = prodUIDDict[uids[j]];
                           dataGridViewTag[2, j].Value = prodPriceDict[prodUIDDict[uids[j]]];
                           dataGridViewTag[3, j].Value = 1;

                       }
                       else
                       {
                           //not exist , have to add.
                           dataGridViewTag[0, j].Value = uids[j];
                           dataGridViewTag[1, j].Value = "����ʿ�";
                           //if (!addUidList.Contains(uids[j]))
                           //{
                           //    addUidList.Add(uids[j]);
                           //}
                       }

                   }
                    
                   labelTagNumber.Text = uids.Count + "";
                   var except = Enumerable.Except(prodUIDDict.Keys.ToArray(), dataGridViewTag.Rows.OfType<DataGridViewRow>().Select(r => r.Cells[0].Value.ToString()).ToArray());
                   Dictionary<string, string> total_dict = new Dictionary<string, string>();
                   foreach (string uuid in except)
                   {
                       total_dict.Add(prodUIDDict[uuid], prodPriceDict[prodUIDDict[uuid]].ToString());
                   }
                   var second_check_data = dataGridViewTag.Rows.OfType<DataGridViewRow>().Select(r => r.Cells[0].Value.ToString()).ToArray();
                   if (drowSubFlag)
                   {
                       if(Enumerable.Except(first_check_data, second_check_data).Count() != 0)
                       {
                           SubFrm.dataGridView1.Rows.Clear();
                           SubFrm.dataGridView1.Rows.Add(Enumerable.Except(first_check_data, second_check_data).Count());
                           int count = 0;
                           foreach(string subuid in Enumerable.Except(first_check_data, second_check_data))
                           {
                               SubFrm.dataGridView1[0, count].Value = prodUIDDict[subuid].ToString();
                               SubFrm.dataGridView1[1, count].Value = prodPriceDict[prodUIDDict[subuid].ToString()].ToString();
                               count++;
                           }


                       }
                   }
                   string total_str = ToPrettyString(total_dict);
                   total_prod_tb.Text = total_str;

               }));
            }

            this.Invoke((EventHandler)(delegate
            {
                buttonStop.Enabled = false;
                buttonStart.Enabled = true;
                bInventoryFlg = false;
                inventoryThrd = null;
            }));
            
        }



        private void buttonStop_Click(object sender, EventArgs e)
        {
            buttonStop.Enabled = false;
            bInventoryFlg = false;
        }

        /// <summary>
        /// ������ �ҷ��ִ� �Լ�
        /// </summary>
        /// <param name="prod"></param>
        private void prod_button_function(string prod, string price)
        {
            try
            {
                buttonStop.Enabled = false;
                bInventoryFlg = false;
                this.Invoke((EventHandler)(delegate
                {
                    int delete_count = dataGridViewTag.SelectedRows.Count;
                    var enumer = dataGridViewTag.SelectedRows.GetEnumerator();
                    enumer.MoveNext();
                    for (int j = 0; j < delete_count; j++)
                    {
                        DataGridViewRow selected = (DataGridViewRow)enumer.Current;
                        //MessageBox.Show(selected.Cells[0].Value.ToString());
                        string uid = selected.Cells[0].Value.ToString();
                        addUidList.Add(uid);
                        enumer.MoveNext();
                    }
                }));
                Mapping map = new Mapping(prod, Convert.ToInt32(price));
                prodThrd = new Thread(map.ThreadProd);
                prodThrd.Start();
                prodThrd.Join();
                this.Invoke((EventHandler)(delegate
                {
                    try
                    {
                        datagrid_close();
                        datagrid_start();
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("��ǰ ����� ���� ����� ��ǰ�� �����ϴ�.");
                    }
                }));

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message.ToString());
            }
            finally
            {
                //thread�� ������ ��ٷ����ϱ⶧���� finally�� ����� ó��
                addUidList.Clear();
                buttonStart.Enabled = true;
                //���� ��ư
                buttonStop.Enabled = false;
                bInventoryFlg = false;
            }
        }

        /// <summary>
        /// ���ο� ������ uid ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void apply_new_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (nametxt.Text == "" || pricetxt.Text == "")
                {
                    throw new Exception("��ǰ ���� �Ǵ� ��ǰ �̸��� �Է����ֽʽÿ�.");
                }
                this.Invoke((EventHandler)(delegate
                {
                    save_data();
                }));
                prod_button_function(nametxt.Text, pricetxt.Text);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message.ToString());
            }
        }

        /// <summary>
        /// ��ǰ ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void delete_button_Click(object sender, EventArgs e)
        {
            //MessageBox.Show();
            this.Invoke((EventHandler)(delegate
            {
                int delete_count = dataGridViewTag.SelectedRows.Count;
                var enumer = dataGridViewTag.SelectedRows.GetEnumerator();
                enumer.MoveNext();
                for (int j = 0; j < delete_count; j++)
                {
                    DataGridViewRow selected = (DataGridViewRow)enumer.Current;
                    //MessageBox.Show(selected.Cells[0].Value.ToString());
                    string uid = selected.Cells[0].Value.ToString();
                    prodUIDDict.Remove(uid);
                    MessageBox.Show(uid + "�� �����մϴ�.");

                    enumer.MoveNext();
                }
                MessageBox.Show($"�� {delete_count}�� �����Ϸ� ");
                datagrid_close();
                datagrid_start();
            }));
        }

        /// <summary>
        /// ������ ȣ���� ���� ���� Ŭ���� ����, �����Ͱ���
        /// </summary>
        public class Mapping
        {
            // State information used in the task.
            private string prod;
            private int price;
            // The constructor obtains the state information.
            public Mapping(string prod, int price)
            {
                this.prod = prod;
                this.price = price;

            }

            public void ThreadProd()
            {
                foreach (string uid in addUidList)
                {
                    try
                    {

                        prodUIDDict.Add(uid, prod);
                        if (!prodPriceDict.ContainsKey(prod))
                        {
                            //�ѹ��� �߰��ص���
                            prodPriceDict.Add(prod, price);
                        }
                    }
                    catch
                    {
                        MessageBox.Show("������ uid�� ��ǰ���� �߰��� �� �����ϴ�.. UID : " + uid);

                    }
                }
                prodThrd = null;
            }
        }

        /// <summary>
        /// �ø�������� Ŭ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [Serializable]
        public class Product
        {
            public string name { get; set; }
            public string price { get; set; }

            public override string ToString()
            {
                return
                    $"{name} : {price}";
            }
        }

        private void apply_button_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(prodlist.SelectedItem.ToString());
            string prod_string = prodlist.SelectedItem.ToString();
            if(prod_string == "")
            {
                MessageBox.Show("���� ��ϵ� ��ǰ�� �������ֽʽÿ�");
                return;
            }
            string[] prod_arr = prodlist.SelectedItem.ToString().Split(':');
            string prod_name = prod_arr[0].Trim();
            string prod_price = prod_arr[1].Trim();
            prod_button_function(prod_name, prod_price);

        }

        private void save_data()
        {
            try
            {
                //��Ʈ�� ����
                //.dat Ȱ��ȭ���� ���� ����
                using (Stream product_data = new FileStream(nametxt.Text + ".dat", FileMode.Create))
                {
                    Product prod = new Product()
                    {
                        name = nametxt.Text,
                        price = pricetxt.Text
                    };

                    BinaryFormatter binaryFormatter = new BinaryFormatter();
                    binaryFormatter.Serialize(product_data, prod);
                    product_data.Close();
                }
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message.ToString());
            }
            finally
            {
                prodlist.Items.Add($"{nametxt.Text} : {pricetxt.Text}��");
                SubFrm.��ǰ.Items.Add($"{nametxt.Text} : {pricetxt.Text}��");
            }

        }

        private void load_data(string file_name)
        {
            using (Stream prod_data = new FileStream(file_name, FileMode.Open))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                //file load
                Product prod = binaryFormatter.Deserialize(prod_data) as Product;
                string str_prod = prod.ToString();
                prodlist.Items.Add(str_prod);
            }
        }

        public string ToPrettyString(Dictionary<string,string> dict)
        {
            var str = new StringBuilder();
            foreach (var pair in dict)
            {
                str.Append(String.Format("{0} {1} ��, ", pair.Key, pair.Value));
            }
            return str.ToString();
        }

    }


}