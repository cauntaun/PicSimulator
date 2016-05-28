using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml.Linq;
using PicSimulator;
using System.Text.RegularExpressions;


namespace PicSimulator
{
    public partial class Form1 : Form
    {
        private PicSimulator picSimulator;
        private Log log = null;
        private int test = 1;

        public Form1()
        {
            InitializeComponent();
            //testLabel.DataBindings.Add("Text", picSimulator, "Unit");
        }

        /// <summary>
        /// Erstelle neuen PicSimulator und lade Befehle aus LST-Datei.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dateiLadenToolStrip_Click(object sender, EventArgs e) //Klick auf Menü
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Nur .lst Dateien. |*.lst;";

            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
            }
            picSimulator.Reset();
            picSimulator.LoadLST(ofd.FileName);
        }

        /// <summary>
        /// Schreibe die Konsolenausgabe in das consoleLog-Fenster.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            picSimulator = new PicSimulator();
            wLabel.DataBindings.Add("Text", picSimulator, "wRegister");
            pcLabel.DataBindings.Add("Text", picSimulator, "ProgramCounter");
            cLabel.DataBindings.Add("Text", picSimulator, "CBit");
            dcLabel.DataBindings.Add("Text", picSimulator, "DCBit");
            zLabel.DataBindings.Add("Text", picSimulator, "ZBit");
            pdLabel.DataBindings.Add("Text", picSimulator, "PDBit");
            toLabel.DataBindings.Add("Text", picSimulator, "TOBit");
            rp0Label.DataBindings.Add("Text", picSimulator, "RP0Bit");
            rp1Label.DataBindings.Add("Text", picSimulator, "RP1Bit");
            pclLabel.DataBindings.Add("Text", picSimulator, "PCL");
            timeLabel.DataBindings.Add("Text", picSimulator, "CycleCounter");

            log = new Log(consoleLog);
            Console.SetOut(log);
        }

        private void btn_Clear_Click(object sender, EventArgs e)
        {
            dataGridView_Lst.Rows.Clear();
            consoleLog.Clear();
        }

        public void addLstLine(String line)
        {
            int n = dataGridView_Lst.Rows.Add();
            dataGridView_Lst.Rows[n].Cells[0].Value = line;
        }

        public void AddStorageSet(int[] storage)
        {
            //storageGridView.RowHeadersWidth = 30;
            Console.Write("[+] Initializing Storage\n");
            for (int i = 0; i < (storage.Length) / 8; i++)
            {

                int n = storageGridView.Rows.Add();
                if (i != 16)
                {
                    storageGridView.Rows[n].Cells[0].Value = storage[8 * i + 0].ToString("X2");
                    storageGridView.Rows[n].Cells[1].Value = storage[8 * i + 1].ToString("X2");
                    storageGridView.Rows[n].Cells[2].Value = storage[8 * i + 2].ToString("X2");
                    storageGridView.Rows[n].Cells[3].Value = storage[8 * i + 3].ToString("X2");
                    storageGridView.Rows[n].Cells[4].Value = storage[8 * i + 4].ToString("X2");
                    storageGridView.Rows[n].Cells[5].Value = storage[8 * i + 5].ToString("X2");
                    storageGridView.Rows[n].Cells[6].Value = storage[8 * i + 6].ToString("X2");
                    storageGridView.Rows[n].Cells[7].Value = storage[8 * i + 7].ToString("X2");
                }
                else
                {
                    storageGridView.Rows[n].Cells[0].Value = storage[0].ToString("X2");
                    storageGridView.Rows[n].Cells[1].Value = storage[8 * i + 1].ToString("X2");
                    storageGridView.Rows[n].Cells[2].Value = storage[2].ToString("X2");
                    storageGridView.Rows[n].Cells[3].Value = storage[3].ToString("X2");
                    storageGridView.Rows[n].Cells[4].Value = storage[4].ToString("X2");
                    storageGridView.Rows[n].Cells[5].Value = storage[5].ToString("X2");
                    storageGridView.Rows[n].Cells[6].Value = storage[8 * i + 6].ToString("X2");
                    storageGridView.Rows[n].Cells[7].Value = storage[8 * i + 7].ToString("X2");
                }
            }
            int counter = 0;
            for (int i = 0; i <= 31; i++)
            {
                storageGridView.Rows[i].HeaderCell.Value = counter.ToString("X2");
                counter += 8;
            }
        }

        public void UpdateStorageSet()
        {
            int[] storage = picSimulator.GetRegisterSet().GetRegister();
            Console.Write("[+] Updating Storage\n");
            for (int i = 0; i < storage.Length/8; i++)
            {
                if (i != 16)
                {
                    storageGridView.Rows[i].Cells[0].Value = storage[8 * i + 0].ToString("X2");
                    storageGridView.Rows[i].Cells[1].Value = storage[8 * i + 1].ToString("X2");
                    storageGridView.Rows[i].Cells[2].Value = storage[8 * i + 2].ToString("X2");
                    storageGridView.Rows[i].Cells[3].Value = storage[8 * i + 3].ToString("X2");
                    storageGridView.Rows[i].Cells[4].Value = storage[8 * i + 4].ToString("X2");
                    storageGridView.Rows[i].Cells[5].Value = storage[8 * i + 5].ToString("X2");
                    storageGridView.Rows[i].Cells[6].Value = storage[8 * i + 6].ToString("X2");
                    storageGridView.Rows[i].Cells[7].Value = storage[8 * i + 7].ToString("X2");
                }
                else
                {
                    storageGridView.Rows[i].Cells[0].Value = storage[0].ToString("X2");
                    storageGridView.Rows[i].Cells[1].Value = storage[8 * i + 1].ToString("X2");
                    storageGridView.Rows[i].Cells[2].Value = storage[2].ToString("X2");
                    storageGridView.Rows[i].Cells[3].Value = storage[3].ToString("X2");
                    storageGridView.Rows[i].Cells[4].Value = storage[4].ToString("X2");
                    storageGridView.Rows[i].Cells[5].Value = storage[5].ToString("X2");
                    storageGridView.Rows[i].Cells[6].Value = storage[8 * i + 6].ToString("X2");
                    storageGridView.Rows[i].Cells[7].Value = storage[8 * i + 7].ToString("X2");
                }
            }
        }

        public void ResetStorageSet()
        {
            storageGridView.Rows.Clear();
            storageGridView.Refresh();
        }

        public void HighlightLine(int lineNumber)
        {
            dataGridView_Lst.Rows[lineNumber].Selected = true;
            dataGridView_Lst.FirstDisplayedScrollingRowIndex = lineNumber;
            //dataGridView_Lst.Rows[lineNumber].DefaultCellStyle.BackColor = Color.LightBlue;
        }

        private void testLabel_Click(object sender, EventArgs e)
        {
            picSimulator.WRegister = 3.ToString("X2");
        }

        private void btn_nextStep_Click(object sender, EventArgs e)
        {
            picSimulator.NextStep();
        }

        public void SetWRegister(int wRegister)
        {
            picSimulator.WRegister = wRegister.ToString("X2");
        }

        public int GetWRegister()
        {
            return Int32.Parse(picSimulator.WRegister);
        }

        private void dcLabel_Click(object sender, EventArgs e)
        {
            picSimulator.DCBit = "";
        }

        private void cLabel_Click(object sender, EventArgs e)
        {
            picSimulator.CBit = "";
        }

        private void zLabel_Click(object sender, EventArgs e)
        {
            picSimulator.ZBit = "";
        }

        private void pdLabel_Click(object sender, EventArgs e)
        {
            picSimulator.PDBit = "";
        }

        private void toLabel_Click(object sender, EventArgs e)
        {
            picSimulator.TOBit = "";
        }

        private void rp0Label_Click(object sender, EventArgs e)
        {
            picSimulator.RP0Bit = "";
        }

        public void UpdateStack(Stack<int> stack)
        {
            // Clear stack
            stackGridView.Rows.Clear();
            int[] stackarr = stack.ToArray();
            for (int i = stackarr.Length; i > 0; i--)
            {
                int n = stackGridView.Rows.Add();
                stackGridView.Rows[n].Cells[0].Value = stackarr[i-1].ToString("0000");
            }
        }
        
    }
}
