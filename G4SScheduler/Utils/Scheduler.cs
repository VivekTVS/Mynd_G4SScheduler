using Ionic.Zip;
using Quartz;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Renci.SshNet;
using System.Web.Hosting;
using System.Text;
using ClosedXML.Excel;

namespace G4SScheduler.Utils
{
    public class Scheduler : Controller, IJob
    {

        //public string GetTaskFileForDownload1(int CountryIds, string DepartmentIds, int Month, int Year, int CustID, int CompID, int SiteID, int uid, string SessionID)
        //{
        //    Response res = new Response();
        //    DataTable dt = new DataTable();
        //    try
        //    {
        //        SqlParameter[] parameters = new SqlParameter[]
        //        { new SqlParameter("@Paydate", uid),
        //           };
        //        dt = DataLib.ExecuteDataTable("[GetSFTPDATA]", CommandType.StoredProcedure, parameters);
        //        if (!(Directory.Exists(System.Web.HttpContext.Current.Server.MapPath("~/Docs/TaskFileDownload"))))
        //        { Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath("~/Docs") + "\\TaskFileDownload"); }
        //        if (dt.Rows.Count > 0)
        //        {


        //            string password = "";
        //            string path = System.Web.HttpContext.Current.Server.MapPath("~/Docs/TaskFileDownload");
        //            //if ((Directory.Exists(path + "\\" + SessionID)))
        //            //{ Directory.Delete(path + "\\" + SessionID, true); }
        //            //Directory.CreateDirectory(path + "\\" + SessionID);
        //            //if ((Directory.Exists(path + "\\" + SessionID + "\\" + Month + "-" + Year)))
        //            //{ Directory.Delete(path + "\\" + SessionID + "\\" + Month + "-" + Year, true); }
        //            //Directory.CreateDirectory(path + "\\" + SessionID + "\\" + Month + "-" + Year);
        //            DataTable dtCustomers = dt.DefaultView.ToTable("Customer", true, "Customer");

        //            foreach (DataRow siterow in dtSiteData.Rows)
        //            {

        //                string filepath = "";
        //                string filenameDownload = "";
        //                string filename = siterow["Name"].ToString();

        //                //else
        //                //{

        //                filepath = System.Web.HttpContext.Current.Server.MapPath("~/Docs/Task/") + filename;
        //                filenameDownload = siterow["Desc"].ToString();
        //                string FileNameWithoutext = Path.GetFileNameWithoutExtension(filenameDownload);
        //                string Extenction = Path.GetExtension(filenameDownload);
        //                //  string CopyPath = System.Web.HttpContext.Current.Server.MapPath("~/Docs/TaskFileDownload/") + SessionID + Month + "-" + Year + "\\" + row["Customer"] + "\\" + custrow["Company"] + "\\" + comprow["Site"] + "\\" + filename;
        //                string CopyPath = System.Web.HttpContext.Current.Server.MapPath("~/Docs/TaskFileDownload/") + SessionID + "\\" + Month + "-" + Year + "\\" + row["Customer"] + "\\" + custrow["Company"] + "\\" + comprow["Site"] + "\\" + filenameDownload;
        //                //if (CopyPath.Length >= 248)
        //                //{
        //                //    int fixedlenght = 248;
        //                //    int pathlenght = CopyPath.Length;
        //                //    int sublenght = pathlenght - fixedlenght;
        //                //    string Subfilename = FileNameWithoutext.Remove(0, sublenght);
        //                //    CopyPath = System.Web.HttpContext.Current.Server.MapPath("~/Docs/TaskFileDownload/") + SessionID + "\\" + Month + "-" + Year + "\\" + row["Customer"] + "\\" + custrow["Company"] + "\\" + comprow["Site"] + "\\" + Subfilename + "_" + F_count + Extenction;
        //                //}
        //                //else
        //                //{
        //                CopyPath = System.Web.HttpContext.Current.Server.MapPath("~/Docs/TaskFileDownload/") + SessionID + "\\" + Month + "-" + Year + "\\" + row["Customer"] + "\\" + custrow["Company"] + "\\" + comprow["Site"] + "\\" + FileNameWithoutext + "_" + F_count + Extenction;
        //                //  }

        //                if (System.IO.File.Exists(CopyPath))
        //                    System.IO.File.Delete(CopyPath);
        //                if (System.IO.File.Exists(filepath))
        //                {
        //                    System.IO.File.Copy(filepath, CopyPath);
        //                }

        //              //  F_count++;
        //                //}
        //            }

        //            res.Data = password;
        //            res.IsSuccess = true;
        //            res.Message = "Files Generated";


        //        }
        //        else
        //        {
        //            res.IsSuccess = false;
        //            res.Message = "file not found for this filter criteria.";
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        res.Data = "";
        //        res.IsSuccess = false;
        //        res.Message = "Files Generation Failed";
        //    }



        //    return res;



        //}
        public string CreateFolder(string path)
        {
            // System.IO.Path.GetFullPath("D:\Reports\" + Session("comp") + "\") + path
            // string ff1 = HttpContext.Current.Server.MapPath("~/ComReports/" + Session("comp") + "/") + path;
            string ff = HostingEnvironment.MapPath("~/Docs/TaskFileDownload/") + path;
            if (!Directory.Exists(ff))
                Directory.CreateDirectory(ff);
            return ff;
        }

        public string TransferG4SFiletest()
        {
            //  Response res = new Response();
            DateTime Paydate = DateTime.Today.AddMonths(0);
            DateTime LDofPM = Paydate.AddDays(0 - Paydate.Day);
            DataTable dt = new DataTable();
           
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                { new SqlParameter("@Paydate", LDofPM),
                   };
                dt = DataLib.ExecuteDataTable("[GetCMDSFTP]", CommandType.StoredProcedure, parameters);
               // if (!(Directory.Exists(HostingEnvironment.MapPath("~/Docs/TaskFileDownload"))))
                    // { Directory.CreateDirectory(HostingEnvironment.MapPath("~/Docs") + "\\TaskFileDownload"); }
                    if (!Directory.Exists(HostingEnvironment.MapPath("~/Docs/TaskFileDownload")))
                    { Directory.CreateDirectory(HostingEnvironment.MapPath("~/Docs/TaskFileDownload")); }
                if (dt.Rows.Count > 0)
                {


                    string password = "";
                    string path = HostingEnvironment.MapPath("~/Docs/TaskFileDownload");
                    string FindString = "D:\\G4SACT_SFTP_DOCUMENTS";
                   // string testing = "E:\\G4S LiveProject\\PCT12";
                  //  string Findtesting = "D:\\Websites\\Load_Balancer\\G4SACT";
                  

                    //string output = input.Replace("old_value", "new_value");
                    foreach (DataRow siterow in dt.Rows)
                    {
                        try
                        {
                            var output = string.Empty;
                        string QueryString = siterow["Path"].ToString();
                        QueryString = QueryString.Replace(FindString, path);
                      //  QueryString = QueryString.Replace(Findtesting, testing);//testing code
                     string   parentDirectory = Path.GetDirectoryName(siterow["sftp_file_path"].ToString());
                        string LocalPath = path + "\\" + siterow["sftp_file_path"].ToString();
                        CreateFolder(parentDirectory);
                        int TId = Convert.ToInt32(siterow["tid"]);
                        //   string filename = siterow["Name"].ToString();
                        if (System.IO.File.Exists(LocalPath))
                            System.IO.File.Delete(LocalPath);


                        //filepath = System.Web.HttpContext.Current.Server.MapPath("~/Docs/Task/") + filename;
                        //filenameDownload = siterow["Desc"].ToString();
                        //string FileNameWithoutext = Path.GetFileNameWithoutExtension(filenameDownload);
                        //string Extenction = Path.GetExtension(filenameDownload);
                        System.Diagnostics.Process process = new System.Diagnostics.Process();
                        System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                        startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                        startInfo.FileName = "cmd.exe";
                        startInfo.Arguments = "/C " + QueryString  ;
                        process.StartInfo = startInfo;
                        process.StartInfo.UseShellExecute = false;
                        process.StartInfo.RedirectStandardOutput = true;
                        var proc = process.Start();



                            //if (readOutput)
                            //{
                            //    // output = proc.StandardOutput.ReadToEnd();
                            //    output = process.StandardOutput.ReadToEnd();
                            //}

                            //       process.WaitForExit(60000);

                            while (!process.StandardOutput.EndOfStream)
                            {
                                string line = process.StandardOutput.ReadLine();
                                if (line == "1 File(s) copied")
                                {
                                    UPdateSFTPTID(TId,LDofPM);
                                }
                            }
                            //* Read the output (or the error)
                            //  string output1 = process.StandardOutput.ReadToEnd();
                            //  //Console.WriteLine(output1);
                            //  string err = process.StandardError.ReadToEnd();
                            ////  Console.WriteLine(err);
                            process.WaitForExit();

                            //if (System.IO.File.Exists(CopyPath))
                            //    System.IO.File.Delete(CopyPath);
                            //if (System.IO.File.Exists(filepath))
                            //{
                            //    System.IO.File.Copy(filepath, CopyPath);
                            //}
                        }
                        catch (Exception Ex)
                        {
                            continue;
                        }

                    }

                    SFTPExcelLog();

                }
                else
                {
                    //res.IsSuccess = false;
                    //res.Message = "file not found for this filter criteria.";
                }

            }
            catch (Exception ex)
            {
             string s=   ex.Message;
                
                //res.IsSuccess = false;
                //res.Message = "Files Generation Failed";
            }



            return "";



        }

        public string TransferG4SFile()
        {
            //  Response res = new Response();
            DateTime Paydate = DateTime.Today.AddMonths(0);
            DateTime LDofPM = Paydate.AddDays(0 - Paydate.Day);
            DataTable dt = new DataTable();

            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                { new SqlParameter("@Paydate", LDofPM),
                   };
                dt = DataLib.ExecuteDataTable("[GetCMDSFTP]", CommandType.StoredProcedure, parameters);
                // if (!(Directory.Exists(HostingEnvironment.MapPath("~/Docs/TaskFileDownload"))))
                // { Directory.CreateDirectory(HostingEnvironment.MapPath("~/Docs") + "\\TaskFileDownload"); }
                if (!Directory.Exists(HostingEnvironment.MapPath("~/Docs/TaskFileDownload")))
                { Directory.CreateDirectory(HostingEnvironment.MapPath("~/Docs/TaskFileDownload")); }
                if (dt.Rows.Count > 0)
                {


                    string password = "";
                    string path = HostingEnvironment.MapPath("~/Docs/TaskFileDownload");
                    string FindString = "D:\\G4SACT_SFTP_DOCUMENTS";
                   //  string testing = "E:\\G4S LiveProject\\PCT12";
                    //  string Findtesting = "D:\\Websites\\Load_Balancer\\G4SACT";


                    //string output = input.Replace("old_value", "new_value");
                    foreach (DataRow siterow in dt.Rows)
                    {
                        try
                        {
                            var output = string.Empty;
                            string QueryString = siterow["Path"].ToString();
                          //  QueryString = QueryString.Replace(FindString, path);
                          //   QueryString = QueryString.Replace(Findtesting, testing);//testing code
                            string parentDirectory = Path.GetDirectoryName(siterow["sftp_file_path"].ToString());
                            string LocalPath = path + "\\" + siterow["sftp_file_path"].ToString();
                            CreateFolder(parentDirectory);
                            string Source = siterow["original_file_path"].ToString();
                         //   Source = Source.Replace(Findtesting, testing);//testing code
                            int TId = Convert.ToInt32(siterow["tid"]);
                            //   string filename = siterow["Name"].ToString();
                            if (System.IO.File.Exists(LocalPath))
                                System.IO.File.Delete(LocalPath);
                            if (System.IO.File.Exists(Source))
                            {
                                System.IO.File.Copy(Source, LocalPath);
                             //   UPdateSFTPTID(FileID, 1);
                               UPdateSFTPTID(TId, LDofPM);
                            }

                           



                          
 
                        }
                        catch (Exception Ex)
                        {
                            continue;
                        }

                    }

                    SFTPExcelLog();

                }
                else
                {
                    //res.IsSuccess = false;
                    //res.Message = "file not found for this filter criteria.";
                }

            }
            catch (Exception ex)
            {
                string s = ex.Message;

                //res.IsSuccess = false;
                //res.Message = "Files Generation Failed";
            }



            return "";



        }

        public void SFTPExcelLog()
        {
            DateTime Paydate = DateTime.Today.AddMonths(-1);
            string Month = Paydate.ToString("MMMM");
            string Year = "2023";
            DateTime Paydate1 = DateTime.Today.AddMonths(0);
            DateTime LDofPM = Paydate1.AddDays(0 - Paydate1.Day);
            DataSet DS = new DataSet();
            DataTable SFTPdt = new DataTable();
            SqlParameter[] parameters = new SqlParameter[]
               { new SqlParameter("@Paydate", LDofPM),
                  };
            DS = DataLib.ExecuteDataSet("[GetSFTPExcellog]", CommandType.StoredProcedure, parameters);
            SFTPdt=DS.Tables[0];
            DataTable custTable = new DataTable("log");
            
            // DataColumn dc = new DataColumn("S.No", typeof(String));
            custTable.Columns.Add("S.No", typeof(String));
            custTable.Columns.Add("sftp_file_path", typeof(String));
            custTable.Columns.Add("Company_Code", typeof(String));
            custTable.Columns.Add("Site_Code", typeof(String));
            custTable.Columns.Add("location_code", typeof(String));
            if (SFTPdt.Rows.Count > 0)
            {
                try
                {
                    string FileName = "SFTPlogReport" + Month + ".xlsx";
                    // string FilePath = HostingEnvironment.MapPath("~/Docs/Temp/") + FileName;
                    string FilePath = HostingEnvironment.MapPath("~/Docs/TaskFileDownload/G4FLKGRD") + "\\" + Year + "\\" + Month + "\\" + FileName;
                    if (System.IO.File.Exists(FilePath))
                        System.IO.File.Delete(FilePath);
                    DataRow row;
                    foreach (DataRow siterow in SFTPdt.Rows)
                    {
                        string sftppath = siterow["sftp_file_path"].ToString();
                        row = custTable.NewRow();
                        // Split authors separated by a comma followed by space  
                        string[] sftpArraypath = sftppath.Split('\\');
                        string company = "";
                        string Site = "";
                        string loction = "";
                        loction= sftpArraypath[3].ToString();
                        company = sftpArraypath[4].ToString();
                        Site = sftpArraypath[5].ToString();
                        row["S.No"] = siterow["SNo."].ToString();
                        row["sftp_file_path"] = siterow["sftp_file_path"].ToString();
                        row["Company_Code"] = company;
                        row["location_code"] = loction;
                        row["Site_Code"] = Site;
                        custTable.Rows.Add(row);
                    }
                    if (DS.Tables[1].Rows.Count > 0)
                    {
                        row = custTable.NewRow();
                        row["sftp_file_path"]= DS.Tables[1].Rows[0]["TOTAL"].ToString(); 
                        custTable.Rows.Add(row);
                    }
                    DataTableToExcel(custTable, FilePath);
                    SendMails(FilePath);
                }
                catch { throw; }
            }
        }

        public void SendMails(string Path)
        {
            try
            {
                string Bcc = "vivek.singh@myndsol.com,amarpal.singh@myndsol.com,vineet.kumar@myndsol.com ";
                string FileName = Path;
                    StringBuilder mailbody = new StringBuilder("");
                    mailbody.Append(" <body><header style=\"width:996px; margin:auto;\"><div style=\"font-family:Arial, Helvetica, sans-serif; color:#444; font-size:18px;font-weight:bold;\"> ");
                    mailbody.Append(" Dear User,       </div></header><section style=\"width:996px; margin:auto;\"><div style=\"color:#333;font-size:14px;font-family:Arial, Helvetica, sans-serif;\"><p>Files transfer in SFTP folder structure format log and not transfered files count written in last row of excel  .");
                    mailbody.Append("</p></div> ");
                    mailbody.Append(" </section> <footer style=\"width:996px; margin:auto;\"> <div style=\"color:#333;font-size:14px;font-family:Arial, Helvetica, sans-serif;padding:5px 0px;\">Thanks & Regards <br> Team IT</div> ");
                    mailbody.Append(" </footer></body>");
                    MailUtill.SendMail("vipin.sharma@myndsol.com,", "SFTP File transfer log ", mailbody.ToString(), "", FileName, Bcc);

                
            }
            catch { }
            // dtRecords.DefaultView

        }
        public static string DataTableToCSV(DataTable dt, string seperator,string Path)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                IEnumerable<string> columnNames = dt.Columns.Cast<DataColumn>().
                                  Select(column => column.ColumnName);
                sb.AppendLine(string.Join(seperator, columnNames));
                foreach (DataRow row in dt.Rows)
                {
                    IEnumerable<string> fields = row.ItemArray.Select(field =>
                     string.Concat("\"", field.ToString().Replace("\"", "\"\"").Replace("=", "'=").Replace("+", "'+"), "\"").Replace("-", "'-"));
                    sb.AppendLine(string.Join(seperator, fields));
                }
            //    string FilePath = HostingEnvironment.MapPath("~/Docs/Temp/") + DateTime.Now.Year.ToString() + DateTime.Now.Month + DateTime.Now.Date.DayOfYear + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second + DateTime.Now.Millisecond + ".csv";
                System.IO.File.WriteAllText(Path, sb.ToString());
                return Path;
            }
            catch
            {
                throw new Exception("DataTableToCSV");
            }
        }

        public static string DataTableToExcel(DataTable dt, string Path)
        {
            try
            {
                
                string FilePath = Path;
                if (System.IO.File.Exists(Path))
                    System.IO.File.Delete(Path);
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt, "Result");
                    wb.SaveAs(FilePath);
                }
                return FilePath;
            }
            catch { throw; }
        }
        public int UPdateSFTPTID(int TID,DateTime Paydate)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@SFTPId", TID),
                     new SqlParameter("@paydate", Paydate),
                };
                return Convert.ToInt32(DataLib.ExecuteScaler("UpdateSFTPTIDnew", CommandType.StoredProcedure, parameters));
            }
            catch
            {
                throw;
            }
        }
        public string CreateZipfile()
        {
            
        //    DateTime Paydate = DateTime.Now;
            DateTime Paydate = DateTime.Today.AddMonths(-1);
            var firstDayOfMonth = Paydate;
            string Month = firstDayOfMonth.ToString("MMMM");
            int year = firstDayOfMonth.Year;
            string   ZipPath = "";
            string OriginalZipPath = "";
            try
            {

                if (year > 1900)
                {
                    //ZipPath = System.Web.HttpContext.Current.Server.MapPath("~/Docs/TaskFileDownload");
                    ZipPath = HostingEnvironment.MapPath("~/Docs/TaskFileDownload");
                    //if (System.IO.File.Exists(ZipPath + "\\G4FLKGRD" + "\\" + year + "\\" + Month + ".zip"))
                    //{ System.IO.File.Delete(ZipPath + "\\G4FLKGRD" + "\\" + year + "\\" + Month + ".zip"); }

                    ZipFile zipfile = new ZipFile();
                    //  OriginalZipPath = ZipPath + "\\G4FLKGRD" + "\\2021\\December";
                    OriginalZipPath = ZipPath + "\\G4FLKGRD" + "\\" + year + "\\" + Month;
                    if (Directory.Exists(OriginalZipPath))
                    {
                        // zipfile.AddDirectory(ZipPath + SessionID);//, ZipPath + SessionID  + ".zip");

                        //  zipfile.Save(ZipPath   + Month + "-" + Year + ".zip");

                        //string[] Filenames = Directory.GetDirectoryRoot(ZipPath + SessionID); //Directory.GetFiles(ZipPath);
                        //   string[] Filenames = Directory.GetFiles(ZipPath, SessionID + Month + "-" + Year + ".zip"); //Directory.GetDirectories(ZipPath, SessionID);





                        //Filenames = Directory.GetDirectoryRoot(ZipPath + SessionID)
                        using (ZipFile zip = new ZipFile())
                    {
                            // zip.Password = ret.Data;
                            //    zip.Encryption = EncryptionAlgorithm.PkzipWeak;
                            // zip.AddFiles(OriginalZipPath);//Zip file inside filename 
                            zip.ParallelDeflateThreshold = 0;
                            zip.UseZip64WhenSaving = Zip64Option.Always;
                            zip.MaxOutputSegmentSize = 1024 * 1024 * 1024;
                            zip.AddDirectory(OriginalZipPath);
                        zip.Save(OriginalZipPath + ".zip");//location and name for creating zip file  
                    }
                    //byte[] fileBytes = System.IO.File.ReadAllBytes(ZipPath + SessionID + ".zip");
                    //System.IO.File.Delete(ZipPath + SessionID + ".zip");
                    //System.IO.File.Delete(ZipPath + SessionID + Month + "-" + Year + ".zip");
                    //System.IO.Directory.Delete(ZipPath + SessionID, true);
                    //return File(fileBytes, "application/zip", SessionID + Month + "-" + Year + ".zip");
                    return "Zip process done for month " + Month + "_" + year;
                }
                    return "File not exist.";
                }
                else
                {
                    return "Zip process not done for month " + Month + "_" + year;
                }
            }
            catch (Exception ex)
            {
                if (System.IO.File.Exists(OriginalZipPath + ".zip"))
                {
                    System.IO.File.Delete(OriginalZipPath + ".zip");
                }
                //if (System.IO.File.Exists(ZipPath + SessionID + Month + "-" + Year + ".zip"))
                //{
                //    System.IO.File.Delete(ZipPath + SessionID + Month + "-" + Year + ".zip");
                //}
                //if (System.IO.Directory.Exists(ZipPath + SessionID))
                //{
                //    System.IO.Directory.Delete(ZipPath + SessionID, true);
                //}


                return "Zip process not done for month " + Month + "_" + year;
            }
          
        }



      


        public string CopyToSFTP(string G4SSource)
        {

            // Dim date_ As DateTime = DateTime.Parse(paydate)
            DateTime Paydate = DateTime.Today.AddMonths(-1);
            var firstDayOfMonth = Paydate;
            string Month = firstDayOfMonth.ToString("MMMM");
            int year = firstDayOfMonth.Year;

            try
            {
                string DirName = "";// Path.GetDirectoryName(G4SSource);
                 //   DirName = System.Web.HttpContext.Current.Server.MapPath("~/Docs/TaskFileDownload/G4FLKGRD/2021/December");
               DirName = HostingEnvironment.MapPath("~/Docs/TaskFileDownload/G4FLKGRD")+"\\"+year+"\\"+Month;
                DirName = DirName+ ".zip";
                string SftpDir = "G4FLKGRD\\" + year;
                if (System.IO.File.Exists(DirName))
                {
                    //   string MapPathName = System.Web.Hosting.HostingEnvironment.MapPath("~/" + items[0] + "/" + items[1] + "");
                    string source = "";
                    //   if (tokens.Length > 1)
                    //source = MapPathName + tokens[1];
                    source = DirName;



                    // string destination = Path.GetDirectoryName(filepath);
                    string host = "sftp2.myndsolution.com";
                    string username = "mynd_g4s";
                    string password = "M!n$1221";
                    int port = 22;

                    using (SftpClient client = new SftpClient(host, port, username, password))
                    {
                        // client.Connect()

                        int _connectiontRetryAttempts = 10;
                        int attempts = 0;
                        if (client.IsConnected)
                        {
                        }
                        else
                            do
                            {
                                try
                                {
                                    client.Connect();
                                }
                                catch (Renci.SshNet.Common.SshConnectionException e)
                                {
                                    attempts += 1;
                                }
                            }
                            while (attempts < _connectiontRetryAttempts && !client.IsConnected);

                         CreateServerDirectoryIfItDoesntExist(SftpDir, client);

                        using (FileStream fs = new FileStream(source, FileMode.Open))
                        {
                            client.BufferSize = 4 * 1024;
                            client.UploadFile(fs, Path.GetFileName(source), true);
                        }
                    }
                    return "File CopyToSFTP Success.";

                }
                else
                {
                    return "Zip File not exist.";
                }
            }
            catch (Exception ex)
            {
                return   ex.Message;
            }
        }

        private void CreateServerDirectoryIfItDoesntExist(string serverDestinationPath, SftpClient sftpClient)
        {
            if (serverDestinationPath[0] == '\\')
                serverDestinationPath = serverDestinationPath.Substring(1);
            string[] directories = serverDestinationPath.Split('\\');

            for (int i = 0; i <= directories.Length - 1; i++)
            {
                string dirName = directories[i];
                if (!sftpClient.Exists(dirName))
                    sftpClient.CreateDirectory(dirName);
                sftpClient.ChangeDirectory(dirName);
            }
        }
        public void Execute(IJobExecutionContext context)
        {

            //Check the server Name
            string ServerName = Environment.MachineName;
            try
            {
             //   CreateZipfile();

            }
            catch { }


        }
    }
}