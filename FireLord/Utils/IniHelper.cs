using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.Library;

namespace FireLord.Utils
{
    public class IniHelper
    {
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filepath);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retval, int size, string filePath);

        [DllImport("kernel32", EntryPoint = "GetPrivateProfileString")]
        private static extern uint GetPrivateProfileStringA(string section, string key, string def, byte[] retVal, int size, string filePath);

        private string FilePath = "";
        private string Section = "";

        private Dictionary<string, string> List = new Dictionary<string, string>();

        /// <summary>
        /// INI工具类
        /// </summary>
        /// <param name="_filePath"></param>
        /// <param name="_section"></param>
        public IniHelper(string _filePath = "config.ini", string _section = "default")
        {
            FilePath = _filePath;

            Section = _section;

            Reload();
        }

        /// <summary>
        /// 重新加载
        /// </summary>
        public void Reload()
        {
            this.List = new Dictionary<string, string>();

            List<string> keyList = _getKeyList();
            foreach (var key in keyList)
            {
                if (this.List.ContainsKey(key))
                    this.List[key] = Get(key);
                else
                    this.List.Add(key, Get(key));
            }
        }

        /// <summary>
        /// 获取key列表
        /// </summary>
        /// <returns></returns>
        public string[] GetKeyList()
        {
            return this.List.Keys.ToArray();
        }

        /// <summary>
        /// 获取所有KEY
        /// </summary>
        /// <returns></returns>
        private List<string> _getKeyList()
        {
            List<string> result = new List<string>();
            byte[] buf = new byte[65536];
            uint len = GetPrivateProfileStringA(Section, null, null, buf, buf.Length, FilePath);

            int j = 0;
            for (int i = 0; i < len; i++)
                if (buf[i] == 0)
                {
                    result.Add(Encoding.Default.GetString(buf, j, i - j));
                    j = i + 1;
                }

            return result;
        }

        /// <summary>
        /// 获取值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultVal"></param>
        /// <returns></returns>
        public string Get(string key, string defaultVal = "")
        {
            if (this.List.ContainsKey(key))
            {
                return this.List[key];
            }

            StringBuilder s = new StringBuilder(1024);
            GetPrivateProfileString(Section, key, defaultVal, s, 1024, FilePath);

            return s.ToString();
        }

        /// <summary>
        /// 设置值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val"></param>
        public void Set(string key, string val)
        {
            this.List[key] = val;
            WritePrivateProfileString(Section, key, val, FilePath);
        }

        /// <summary>
        /// 删除key
        /// </summary>
        /// <param name="key"></param>
        public void Del(string key)
        {
            this.List.Remove(key);
            WritePrivateProfileString(Section, key, null, FilePath);
        }

        /// <summary>
        /// 获取int
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultVal"></param>
        /// <returns></returns>
        public int GetInt(string key, int defaultVal = 0)
        {
            string str = Get(key, defaultVal.ToString());

            int val = defaultVal;
            bool bo = int.TryParse(str, out val);

            return bo ? val : defaultVal;
        }

        /// <summary>
        /// 获取float
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultVal"></param>
        /// <returns></returns>
        public float GetFloat(string key, float defaultVal = 0)
        {
            string str = Get(key, defaultVal.ToString());

            float val = defaultVal;
            bool bo = float.TryParse(str, out val);

            return bo ? val : defaultVal;
        }

        /// <summary>
        /// 获取bool
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultVal"></param>
        /// <returns></returns>
        public bool GetBool(string key, bool defaultVal = false)
        {
            string str = Get(key, defaultVal ? "1" : "0");

            return str == "1";
        }

        /// <summary>
        /// 设置int
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val"></param>
        public void SetInt(string key, int val)
        {
            Set(key, val.ToString());
        }

        /// <summary>
        /// 设置float
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val"></param>
        public void SetFloat(string key, float val)
        {
            Set(key, val.ToString());
        }

        /// <summary>
        /// 设置bool
        /// </summary>
        /// <param name="key"></param>
        /// <param name="bo"></param>
        public void SetBool(string key, bool bo)
        {
            Set(key, bo ? "1" : "0");
        }
    }
}
