using MaterialSkin;
using MaterialSkin.Controls;
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
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sekiro_Save_Organizer
{
    public partial class Form1 : MaterialForm
    {
        List<string> _items = new List<string>();

        public Form1()
        {
            InitializeComponent();
            WebClient webClient = new WebClient();
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
            if (!webClient.DownloadString("https://pastebin.com/raw/paSm7NLU").Contains("1.2"))
            {
                if (MessageBox.Show("Update available", "SekiroSaveOrganizerUpdater", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    System.Diagnostics.Process.Start("https://github.com/PapaElGunmen/Sekiro-Save-Organizer/releases");
                }
                else
                {

                }
            }
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
            catch
            {
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
                button8.Text = "Refresh Profiles";
                button6.Text = "Settings";
                button1.Text = "Edit";
                button2.Text = "Import";
                button3.Text = "Load";
                button4.Text = "Replace";
                button5.Text = "Remove";
                button7.Text = "Profiles";
            }
            //Spanish
            else if (Settings.Default.Lang == "es")
            {
                button8.Text = "Refrescar Perfiles";
                button7.Text = "Perfiles";
                button1.Text = "Editar";
                button2.Text = "Importar";
                button3.Text = "Cargar";
                button4.Text = "Reemplazar";
                button5.Text = "Borrar";
                button6.Text = "Ajustes";
            }
        }

        //Load Saves
        public void LoadSaves(ArrayList saves)
        {
            try
            {
                //Clear data source
                _items.Clear();

                //Load data source
                foreach (string i in saves)
                    {
                        _items.Add(i);
                        // Change the DataSource.

                    }
                    Saves.DataSource = null;
                    Saves.DataSource = _items;
            }
            catch
            {
            }
        }

        //Load MAIN
        private void Form1_Load(object sender, EventArgs e)
        {
            List<string> _items = new List<string>();
            //Load Language
            this.Lang();

            //Hot Keys
            KeyPreview = true;
            
                       
            try
            {
                //Load ComboBox
                this.LoadComboBox();
                if (Settings.Default.Profiles != null)
                {
                    comboBox1.SelectedIndex = 0;
                }
            }
            catch { }

            //LoadSaves();
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
            catch
            {
            }
            
        }

        //Name Change
        private void button5_Click(object sender, EventArgs e)
        {
            
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
            
               try
               {
                   Directory.Delete(path, true);
               }
               catch
               {
               }

            int saveindex = Saves.SelectedIndex;

            try
            {
                _items.RemoveAt(saveindex);
            }
            catch
            {
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
                button1.PerformClick();
            }
            //Import Button
            if (e.KeyCode == Settings.Default.import)
            {
                button2.PerformClick();
            }
            //Load Button
            if (e.KeyCode == Settings.Default.load)
            {
                button3.PerformClick();
            }
            //Replace Button
            if (e.KeyCode == Settings.Default.replace)
            {
                button4.PerformClick();
            }
            //Remove Button
            if (e.KeyCode == Settings.Default.remove)
            {
                button5.PerformClick();
            }
        }

        //Load different profile saves
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
          
            try
            {
                ClearSaves();
                string profile = comboBox1.Text;
                ArrayList saves = new ArrayList();


                //Profile path
                string profilepath = @Path.Combine(Properties.Settings.Default.profilepath, profile);

                var folders = Directory.GetDirectories(profilepath);

                //Search for saves in profile folder
                foreach (string dir in folders)
                {
                    saves.Add(System.IO.Path.GetFileName(dir));
                }
                //Load Saves
                LoadSaves(saves);
            }

            catch
            {
            }
        }

        //Clear data in saves listbox
        public void ClearSaves()
        {
            Saves.DataSource = null;
            Saves.Items.Clear();
        }



        //Name Change
        private void materialFlatButton1_Click(object sender, EventArgs e)
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
            catch
            {
            }
        }

        //Import
        private void materialFlatButton2_Click(object sender, EventArgs e)
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
            catch
            {
            }
        }

        //Load
        private void materialFlatButton3_Click(object sender, EventArgs e)
        {

            string filename = System.IO.Path.GetFileName(Properties.Settings.Default.savefilepath);
            string profilepath = @Path.Combine(Properties.Settings.Default.profilepath, comboBox1.Text, Saves.Text);
            string Savefilepath = @Properties.Settings.Default.savefilepath;
            System.IO.File.Copy(Path.Combine(profilepath, filename), Savefilepath, true);
        }

        //Replace
        private void materialFlatButton4_Click(object sender, EventArgs e)
        {
            string filename = System.IO.Path.GetFileName(Properties.Settings.Default.savefilepath);
            string profilepath = @Path.Combine(Properties.Settings.Default.profilepath, comboBox1.Text, Saves.Text);
            string Savefilepath = @Properties.Settings.Default.savefilepath;
            System.IO.File.Copy(Savefilepath, Path.Combine(profilepath, filename), true);

            SaveSettings();
        }

        //Remove
        private void materialFlatButton5_Click(object sender, EventArgs e)
        {
            string path = Path.Combine(Properties.Settings.Default.profilepath, comboBox1.Text, Saves.Text);

            try
            {
                Directory.Delete(path, true);
            }
            catch
            {
            }

            int saveindex = Saves.SelectedIndex;

            try
            {
                _items.RemoveAt(saveindex);
            }
            catch
            {
            }
            Saves.DataSource = null;
            Saves.DataSource = _items;

            SaveSettings();
        }

        //Settings
        private void materialFlatButton6_Click(object sender, EventArgs e)
        {
            Form3 Settings = new Form3();
            Settings.Show();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        //Refresh Profiles
        private void materialFlatButton8_Click(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            LoadComboBox();
            try
            {
                comboBox1.SelectedIndex = 0;
            }
            catch { }
        }

        //Profiles
        private void materialFlatButton7_Click(object sender, EventArgs e)
        {
            Form2 Profiles = new Form2();
            Profiles.Show();
        }
    }
}
