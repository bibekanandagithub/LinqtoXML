using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Linq;
using System.Xml.Linq;
using System.IO;

namespace LinqtoXML
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            XElement xe = XElement.Load("test.xml");
            IEnumerable<XElement> allemp = xe.Elements();
            StringBuilder sb = new StringBuilder();
            foreach(var emp in allemp)
            {
                sb.Append(emp.Element("Name").Value);
                sb.Append(Environment.NewLine);
            }
            MessageBox.Show(sb.ToString());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show(CheckFolderExist("c:\\Builds\\"));

        }
        private string CheckFolderExist(string rootfolder="c:\\build")
        {
            string attachedpath = string.Empty;
            StringBuilder sb = new StringBuilder();
            XDocument xmlDoc = XDocument.Load("Distribution.xml");
            var files = from m in xmlDoc.Root.Elements("general")
                        select m;


            foreach (var a in files)
            {
                sb.Append(a.Element("Enviornment").Value);
            }
            string mainpath = rootfolder;
            if (Directory.Exists(rootfolder))
            {
                
                 attachedpath = mainpath + "\\" + sb.ToString() + "\\";
                if(Directory.Exists(attachedpath))
                {
                    MessageBox.Show("It exist");
                }
                else
                {
                    //  MessageBox.Show("It does not exist");
                    Directory.CreateDirectory(attachedpath);
                }
            }
            else
            {
                Directory.CreateDirectory(rootfolder);
                Directory.CreateDirectory(rootfolder + "//" + sb.ToString());
            }
            return attachedpath;
        }

        private string ListofTestFolder()
        {
            StringBuilder sb = new StringBuilder();
            XDocument xmlDoc = XDocument.Load("Distribution.xml");
            //var host = xmlDoc.Descendants("Host").Select(d =>
            //new
            //{
            //    Hostname = d.Attribute("Shortname").Value,
            //    TestFolders = d.Elements("TestFolder").Descendants("TestFolder").Select(x => new {
            //        tfolder=x.Element("TestFolder").Value
            //    })
            //}

            //).Where(m=>m.Hostname== "DESKTOP-DQ71QCG").ToList();

            //var TestFolders = xmlDoc.Descendants("TestFolders")
            //.Select(e => e.Elements("TestFolder")).ToList();

            var host = xmlDoc.Descendants("Host").Select(d =>
            new
            {
                Hostname = d.Attribute("Shortname").Value,
                TestFolders = d.Descendants("TestFolders").Descendants("TestFolder").Select(x => x).ToList()
                
                    
                
            }

            ).Where(m => m.Hostname == "DESKTOP-DQ71QCG").ToList();

            foreach(var results in host)
            {
                foreach(var folderlist in results.TestFolders)
                {
                    sb.Append(folderlist.Value);
                    sb.Append(Environment.NewLine);
                }
            }
            MessageBox.Show(sb.ToString());
            return null;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ListofTestFolder();
        }
    }
}
