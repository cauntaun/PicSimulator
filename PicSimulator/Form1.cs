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
        public Form1()
        {
            InitializeComponent();
        }


        void Clear_all()
        {
            dataGridViewLst.Columns.Clear();
        }

        void PresentLst(string file)
        {                  
            dataGridViewLst.ColumnCount = 3;
            dataGridViewLst.Columns[0].Name = "Speicheradresse";
            dataGridViewLst.Columns[1].Name = "Command";
            dataGridViewLst.Columns[2].Name = "Argument";



           dataGridViewLst.Rows.Add(new object[] {InstructionDecoder.ReadLst});
        }


        /// <summary>
        /// Auswahl der LST-Datei
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dateiLadenToolStrip_Click(object sender, EventArgs e) //Klick auf Menü
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Nur .lst Dateien. |*.lst;";

            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK){               
            }

            InstructionSet instructionset;

            /// Übergabe Filepfad an Einlesefunktion
            instructionset = InstructionDecoder.ReadLst(ofd.FileName);                          
        }

        /// <summary>
        /// Größe des Simulators
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            this.Size = new Size(1020, 800);
        }
    }
}
