using System;
using System.Windows;
using System.Xml;

namespace ic_req_helper
{
    class File
    {
        public static void ParseTextData(string text)
        {
            // Todo: add text field exception

            XmlDocument xml = new XmlDocument();
            xml.LoadXml(text);

            XmlNodeList nodeList;
            XmlNode root = xml.DocumentElement;

            nodeList = root.SelectNodes("//item");

            foreach (XmlNode item in nodeList)
            {
                string component = item.Attributes["component"].Value;

                int index = component.IndexOf("/");

                string packageName = component.Substring(0, index).Replace("ComponentInfo{", "");
                string activity = component.Substring(index + 1).Replace("ComponentInfo{", "").Replace("}", "");
                string appName = item.Attributes["drawable"].Value;

                MessageBox.Show(packageName + "\n" + activity + "\n" + appName);
            }
        }
    }
}
