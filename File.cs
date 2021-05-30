﻿using System;
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
        readonly public static string[] paths = new string[6];

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

            int totalFiles = 5;

            /* 
             * 0. appfilter.xml
             * 1. appmap.xml
             * 2. theme_resources.xml
             * 3. drawable.xml
             * 4. icon_pack.xml
             */

            XmlDocument[] xmlFiles = new XmlDocument[totalFiles];
            XmlNode[] xmlRoots = new XmlNode[totalFiles];
            
            for (int i = 0; i < totalFiles; i++)
            {
                xmlFiles[i] = new XmlDocument();
                
                if (i == 0)
                {
                    xmlFiles[0].Load(paths[0]);
                }
                else
                {
                    xmlFiles[i].Load(paths[i + 1]);
                }

                xmlRoots[i] = xmlFiles[i].DocumentElement;

                foreach (var j in compList)
                {
                    XmlElement element;

                    if (i == 2)
                    {
                        element = xmlFiles[i].CreateElement("AppIcon");
                    } else
                    {
                        element = xmlFiles[i].CreateElement("item");
                    }

                    switch (i)
                    {
                        case 0:
                            element.SetAttribute("component", "ComponentInfo{" + j.Item1 + "/" + j.Item2 + "}");
                            element.SetAttribute("drawable", j.Item3);
                            break;
                        case 1:
                            element.SetAttribute("class", j.Item2);
                            element.SetAttribute("drawable", j.Item3);
                            break;
                        case 2:
                            element.SetAttribute("name", j.Item1 + "/" + j.Item2);
                            element.SetAttribute("image", j.Item3);
                            break;
                        case 3:
                            element.SetAttribute("drawable", j.Item3);
                            break;
                        case 4:
                            element.InnerText = j.Item3;
                            break;
                    }

                    switch (i)
                    {
                        case 0:
                        case 1:
                        case 2:
                            xmlRoots[i].InsertAfter(element, xmlRoots[i].LastChild);
                            break;

                        case 3:
                            XmlNode node1 = xmlFiles[i].SelectSingleNode("//category[@title='All']");
                            xmlRoots[i].InsertAfter(element, node1);
                            break;

                        case 4:
                            XmlNode node2 = xmlFiles[i].SelectSingleNode("//string-array[@name='all']");
                            node2.AppendChild(element);
                            break;
                    }

                    if (i < 3)
                    {
                        // Add comment to each node

                        TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
                        XmlComment comment = xmlFiles[i].CreateComment(" " + textInfo.ToTitleCase(j.Item3.Replace("__", "").Replace("_", " ")) + " ");
                        xmlRoots[i].InsertBefore(comment, element);
                    }
                }

                if (i == 0)
                {
                    xmlFiles[0].Save(paths[0]);
                    System.IO.File.Copy(paths[0], paths[1], true);
                }
                else
                {
                    xmlFiles[i].Save(paths[i + 1]);
                }
            }

            MessageBox.Show("All files has been successfully updated.", "Success", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }
    }
}
