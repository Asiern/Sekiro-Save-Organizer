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
