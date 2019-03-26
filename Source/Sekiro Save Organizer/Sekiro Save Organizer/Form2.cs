using Sekiro_Save_Organizer.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sekiro_Save_Organizer
{
    public partial class Form2 : Form
    {
        List<string> _items = new List<string>();

        public Form2()
        {
            InitializeComponent();
        }

        public void SaveSettings()
        {
            var ProfileList = new ArrayList();
            foreach (object item in Profiles.Items)
            {
                ProfileList.Add(item);
            }

            Properties.Settings.Default.Profiles = ProfileList;
            Properties.Settings.Default.Save();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            //Select Language
            Lang();
            //Load Savefilepath
            Savefilepath.Text = Properties.Settings.Default.savefilepath;
            //Load Profilepath
            Profilepath.Text = Properties.Settings.Default.profilepath;
            //Load Profiles
            try
            {
                foreach (string i in Properties.Settings.Default.Profiles)
                {
                    _items.Add(i);
                    // Change the DataSource.
                    Profiles.DataSource = null;
                    Profiles.DataSource = _items;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
                groupBox1.Text = "Paths";
                groupBox2.Text = "Profiles";
                label1.Text = "Savefile path";
                label2.Text = "Profiles path";
                button5.Text = "Browse";
                button6.Text = "Browse";
                button1.Text = "New";
                button2.Text = "Edit";
                button4.Text = "Delete";
                this.Text = "Profiles";

            }
            //Spanish
            else if (Settings.Default.Lang == "es")
            {
                groupBox1.Text = "Rutas";
                groupBox2.Text = "Perfiles";
                label1.Text = "Ruta de los archivos de guardado";
                label2.Text = "Ruta del guardado de los perfiles";
                button5.Text = "Buscar";
                button6.Text = "Buscar";
                button1.Text = "Nuevo";
                button2.Text = "Editar";
                button4.Text = "Borrar";
                this.Text = "Perfiles";
            }
        }

        //Browse Button Savefilepath
        private void button5_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.savefilepath = openFileDialog.FileName.ToString();
                Properties.Settings.Default.Save();
                Savefilepath.Text = Properties.Settings.Default.savefilepath;
            }
        }

        //Brwose Button Profilepath
        private void button6_Click(object sender, EventArgs e)
        {
            
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.profilepath = folderBrowserDialog1.SelectedPath.ToString();
                Properties.Settings.Default.Save();
                Profilepath.Text = Properties.Settings.Default.profilepath;
            }
        }

        //Get Profile Name
        public string GetProfileName()
        {
            string Profilename;

            //Inputbox Code
            Profilename = Microsoft.VisualBasic.Interaction.InputBox(" ","Profile Name","New Profile",100,0);

            return Profilename;
        }

        //Add Profiles
        private void button1_Click(object sender, EventArgs e)
        {
            string profilename = GetProfileName();
            _items.Add(profilename);

            // Change the DataSource.
            Profiles.DataSource = null;
            Profiles.DataSource = _items;
                        
            Form1 frm1 = new Form1();
            frm1.LoadComboBox();

            SaveSettings();

        }

        //Edit Profile Names
        private void button2_Click(object sender, EventArgs e)
        {
            int profileindex = Profiles.SelectedIndex;
            _items[profileindex] = GetProfileName();
            Profiles.DataSource = null;
            Profiles.DataSource = _items;

            SaveSettings();
        }

        //Remove Profiles
        private void button4_Click(object sender, EventArgs e)
        {
            int profileindex = Profiles.SelectedIndex;

            try
            {
                _items.RemoveAt(profileindex);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            Profiles.DataSource = null;
            Profiles.DataSource = _items;

            SaveSettings();
        }
             
         
    }
}
