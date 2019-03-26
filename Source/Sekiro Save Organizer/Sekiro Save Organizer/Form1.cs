using Sekiro_Save_Organizer.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sekiro_Save_Organizer
{
    public partial class Form1 : Form
    {
        List<string> _items = new List<string>();

        public Form1()
        {
            InitializeComponent();
        }

        //Save Settings
        public void SaveSettings()
        {
            var SaveList = new ArrayList();
            foreach (object item in Saves.Items)
            {
                SaveList.Add(item);
            }

            Properties.Settings.Default.Saves = SaveList;
            Properties.Settings.Default.Save();
        }

        //Open Profiles
        private void button4_Click(object sender, EventArgs e)
        {
            Form2 Profiles = new Form2();
            Profiles.Show();
        }

        //Load ComboBox
        public void LoadComboBox()
        {
            try
            {
                comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
                foreach (string i in Properties.Settings.Default.Profiles)
                {
                    comboBox1.Items.Add(i);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //Load MAIN
        private void Form1_Load(object sender, EventArgs e)
        {
            //Hot Keys
            KeyPreview = true;
            //Lang
            if (Settings.Default.Lang == "")
            {
                Settings.Default.Lang = "en";

            }
            else if (Settings.Default.Lang == "en")
            {
                
            }
            else if (Settings.Default.Lang == "es")
            {
                
            }
            //Load ComboBox
            LoadComboBox();
            try
            {
                comboBox1.SelectedIndex = 0;
            }
            catch { }

            //Load Saves
            try
            {

                foreach (string i in Properties.Settings.Default.Saves)
                {
                    _items.Add(i);
                    // Change the DataSource.
                    Saves.DataSource = null;
                    Saves.DataSource = _items;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //Get Save Name
        public string GetSaveName()
        {
            string Profilename;

            //Inputbox Code
            Profilename = Microsoft.VisualBasic.Interaction.InputBox(" ", "Change Name", "Save file", 100, 0);

            return Profilename;
        }

        //Import Button
        private void button1_Click(object sender, EventArgs e)
        {
            //Select Path
            string filename = System.IO.Path.GetFileName(Properties.Settings.Default.savefilepath);
            string profilepath = @Path.Combine(Properties.Settings.Default.profilepath, comboBox1.Text, filename);
            string Savefilepath = @Properties.Settings.Default.savefilepath;
            if (!System.IO.Directory.Exists(profilepath))
            {
                System.IO.Directory.CreateDirectory(profilepath);
            }

            try
            {
               
                System.IO.File.Copy(Savefilepath, Path.Combine(profilepath, filename), false);
                //Add Item to listbox
                _items.Add(filename);

                // Change the DataSource.
                Saves.DataSource = null;
                Saves.DataSource = _items;

                //Save Settings
                SaveSettings();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        //Name Change
        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                string path = Properties.Settings.Default.profilepath;
                int saveindex = Saves.SelectedIndex;
                string name = Saves.SelectedItem.ToString();
                string newname = GetSaveName();
                _items[saveindex] = newname;
                Saves.DataSource = null;
                Saves.DataSource = _items;
                //Rename dir            
                Directory.Move(Path.Combine(path, comboBox1.Text, name), Path.Combine(path, comboBox1.Text, newname));

                SaveSettings();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //Load Saves
        private void button3_Click(object sender, EventArgs e)
        {
            string filename = System.IO.Path.GetFileName(Properties.Settings.Default.savefilepath);
            string profilepath = @Path.Combine(Properties.Settings.Default.profilepath, comboBox1.Text, Saves.Text);
            string Savefilepath = @Properties.Settings.Default.savefilepath;
            System.IO.File.Copy(Path.Combine(profilepath, filename), Savefilepath, true);
        }

        //Replace Saves
        private void button2_Click(object sender, EventArgs e)
        {
            string filename = System.IO.Path.GetFileName(Properties.Settings.Default.savefilepath);
            string profilepath = @Path.Combine(Properties.Settings.Default.profilepath, comboBox1.Text, Saves.Text);
            string Savefilepath = @Properties.Settings.Default.savefilepath;
            System.IO.File.Copy(Savefilepath, Path.Combine(profilepath, filename), true);

            SaveSettings();
        }

        //Remove Saves
        private void button6_Click(object sender, EventArgs e)
        {
            string path = Path.Combine(Properties.Settings.Default.profilepath, comboBox1.Text, Saves.Text);
            MessageBox.Show("You are about to delete " + Saves.Text);
            try
            {
                Directory.Delete(path, true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            int saveindex = Saves.SelectedIndex;

            try
            {
                _items.RemoveAt(saveindex);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            Saves.DataSource = null;
            Saves.DataSource = _items;

            SaveSettings();
        }

        //Open Settings
        private void button7_Click(object sender, EventArgs e)
        {
            Form3 Settings = new Form3();
            Settings.Show();
        }

        //Refresh Profiles
        private void button8_Click(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            LoadComboBox();
            try
            {
                comboBox1.SelectedIndex = 0;
            }
            catch { }
        }

        //Hot Keys
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {         
            //Edit Button
            if(e.KeyCode == Settings.Default.edit)
            {
                button5.PerformClick();
            }
            //Import Button
            if (e.KeyCode == Settings.Default.import)
            {
                button1.PerformClick();
            }
            //Load Button
            if (e.KeyCode == Settings.Default.load)
            {
                button3.PerformClick();
            }
            //Replace Button
            if (e.KeyCode == Settings.Default.replace)
            {
                button2.PerformClick();
            }
            //Remove Button
            if (e.KeyCode == Settings.Default.remove)
            {
                button6.PerformClick();
            }
        }
    }
}
