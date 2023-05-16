/********************************************************************
	created:	2019/03/11
	created:	11:3:2019   11:38
	file base:	StringTools
	file ext:	cs
	author:		Wusunquan
	
	purpose:	Some tools about string
*********************************************************************/
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace IvyCore
{
    public class StringTools
    {

        /// <summary>
        /// Combine Strings
        /// </summary>
        /// <param name="contents">Need to be Combine strings</param>
        /// <returns>new Combine String</returns>
        public static string Combine(params string[] contents)
        {
            StringBuilder sb = new StringBuilder();
            for (var i = 0; i < contents.Length; ++i)
            {
                sb.Append(contents[i]);
            }
            return sb.ToString();
        }

        /// <summary>
        /// Check the string format
        /// </summary>
        /// <param name="content">need checking string</param>
        /// <returns></returns>
        public static bool IsStringOnlyContainEnglishNumber(string content)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(content, @"^[_A-Za-z0-9]+$");
        }

        public static string GetFileNamePath(string path)
        {
            path = path.Replace("\\", "/");
            var lastSlashIndex = path.LastIndexOf("/");
            if (lastSlashIndex == -1)
                return string.Empty;
            ++lastSlashIndex;
            string fileName = path.Substring(lastSlashIndex, path.Length - lastSlashIndex);
            var lastDotIndex = fileName.LastIndexOf(".");
            if (lastDotIndex == -1)
                return string.Empty;
            fileName = fileName.Substring(0, lastDotIndex);
            return fileName;
        }
    }
}