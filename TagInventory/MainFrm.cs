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
using System.Xml.Serialization;

namespace TagInventory
{
    
    public partial class MainFrm : Form
    {
        private UIntPtr hReader = UIntPtr.Zero;
        private UIntPtr InvenParamSpecList = UIntPtr.Zero;


        private bool bInventoryFlg = false;
        private Thread inventoryThrd = null;
        static private Thread buyThrd = null;
        static private Thread prodThrd = null;
        static private Dictionary<string, int> prodPriceDict = new Dictionary<string, int>();
        static public Dictionary<string, string> prodUIDDict = new Dictionary<string, string>();
        static private List<string> addUidList = new List<string>();
        //소비자단 flag
        static public bool drawSubFlag = false;
        //이후에 확장가능성 플래그 주석
        ///결제가 붙으면 해당 플래그사용
        static public bool finishSubFlag = false;
        private bool dataRememberFlag = true;

        //구매시작시 저장하는 데이터
        string[] fisrt_check_data = new string[] { };

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

            //prodlist에 데이터 추가
            DirectoryInfo di = new DirectoryInfo("./");
            FileInfo[] prod_files = di.GetFiles("*.dat");
            if(prod_files.Length != 0)
            {
                for(int index=0; index < prod_files.Length; index++)
                {
                    string fname = prod_files[index].ToString();

                    //prodlist.Items.Add(prod_files[index].ToString());
                    load_data(fname);
       
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
            //dataGridViewTag.Rows.Add(3);
            //dataGridViewTag[0, 0].Value = "123";
            //dataGridViewTag[1, 0].Value = "등록필요";
            //dataGridViewTag[0, 1].Value = "12345";
            //dataGridViewTag[1, 1].Value = "등록필요";
            //dataGridViewTag[0, 2].Value = "1236";
            //dataGridViewTag[1, 2].Value = "등록필요";

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

                if (drawSubFlag == true)
                {
                    if (dataRememberFlag == true)
                    {
                        fisrt_check_data = dataGridViewTag.Rows.OfType<DataGridViewRow>().Select(r => r.Cells[0].Value.ToString()).ToArray();
                        dataRememberFlag = false;
                    }
                }
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
                           try
                           {
                               present_price += Int32.Parse(dataGridViewTag[2, c].Value.ToString());
                           }catch(Exception eaa)
                           {
                               //등록이 안되어있는 상품은 해당 밸류를 parse할 수 없음
                               continue;
                           }
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
                           dataGridViewTag[1, j].Value = "등록필요";
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
                       total_dict.Add(uuid, prodPriceDict[prodUIDDict[uuid]].ToString());
                   }
                   try
                   {
                       var second_check_data = dataGridViewTag.Rows.OfType<DataGridViewRow>().Select(r => r.Cells[0].Value.ToString()).ToArray();

                       if (drawSubFlag)
                       {
                           Mapping map = new Mapping(fisrt_check_data, second_check_data);
                           buyThrd = new Thread(map.ThreadBuy);
                           buyThrd.Start();
                           buyThrd.Join();
                       }
                   }catch(Exception e)
                   {
                       MessageBox.Show(e.Message.ToString());
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
        /// 쓰레드 불러주는 함수
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
                        //inventoryThrd = null;
                        datagrid_close();
                        datagrid_start();
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("상품 등록중 오류 등록할 상품이 없습니다.");
                    }
                }));

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message.ToString());
            }
            finally
            {
                //thread를 무조건 기다려야하기때문에 finally에 동기로 처리
                addUidList.Clear();
                buttonStart.Enabled = true;
                //정지 버튼
                buttonStop.Enabled = false;
                bInventoryFlg = false;
            }
        }

        /// <summary>
        /// 새로운 정보로 uid 매핑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void apply_new_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (nametxt.Text == "" || pricetxt.Text == "")
                {
                    throw new Exception("상품 가격 또는 상품 이름을 입력해주십시오.");
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
        /// 상품 삭제
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
                    MessageBox.Show(uid + "를 삭제합니다.");

                    enumer.MoveNext();
                }
                MessageBox.Show($"총 {delete_count}건 삭제완료 ");
                datagrid_close();
                datagrid_start();
            }));
        }

        /// <summary>
        /// 쓰레드 호출을 위한 내부 클래스 구현, 데이터공유
        /// </summary>
        public class Mapping
        {
            // State information used in the task.
            private string prod;
            private int price;
            private string[] first_check_data;
            private string[] second_check_data;

            // The constructor obtains the state information.
            public Mapping(string prod, int price)
            {
                this.prod = prod;
                this.price = price;

            }

            public Mapping(string[] first_check_data, string[] second_check_data)
            {
                this.first_check_data = first_check_data;
                this.second_check_data = second_check_data;
            }

            public void ThreadBuy()
            {
                try
                {
                    if (Enumerable.Except(first_check_data, second_check_data).Count() != 0)
                    {
                        SubFrm.dataGridView1.Rows.Clear();
                        SubFrm.dataGridView1.Rows.Add(Enumerable.Except(first_check_data, second_check_data).Count());
                        int count = 0;
                        int total_price_sub = 0;
                        foreach (string subuid in Enumerable.Except(first_check_data, second_check_data))
                        {
                            SubFrm.dataGridView1[0, count].Value = prodUIDDict[subuid].ToString();
                            SubFrm.dataGridView1[1, count].Value = prodPriceDict[prodUIDDict[subuid].ToString()].ToString();
                            total_price_sub += prodPriceDict[prodUIDDict[subuid].ToString()];
                            SubFrm.total_price.Text = total_price_sub.ToString();
                            count++;
                        }
                    }
                }catch(Exception e)
                {
                    MessageBox.Show("오류");
                }
                buyThrd = null;
                
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
                            //한번만 추가해도됨
                            prodPriceDict.Add(prod, price);
                        }
                    }
                    catch
                    {
                        MessageBox.Show("동일한 uid를 상품으로 추가할 수 없습니다.. UID : " + uid);

                    }
                }
                prodThrd = null;
            }
        }

        public class UidDictClass
        {
            [XmlAttribute]
            public string uid;
            [XmlAttribute]
            public string product_name;
        }

        public class PriceDictClass
        {
            [XmlAttribute]
            public string product_name;
            [XmlAttribute]
            public int price;
        }

        /// <summary>
        /// 시리얼라이즈 클래스
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
                MessageBox.Show("기존 등록된 물품을 선택해주십시오");
                return;
            }
            string[] prod_arr =prod_string.Split(':');
            string prod_name = prod_arr[0].Trim();
            string prod_price = prod_arr[1].Trim();
            this.Invoke((EventHandler)(delegate
            {
                prod_button_function(prod_name, prod_price);
            }));

        }

        private void save_data()
        {
            try
            {
                //스트림 생성
                //.dat 활성화되지 않은 파일
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
                prodlist.Items.Add($"{nametxt.Text} : {pricetxt.Text}");
                SubFrm.상품.Items.Add($"{nametxt.Text} : {pricetxt.Text}원");
            }

        }

        private void load_data(string file_name)
        {
            using (Stream prod_data = new FileStream(file_name, FileMode.Open))
            {
                if (file_name == "uids.dat")
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(UidDictClass[]), new XmlRootAttribute() { ElementName = "Uids" });
                    prodUIDDict = ((UidDictClass[])serializer.Deserialize(prod_data)).ToDictionary(i => i.uid, i => i.product_name);
                }else if(file_name == "prices.dat")
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(PriceDictClass[]), new XmlRootAttribute() { ElementName = "price" });
                    prodPriceDict = ((PriceDictClass[])serializer.Deserialize(prod_data)).ToDictionary(o => o.product_name, o => o.price);
                }
                else
                {
                    BinaryFormatter binaryFormatter = new BinaryFormatter();
                    Product prod = binaryFormatter.Deserialize(prod_data) as Product;
                    string str_prod = prod.ToString();
                    prodlist.Items.Add(str_prod);
                }
                
                //file load
                
            }
        }

        public string ToPrettyString(Dictionary<string,string> dict)
        {
            var str = new StringBuilder();
            foreach (var pair in dict)
            {
                str.Append(String.Format("{0} {1} 원, ", prodUIDDict[pair.Key], pair.Value));
            }
            return str.ToString();
        }

    }


}