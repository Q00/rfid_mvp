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
        private bool bInventoryFlg = false;
        private Thread inventoryThrd = null;
        public MainFrm()
        {
            InitializeComponent();
            comboBoxCommunication.Items.Add("COM");
            comboBoxCommunication.Items.Add("NET");
            comboBoxCommunication.SelectedIndex = 0;

            RFIDLIB.rfidlib_reader.RDR_LoadReaderDrivers(@"\Drivers");
            UInt32 nComCount = RFIDLIB.rfidlib_reader.COMPort_Enum();
            for (UInt32 j = 0; j < nComCount; j++)
            {
                StringBuilder nameBuf = new StringBuilder();
                nameBuf.Append('\0',128);
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

            comboBoxCommunication.Enabled = true;
            buttonOpen.Enabled = true;
            buttonClose.Enabled = false;
            comboBoxCOM.Enabled = true;
            comboBoxBaud.Enabled = true;
            comboBoxFrame.Enabled = true;
            textBoxIP.Enabled = true;
            textBoxPort.Enabled = true;
            buttonStart.Enabled = false;
            buttonStop.Enabled = false;
        }

        private void buttonOpen_Click(object sender, EventArgs e)
        {
            String connstr = "";
            switch (comboBoxCommunication.SelectedIndex)
            {
                case 0:
                    connstr = RFIDLIB.rfidlib_def.CONNSTR_NAME_RDTYPE + "=" + "RD5100" + ";" +
                        RFIDLIB.rfidlib_def.CONNSTR_NAME_COMMTYPE + "=" + RFIDLIB.rfidlib_def.CONNSTR_NAME_COMMTYPE_COM + ";" +
                        RFIDLIB.rfidlib_def.CONNSTR_NAME_COMNAME + "=" + comboBoxCOM.Text + ";" +
                        RFIDLIB.rfidlib_def.CONNSTR_NAME_COMBARUD + "=" + comboBoxBaud.Text + ";" +
                        RFIDLIB.rfidlib_def.CONNSTR_NAME_COMFRAME + "=" + comboBoxFrame.Text + ";" +
                        RFIDLIB.rfidlib_def.CONNSTR_NAME_BUSADDR + "=" + "255";
                    break;
                case 1:
                    connstr = RFIDLIB.rfidlib_def.CONNSTR_NAME_RDTYPE + "=" + "RD5100" + ";" +
                         RFIDLIB.rfidlib_def.CONNSTR_NAME_COMMTYPE + "=" + RFIDLIB.rfidlib_def.CONNSTR_NAME_COMMTYPE_NET + ";" +
                         RFIDLIB.rfidlib_def.CONNSTR_NAME_REMOTEIP + "=" + textBoxIP.Text + ";" +
                         RFIDLIB.rfidlib_def.CONNSTR_NAME_REMOTEPORT + "=" + textBoxPort.Text + ";" +
                         RFIDLIB.rfidlib_def.CONNSTR_NAME_LOCALIP + "=" + "";
                    break;
                default:
                    break;
            }

            int iret = RFIDLIB.rfidlib_reader.RDR_Open(connstr, ref hReader);
            if(iret!=0)
            {
                return;
            }
            InvenParamSpecList = RFIDLIB.rfidlib_reader.RDR_CreateInvenParamSpecList();
            RFIDLIB.rfidlib_aip_iso15693.ISO15693_CreateInvenParam(InvenParamSpecList, 0, 0, 0, 0);
            comboBoxCommunication.Enabled = false;
            buttonOpen.Enabled = false;
            buttonClose.Enabled = true;
            comboBoxCOM.Enabled = false;
            comboBoxBaud.Enabled = false;
            comboBoxFrame.Enabled = false;
            textBoxIP.Enabled = false;
            textBoxPort.Enabled = false;
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
            comboBoxCommunication.Enabled = true;
            buttonOpen.Enabled = true;
            buttonClose.Enabled = false;
            comboBoxCOM.Enabled = true;
            comboBoxBaud.Enabled = true;
            comboBoxFrame.Enabled = true;
            textBoxIP.Enabled = true;
            textBoxPort.Enabled = true;
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
                if(iret==0)
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
            while (bInventoryFlg)
            {
                List<string>uids = new List<string>();
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
                   }
                   else if (dataGridViewTag.RowCount < uids.Count)
                   {
                       dataGridViewTag.Rows.Add(uids.Count - dataGridViewTag.RowCount);
                   }
                   for (int j = 0; j < uids.Count; j++)
                   {
                       dataGridViewTag[0, j].Value = uids[j];
                   }

                   labelTagNumber.Text = uids.Count + "";
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
    }
}