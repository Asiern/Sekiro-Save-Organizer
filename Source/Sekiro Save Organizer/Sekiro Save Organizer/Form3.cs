using MaterialSkin.Controls;
using Sekiro_Save_Organizer.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sekiro_Save_Organizer
{
    public partial class Form3 : MaterialForm
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            //Select Language
            Lang();

            //Combobox Lang selected index
            switch (Properties.Settings.Default.Lang)
            {
                case "en":
                    comboBox1.SelectedIndex = 0;
                    break;
                case "es":
                    comboBox1.SelectedIndex = 1;
                    break;
            }

            StringConverter sc = new StringConverter();

            //Load
            string text = sc.ConvertToString(Properties.Settings.Default.edit);
            textBox1.Text = text;

            //Import
            string text2 = sc.ConvertToString(Properties.Settings.Default.import);
            textBox2.Text = text2;

            //Load
            string text3 = sc.ConvertToString(Properties.Settings.Default.load);
            textBox3.Text = text3;

            //Replace
            string text4 = sc.ConvertToString(Properties.Settings.Default.replace);
            textBox4.Text = text4;

            //Remove
            string text5 = sc.ConvertToString(Properties.Settings.Default.remove);
            textBox5.Text = text5;
        }
        
        //Language
        public void Lang()
        {
            //Default
            if (Settings.Default.Lang == "")
            {
                Settings.Default.Lang = "en";
            }
            //English
            else if (Settings.Default.Lang == "en")
            {
                this.Text = "Settings";
                label1.Text = "Edit";
                label2.Text = "Import";
                label3.Text = "Load";
                label5.Text = "Remove";
                label4.Text = "Replace";
                label6.Text = "Language";


            }
            //Spanish
            else if (Settings.Default.Lang == "es")
            {
                this.Text = "Ajustes";
                label1.Text = "Editar";
                label2.Text = "Importar";
                label3.Text = "Cargar";
                label5.Text = "Borrar";
                label4.Text = "Remplazar";
                label6.Text = "Lenguaje";
            }
        }

        //Save Settings
        public void SaveSettings()
        {
            Properties.Settings.Default.Save();
        }

        //Edit Hotkey Change     
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                KeysConverter kc = new KeysConverter();
                string tmpkey = textBox1.Text;
                Keys key = (Keys)kc.ConvertFromString(tmpkey);
                Properties.Settings.Default.edit = key;

                SaveSettings();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                textBox1.Text = "";
            }
        }

        //Import Hotkey Change 
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                KeysConverter kc = new KeysConverter();
                string tmpkey2 = textBox2.Text;
                Keys key2 = (Keys)kc.ConvertFromString(tmpkey2);
                Properties.Settings.Default.import = key2;

                SaveSettings();
            }
            catch (Exception ex)
            {
                textBox2.Text = "";
                MessageBox.Show(ex.Message);
            }
        }

        //Load Hotkey Change 
        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            try
            {
                KeysConverter kc = new KeysConverter();
                string tmpkey3 = textBox3.Text;
                Keys key3 = (Keys)kc.ConvertFromString(tmpkey3);
                Properties.Settings.Default.load = key3;

                SaveSettings();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                textBox3.Text = "";
            }
        }

        //Replace Hotkey Change 
        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            try
            {
                KeysConverter kc = new KeysConverter();
                string tmpkey4 = textBox4.Text;
                Keys key4 = (Keys)kc.ConvertFromString(tmpkey4);
                Properties.Settings.Default.replace = key4;

                SaveSettings();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                textBox4.Text = "";
            }
        }

        //Remove Hotkey Change 
        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            try
            {
                KeysConverter kc = new KeysConverter();
                string tmpkey5 = textBox5.Text;
                Keys key5 = (Keys)kc.ConvertFromString(tmpkey5);
                Properties.Settings.Default.remove = key5;

                SaveSettings();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                textBox5.Text = "";
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Language = comboBox1.Text;
            switch (Language)
            {
                case "English":
                    Properties.Settings.Default.Lang = "en";
                    break;
                case "Spanish":
                    Properties.Settings.Default.Lang = "es";
                    break;
            }
            SaveSettings();
            
        }
    }

}
