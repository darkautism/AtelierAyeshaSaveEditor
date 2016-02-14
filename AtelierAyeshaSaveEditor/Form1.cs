using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AtelierAyeshaSaveEditor {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
            Init();
        }

        string saveDir;
        ParamSFOParser SFO;
        private void Init() {
            Task t = new Task(() => {
                AtelierAyeshaDataType.InitLists();
                basketItemName_cb.DataSource = new BindingSource(AtelierAyeshaDataType.ItemList, null);
                basketItemName_cb.DisplayMember = "Value";
                basketItemName_cb.ValueMember = "Key";
                boxItemName_cb.DataSource = new BindingSource(AtelierAyeshaDataType.ItemList, null);
                boxItemName_cb.DisplayMember = "Value";
                boxItemName_cb.ValueMember = "Key";
                basketItemPotential1_cb.DataSource = new BindingSource(AtelierAyeshaDataType.PotentialList, null);
                basketItemPotential1_cb.DisplayMember = "Value";
                basketItemPotential1_cb.ValueMember = "Key";
                basketItemPotential2_cb.DataSource = new BindingSource(AtelierAyeshaDataType.PotentialList, null);
                basketItemPotential2_cb.DisplayMember = "Value";
                basketItemPotential2_cb.ValueMember = "Key";
                basketItemPotential3_cb.DataSource = new BindingSource(AtelierAyeshaDataType.PotentialList, null);
                basketItemPotential3_cb.DisplayMember = "Value";
                basketItemPotential3_cb.ValueMember = "Key";
                basketItemPotential4_cb.DataSource = new BindingSource(AtelierAyeshaDataType.PotentialList, null);
                basketItemPotential4_cb.DisplayMember = "Value";
                basketItemPotential4_cb.ValueMember = "Key";
                basketItemPotential5_cb.DataSource = new BindingSource(AtelierAyeshaDataType.PotentialList, null);
                basketItemPotential5_cb.DisplayMember = "Value";
                basketItemPotential5_cb.ValueMember = "Key";
                basketItemEffect1_cb.DataSource = new BindingSource(AtelierAyeshaDataType.EffectList, null);
                basketItemEffect1_cb.DisplayMember = "Value";
                basketItemEffect1_cb.ValueMember = "Key";
                basketItemEffect2_cb.DataSource = new BindingSource(AtelierAyeshaDataType.EffectList, null);
                basketItemEffect2_cb.DisplayMember = "Value";
                basketItemEffect2_cb.ValueMember = "Key";
                basketItemEffect3_cb.DataSource = new BindingSource(AtelierAyeshaDataType.EffectList, null);
                basketItemEffect3_cb.DisplayMember = "Value";
                basketItemEffect3_cb.ValueMember = "Key";
                basketItemEffect4_cb.DataSource = new BindingSource(AtelierAyeshaDataType.EffectList, null);
                basketItemEffect4_cb.DisplayMember = "Value";
                basketItemEffect4_cb.ValueMember = "Key";
                boxItemPotential1_cb.DataSource = new BindingSource(AtelierAyeshaDataType.PotentialList, null);
                boxItemPotential1_cb.DisplayMember = "Value";
                boxItemPotential1_cb.ValueMember = "Key";
                boxItemPotential2_cb.DataSource = new BindingSource(AtelierAyeshaDataType.PotentialList, null);
                boxItemPotential2_cb.DisplayMember = "Value";
                boxItemPotential2_cb.ValueMember = "Key";
                boxItemPotential3_cb.DataSource = new BindingSource(AtelierAyeshaDataType.PotentialList, null);
                boxItemPotential3_cb.DisplayMember = "Value";
                boxItemPotential3_cb.ValueMember = "Key";
                boxItemPotential4_cb.DataSource = new BindingSource(AtelierAyeshaDataType.PotentialList, null);
                boxItemPotential4_cb.DisplayMember = "Value";
                boxItemPotential4_cb.ValueMember = "Key";
                boxItemPotential5_cb.DataSource = new BindingSource(AtelierAyeshaDataType.PotentialList, null);
                boxItemPotential5_cb.DisplayMember = "Value";
                boxItemPotential5_cb.ValueMember = "Key";
                boxItemEffect1_cb.DataSource = new BindingSource(AtelierAyeshaDataType.EffectList, null);
                boxItemEffect1_cb.DisplayMember = "Value";
                boxItemEffect1_cb.ValueMember = "Key";
                boxItemEffect2_cb.DataSource = new BindingSource(AtelierAyeshaDataType.EffectList, null);
                boxItemEffect2_cb.DisplayMember = "Value";
                boxItemEffect2_cb.ValueMember = "Key";
                boxItemEffect3_cb.DataSource = new BindingSource(AtelierAyeshaDataType.EffectList, null);
                boxItemEffect3_cb.DisplayMember = "Value";
                boxItemEffect3_cb.ValueMember = "Key";
                boxItemEffect4_cb.DataSource = new BindingSource(AtelierAyeshaDataType.EffectList, null);
                boxItemEffect4_cb.DisplayMember = "Value";
                boxItemEffect4_cb.ValueMember = "Key";
            });
            t.Start();
        }

        string gameID = null;
        private void button1_Click(object sender, EventArgs e) {
            // ofd.Filter = "Ps3 Save (USR-DATA)|USR-DATA|All files (*.*)|*.*";
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK) {
                if (saveDir != null) {
                    /////////////////////////
                    // Encrypt
                    /////////////////////////
                    gameID = ((string)SFO.getValue("SAVEDATA_DIRECTORY")).Split('-')[0];
                    Utility.encryptSave(gameID, saveDir);
                    saveDir = null;
                }

                saveDir = folderBrowserDialog1.SelectedPath;
                if (!File.Exists(saveDir + @"\USR-DATA") || !File.Exists(saveDir + @"\ICON0.PNG") || !File.Exists(saveDir + @"\PARAM.SFO")
                    || !File.Exists(saveDir + @"\PIC1.PNG") || !File.Exists(saveDir + @"\PARAM.PFD")) {
                    saveDir = null;
                    MessageBox.Show("錯誤的存檔");
                    return;
                }




                /////////////////////////
                // SFO Block
                /////////////////////////
                SFO = new ParamSFOParser(saveDir + @"\PARAM.SFO");
                // tabPage4.BackgroundImage = Image.FromFile( saveDir + @"\PIC1.PNG" );

                imageLabel.Image = Image.FromFile(saveDir + @"\ICON0.PNG");
                TITLE.Text = (string)SFO.getValue("TITLE");
                SUB_TITLE.Text = (string)SFO.getValue("SUB_TITLE");
                DETAIL.Text = (string)SFO.getValue("DETAIL");

                /////////////////////////
                // Decrypt
                /////////////////////////
                gameID = ((string)SFO.getValue("SAVEDATA_DIRECTORY")).Split('-')[0];

                Utility.decryptSave(gameID, saveDir);
                // Get the output into a string
                // string result = proc.StandardOutput.ReadToEnd();


                // //////////////////////
                // SaveBlock
                /////////////////////////
                boxList_lv.Items.Clear();
                basketList_lv.Items.Clear();
                BigEndianBinaryReader SaveDataFile = new BigEndianBinaryReader(new FileStream(saveDir + @"\USR-DATA", FileMode.Open));
                SaveDataFile.BaseStream.Position = AtelierAyeshaDataType.CollectionBasketOffset;
                BasketItem basketTmp;
                for (int i = 0; i < 120; i++) {
                    basketTmp = new BasketItem(SaveDataFile);
                    if (basketTmp.ID == 0xFFFF) {
                        break;
                    }
                    ListViewItem lvi = new ListViewItem();
                    lvi.Text = i + "";
                    lvi.SubItems.Add(basketTmp.Name);
                    lvi.Tag = basketTmp;
                    basketList_lv.Items.Add(lvi);
                }

                BoxItem boxTemp;
                SaveDataFile.BaseStream.Position = AtelierAyeshaDataType.BoxOffset;
                boxList_lv.BeginUpdate();
                for (int i = 0; i < 10000; i++) {
                    boxTemp = new BoxItem(SaveDataFile);
                    if (boxTemp.ID == 0xFFFF || boxTemp.Count == 0) {
                        break;
                    }
                    ListViewItem lvi = new ListViewItem();
                    lvi.Text = i + "";
                    lvi.SubItems.Add(boxTemp.Name);
                    lvi.Tag = boxTemp;
                    boxList_lv.Items.Add(lvi);
                }
                boxList_lv.EndUpdate();

                // 錢
                SaveDataFile.BaseStream.Position = AtelierAyeshaDataType.money;
                money_nud.Value = Convert.ToDecimal(SaveDataFile.ReadUInt32());
                // pt1
                SaveDataFile.BaseStream.Position = AtelierAyeshaDataType.pt;
                pt1_nud.Value = Convert.ToDecimal(SaveDataFile.ReadUInt32());
                readExtensionFile(SaveDataFile.BaseStream);

                SaveDataFile.Close();
            }


        }

        private void button2_Click(object sender, EventArgs e) {
            if (saveDir != null) {
                BigEndianBinaryWriter SaveDataFile = new BigEndianBinaryWriter(new FileStream(saveDir + @"\USR-DATA", FileMode.Open));
                SaveDataFile.BaseStream.Position = AtelierAyeshaDataType.CollectionBasketOffset;
                foreach (ListViewItem lvi in basketList_lv.Items) {
                    SaveDataFile.Write(((BasketItem)lvi.Tag).ToByteArray());
                }

                SaveDataFile.BaseStream.Position = AtelierAyeshaDataType.BoxOffset;
                foreach (ListViewItem lvi in boxList_lv.Items) {
                    SaveDataFile.Write(((BoxItem)lvi.Tag).ToByteArray());
                }
                // 錢
                SaveDataFile.BaseStream.Position = AtelierAyeshaDataType.money;
                SaveDataFile.Write(Convert.ToUInt32(money_nud.Value));
                // pt1
                SaveDataFile.BaseStream.Position = AtelierAyeshaDataType.pt;
                SaveDataFile.Write(Convert.ToUInt32(pt1_nud.Value));
                // pt2 兩個地方都要寫
                SaveDataFile.BaseStream.Position = AtelierAyeshaDataType.pt2;
                SaveDataFile.Write(Convert.ToUInt32(pt1_nud.Value));

                // 寫入資料表
                if (checkBox1.Checked) {
                    DataTable dt = dataSet1.Tables["extensionTable"];
                    foreach (DataRow dr in dt.Rows) {
                        string type = dr["hackType"].ToString();
                        SaveDataFile.BaseStream.Position = long.Parse(dr["hackOffset"].ToString(), System.Globalization.NumberStyles.HexNumber, CultureInfo.InvariantCulture.NumberFormat);
                        if (type.Trim().Equals("int")) {
                            SaveDataFile.Write(Convert.ToInt32(dr["SetValue"]));
                        } else if (type.Trim().Equals("uint")) {
                            SaveDataFile.Write(Convert.ToUInt32(dr["SetValue"]));
                        } else if (type.Trim().Equals("short")) {
                            SaveDataFile.Write(Convert.ToInt16(dr["SetValue"]));
                        } else if (type.Trim().Equals("ushort")) {
                            SaveDataFile.Write(Convert.ToUInt16(dr["SetValue"]));
                        } else if (type.Trim().Equals("byte")) {
                            SaveDataFile.Write(Convert.ToByte(dr["SetValue"]));
                        }
                    }
                }

                dataSet1.WriteXml("extension.xml");
                readExtensionFile(SaveDataFile.BaseStream);
                SaveDataFile.Close();


                MessageBox.Show("存檔成功!");
            } else {
                MessageBox.Show("尚未開啟存檔");
            }
        }



        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) {
            if (basketList_lv.SelectedItems.Count == 1) {
                BasketItem bi = (BasketItem)basketList_lv.SelectedItems[0].Tag;
                bi.Potential[0] = (ushort)((ComboBox)sender).SelectedValue;
            }
        }

        private void basketList_lv_SelectedIndexChanged(object sender, EventArgs e) {
            ListView lv = (ListView)sender;
            if (lv.SelectedItems.Count == 1) {
                BasketItem bi = (BasketItem)lv.SelectedItems[0].Tag;
                basketItemPotential1_cb.SelectedValue = bi.Potential[0];
                basketItemPotential2_cb.SelectedValue = bi.Potential[1];
                basketItemPotential3_cb.SelectedValue = bi.Potential[2];
                basketItemPotential4_cb.SelectedValue = bi.Potential[3];
                basketItemPotential5_cb.SelectedValue = bi.Potential[4];

                basketItemEffect1_cb.SelectedValue = bi.Effect[0];
                basketItemEffect2_cb.SelectedValue = bi.Effect[1];
                basketItemEffect3_cb.SelectedValue = bi.Effect[2];
                basketItemEffect4_cb.SelectedValue = bi.Effect[3];

                basketQuality_nud.Value = bi.Quality;
                basketCount_nud.Value = bi.Count;
                basketItemName_cb.SelectedValue = bi.ID;
                basketItemisAppraisal.Checked = bi.isAppraisal;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e) {
            if (basketList_lv.SelectedItems.Count == 1) {
                BasketItem bi = (BasketItem)basketList_lv.SelectedItems[0].Tag;
                bi.Potential[1] = (ushort)((ComboBox)sender).SelectedValue;
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e) {

            if (basketList_lv.SelectedItems.Count == 1) {
                BasketItem bi = (BasketItem)basketList_lv.SelectedItems[0].Tag;
                bi.Potential[2] = (ushort)((ComboBox)sender).SelectedValue;
            }
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e) {

            if (basketList_lv.SelectedItems.Count == 1) {
                BasketItem bi = (BasketItem)basketList_lv.SelectedItems[0].Tag;
                bi.Potential[3] = (ushort)((ComboBox)sender).SelectedValue;
            }
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e) {

            if (basketList_lv.SelectedItems.Count == 1) {
                BasketItem bi = (BasketItem)basketList_lv.SelectedItems[0].Tag;
                bi.Potential[4] = (ushort)((ComboBox)sender).SelectedValue;
            }
        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e) {

            if (basketList_lv.SelectedItems.Count == 1) {
                BasketItem bi = (BasketItem)basketList_lv.SelectedItems[0].Tag;
                bi.Effect[0] = (ushort)((ComboBox)sender).SelectedValue;
            }
        }

        private void comboBox7_SelectedIndexChanged(object sender, EventArgs e) {

            if (basketList_lv.SelectedItems.Count == 1) {
                BasketItem bi = (BasketItem)basketList_lv.SelectedItems[0].Tag;
                bi.Effect[1] = (ushort)((ComboBox)sender).SelectedValue;
            }
        }

        private void comboBox8_SelectedIndexChanged(object sender, EventArgs e) {

            if (basketList_lv.SelectedItems.Count == 1) {
                BasketItem bi = (BasketItem)basketList_lv.SelectedItems[0].Tag;
                bi.Effect[2] = (ushort)((ComboBox)sender).SelectedValue;
            }
        }

        private void comboBox9_SelectedIndexChanged(object sender, EventArgs e) {
            if (basketList_lv.SelectedItems.Count == 1) {
                BasketItem bi = (BasketItem)basketList_lv.SelectedItems[0].Tag;
                bi.Effect[3] = (ushort)((ComboBox)sender).SelectedValue;
            }
        }

        private void boxList_lv_SelectedIndexChanged(object sender, EventArgs e) {
            ListView lv = (ListView)sender;
            if (lv.SelectedItems.Count == 1) {
                BoxItem bi = (BoxItem)lv.SelectedItems[0].Tag;
                boxItemPotential1_cb.SelectedValue = bi.Potential[0];
                boxItemPotential2_cb.SelectedValue = bi.Potential[1];
                boxItemPotential3_cb.SelectedValue = bi.Potential[2];
                boxItemPotential4_cb.SelectedValue = bi.Potential[3];
                boxItemPotential5_cb.SelectedValue = bi.Potential[4];

                boxItemEffect1_cb.SelectedValue = bi.Effect[0];
                boxItemEffect2_cb.SelectedValue = bi.Effect[1];
                boxItemEffect3_cb.SelectedValue = bi.Effect[2];
                boxItemEffect4_cb.SelectedValue = bi.Effect[3];

                boxQuality_nud.Value = bi.Quality;
                boxCount_nud.Value = bi.Count;
                boxItemName_cb.SelectedValue = bi.ID;
                boxItemisAppraisal.Checked = bi.isAppraisal;
            }
        }

        private void boxItemPotential1_cb_SelectedIndexChanged(object sender, EventArgs e) {
            if (boxList_lv.SelectedItems.Count == 1) {
                BoxItem bi = (BoxItem)boxList_lv.SelectedItems[0].Tag;
                bi.Potential[0] = (ushort)((ComboBox)sender).SelectedValue;
            }
        }

        private void boxItemPotential2_cb_SelectedIndexChanged(object sender, EventArgs e) {
            if (boxList_lv.SelectedItems.Count == 1) {
                BoxItem bi = (BoxItem)boxList_lv.SelectedItems[0].Tag;
                bi.Potential[1] = (ushort)((ComboBox)sender).SelectedValue;
            }
        }

        private void boxItemPotential3_cb_SelectedIndexChanged(object sender, EventArgs e) {
            if (boxList_lv.SelectedItems.Count == 1) {
                BoxItem bi = (BoxItem)boxList_lv.SelectedItems[0].Tag;
                bi.Potential[2] = (ushort)((ComboBox)sender).SelectedValue;
            }
        }

        private void boxItemPotential4_cb_SelectedIndexChanged(object sender, EventArgs e) {
            if (boxList_lv.SelectedItems.Count == 1) {
                BoxItem bi = (BoxItem)boxList_lv.SelectedItems[0].Tag;
                bi.Potential[3] = (ushort)((ComboBox)sender).SelectedValue;
            }
        }

        private void boxItemPotential5_cb_SelectedIndexChanged(object sender, EventArgs e) {
            if (boxList_lv.SelectedItems.Count == 1) {
                BoxItem bi = (BoxItem)boxList_lv.SelectedItems[0].Tag;
                bi.Potential[4] = (ushort)((ComboBox)sender).SelectedValue;
            }
        }

        private void boxItemEffect1_cb_SelectedIndexChanged(object sender, EventArgs e) {
            if (boxList_lv.SelectedItems.Count == 1) {
                BoxItem bi = (BoxItem)boxList_lv.SelectedItems[0].Tag;
                bi.Effect[0] = (ushort)((ComboBox)sender).SelectedValue;
            }
        }

        private void boxItemEffect2_cb_SelectedIndexChanged(object sender, EventArgs e) {
            if (boxList_lv.SelectedItems.Count == 1) {
                BoxItem bi = (BoxItem)boxList_lv.SelectedItems[0].Tag;
                bi.Effect[1] = (ushort)((ComboBox)sender).SelectedValue;
            }
        }

        private void boxItemEffect3_cb_SelectedIndexChanged(object sender, EventArgs e) {
            if (boxList_lv.SelectedItems.Count == 1) {
                BoxItem bi = (BoxItem)boxList_lv.SelectedItems[0].Tag;
                bi.Effect[2] = (ushort)((ComboBox)sender).SelectedValue;
            }
        }

        private void boxItemEffect4_cb_SelectedIndexChanged(object sender, EventArgs e) {
            if (boxList_lv.SelectedItems.Count == 1) {
                BoxItem bi = (BoxItem)boxList_lv.SelectedItems[0].Tag;
                bi.Effect[3] = (ushort)((ComboBox)sender).SelectedValue;
            }
        }

        private void basketQuality_nud_ValueChanged(object sender, EventArgs e) {
            BasketItem bi = (BasketItem)basketList_lv.SelectedItems[0].Tag;
            bi.Quality = (int)basketQuality_nud.Value;
        }

        private void boxQuality_nud_ValueChanged(object sender, EventArgs e) {
            BoxItem bi = (BoxItem)boxList_lv.SelectedItems[0].Tag;
            bi.Quality = (int)boxQuality_nud.Value;
        }

        private void boxItemCount_nud_ValueChanged(object sender, EventArgs e) {
            BoxItem bi = (BoxItem)boxList_lv.SelectedItems[0].Tag;
            bi.Count = (int)boxCount_nud.Value;
        }

        private void basketItemName_cb_SelectedIndexChanged(object sender, EventArgs e) {
            if (basketList_lv.SelectedItems.Count == 1) {
                BasketItem bi = (BasketItem)basketList_lv.SelectedItems[0].Tag;
                bi.ID = (ushort)basketItemName_cb.SelectedValue;
                basketList_lv.SelectedItems[0].SubItems[1].Text = basketItemName_cb.Text;
            }
        }

        private void boxItemName_cb_SelectedIndexChanged(object sender, EventArgs e) {
            if (boxList_lv.SelectedItems.Count == 1) {
                BoxItem bi = (BoxItem)boxList_lv.SelectedItems[0].Tag;
                bi.ID = (ushort)boxItemName_cb.SelectedValue;
                boxList_lv.SelectedItems[0].SubItems[1].Text = boxItemName_cb.Text;
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e) {
            if (basketList_lv.SelectedItems.Count == 1) {
                BasketItem bi = (BasketItem)basketList_lv.SelectedItems[0].Tag;
                bi.Count = (int)basketCount_nud.Value;
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e) {
            if ((tabControl1.SelectedTab == tabPage2 || tabControl1.SelectedTab == tabPage3) && saveDir != null) {
                addItem_btn.Enabled = true;
            } else {
                addItem_btn.Enabled = false;
            }
        }

        private void addItem_btn_Click(object sender, EventArgs e) {
            if (tabControl1.SelectedTab == tabPage2 && basketList_lv.Items.Count <= 120) {
                ListViewItem lvi = new ListViewItem();
                BasketItem bi = new BasketItem();
                lvi.Tag = bi;
                lvi.SubItems.Add(bi.Name);
                lvi.Text = basketList_lv.Items.Count + "";
                basketList_lv.Items.Add(lvi);
                lvi.Selected = true;
            } else if (tabControl1.SelectedTab == tabPage3 && boxList_lv.Items.Count <= 10000) {
                ListViewItem lvi = new ListViewItem();
                BoxItem bi = new BoxItem();
                lvi.Tag = bi;
                lvi.SubItems.Add(bi.Name);
                lvi.Text = boxList_lv.Items.Count + "";
                boxList_lv.Items.Add(lvi);
                lvi.Selected = true;
            }
        }

        private void readExtensionFile(Stream inStream) {
            dataSet1.Tables["extensionTable"].Rows.Clear();
            dataSet1.ReadXml("extension.xml");
            BigEndianBinaryReader saveFileBinaryReader = new BigEndianBinaryReader(inStream);
            foreach (DataRow dr in dataSet1.Tables["extensionTable"].Rows) {
                long pointer;
                if (!long.TryParse(dr["hackOffset"].ToString(), System.Globalization.NumberStyles.HexNumber, CultureInfo.InvariantCulture.NumberFormat, out pointer)) {
                    MessageBox.Show("位置格式不正確");
                    dataSet1.Tables["extensionTable"].Rows.Clear();
                    return;
                }
                if (pointer > saveFileBinaryReader.BaseStream.Length) {
                    MessageBox.Show("超過存檔可以表示的範圍");
                    dataSet1.Tables["extensionTable"].Rows.Clear();
                    return;
                }
                saveFileBinaryReader.BaseStream.Position = pointer;
                if (dr["hackType"].Equals("int")) {
                    dr["hackValue"] = saveFileBinaryReader.ReadInt32();
                } else if (dr["hackType"].Equals("uint")) {
                    dr["hackValue"] = (saveFileBinaryReader.ReadUInt32());
                } else if (dr["hackType"].Equals("short")) {
                    dr["hackValue"] = saveFileBinaryReader.ReadInt16();
                } else if (dr["hackType"].Equals("ushort")) {
                    dr["hackValue"] = saveFileBinaryReader.ReadUInt16();
                } else if (dr["hackType"].Equals("byte")) {
                    dr["hackValue"] = saveFileBinaryReader.ReadByte();
                } else {
                    MessageBox.Show("讀取失敗，不支援的數據格式");
                    return;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e) {
            if (saveDir != null) {
                dataSet1.Tables["extensionTable"].Rows.Clear();
                dataSet1.ReadXml("extension.xml");
                BigEndianBinaryReader saveFileBinaryReader = new BigEndianBinaryReader(new FileStream(saveDir + @"\USR-DATA", FileMode.Open));
                foreach (DataRow dr in dataSet1.Tables["extensionTable"].Rows) {
                    long pointer;
                    if (!long.TryParse(dr["hackOffset"].ToString(), System.Globalization.NumberStyles.HexNumber, CultureInfo.InvariantCulture.NumberFormat, out pointer)) {
                        MessageBox.Show("位置格式不正確");
                        dataSet1.Tables["extensionTable"].Rows.Clear();
                        return;
                    }
                    if (pointer > saveFileBinaryReader.BaseStream.Length) {
                        MessageBox.Show("超過存檔可以表示的範圍");
                        dataSet1.Tables["extensionTable"].Rows.Clear();
                        return;
                    }
                    saveFileBinaryReader.BaseStream.Position = pointer;
                    if (dr["hackType"].Equals("int")) {
                        dr["hackValue"] = saveFileBinaryReader.ReadInt32();
                    } else if (dr["hackType"].Equals("uint")) {
                        dr["hackValue"] = saveFileBinaryReader.ReadUInt32();
                    } else if (dr["hackType"].Equals("short")) {
                        dr["hackValue"] = saveFileBinaryReader.ReadInt16();
                    } else if (dr["hackType"].Equals("ushort")) {
                        dr["hackValue"] = saveFileBinaryReader.ReadUInt16();
                    } else if (dr["hackType"].Equals("byte")) {
                        dr["hackValue"] = saveFileBinaryReader.ReadByte();
                    } else {
                        MessageBox.Show("讀取失敗，不支援的數據格式");
                        return;
                    }
                }
                saveFileBinaryReader.Close();
            } else {
                MessageBox.Show("無法讀取存檔");
            }
        }

        private void dataGridView1_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e) {
            e.Row.Cells[0].Value = "新地址";
            e.Row.Cells[1].Value = "0";
            e.Row.Cells[2].Value = "int";
            e.Row.Cells[4].Value = 0;
        }


        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e) {
            if (saveDir != null) {
                long pointer;
                if (dataGridView1.Rows[e.RowIndex].Cells[1].Value == null) {
                    dataGridView1.Rows[e.RowIndex].Cells[1].Value = 0;
                }
                if (dataGridView1.Rows[e.RowIndex].Cells[2].Value == null) {
                    dataGridView1.Rows[e.RowIndex].Cells[2].Value = "int";
                }
                long.TryParse(dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString()
                    , System.Globalization.NumberStyles.HexNumber, CultureInfo.InvariantCulture.NumberFormat, out pointer);

                BigEndianBinaryReader saveFileBinaryReader = new BigEndianBinaryReader(new FileStream(saveDir + @"\USR-DATA", FileMode.Open));
                saveFileBinaryReader.BaseStream.Position = pointer;
                string type = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                if (type.Trim().Equals("int")) {
                    dataGridView1.Rows[e.RowIndex].Cells[3].Value =
                        saveFileBinaryReader.ReadInt32();
                } else if (type.Trim().Equals("uint")) {
                    dataGridView1.Rows[e.RowIndex].Cells[3].Value =
                        saveFileBinaryReader.ReadUInt32();
                } else if (type.Trim().Equals("short")) {
                    dataGridView1.Rows[e.RowIndex].Cells[3].Value =
                         saveFileBinaryReader.ReadInt16();
                } else if (type.Trim().Equals("ushort")) {
                    dataGridView1.Rows[e.RowIndex].Cells[3].Value =
                       saveFileBinaryReader.ReadUInt16();
                } else if (type.Trim().Equals("byte")) {
                    dataGridView1.Rows[e.RowIndex].Cells[3].Value =
                        saveFileBinaryReader.ReadByte();
                }
                saveFileBinaryReader.Close();
            } else {
                MessageBox.Show("無法讀取存檔");
            }
        }

        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e) {
            try {
                if (saveDir == null) {
                    MessageBox.Show("無法讀取存檔");
                } else if (e.ColumnIndex == 1) {
                    long pointer = long.Parse(e.FormattedValue.ToString(), System.Globalization.NumberStyles.HexNumber, CultureInfo.InvariantCulture.NumberFormat);
                    FileStream fs = new FileStream(saveDir + @"\USR-DATA", FileMode.Open);
                    if (pointer > fs.Length) {
                        fs.Close();
                        throw new Exception("超過存檔長度");
                    }
                    fs.Close();
                } else if (e.ColumnIndex == 2) {
                    string type = e.FormattedValue.ToString().Trim();
                    string value = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                    if (type.Equals("int")) {
                        int.Parse(value);
                    } else if (type.Equals("uint")) {
                        uint.Parse(value);
                    } else if (type.Equals("short")) {
                        short.Parse(value);
                    } else if (type.Equals("ushort")) {
                        ushort.Parse(value);
                    } else if (type.Equals("byte")) {
                        byte.Parse(value);
                    }
                } else if (e.ColumnIndex == 4) {
                    string type = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                    string value = e.FormattedValue.ToString().Trim();
                    if (type.Equals("int")) {
                        int.Parse(value);
                    } else if (type.Equals("uint")) {
                        uint.Parse(value);
                    } else if (type.Equals("short")) {
                        short.Parse(value);
                    } else if (type.Equals("ushort")) {
                        ushort.Parse(value);
                    } else if (type.Equals("byte")) {
                        byte.Parse(value);
                    }
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
                e.Cancel = true;
                dataGridView1.CancelEdit();
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e) {
            if (saveDir != null) {
                dataSet1.WriteXml("extension.xml");
                /////////////////////////
                // Encrypt
                /////////////////////////
                gameID = ((string)SFO.getValue("SAVEDATA_DIRECTORY")).Split('-')[0];
                Utility.encryptSave(gameID, saveDir);

                saveDir = null;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            System.Diagnostics.Process.Start(linkLabel1.Text.ToString());
        }

        private void button3_Click_1(object sender, EventArgs e) {
            if (saveDir != null) {

                foreach (KeyValuePair<ushort, string> pair in AtelierAyeshaDataType.ItemList) {
                    if (boxList_lv.Items.Count <= 10000 && pair.Key != 0xFFFF) {
                        ListViewItem lvi = new ListViewItem();
                        BoxItem bi = new BoxItem();
                        bi.Quality = 120;
                        bi.Count = 255;
                        bi.ID = pair.Key;
                        lvi.Tag = bi;
                        lvi.SubItems.Add(bi.Name);
                        lvi.Text = boxList_lv.Items.Count + "";
                        boxList_lv.Items.Add(lvi);
                        lvi.Selected = true;
                    }
                }
            }
        }

        private void basketItemisAppraisal_CheckedChanged(object sender, EventArgs e) {
            if (basketList_lv.SelectedItems.Count == 1) {
                BasketItem bi = (BasketItem)basketList_lv.SelectedItems[0].Tag;
                bi.isAppraisal = basketItemisAppraisal.Checked;
            }
        }

        private void boxItemisAppraisal_CheckedChanged(object sender, EventArgs e) {
            if (boxList_lv.SelectedItems.Count == 1) {
                BoxItem bi = (BoxItem)boxList_lv.SelectedItems[0].Tag;
                bi.isAppraisal = boxItemisAppraisal.Checked;
            }
        }

    }
}
