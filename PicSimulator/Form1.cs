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


        void ReadLst(string file) //einlesen Lst
        {
            Regex rgxZahl = new Regex(@"^[0-9][0-9][0-9][0-9][0-9]");
            Regex rgxBefehl = new Regex(@"^[0-9][0-9][0-9][0-9]");

            Command Command = new Command();
            char[] delimiterChar = {' '};
            string[] speicheradresse = new string[1000];
            string[] command = new string[1000];
            
            dataGridViewLst.ColumnCount = 3;
            dataGridViewLst.Columns[0].Name = "Speicheradresse";
            dataGridViewLst.Columns[1].Name = "Befehl";
            dataGridViewLst.Columns[2].Name = "Argument";


            StreamReader sr = new StreamReader(file);
            string line;

            while ((line = sr.ReadLine()) != null)
            {
                string[] ausgabe = line.Split(delimiterChar);
                for (int i = 0; i < ausgabe.Length; i++)
                {
                    if (rgxZahl.IsMatch(ausgabe[i]) == true && ausgabe[i]!="") //Filter Speicheradresse
                    {
                        speicheradresse[i]=ausgabe[i];
                        Command.setCommand(ausgabe);
                    }

                    if (rgxBefehl.IsMatch(ausgabe[i]) == true && ausgabe[i] != "") //Filter Command
                    {
                        command[i] = ausgabe[i];
                        Command.setCommand(ausgabe);
                    }
                }
            }
            for (int i = 0; i < 40; i++)
            {
                dataGridViewLst.Rows.Add(new object[] {speicheradresse[i], command[i]});
            }
        }


        private void dateiLadenToolStrip_Click(object sender, EventArgs e) //Klick auf Menü
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Nur .lst Dateien. |*.lst;";

            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK){               
            }

            ReadLst(ofd.FileName);                          //Übergabe Filepfad an Einlesefunktion
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            this.Size = new Size(1020, 800);
        }
    }
}
