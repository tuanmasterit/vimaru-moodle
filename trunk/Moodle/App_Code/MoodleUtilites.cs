﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Xml;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;

namespace Moodle
{
    public class MoodleUtilites
    {
        /// <summary>
        /// Write text to a file
        /// </summary>
        /// <param name="filePath">file path</param>
        /// <param name="text">text to write</param>
        public static void WriteTextToFile(string filePath, string text)
        {
            StreamWriter file = new StreamWriter(filePath);
            file.WriteLine(text);
            file.Close();
        }
        /// <summary>
        /// Read text from file
        /// </summary>
        /// <param name="filePath">file path</param>
        /// <returns>List line of file</returns>
        public static List<string> ReadTextFile(string filePath)
        {
            List<string> list = new List<string>();
            StreamReader file = new StreamReader(filePath);

            while (!file.EndOfStream)
            {
                list.Add(file.ReadLine());
            }

            file.Close();

            return list;
        }
        /// <summary>
        /// Get Service Table
        /// </summary>
        /// <param name="filePath">file path</param>
        /// <returns>DataTable</returns>
        public static DataTable GetServiceTable(string filePath)
        {
            DataTable rs = new DataTable();
            rs.Columns.Add("FullName");
            rs.Columns.Add("ShortName");
            StreamReader file = new StreamReader(filePath);
            
            while (!file.EndOfStream)
            {
                string[] s = file.ReadLine().Split(new char[] { '-' });
                DataRow row = rs.NewRow();
                row["FullName"] = s[0];
                row["ShortName"] = s[1];
                rs.Rows.Add(row);
            }

            file.Close();

            return rs;
        }
        /// <summary>
        /// Method for transform all XmlNode from XmlDocument to a TreeView
        /// </summary>
        /// <param name="xmlNode">a XmlNode</param>
        /// <param name="treeNode">a TreeNode</param>
        public static void AddNode(XmlNode xmlNode, TreeNode treeNode)
        {
            XmlNode xNode;
            TreeNode tNode;
            XmlNodeList nodeList;
            int i = 0;
            if (xmlNode.HasChildNodes)
            {
                nodeList = xmlNode.ChildNodes;
                for (i = 0; i <= nodeList.Count - 1; i++)
                {
                    xNode = xmlNode.ChildNodes[i];
                    try
                    {
                        treeNode.ChildNodes.Add(new TreeNode(xNode.Attributes["name"].Value.ToString()));
                    }
                    catch //(System.Exception ex)
                    {
                        treeNode.ChildNodes.Add(new TreeNode(xNode.Name));
                    }

                    tNode = treeNode.ChildNodes[i];
                    AddNode(xNode, tNode);
                }
            }
            else
            {
                treeNode.Text = xmlNode.InnerText.ToString();
            }
        }

        /// <summary>
        /// Method for get id number from full name
        /// </summary>
        /// <param name="fullName">full name</param>
        /// <returns>id number string</returns>
        public static string GetIdNumber(string fullName)
        {
            string rs = "";

            //split full name by char ' ' into string array
            string[] split = fullName.Split(new char[]{' '});
            //get total elements (- 1 is last word) of string array
            int len = split.Length-1;

            //cut and join first letter of each word, except last word
            for (int i = 0; i < len; ++i)
            {
                rs += split[i].Substring(0, 1);
            }

            //split last word by char '-' into string array
            string[] s = split[len].Split(new char[] { '-' });

            //cut and join in result (N01-20-02-2012 => 0112)
            rs += s[0].Substring(1) + s[s.Length - 1].Substring(2);

            //return lower result string 
            return rs.ToLower();
        }

        /// <summary>
        /// Method for converting a System.DateTime value to a UNIX Timestamp
        /// </summary>
        /// <param name="value">date to convert</param>
        /// <returns>Timestamp</returns>
        public static int ConvertToTimestamp(DateTime value)
        {
            //create Timespan by subtracting the value provided from
            //the Unix Epoch
            TimeSpan span = (value - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime());

            //return the total seconds (which is a UNIX timestamp)
            return (int)span.TotalSeconds;
        }

        /// <summary>
        /// Method for converting a Date Time String to a UNIX Timestamp
        /// </summary>
        /// <param name="dateString">date string to convert</param>
        /// <returns>Timestamp</returns>
        public static int ConvertToTimestamp(string dateString)
        {
            //get a System.DateTime from DateTime String
            DateTime value = ConvertToDate(dateString);
            //create Timespan by subtracting the value provided from
            //the Unix Epoch
            TimeSpan span = (value - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime());

            //return the total seconds (which is a UNIX timestamp)
            return (int)span.TotalSeconds;
        }
        /// <summary>
        /// Method for converting a UNIX timestamp to a regular
        /// System.DateTime value (and also to the current local time)
        /// </summary>
        /// <param name="timestamp">value to be converted</param>
        /// <returns>System.DateTime</returns>
        public static DateTime ConvertTimestamp(int timestamp)
        {
            //create a new DateTime value based on the Unix Epoch
            DateTime converted = new DateTime(1970, 1, 1, 0, 0, 0, 0);

            //add the timestamp to the value
            DateTime newDateTime = converted.AddSeconds(timestamp);

            //return the System.DateTime value
            return newDateTime.ToLocalTime();
        }

        /// <summary>
        /// Method for converting a Date Time String in cultureInfo("fr-FR")
        ///  to a regular System.DateTime value (and also to the current local time)
        /// </summary>
        /// <param name="dateString">date string to be converted</param>
        /// <returns>System.DateTime</returns>
        public static DateTime ConvertToDate(string dateString)
        {
            //get string array from date string in format "dd-MM-yyyy" 
            string[] s = dateString.Split(new char[] { '-' });

            //return the System.DateTime value
            return new DateTime(Convert.ToInt32(s[2]), Convert.ToInt32(s[1]),Convert.ToInt32(s[0])).ToLocalTime();
        }
    }
}