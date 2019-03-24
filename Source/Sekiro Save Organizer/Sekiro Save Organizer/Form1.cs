using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
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

        private void Form1_Load(object sender, EventArgs e)
        {
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
            string profilepath = @Path.Combine(Properties.Settings.Default.profilepath, comboBox1.Text,filename);
            string Savefilepath = @Properties.Settings.Default.savefilepath;
            if (!System.IO.Directory.Exists(profilepath))
            {
                System.IO.Directory.CreateDirectory(profilepath);
            }

            try
            {
                System.IO.File.Copy(Savefilepath, Path.Combine(profilepath, filename), true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            //Add Item to listbox
            _items.Add(filename);

            // Change the DataSource.
            Saves.DataSource = null;
            Saves.DataSource = _items;

            //Save Settings
            SaveSettings();           
        }

        //Name Change
        private void button5_Click(object sender, EventArgs e)
        {
            string path = Properties.Settings.Default.profilepath;
            int saveindex = Saves.SelectedIndex;
            string name = Saves.SelectedItem.ToString();
            string newname = GetSaveName();
            _items[saveindex] = newname;
            Saves.DataSource = null;
            Saves.DataSource = _items;
            //Rename dir            
            Directory.Move(Path.Combine(path,comboBox1.Text,name),Path.Combine(path,comboBox1.Text,newname));

            SaveSettings();
        }

        //Load Saves
        private void button3_Click(object sender, EventArgs e)
        {
            string filename = System.IO.Path.GetFileName(Properties.Settings.Default.savefilepath);
            string profilepath = @Path.Combine(Properties.Settings.Default.profilepath, comboBox1.Text, Saves.Text);
            string Savefilepath = @Properties.Settings.Default.savefilepath;            
            System.IO.File.Copy(Path.Combine(profilepath, filename),Savefilepath, true);
        }

        //Replace
        private void button2_Click(object sender, EventArgs e)
        {
            string filename = System.IO.Path.GetFileName(Properties.Settings.Default.savefilepath);
            string profilepath = @Path.Combine(Properties.Settings.Default.profilepath, comboBox1.Text, Saves.Text);
            string Savefilepath = @Properties.Settings.Default.savefilepath;
            System.IO.File.Copy(Savefilepath,Path.Combine(profilepath, filename), true);

            SaveSettings();
        }

        //Remove
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
        }

        //Load List
        public void LoadList()
        {
            ArrayList Saves = new ArrayList();
            Saves = Properties.Settings.Default.Saves;
            int i = 0;
            int z;
            while ( Saves[i] != comboBox1.Text)
            {
                i++;
            }
            z = i + 1;
            while (Saves[z] != " ")
            {

            }
        }
    }

}
