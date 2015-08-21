using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Devoxelation
{
    public partial class SettingsEditor : Form
    {
        public SettingsEditor()
        {
            InitializeComponent();
        }

        private void exitSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void creditsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("d005578a - Devoxelation" + "\n" + "Programmed By:" + "\n" + "\tJosh Dadak");
        }

        private void reload_Click(object sender, EventArgs e)
        {
            String firstNum, lastNum;

            // Gets the first 4 and last 4 numbers from the Selectionbox, and saves them to a variable
            firstNum = resolutionSelect.Text.Substring(0, 4);
            lastNum = resolutionSelect.Text.Substring(Math.Max(0, resolutionSelect.Text.Length - 4));

            // Removes any spaces from the above strings.
            firstNum = firstNum.Replace(" ", "");
            lastNum = lastNum.Replace(" ", "");

            // Loads the Application Settings XML file
            System.Xml.XmlDocument appConfigXML = new System.Xml.XmlDocument();
            System.Xml.XmlDocument serConfigXML = new System.Xml.XmlDocument();
            appConfigXML.Load(Game._path + "\\Content\\ApplicationSettings.xml");
            serConfigXML.Load(Game._path + "\\Content\\ServiceSettings.xml");

            // Sets the XML attributes to the current values of the editor
            appConfigXML.SelectSingleNode("//ScreenTitle").InnerText = windowTitleBox.Text;
            appConfigXML.SelectSingleNode("//FullScreen").InnerText = isFullScreen.Text;
            appConfigXML.SelectSingleNode("//ScreenWidth").InnerText = Convert.ToString(firstNum);
            appConfigXML.SelectSingleNode("//ScreenHeight").InnerText = Convert.ToString(lastNum);

            // Saves the Application Settings XML file
            appConfigXML.Save(Game._path + "\\Content\\ApplicationSettings.xml");
            serConfigXML.Save(Game._path + "\\Content\\ServiceSettings.xml");

            MessageBox.Show("Reload Application for changes to take effect");
            System.Diagnostics.Process.Start(Game._path + @"\\Devoxelation.exe");
            Application.Exit();
        }

        private void SettingsEditor_Load(object sender, EventArgs e)
        {
            // Loads the Application Settings XML file
            System.Xml.XmlDocument appConfigXML = new System.Xml.XmlDocument();
            System.Xml.XmlDocument serConfigXML = new System.Xml.XmlDocument();
            appConfigXML.Load(Game._path + "\\Content\\ApplicationSettings.xml");
            serConfigXML.Load(Game._path + "\\Content\\ServiceSettings.xml");

            // First Tab (Application Settings)
            windowTitleBox.Text = appConfigXML.SelectSingleNode("//ScreenTitle").InnerText;
            resolutionSelect.Text = appConfigXML.SelectSingleNode("//ScreenWidth").InnerText + " x " + appConfigXML.SelectSingleNode("//ScreenHeight").InnerText;
            isFullScreen.Text = Convert.ToString(appConfigXML.SelectSingleNode("//FullScreen").InnerText);
        }
    }
}
