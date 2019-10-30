using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
namespace PMACam
{
    public class INIOperation
    {
        public string path;
        
        //[DllImport("kernel32")]
        //private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        [DllImport("kernel32")]
        private static extern bool WritePrivateProfileString(string section, string key, string val, string filePath);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string defVal, StringBuilder retVal, int size, string filePath);
        [DllImport("kernel32", EntryPoint = "GetPrivateProfileString")]
        private static extern uint GetPrivateProfileStringA(string section, string key,string def, Byte[] retVal, int size, string filePath);

        //读Sections list
        public  List<string> ReadSections()
        {
            return ReadSections(path);
        }
        public List<string> ReadSections(string iniFilename)
        {
            List<string> result = new List<string>();
            Byte[] buf = new Byte[65536];
            uint len = GetPrivateProfileStringA(null, null, null, buf, buf.Length, iniFilename);
            int j = 0;
            for (int i = 0; i < len; i++)
                if (buf[i] == 0)
                {
                    result.Add(Encoding.Default.GetString(buf, j, i - j));
                    j = i + 1;
                }
            return result;
        }

        //读Keys list
        public List<string> ReadKeys(String SectionName)
        {
            return ReadKeys(SectionName, path);
        }

        public  List<string> ReadKeys(string SectionName, string iniFilename)
        {
            List<string> result = new List<string>();
            Byte[] buf = new Byte[65536];
            uint len = GetPrivateProfileStringA(SectionName, null, null, buf, buf.Length, iniFilename);
            int j = 0;
            for (int i = 0; i < len; i++)
                if (buf[i] == 0)
                {
                    result.Add(Encoding.Default.GetString(buf, j, i - j));
                    j = i + 1;
                }
            return result;
        }

        //读取某个section下，values list
        public List<string> ReadSectionValues(string Section)
        {
            List<string> keys=new List<string>();
            List<string> Values=new List<string>();
            keys = ReadKeys(Section);
            foreach (string key in keys)
            {
                Values.Add(IniReadValue(Section,key));
            }
            return Values;
        }
        
        //读key value
        public string IniReadValue(string section, string skey)
        {
            StringBuilder temp = new StringBuilder(500);
            int i = GetPrivateProfileString(section, skey, "", temp, 500, path);
            return temp.ToString();
        }

        //写key value
        public void IniWrite(string section, string key, string value)
        {
            //WritePrivateProfileString(section, key, value, path);
            if (!WritePrivateProfileString(section, key, value, path))
            {
                throw (new ApplicationException("写Ini文件出错"));
            }
        }

        //清除某个Section 
         public void EraseSection(string Section) 
        {
             if (!WritePrivateProfileString(Section, null, null, path))
             {
                 throw (new ApplicationException("无法清除Ini文件中的Section")); 
                 
             }
        }
         //删除某个Section下的键
        public void DeleteKey(string Section, string Ident)
        {
            WritePrivateProfileString(Section, Ident, null, path);
        }


        //执行完对Ini文件的修改之后，应该调用本方法更新缓冲区。
        public void UpdateFile() { WritePrivateProfileString(null, null, null, path); }

        //检查某个Section下的某个键值是否存在
        public bool ValueExists(string Section, string Ident)
        {
            List<string> keys=new List<string>();
            keys=ReadKeys(Section);
            return keys.Contains(Ident); 
        }

        //重命名
        //1. 旧section，是否存在，不存在则返回；若存在则，将key，value放入字典中；新section名是否已在ini档中，若存在则返回，若不存在则将字典放入其中。
        //2. 删除旧section
        
        public bool ReNameSection(string oldsection,string newsection)
        {
            List<string> secTemp = new List<string>();
            Dictionary<string, string> kvTemp = new Dictionary<string, string>();
            secTemp=ReadSections();
            if (secTemp.Contains(newsection))
            {
                return false;
            }

            if (!secTemp.Contains(oldsection))
            {
                return false;
            }
            List<string> keystemp=new List<string>();
            keystemp = ReadKeys(oldsection);
            foreach (string keytemp in keystemp)
            {
                string valuetemp = IniReadValue(oldsection, keytemp);
                kvTemp.Add(keytemp, valuetemp);
            }

            EraseSection(oldsection);

            foreach (KeyValuePair<string,string> kv in kvTemp)
            {
                IniWrite(newsection,kv.Key,kv.Value);
            }
            return true;
        }

        //setion 名互换(将section1、section2读到buffer，删除section1，,section2，将buffer写到section2，section1)
        public bool ExchangeName(string section1, string section2)
        {
            List<string> sections=new List<string>();
            sections=ReadSections();
            if (!sections.Contains(section1))
            {
                return false;
            }
            if (!sections.Contains(section2))
            {
                return false;
            }
            Dictionary<string, string> section1temp =new Dictionary<string, string>();
            Dictionary<string, string> section2temp =new Dictionary<string, string>();
            List<string> keys1=new List<string>();
            keys1=ReadKeys(section1);
            foreach (string k in keys1)
            {
                section1temp.Add(k,IniReadValue(section1,k));
            }
            

            List<string> keys2 = new List<string>();
            keys2 = ReadKeys(section2);
            foreach (string k2 in keys2)
            {
                section2temp.Add(k2, IniReadValue(section2, k2));
            }


            EraseSection(section1);
            EraseSection(section2);

            foreach (KeyValuePair<string,string> k1 in section1temp)
            {
                IniWrite(section2,k1.Key,k1.Value);
            }

            foreach (KeyValuePair<string,string> k2 in section2temp)
            {
                IniWrite(section1,k2.Key,k2.Value);
            }

            return true;
        }
        //构造函数
        public INIOperation(string Path)
        {
            this.path = Path;
            if (!File.Exists(Path))
            {
                using (FileStream myFs = new FileStream(Path, FileMode.Create))
                {
                    
                }
            }
        }

    }
}
