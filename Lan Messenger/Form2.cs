using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace Lan_messenger
{
    public partial class Form2 : Form
    {
        public string txt = null;

        public Form2(string c_id)
        {

            InitializeComponent();
            this.Text = c_id;
            // this.FormClosed += new FormClosedEventHandler(Form2_FormClosed);

        }
        public string get_text()
        {
            string tmp = txt;
            //txt = null;
            return tmp;

        }
        public void set_text(string msg)
        {
            string a = null;
            a = msg + "\r\n";

            this.textBox1.AppendText(a);
            //if (!this.Visible)
            //  this.Show();
            //MessageBox.Show(msg);

            //Application.DoEvents();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string p1 = textBox2.Text;
                txt = textBox2.Text.Trim();
                //MessageBox.Show(textBox2.Text);
                if (textBox2.Text != "\r\n" && textBox2.Text != "\n" && textBox2.Text != null)
                {

                    string a = "ME:  " + textBox2.Text.Trim();


                    try
                    {
                        if (!(string.IsNullOrEmpty(textBox2.Text.Trim())))
                        {
                            this.textBox1.AppendText(a);
                            this.textBox1.AppendText("\r\n");
                            //textBox1.Text = textBox1.Text;
                            //textBox1.Text += "\r\n";
                        }
                    }

                    catch (Exception p)
                    {
                        //    MessageBox.Show(p.Message);
                    }
                    textBox2.Text = null;
                }
            }
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            Hashtable temp = (Hashtable)Form1.hash_obj();
            //foreach (DictionaryEntry de in temp)
            //  MessageBox.Show(de.Key.ToString());


            temp.Remove(this.Text);


        }



        private void button1_Click(object sender, EventArgs e)
        {

            string p1 = textBox2.Text;
            txt = textBox2.Text.Trim();
            //MessageBox.Show(textBox2.Text);
            if (textBox2.Text != "\r\n" && textBox2.Text != "\n" && textBox2.Text != null)
            {

                string a = "ME:  " + textBox2.Text.Trim();


                try
                {
                    if (!(string.IsNullOrEmpty(textBox2.Text.Trim())))
                    {
                        this.textBox1.AppendText(a);
                        this.textBox1.AppendText("\r\n");
                        //textBox1.Text = textBox1.Text;
                        //textBox1.Text += "\r\n";
                    }
                }

                catch (Exception p)
                {
                    //    MessageBox.Show(p.Message);
                }
                textBox2.Text = null;
            }
        }

        public void signed_off(string name)
        {
            textBox2.Text = null;
            textBox2.Text = name + " have been signed off";
            this.textBox2.ReadOnly = true;
            this.button1.Enabled = false;
        }
        public void signed_in()
        {
            this.textBox2.Text = null;
            this.textBox2.ReadOnly = false;
            this.button1.Enabled = true;
        }
        public void myself_offline()
        {
            textBox2.Text = null;
            textBox2.Text = "You have signed off";
            this.textBox2.ReadOnly = true;
            this.button1.Enabled = false;

        }

    }
}
