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

        /// <summary>
        /// Einlesen der LST-Datei
        /// </summary>
        /// <param name="file">Pfad der LST-Datei</param>
        void ReadLst(string file)
        {
            Regex rgxZahl = new Regex(@"^[0-9]$*");

            Command Command = new Command();
            char[] delimiterChar = { ' ' };
             
            string[] speicheradresse = new string[1000];
            string[] command = new string[1000];
            string[] argument = new string[2];

            dataGridViewLst.ColumnCount = 3;
            dataGridViewLst.Columns[0].Name = "Speicheradresse";
            dataGridViewLst.Columns[1].Name = "Command";
            dataGridViewLst.Columns[2].Name = "Argument";


            /// einlesen LST-File mithilfe Filepfad
            StreamReader sr = new StreamReader(file);
            /// jeweils aktuelle Zeile
            string line;

            while ((line = sr.ReadLine()) != null)
            {
                /// Regex Prüfung ob Zeile (line) mit Zahl beginnt
                if (rgxZahl.IsMatch(line) == true)
                {
                   /// Aufsplittung der Zeile (line) an Leerzeichen
                   string[] ausgabe = line.Split(delimiterChar);
                   /// Durchloopen von ausgabe (Zeile)
                   for (int i = 0; i < (ausgabe.Length - 1); i++)
                   {
                       ///Wenn gesplittete Zeile (ausgabe) mit Zahl beginnt
                       if (rgxZahl.IsMatch(ausgabe[i]) == true)   
                        {
                            // /erster Index = Speicheradresse
                            speicheradresse[i] = ausgabe[i];
                            /// zweiter Index = Command + Argument
                            command[i] = Command.setCommand(ausgabe[i + 1]); 
                            argument[i] = Command.setArgument(ausgabe[i + 1]);    
                            dataGridViewLst.Rows.Add(new object[] { speicheradresse[i], command[i], argument[i]});
                            break;
                        }
                   }
                }
            }
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

            /// Übergabe Filepfad an Einlesefunktion
            ReadLst(ofd.FileName);                          
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
