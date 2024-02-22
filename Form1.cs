using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;

namespace Orszagok
{
    public partial class Form1 : Form
    {
        public static List<Orszag> lista = new List<Orszag>();

        public Form1()
        {
            InitializeComponent();
        }

        private void menuKilepes_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //var sr = new StreamReader("orszagok.txt", Encoding.GetEncoding("iso-8859-1"));
            var sr = new StreamReader("orszagok.txt", Encoding.Default);
            string[] haromSor = new string[3];
            while (!sr.EndOfStream)
            {
                for (int i = 0; i < 3; i++)
                {
                    haromSor[i] = sr.ReadLine();
                }
                var egyOrszagAdatai = new Orszag(haromSor);
                lista.Add(egyOrszagAdatai);
            }
            sr.Close();

            foreach (var sor in lista)
            {
                richTextBox1.Text += sor.Nev + "\n";
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            int nepesseg10mfolott = (from n in lista
                                     where n.Nepesseg > 10000
                                     select n).Count();
            textBox1.Text = nepesseg10mfolott.ToString();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            int nepesseg10malatt = (from n in lista
                                    where n.Nepesseg <= 10000
                                    select n).Count();
            textBox1.Text = nepesseg10malatt.ToString();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var legkisebbnagyobb = from sor in lista
                                   orderby sor.Nepesseg
                                   select sor;

            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    lblEredmeny.Text = legkisebbnagyobb.Last().Nev + " ";
                    lblEredmeny.Text += (legkisebbnagyobb.Last().Nepesseg*1000).ToString("#,##0") + " Fő";
                    break;
                case 1:
                    lblEredmeny.Text = legkisebbnagyobb.First().Nev + " ";
                    lblEredmeny.Text += (legkisebbnagyobb.First().Nepesseg * 1000).ToString("#,##0") + " Fő";
                    break;
            }
        }

        private void btnAtlag_Click(object sender, EventArgs e)
        {
            var atlag = lista.Select(x => x.Nepesseg).Average();
            //var atlag = (from sor in lista
            //             select sor.Nepesseg).Average();
            lblEredmeny.Text = atlag.ToString("#,##0.0") + " fő";
        }

        private void menuEuropaiak_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 europaiak = new Form2();
            europaiak.ShowDialog();
            europaiak.Dispose();
            this.Show();
        }
    }

    public class Orszag
    {
        public string Nev { get; set; }
        public int Nepesseg { get; set; }
        public string Kontinens { get; set; }

        public Orszag(string[] sorok)
        {
            TextInfo textInfo = new CultureInfo("hu-HU", false).TextInfo;
            sorok[0] = textInfo.ToTitleCase(sorok[0].ToLower());
            Nev = sorok[0];
            Nepesseg = Convert.ToInt32(sorok[1]);
            Kontinens = sorok[2];
        }
    }
}
