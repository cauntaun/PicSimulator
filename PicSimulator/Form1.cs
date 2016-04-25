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

        public Form1()
        {
            InitializeComponent();
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

            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK){               
            }

            picSimulator = new PicSimulator();
            picSimulator.LoadLST(ofd.FileName);
        }

        /// <summary>
        /// Schreibe die Konsolenausgabe in das consoleLog-Fenster.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
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

        public void HighlightLine(int lineNumber)
        {
            dataGridView_Lst.Rows[lineNumber].Selected = true;
            //dataGridView_Lst.Rows[lineNumber].DefaultCellStyle.BackColor = Color.LightBlue;
        }
    }
}
