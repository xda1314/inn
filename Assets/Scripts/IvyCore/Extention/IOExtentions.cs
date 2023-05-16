using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace IvyCore{

    public static class IOExtentions
    {
        /// <summary>
        /// 创建新的文件夹,如果存在则不创建
        /// </summary>
        public static string CreateDirIfNotExists(this string dirFullPath)
        {
            if (!Directory.Exists(dirFullPath))
            {
                Directory.CreateDirectory(dirFullPath);
            }

            return dirFullPath;
        }

        /// <summary>
        /// 删除文件夹，如果存在
        /// </summary>
        public static void DeleteDirIfExists(this string dirFullPath)
        {
            if (Directory.Exists(dirFullPath))
            {
                Directory.Delete(dirFullPath, true);
            }
        }

        /// <summary>
        /// 清空 Dir,如果存在。
        /// </summary>
        public static void EmptyDirIfExists(this string dirFullPath)
        {
            if (Directory.Exists(dirFullPath))
            {
                Directory.Delete(dirFullPath, true);
            }

            Directory.CreateDirectory(dirFullPath);
        }

        /// <summary>
        /// 删除文件 如果存在
        /// </summary>
        /// <param name="fileFullPath"></param>
        /// <returns> True if exists</returns>
        public static bool DeleteFileIfExists(this string fileFullPath)
        {
            if (File.Exists(fileFullPath))
            {
                File.Delete(fileFullPath);
                return true;
            }

            return false;
        }

        public static string CombinePath(this string selfPath, string toCombinePath)
        {
            return Path.Combine(selfPath, toCombinePath);
        }
    }
}