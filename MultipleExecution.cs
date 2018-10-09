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
    public partial class MultipleExecution : Form
    {
        DataSet ds = new DataSet();
        Dictionary<string, string> machineDict = new Dictionary<string, string>();
        string enviornment = null;
        public MultipleExecution()
        {
            InitializeComponent();
        }
      
        private async void MultipleExecution_Load(object sender, EventArgs e)
        {

            using (ResXResourceSet resxSet = new ResXResourceSet(@"Release\Credential.resx"))
            {
                enviornment = resxSet.GetString("ENV");
                lbl_username.Text = string.IsNullOrEmpty(resxSet.GetString("UN")) ? "Unknown User".ToUpper() : resxSet.GetString("UN").ToUpper();
                if (enviornment == "DVT")
                {
                    rbtn_STG.Enabled = false;
                    rbtn_PRD.Enabled = false;
                    rbtn_QAC.Enabled = true;
                    rbtn_DVT.Enabled = true;
                }
                if (enviornment == "STG")
                {
                    rbtn_STG.Enabled = true;
                    rbtn_PRD.Enabled = false;
                    rbtn_QAC.Enabled = false;
                    rbtn_DVT.Enabled = false;
                }
                if (enviornment == "PRD")
                {
                    rbtn_STG.Enabled = false;
                    rbtn_PRD.Enabled = true;
                    rbtn_QAC.Enabled = false;
                    rbtn_DVT.Enabled = false;
                }

            }
           

            
            DataTable tempdat = new DataTable();
            using (ds)
            {
                if (File.Exists(HealthCheckResource.MasterFilepath))
                {
                    ds.ReadXml(HealthCheckResource.MasterFilepath);
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        tempdat = ds.Tables[0].Copy();
                        if (tempdat.Rows.Count > 0)
                        {
                            //foreach (DataRow dr in RemoveDuplicateRows(tempdat, "IP").Rows)
                            //{
                            //    treeView1.Nodes.Add(Convert.ToString(dr["IP"]));                               

                            //    await Task.Delay(20);
                            //}

                            foreach (string s in GetIpwiseMachine("DVT"))
                            {
                                treeView1.Nodes.Add(s);

                                await Task.Delay(20);
                            }
                            treeView1.SelectedNode = null;
                        }
                    }
                }
            }
        }

        public static  List<string> GetIpwiseMachine(string enviornment)
        {
            DataSet ds = new DataSet();
            List<string> storeip = new List<string>();
           
            using (ds)
            {
                if (File.Exists(@"HealthCheck\HealthCheckComponent\HealthCheckMachine.xml"))
                {
                    ds.ReadXml(@"HealthCheck\HealthCheckComponent\HealthCheckMachine.xml");
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            if (Convert.ToString(dr["Enviornment"]).Contains(enviornment))
                            {
                                storeip.Add(Convert.ToString(dr["Ipaddress"]));
                            }
                        }
                        return storeip;
                    }
                }
                return null;
            }
        }

        public DataTable RemoveDuplicateRows(DataTable dTable, string colName)
        {
            Hashtable hTable = new Hashtable();
            ArrayList duplicateList = new ArrayList();            
            foreach (DataRow drow in dTable.Rows)
            {
                if (hTable.Contains(drow[colName]))
                    duplicateList.Add(drow);
                else
                    hTable.Add(drow[colName], string.Empty);
            }           
            foreach (DataRow dRow in duplicateList)
                dTable.Rows.Remove(dRow);
            return dTable;
        }
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            DataTable dt = ds.Tables[0];
            StringBuilder sb = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();
            string[] arr = null;
            if (treeView1.SelectedNode!=null)
            {
                DataRow[] drr = ds.Tables[0].Select("IP='" + treeView1.SelectedNode.Text + "'");
                if (drr != null && drr.Count<DataRow>() > 0)
                {
                    sb.Append("IP Address is " + treeView1.SelectedNode.Text);
                    sb.Append(Environment.NewLine);
                    sb.Append("---------------------------------------------------------------------------");
                    sb.Append(Environment.NewLine);
                    foreach (DataRow dr in drr)
                    {
                        arr = Convert.ToString(dr["ConfigFile"]).Split(';');
                        int i = 1;
                        foreach (string rec in arr)
                        {
                            if (rec.Replace("\r\n", string.Empty).Length > 3 || Path.GetFileNameWithoutExtension(rec.Replace("\r\n", string.Empty)).ToLower() == "*.xml")
                            {

                                sb2.Append("(" + i + ")" + "  " + rec.Replace("\r\n", string.Empty));
                                sb2.Append(Environment.NewLine);
                                i++;
                            }
                        }                   

                        sb.Append(Environment.NewLine);
                        sb.Append("Component Name:- " + Convert.ToString(dr["ComponentName"]));
                        sb.Append(Environment.NewLine);
                        sb.Append("Folder Path:- " + Convert.ToString(dr["FolderPath"]));
                        sb.Append(Environment.NewLine);                     
                        sb.Append("List of Config Files" + Environment.NewLine);
                        sb.Append("--------------------------" + Environment.NewLine);
                        sb.Append(Convert.ToString(sb2));                       
                        sb.Append(Environment.NewLine);
                        sb.Append("---------------------------------------------------------------------------");
                        sb2.Clear();
                    }
                }

                lbl_dynamicvalue.Text = sb.ToString();
              //  ReloadList();
            }
           
        }
        private void ReloadList()
        {
            foreach (KeyValuePair<string, string> keypari in machineDict)
            {
                list_status.Items.Clear();
                string schedulerstatus = GetSchedulerStatus(IOoperation.Taskname, keypari.Key) == null ? "Ready" : GetSchedulerStatus(IOoperation.Taskname, keypari.Key);
                ListViewItem lvi = new ListViewItem(treeView1.SelectedNode.Text + "  " + schedulerstatus);
                list_status.Items.Add(lvi);
            }
           
        }

        public string GetSchedulerStatus(string taskname, string machinename)
        {
            Task<string> t = XmlClass.SchedulerOperation.GetTaskInformation(IOoperation.Taskname, machinename);
            return t.Result;

        }

        private void TreeviewCommand_Opening(object sender, CancelEventArgs e)
        {
          
        }

        private void TreeviewCommand_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            ToolStripItem item = e.ClickedItem;
            if(item.Text== "Clear Result")
            {
                MessageBox.Show(item.Text, "And the clicked option is...");
            }

           
           
        }

        private void btn_execute_Click(object sender, EventArgs e)
        {
            try
            {
                if (! machineDict.Keys.Contains(treeView1.SelectedNode.Text.Trim()))
                {
                    machineDict.Add(treeView1.SelectedNode.Text.Trim(), "");
                }

               
                List<string> pathlst = new List<string>();
                List<string> batchfilelist = new List<string>();

                DataRow[] drr = ds.Tables[0].Select("IP='" + treeView1.SelectedNode.Text + "'");
                string[] arr = null;
                StringBuilder sb = new StringBuilder();
                string SchedulerName = string.Empty;
                string rbtntext = string.Empty;
                if (rbtn_DVT.Checked) { rbtntext = "devtest"; } else if (rbtn_STG.Checked) { rbtntext = "staging"; }
                if (rbtn_PRD.Checked) { rbtntext = "Production"; } else if (rbtn_QAC.Checked) { rbtntext = "qac"; }
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
                            Utility.ReportFolderCreate(" \\\\" + treeView1.SelectedNode.Text + "\\" + Convert.ToString("c:\\Report".Replace(Release.DefaultConfig.HomePath, ""))); //check
                        }
                        catch (Exception ex) { }

                        HealthCheck.ExecuteCS.Utility.ConfigFileCreate(Convert.ToString(dr["FolderPath"]), rbtntext, arr, SchedulerName);
                        string DestinationLocation = "\\\\" + treeView1.SelectedNode.Text + "\\" + Convert.ToString(dr["FolderPath"]).Replace(Release.DefaultConfig.HomePath, "") + "" + "\\" + SchedulerName + ".bat";
                        Task t = IOoperation.MoveLocation(System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "\\" + SchedulerName + ".bat", DestinationLocation);

                    }
                    HealthCheck.ExecuteCS.Utility.ConfigFileCreate(pathlst.ToArray(), batchfilelist.ToArray());
                    ArrayList al = new ArrayList();
                    al = new Helper_Automation().GetXMLsource(Release.DefaultConfig.PathListXmlFileName, "DirectoryPath");
                    string remotelocation = " \\\\" + treeView1.SelectedNode.Text + "\\" + Convert.ToString(al[0]).Replace(Release.DefaultConfig.HomePath, "");
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
                            SchedulerOperation.CreateTaskRunOnce("callAllHelathCheck.bat", Convert.ToString(al[0]), treeView1.SelectedNode.Text);
                        }
                        else
                        {
                            SchedulerOperation.CreateTaskRunOnceWithDifferentaccount("callAllHelathCheck.bat", Convert.ToString(al[0]), treeView1.SelectedNode.Text);
                        }
                    }

                }

            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            


            #region Moveto Destinatin Location
          
            #endregion
        }
        private async void CreateBatchFile(string folderpath, string[] configFileArr, string batchfilename)
        {
            string env = string.Empty;
            if (treeView1.SelectedNode != null)
            {
                try
                {
                    string rbtntext = string.Empty;
                    if (rbtn_DVT.Checked) { rbtntext = "devtest"; } else if (rbtn_STG.Checked) { rbtntext = "staging"; }
                    if (rbtn_PRD.Checked) { rbtntext = "Production"; } else if (rbtn_QAC.Checked) { rbtntext = "qac"; }
                    HealthCheck.ExecuteCS.Utility.ConfigFileCreate(folderpath, rbtntext, configFileArr, batchfilename);
                    await Task.Delay(500);
                    await IOoperation.MoveLocation(System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "\\" + "HealthCheckExec.bat", folderpath + "\\" + "HealthCheckExec.bat");
                    await Task.Delay(300);

                    using (ResXResourceSet resxSet = new ResXResourceSet(@"Release\Credential.resx"))
                    {
                        env = resxSet.GetString("ENV");

                    }

                    if (env == "DVT")
                    {
                        SchedulerOperation.CreateTaskRunOnceHealthCheck("HealthCheckExec.bat", folderpath, batchfilename);
                    }
                    else
                    {
                        SchedulerOperation.CreateTaskRunOnceWithDifferentaccountHealthCheck("HealthCheckExec.bat", folderpath);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Select Component!!!");
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            TreeNode tn = treeView1.SelectedNode;
            if (tn!=null)
            {
                string enviornment = string.Empty;
                using (ResXResourceSet resxSet = new ResXResourceSet(@"Release\Credential.resx"))
                {
                    enviornment = resxSet.GetString("ENV");
                }
                if (enviornment != "" && enviornment != "DVT")
                {
                    HealthCheck.ExecuteCS.Utility.VpnCreate();
                    ArrayList al = new ArrayList();
                    al = new Helper_Automation().GetXMLsource(Release.DefaultConfig.PathListXmlFileName, "DirectoryPath");
                    string sourepath = Convert.ToString(al[0]);
                    string DestinationLocation = "\\\\" + treeView1.SelectedNode.Text + "\\" + sourepath.Replace(Release.DefaultConfig.HomePath, "");
                    Task t = IOoperation.MoveLocation(System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "\\" + "vpnConnect" + ".bat", DestinationLocation + "\\" + "vpnConnect" + ".bat");

                    using (ResXResourceSet resxSet = new ResXResourceSet(@"Release\Credential.resx"))
                    {
                        enviornment = resxSet.GetString("ENV");
                        if (enviornment == "DVT")
                        {
                            // SchedulerOperation.CreateTaskRunOnce("execution.bat", sourepath, chk_Machine.SelectedItem.ToString());
                            SchedulerOperation.CreateTaskRunOnce("vpnConnect.bat", Convert.ToString(al[0]), treeView1.SelectedNode.Text);
                        }
                        else
                        {
                            SchedulerOperation.CreateTaskRunOnceWithDifferentaccount("vpnConnect.bat", Convert.ToString(al[0]), treeView1.SelectedNode.Text);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Enviornment should be staging or production..");
                }
            }
            else
            {
                MessageBox.Show("Select IP");
            }
        }

        private void lnk_relogin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Login obj = new Login();
            obj.ShowDialog();
        }
        private List<string> GetStatusofScheduleList(string IPaddress)
        {
            try
            {
                DataTable temp = new DataTable();
                DataSet ds2 = new DataSet();
                List<string> Schedulername = new List<string>();
                using (ds2)
                {
                    if (File.Exists(HealthCheckResource.MasterFilepath))
                    {

                        ds2.ReadXml(HealthCheckResource.MasterFilepath);
                        if (ds2 != null && ds2.Tables.Count > 0)
                        {
                            temp.Rows.Clear();
                            temp = ds2.Tables[0].Copy();
                            if (temp.Rows.Count > 0)
                            {
                                foreach (DataRow dr in temp.Select("IP='" + IPaddress + "'").CopyToDataTable().Rows)
                                {
                                    Schedulername.Add(dr.IsNull("SchdulerName") ? string.Empty : Convert.ToString(dr["SchdulerName"]) + ".bat");
                                }
                            }
                        }
                    }
                    temp.Dispose();
                }
                return Schedulername.Count > 0 ? Schedulername : null;
            }catch(Exception ex)
            {

            }
            return null;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ReloadList();
        }
    }
}
