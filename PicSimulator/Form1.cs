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
using System.Diagnostics;

namespace PicSimulator
{
    public interface ISynchronousCall
    {
        void Invoke(Delegate method, params object[] args);
        void Invoke(Action action);
    }

    public partial class Form1 : Form, ISynchronousCall
    {
        private PicSimulator picSimulator;
        public delegate void Highlight(int linenumber);
        public Highlight mydelegate;
        private int stepdelay = 10;
        private Log log = null;
        private int test = 1;
        private Task loopTask;
        private bool run = false;
        private bool firstRun = true;
        private string[] quarzFrequenzen =
        {
            "32.768 kHz", "1 MHz", "2 MHz", "2,4576 MHz", "3 MHz", "3,2768 MHz", "3,68 MHz", "3,686411 MHz", "4 MHz", "4,096 MHz", "4,194304 MHz", "4,433619 MHz", "4,9152 MHz", "5 MHz", "6 MHz", "6,144 MHz", "6,25 MHz", "6,5536 MHz", "8 MHz", "10 MHz", "12 MHz", "16 MHz", "20 MHz", "24 MHz", "32 MHz", "40 MHz", "80 MHz"
        };
        private int aktuelleFrequenz = 8;

        private List<int> breakpoints = new List<int>();

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
            picSimulator.Reset(true);
            picSimulator.LoadLST(ofd.FileName);
        }

        public int GetFrequencyIndex()
        {
            return aktuelleFrequenz;
        }

        public void SetFrequencyIndex(int index)
        {
            aktuelleFrequenz = index;
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
            irpLabel.DataBindings.Add("Text", picSimulator, "IRPBit");
            pclLabel.DataBindings.Add("Text", picSimulator, "PCL");
            ps0Label.DataBindings.Add("Text", picSimulator, "PS0Bit");
            ps1Label.DataBindings.Add("Text", picSimulator, "PS1Bit");
            ps2Label.DataBindings.Add("Text", picSimulator, "PS2Bit");
            psaLabel.DataBindings.Add("Text", picSimulator, "PSABit");
            t0seLabel.DataBindings.Add("Text", picSimulator, "T0SEBit");
            t0csLabel.DataBindings.Add("Text", picSimulator, "T0CSBit");
            intedgLabel.DataBindings.Add("Text", picSimulator, "INTEDGBit");
            rbpuLabel.DataBindings.Add("Text", picSimulator, "RBPUBit");
            tmr0Label.DataBindings.Add("Text", picSimulator, "Timer");
            gieLabel.DataBindings.Add("Text", picSimulator, "GIEBit");
            eeieLabel.DataBindings.Add("Text", picSimulator, "EEIEBit");
            t0ieLabel.DataBindings.Add("Text", picSimulator, "T0IEBit");
            inteLabel.DataBindings.Add("Text", picSimulator, "INTEBit");
            rbieLabel.DataBindings.Add("Text", picSimulator, "RBIEBit");
            t0ifLabel.DataBindings.Add("Text", picSimulator, "T0IFBit");
            intfLabel.DataBindings.Add("Text", picSimulator, "INTFBit");
            rbifLabel.DataBindings.Add("Text", picSimulator, "RBIFBit");
            ra0Label.DataBindings.Add("Text", picSimulator, "RA0Bit");
            ra1Label.DataBindings.Add("Text", picSimulator, "RA1Bit");
            ra2Label.DataBindings.Add("Text", picSimulator, "RA2Bit");
            ra3Label.DataBindings.Add("Text", picSimulator, "RA3Bit");
            ra4Label.DataBindings.Add("Text", picSimulator, "RA4Bit");

            rb0Label.DataBindings.Add("Text", picSimulator, "RB0Bit");
            rb1Label.DataBindings.Add("Text", picSimulator, "RB1Bit");
            rb2Label.DataBindings.Add("Text", picSimulator, "RB2Bit");
            rb3Label.DataBindings.Add("Text", picSimulator, "RB3Bit");
            rb4Label.DataBindings.Add("Text", picSimulator, "RB4Bit");
            rb5Label.DataBindings.Add("Text", picSimulator, "RB5Bit");
            rb6Label.DataBindings.Add("Text", picSimulator, "RB6Bit");
            rb7Label.DataBindings.Add("Text", picSimulator, "RB7Bit");

            

            quarzFaktorLabel.DataBindings.Add("Text", picSimulator, "QuarzFaktor");

            quarzComboBox.DataSource = quarzFrequenzen;
            quarzComboBox.SelectedIndex = 8;


            timeLabel.DataBindings.Add("Text", picSimulator, "CycleCounterDisplayed");

            CheckForIllegalCrossThreadCalls = false;

            mydelegate = new Highlight(HighlightLine);

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

        private void rpbuLabel_Click(object sender, EventArgs e)
        {
            picSimulator.RBPUBit = "";
        }

        private void intedgLabel_Click(object sender, EventArgs e)
        {
            picSimulator.INTEDGBit = "";
        }

        private void t0csLabel_Click(object sender, EventArgs e)
        {
            picSimulator.T0CSBit = "";
        }

        private void t0seLabel_Click(object sender, EventArgs e)
        {
            picSimulator.T0SEBit = "";
        }

        private void psaLabel_Click(object sender, EventArgs e)
        {
            picSimulator.PSABit = "";
        }

        private void ps2Label_Click(object sender, EventArgs e)
        {
            picSimulator.PS2Bit = "";
        }

        private void ps1Label_Click(object sender, EventArgs e)
        {
            picSimulator.PS1Bit = "";
        }

        private void ps0Label_Click(object sender, EventArgs e)
        {
            picSimulator.PS0Bit = "";
        }

        private void rp1Label_Click(object sender, EventArgs e)
        {
            picSimulator.RP1Bit = "";
        }

        private void irpLabel_Click(object sender, EventArgs e)
        {
            picSimulator.IRPBit = "";
        }

        private void gieLabel_Click(object sender, EventArgs e)
        {
            picSimulator.GIEBit = "";
        }

        private void eeieLabel_Click(object sender, EventArgs e)
        {
            picSimulator.EEIEBit = "";
        }

        private void t0ieLabelLabel_Click(object sender, EventArgs e)
        {
            picSimulator.T0IEBit = "";
        }

        private void inteLabel_Click(object sender, EventArgs e)
        {
            picSimulator.INTEBit = "";
        }

        private void rbieLabel_Click(object sender, EventArgs e)
        {
            picSimulator.RBIEBit = "";
        }

        private void t0ifLabel_Click(object sender, EventArgs e)
        {
            picSimulator.T0IFBit = "";
        }

        private void intfLabel_Click(object sender, EventArgs e)
        {
            picSimulator.INTFBit = "";
        }

        private void rbifLabel_Click(object sender, EventArgs e)
        {
            picSimulator.RBIFBit = "";
        }

        private void ra4Label_Click(object sender, EventArgs e)
        {
            if (Int32.Parse(picSimulator.RA4Bit) == 0)
            {
                picSimulator.Timer = -4;
            } else
            {
                picSimulator.Timer = -5;
            }
            picSimulator.RA4Bit = "";

        }

        private void ra3Label_Click(object sender, EventArgs e)
        {
            picSimulator.RA3Bit = "";
        }

        private void ra2Label_Click(object sender, EventArgs e)
        {
            picSimulator.RA2Bit = "";
        }

        private void ra1Label_Click(object sender, EventArgs e)
        {
            picSimulator.RA1Bit = "";
        }

        private void ra0Label_Click(object sender, EventArgs e)
        {
            picSimulator.RA0Bit = "";
        }

        private void rb7Label_Click(object sender, EventArgs e)
        {
            picSimulator.RB7Bit = "";
        }

        private void rb6Label_Click(object sender, EventArgs e)
        {
            picSimulator.RB6Bit = "";
        }

        private void rb5Label_Click(object sender, EventArgs e)
        {
            picSimulator.RB5Bit = "";
        }

        private void rb4Label_Click(object sender, EventArgs e)
        {
            picSimulator.RB4Bit = "";
        }

        private void rb3Label_Click(object sender, EventArgs e)
        {
            picSimulator.RB3Bit = "";
        }

        private void rb2Label_Click(object sender, EventArgs e)
        {
            picSimulator.RB2Bit = "";
        }

        private void rb1Label_Click(object sender, EventArgs e)
        {
            picSimulator.RB1Bit = "";
        }

        private void rb0Label_Click(object sender, EventArgs e)
        {
            picSimulator.RB0Bit = "";
        }

        private void runBtn_Click(object sender, EventArgs e)
        {
            run = true;
            firstRun = true;
            loopTask = new Task(Run);
            loopTask.Start();
        }

        private void stopBtn_Click(object sender, EventArgs e)
        {
            run = false;
            firstRun = true;
        }

        private void Run()
        {
            while (run)
            {
                if (breakpoints.Contains(Int32.Parse(picSimulator.ProgramCounter, System.Globalization.NumberStyles.HexNumber)) && !firstRun)
                {
                    run = false;
                } else
                {
                    picSimulator.NextStep();
                    loopTask.Wait(stepdelay);
                }
                firstRun = false;
            }
        }


        void ISynchronousCall.Invoke(Delegate method, params object[] args)
        {
            // Call the provided action on the UI Thread using Control.Invoke()
            Invoke(method, args);
        }

        void ISynchronousCall.Invoke(Action action)
        {
            // Call the provided action on the UI Thread using Control.Invoke()
            Invoke(action);
        }

        private void setDelayBtn_Click(object sender, EventArgs e)
        {
            try
            {
                stepdelay = Int32.Parse(delayTxtBox.Text);
                delayLabel.Text = stepdelay + " ms";
            }catch
            {
                illegalDelayLabel.Text = "Bitte nur int Werte!";
            }
        }

        private void dataGridView_Lst_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string pc = dataGridView_Lst.Rows[e.RowIndex].Cells[0].Value.ToString().Substring(0, 4);
            if (string.IsNullOrWhiteSpace(pc))
            {
                MessageBox.Show("Sie können nur Zeilen mit Code für einen Breakpoint verwenden");
            } else
            {
                if (!breakpoints.Contains(Int32.Parse(pc, System.Globalization.NumberStyles.HexNumber)))
                {
                    breakpoints.Add(Int32.Parse(pc, System.Globalization.NumberStyles.HexNumber));
                }
                
                UpdateBreakPoints();
                //MessageBox.Show("PC: " + pc + " hinzugefuegt");
            }
        }

        private void breakpointGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int bp = Int32.Parse(breakpointGridView.Rows[e.RowIndex].Cells[0].Value.ToString(), System.Globalization.NumberStyles.HexNumber);
            //MessageBox.Show("BP: " + bp); 
            breakpoints.Remove(bp);
            UpdateBreakPoints();
        }

        private void UpdateBreakPoints()
        {
            breakpointGridView.Rows.Clear();
            for (int i = 0; i < breakpoints.Count; i++)
            {
                int n = breakpointGridView.Rows.Add();
                breakpointGridView.Rows[n].Cells[0].Value = breakpoints[i].ToString("X4");
            }
        }

        private void hilfeToolStrip_Click(object sender, EventArgs e)
        {
            string locationToSavePdf = Path.Combine(Path.GetTempPath(), "Hilfe.pdf"); 
            File.WriteAllBytes(locationToSavePdf, Properties.Resources.Hilfe);   
            Process.Start(locationToSavePdf);   
            
        }

        private void quarzComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            picSimulator.QuarzFaktor = quarzComboBox.SelectedIndex;
            Console.Write(picSimulator.QuarzFaktor);
        }

        private void resetBtn_Click(object sender, EventArgs e)
        {
            // Reset Storage
            picSimulator.GetRegisterSet().InitializeRegister();
            picSimulator.Reset(false);
            HighlightLine(0);
            breakpoints = new List<int>();
            breakpointGridView.Rows.Clear();
            
        }
    }
}
