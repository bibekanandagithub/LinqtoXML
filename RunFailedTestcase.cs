using A_Automation.HealthCheck.ExecuteCS;
using A_Automation.XmlClass;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace A_Automation.HealthCheck
{
    public partial class RunFailedTestcase : Form
    {
        string enviornment = string.Empty;
        public RunFailedTestcase()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Execute(MultipleExecution.IPRerunconfig);

        }
        private void Execute(string IP)
        {
            try
            {

                DataSet ds = new DataSet();
                if (File.Exists(HealthCheckResource.MasterFilepath))
                {
                    ds.ReadXml(HealthCheckResource.MasterFilepath);
                    if (ds != null && ds.Tables.Count > 0)
                    {
                    }
                }
                foreach (var a in listBox1.SelectedItems)
                {
                    lst.Add(Convert.ToString(a));
                }


                List<string> pathlst = new List<string>();
                List<string> batchfilelist = new List<string>();

                DataRow[] drr = ds.Tables[0].Select("IP='" + IP + "'");
                string[] arr = null;
                StringBuilder sb = new StringBuilder();
                string SchedulerName = string.Empty;
                string rbtntext = string.Empty;
                if (lbl_enviornment.Text=="DVT") { rbtntext = "devtest"; } else if (lbl_enviornment.Text == "STG") { rbtntext = "staging"; }
                if (lbl_enviornment.Text == "PRD") { rbtntext = "Production"; } else if (lbl_enviornment.Text == "QAC") { rbtntext = "qac"; }
                if (drr != null && drr.Count<DataRow>() > 0)
                {
                    foreach (DataRow dr in drr)
                    {
                        SchedulerName = Convert.ToString(dr["SchdulerName"]);
                        batchfilelist.Add(SchedulerName + ".bat");
                        pathlst.Add(Convert.ToString(dr["FolderPath"]));
                        #region edit
                        arr = Convert.ToString(dr["ConfigFile"]).Split(';');
                        int i = 1;
                        foreach (string rec in arr)
                        {
                            

                            if (rec.Replace("\r\n", string.Empty).Length > 3 || Path.GetFileNameWithoutExtension(rec.Replace("\r\n", string.Empty)).ToLower() == "*.xml")
                            {

                                sb.Append("(" + i + ")" + "  " + rec.Replace("\r\n", string.Empty));
                                sb.Append(Environment.NewLine);
                                i++;
                            }

                        }
                        #endregion
                        try
                        {
                            Utility.ReportFolderCreate(" \\\\" + IP + "\\" + Convert.ToString("c:\\Report".Replace(Release.DefaultConfig.HomePath, ""))); //check
                        }
                        catch (Exception ex) { }

                        HealthCheck.ExecuteCS.Utility.ConfigFileCreate(Convert.ToString(dr["FolderPath"]), rbtntext, lst.ToArray(), SchedulerName);
                        string DestinationLocation = "\\\\" + IP + "\\" + Convert.ToString(dr["FolderPath"]).Replace(Release.DefaultConfig.HomePath, "") + "" + "\\" + SchedulerName + ".bat";
                        Task t = IOoperation.MoveLocation(System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "\\" + SchedulerName + ".bat", DestinationLocation);

                    }
                    HealthCheck.ExecuteCS.Utility.ConfigFileCreate(pathlst.ToArray(), batchfilelist.ToArray());
                    ArrayList al = new ArrayList();
                    al = new Helper_Automation().GetXMLsource(Release.DefaultConfig.PathListXmlFileName, "DirectoryPath");
                    string remotelocation = " \\\\" + IP + "\\" + Convert.ToString(al[0]).Replace(Release.DefaultConfig.HomePath, "");
                    if (File.Exists(remotelocation + "\\" + "callAllHelathCheck.bat"))
                    {
                        File.Delete(remotelocation + "\\" + "callAllHelathCheck.bat");
                    }
                    File.Move("callAllHelathCheck.bat", remotelocation + "\\" + "callAllHelathCheck.bat");
                    using (ResXResourceSet resxSet = new ResXResourceSet(@"Release\Credential.resx"))
                    {
                        enviornment = resxSet.GetString("ENV");
                        if (enviornment == "DVT")
                        {
                            // SchedulerOperation.CreateTaskRunOnce("execution.bat", sourepath, chk_Machine.SelectedItem.ToString());
                            SchedulerOperation.CreateTaskRunOnce("callAllHelathCheck.bat", Convert.ToString(al[0]), IP);
                        }
                        else
                        {
                            SchedulerOperation.CreateTaskRunOnceWithDifferentaccount("callAllHelathCheck.bat", Convert.ToString(al[0]), IP);
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void RunFailedTestcase_Load(object sender, EventArgs e)
        {
            lbl_IP.Text = MultipleExecution.IPRerunconfig;
            using (ResXResourceSet resxSet = new ResXResourceSet(@"Release\Credential.resx"))
            {
                lbl_enviornment.Text = resxSet.GetString("ENV");
                LoadList(MultipleExecution.IPRerunconfig);
            }
        }

        private void LoadList(string IP)
        {
           

            DataSet ds = new DataSet();
            if (File.Exists(HealthCheckResource.MasterFilepath))
            {
                ds.ReadXml(HealthCheckResource.MasterFilepath);
                if (ds != null && ds.Tables.Count > 0)
                {
                }
            }

               string[] arr = null;
           
                DataRow[] drr = ds.Tables[0].Select("IP='" + IP + "'");
                if (drr != null && drr.Count<DataRow>() > 0)
                {
                  
                    foreach (DataRow dr in drr)
                    {
                        arr = Convert.ToString(dr["ConfigFile"]).Split(';');
                        int i = 1;
                        foreach (string rec in arr)
                        {
                            if (rec.Replace("\r\n", string.Empty).Length > 3 || Path.GetFileNameWithoutExtension(rec.Replace("\r\n", string.Empty)).ToLower() == "*.xml")
                            {

                            listBox1.Items.Add(rec);
                                i++;
                            }
                        }

                       
                    }
                

               // lbl_dynamicvalue.Text = sb.ToString();
                //  ReloadList();
            }

        }
        List<string> lst = new List<string>();
        private void button2_Click(object sender, EventArgs e)
        {
            
        }
    }
}
