using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Xml;

namespace ic_req_helper
{
    internal class File
    {
        readonly static List<Tuple<string, string, string>> compList = new List<Tuple<string, string, string>>();
        readonly static string[] paths = new string[6];

        public static void ProcessXML(string text)
        {
            // Parse input XML text

            XmlDocument xmlField = new XmlDocument();

            try
            {
                xmlField.LoadXml(text);

                XmlNodeList nodeList;
                XmlNode root1 = xmlField.DocumentElement;

                nodeList = root1.SelectNodes("//item");

                foreach (XmlNode item in nodeList)
                {
                    string component = item.Attributes["component"].Value;

                    int index = component.IndexOf("/");

                    string packageName = component.Substring(0, index).Replace("ComponentInfo{", "");
                    string activity = component.Substring(index + 1).Replace("ComponentInfo{", "").Replace("}", "");
                    string appName = item.Attributes["drawable"].Value;

                    compList.Add(Tuple.Create(packageName, activity, appName));
                }
            } catch (XmlException)
            {
                MessageBox.Show("Please use proper XML format, try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            // Process XML text
            XmlDocument[] xmlFiles = new XmlDocument[6]; // Index 1 is never use
            XmlNode[] xmlRoots = new XmlNode[6];

            // appfilter.xml (2)
            xmlFiles[0] = new XmlDocument();

            xmlFiles[0].Load(paths[0]);

            xmlRoots[0] = xmlFiles[0].DocumentElement;

            foreach (var i in compList)
            {
                XmlElement element = xmlFiles[0].CreateElement("item");
                element.SetAttribute("component", "ComponentInfo{" + i.Item1 + "/" + i.Item2 + "}");
                element.SetAttribute("drawable", i.Item3);
                xmlRoots[0].InsertAfter(element, xmlRoots[0].LastChild);

                // Add comment to each node
                TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
                XmlComment comment = xmlFiles[0].CreateComment(" " + textInfo.ToTitleCase(i.Item3.Replace("__", "").Replace("_", " ")) + " ");
                xmlRoots[0].InsertBefore(comment, element);
            }

            // Todo: Implement foreach statement
            xmlFiles[0].Save(paths[0]);
            System.IO.File.Copy(paths[0], paths[1], true);

            // appmap.xml
            xmlFiles[2] = new XmlDocument();

            xmlFiles[2].Load(paths[2]);

            xmlRoots[2] = xmlFiles[2].DocumentElement;

            foreach (var i in compList)
            {
                XmlElement element = xmlFiles[2].CreateElement("item");
                element.SetAttribute("class", i.Item2);
                element.SetAttribute("drawable", i.Item3);
                xmlRoots[2].InsertAfter(element, xmlRoots[2].LastChild);

                // Add comment to each node
                TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
                XmlComment comment = xmlFiles[2].CreateComment(" " + textInfo.ToTitleCase(i.Item3.Replace("__", "").Replace("_", " ")) + " ");
                xmlRoots[2].InsertBefore(comment, element);
            }

            xmlFiles[2].Save(paths[2]);

            MessageBox.Show("DONE!"); // Debug message box
        }
    }
}
