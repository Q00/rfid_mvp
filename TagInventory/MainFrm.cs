using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

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
        private Dictionary<string, int> prodPriceDict = new Dictionary<string, int>();
        static private Dictionary<string, string> prodUIDDict = new Dictionary<string, string>();
        static private List<string> addUidList = new List<string>();
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



            dataGridViewTag.Rows.Clear();
            dataGridViewTag.Rows.Add(3);
            dataGridViewTag[0, 0].Value = "123";
            dataGridViewTag[1, 0].Value = "등록필요";
            dataGridViewTag[0, 1].Value = "12345";
            dataGridViewTag[1, 1].Value = "등록필요";
            dataGridViewTag[0, 2].Value = "1236";
            dataGridViewTag[1, 2].Value = "등록필요";
            //dataGridViewTag[0, 3].Value = "12345";
            //dataGridViewTag[1, 3].Value = "등록필요";
            //dataGridViewTag[0, 4].Value = "1236";
            //dataGridViewTag[1, 4].Value = "등록필요";

            addUidList.Add("123");
            addUidList.Add("12345");
            addUidList.Add("1236");

            int iret = RFIDLIB.rfidlib_reader.RDR_Open(connstr, ref hReader);
            if (iret != 0)
            {
                return;
            }

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
                           count_up(prodUIDDict[uids[j]]);
                       }
                       else
                       {
                           //not exist , have to add.
                           dataGridViewTag[0, j].Value = uids[j];
                           dataGridViewTag[1, j].Value = "등록필요";
                           if (!addUidList.Contains(uids[j]))
                           {
                               addUidList.Add(uids[j]);
                           }
                       }

                   }

                   labelTagNumber.Text = uids.Count + "";
                   total_prod_tb.Text = "콜라 : " + coke.ToString() + ", 사이다 : " + soda.ToString() + ", 우유 : " + milk + ", 커피 : " + coffee.ToString() + ", 샐러드 : " + salad.ToString();

               }));
            }

            this.Invoke((EventHandler)(delegate
            {
                buttonStop.Enabled = false;
                buttonStart.Enabled = true;
                bInventoryFlg = false;
            }));
            inventoryThrd = null;
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            buttonStop.Enabled = false;
            bInventoryFlg = false;
        }

        private void count_up(string prod_name)
        {
            switch (prod_name)
            {
                case "콜라":
                    coke += 1;
                    break;
                case "사이다":
                    soda += 1;
                    break;
                case "커피":
                    coffee += 1;
                    break;
                case "우유":
                    milk += 1;
                    break;
                case "샐러드":
                    salad += 1;
                    break;
            }
        }
        /// <summary>
        /// 쓰레드 불러주는 함수
        /// </summary>
        /// <param name="prod"></param>
        private void prod_button_function(string prod)
        {
            try
            {
                buttonStop.Enabled = false;
                bInventoryFlg = false;
                Mapping map = new Mapping(prod);
                prodThrd = new Thread(map.ThreadProd);
                prodThrd.Start();
                prodThrd.Join();
                this.Invoke((EventHandler)(delegate
                {
                    try
                    {
                        dataGridViewTag.Rows.Clear();
                        dataGridViewTag.Rows.Add(addUidList.Count);

                        for (int j = 0; j < addUidList.Count; j++)
                        {
                            if (prodUIDDict.ContainsKey(addUidList[j]))
                            {
                                //already exist
                                dataGridViewTag[0, j].Value = addUidList[j];
                                dataGridViewTag[1, j].Value = prodUIDDict[addUidList[j]];
                                count_up(prodUIDDict[addUidList[j]]);
                                dataGridViewTag[2, j].Value = prodPriceDict[prodUIDDict[addUidList[j]]];
                                dataGridViewTag[3, j].Value = 1;
                            }
                        }
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
                this.Invoke((EventHandler)(delegate
                {
                    addUidList.Clear();
                    buttonStart.Enabled = true;
                }));

            }
        }

        /// <summary>
        /// 콜라로 uid 매핑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void apply_button_Click(object sender, EventArgs e)
        {
            prod_button_function("콜라");
        }
        
        /// <summary>
        /// 쓰레드 호출을 위한 내부 클래스 구현, 데이터공유
        /// </summary>
        public class Mapping
        {
            // State information used in the task.
            private string prod;
            // The constructor obtains the state information.
            public Mapping(string prod)
            {
                this.prod = prod;

            }

            public void ThreadProd()
            {
                foreach (string uid in addUidList)
                {
                    try
                    {
                        prodUIDDict.Add(uid, prod);
                    }
                    catch
                    {
                        MessageBox.Show("동일한 uid를 상품으로 추가할 수 없습니다.. UID : " + uid);

                    }
                }
                prodThrd = null;
            }
        }

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

            }));
        }
    }


}