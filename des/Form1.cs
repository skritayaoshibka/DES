using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace des
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Label2_Click(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                string decrypted_text = textBox1.Text;
                string key = textBox2.Text;

                EncryptDecrypt encr = new EncryptDecrypt(decrypted_text, key);
                string result = encr.encrypt();
                textBox3.Text = result;
            }
            else
                MessageBox.Show("Введите ключ и текст для шифрования", "Введите данные");
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != "" && textBox3.Text != "")
            {
                string encrypted_text = textBox3.Text;
                string key = textBox2.Text;

                EncryptDecrypt decr = new EncryptDecrypt(encrypted_text, key);
                string result = decr.decrypt();
                textBox1.Text = result;
            }
            else
                MessageBox.Show("Введите ключ и текст для расшифрования", "Введите данные");
        }
    }
}
