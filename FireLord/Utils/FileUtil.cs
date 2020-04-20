using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace FireLord
{
    class FileUtil
    {
        private static String dir = @"F:\Mound&Blade2Log";

        /// <summary>
        /// 写文件到本地
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="html"></param>
        public static void Write(string html, string fileName = "file.txt", FileMode mode = FileMode.Append)
        {
            try
            {
                if (!Directory.Exists(dir))//验证路径是否存在
                {
                    Directory.CreateDirectory(dir);
                    //不存在则创建
                }

                fileName = dir + fileName;
                FileStream fs;
                if (File.Exists(fileName))
                //验证文件是否存在，有则追加，无则创建
                {
                    fs = new FileStream(fileName, mode, FileAccess.Write);
                }
                else
                {
                    fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);
                }
                StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
                sw.WriteLine(html + "\r\n");
                sw.Close();
                fs.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }

        /// <summary>
        /// 写文件到本地
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="html"></param>
        public static void Write(string html, FileMode mode = FileMode.Create)
        {
            try
            {
                if (!Directory.Exists(dir))//验证路径是否存在
                {
                    Directory.CreateDirectory(dir);
                    //不存在则创建
                }
                FileStream fs = new FileStream(dir + "file.txt", mode);
                StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
                sw.Write(html);
                sw.Close();
                fs.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }


        /// <summary>
        /// 写文件到本地
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="html"></param>
        public static void Write(string fileName, byte[] html)
        {
            try
            {
                File.WriteAllBytes(dir + fileName, html);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }

        }


        #region 读文件
        public static string Read(string fileName = "file.log")
        {
            fileName = dir + fileName;
            string str = "";
            if (File.Exists(fileName))
            {
                StreamReader sr = null;
                try
                {
                    sr = new StreamReader(fileName, Encoding.UTF8);
                    str = sr.ReadToEnd(); // 读取文件
                }
                catch { }
                sr.Close();
                sr.Dispose();
            }
            else
            {
                str = "";
            }
            return str;
        }
        #endregion

        #region 读文件 返回字节
        public static byte[] ReadBytesByFileName(string fileName = "file.log")
        {
            fileName = dir + fileName;
            FileStream dwgFsRead = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            byte[] dwgBytes = new byte[(int)dwgFsRead.Length];
            dwgFsRead.Read(dwgBytes, 0, dwgBytes.Length);
            return dwgBytes;
        }
        #endregion

        #region 读文件 返回字节
        public static byte[] ReadBytesByPath(string path)
        {
            FileStream dwgFsRead = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            byte[] dwgBytes = new byte[(int)dwgFsRead.Length];
            dwgFsRead.Read(dwgBytes, 0, dwgBytes.Length);
            return dwgBytes;
        }
        #endregion

        #region 读文件 返回字节
        public static byte[] ReadBytesByPath2(string path)
        {
            //这种方式有个缺点，当这个文件被其它应用打开后则不能读取
            byte[] dwgBytes = File.ReadAllBytes(path);
            return dwgBytes;
        }
        #endregion

        /// <summary>
        /// 获取Assembly的运行路径
        /// </summary>
        ///<returns></returns>
        public static string GetAssemblyPath()
        {
            string _CodeBase = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;

            _CodeBase = _CodeBase.Substring(8, _CodeBase.Length - 8);    // 8是file:// 的长度

            string[] arrSection = _CodeBase.Split(new char[] { '/' });

            string _FolderPath = "";
            for (int i = 0; i < arrSection.Length - 1; i++)
            {
                _FolderPath += arrSection[i] + "/";
            }

            return _FolderPath;
        }
    }
}
