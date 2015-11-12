using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace MovieOrganiser.Helpers
{
    public enum SettingsPersistance { LocalXmlFile, Registry, AppConfig }
    public class SettingsManager
    {
        private SettingsPersistance _persistance;
        private string _localXmlFile;

        public SettingsManager(SettingsPersistance persistance)
        {
            this._persistance = persistance;
            
            if ( _persistance == SettingsPersistance.LocalXmlFile)
            {
                _localXmlFile = AssemblyPath + ".config.xml";
            }
        }

        public string MovieFolder {
            get {
                return read("MovieFolder");
            }
            set {
                write("MovieFolder", value);
            }
        }

        private void write(string setting, string value)
        {
            switch (_persistance)
            {
                case SettingsPersistance.Registry:
                    writeRegistryKey(setting, value);
                    break;
                    
                case SettingsPersistance.LocalXmlFile:
                    writeSettingToLocalXMLFile(setting, value);
                    break;

                default:
                    break;                    
            }
        }

        private string read(string setting)
        {
            return read(setting, null);
        }

        private string read(string setting, string defaultvalue = null)
        {
            switch (_persistance)
            {
                case SettingsPersistance.Registry:
                    return readRegistryKey(setting, defaultvalue);
                    break;

                case SettingsPersistance.LocalXmlFile:
                    return readSettingFromLocalXMLFile(setting, defaultvalue);
                    break;

                default:
                    return defaultvalue;
                    break;
            }
        }


        private string readSettingFromLocalXMLFile(string keyname, string defaultvalue)
        {
            // open the movie file
            XmlDocument xmldoc = new XmlDocument();
            FileStream fs = new FileStream(_localXmlFile, FileMode.Open, FileAccess.Read);
            xmldoc.Load(fs);

            // take the first node (usually "Settings")
            XmlNode topNode = xmldoc.SelectSingleNode("Settings");

            // look for an item with the name of the settings
            XmlNode setting = topNode.SelectSingleNode(keyname);
            
            if ( setting == null )
            {
                if (defaultvalue == null) return null;
                else return defaultvalue;
            }
            else
            {
                return setting.InnerText;
            }
            
        }

        private void writeSettingToLocalXMLFile(string setting, string value)
        {
            XmlDocument xmldoc = new XmlDocument();
            XmlNode topNode;

            // make sure the XML file exist and has a <Settings> root node
            if ( File.Exists(_localXmlFile) == false )
            {
                // create a new settings file with declaration line and create first "settings" node
                XmlNode docNode = xmldoc.CreateXmlDeclaration("1.0", "UTF-8", null);
                xmldoc.AppendChild(docNode);

                topNode = xmldoc.CreateNode(XmlNodeType.Element, "Settings", "");
                xmldoc.AppendChild(topNode);
            }
            else
            {
                // open existing file
                FileStream fs = new FileStream(_localXmlFile, FileMode.Open, FileAccess.Read);
                xmldoc.Load(fs);
                fs.Close();

                // take the first node ("Settings")
                topNode = xmldoc.SelectSingleNode("Settings");
                if (topNode == null)
                {
                    topNode = xmldoc.CreateNode(XmlNodeType.Element, "Settings", "");
                    xmldoc.AppendChild(topNode);
                }
            }

            // look if a node for the setting exist, or create it
            XmlNode settingNode = topNode.SelectSingleNode(setting);

            if (settingNode == null)
            {
                settingNode = xmldoc.CreateNode(XmlNodeType.Element, setting, "");
                settingNode.InnerText = value;
                topNode.AppendChild(settingNode);
            }
            else
            {
                settingNode.InnerText = value;
            }

            // now write the updated settings 
            using (FileStream fs = new FileStream(_localXmlFile, FileMode.Create))
            {
                xmldoc.Save(fs);
                fs.Flush();
                fs.Close();
            }

        }

        private string readRegistryKey(string keyname, string defaultvalue)
        {
            Microsoft.Win32.RegistryKey key;
            try
            {
                key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software", true).CreateSubKey("MovieOrganiser");
                var val = key.GetValue(keyname);

                if ( val == null)
                {
                    // key does not exist. return the default value
                    return defaultvalue; 
                }
                    
                key.Close();
                return val.ToString();
            }
            catch (Exception ex)
            {
                return defaultvalue;
            }
        }

        private void writeRegistryKey(string keyname, string value)
        {
            Microsoft.Win32.RegistryKey key;
            try
            {
                key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software", true).CreateSubKey("MovieOrganiser");

                key.SetValue(keyname, value);
                key.Close();
            }
            catch (Exception ex)
            {
                string msg = "Er ging iets fout tijdens het wegschrijven van een instelling naar de Windows Registry." + 
                    Environment.NewLine + "De instelling is niet opgeslagen. Foutmelding: " + 
                    Environment.NewLine + ex.Message;
                MessageBox.Show(msg, "Fout", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static string AssemblyPath
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetFullPath(path);
            }
        }
    }
}
