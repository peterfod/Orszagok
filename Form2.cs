using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Orszagok
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            foreach (var item in Form1.lista)
            {
                if (item.Kontinens.Equals("Európa"))
                {
                    richTextBox1.Text += item.Nev + '\n';
                }
            }
        }

        private void menuMentes_Click(object sender, EventArgs e)
        {
            richTextBox1.SaveFile("Europaiak.txt", RichTextBoxStreamType.PlainText);
        }
    }
}
