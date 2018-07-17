using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ybm.Framework.Extension
{
    public static class StringExtensions
    {
        public static int GetStableHashCode(this string str)
        {
            unchecked
            {
                int hash1 = (5381 << 16) + 5381;
                int hash2 = hash1;

                for (int i = 0; i < str.Length; i += 2)
                {
                    hash1 = ((hash1 << 5) + hash1) ^ str[i];
                    if (i == str.Length - 1)
                        break;
                    hash2 = ((hash2 << 5) + hash2) ^ str[i + 1];
                }

                return hash1 + (hash2 * 1566083941);
            }
        }

        public static Dictionary<char, string> MyProperty { get; set; }

         static Dictionary<char, string> _UnicodeDictionary { get; set; }
         static Dictionary<char, string> UnicodeDictionary
        {
            get
            {
                if (_UnicodeDictionary != null && _UnicodeDictionary.Any())
                    return _UnicodeDictionary;

                _UnicodeDictionary = new Dictionary<char, string>()
                {
                    { '\u06f0', "0"},
                    { '\u06f1',"1"},
                    { '\u06f2',"2"},
                    { '\u06f3', "3"},
                    { '\u06f4',"4"},
                    { '\u06f5',"5"},
                    { '\u06f6',"6"},
                    { '\u06f7',"7"},
                    { '\u06f8',"8"},
                    { '\u06f9',"9"},

                    { '\u0660', "0"},
                    { '\u0661' ,"1"},
                    { '\u0662',"2"},
                    { '\u0663', "3"},
                    { '\u0664',"4"},
                    { '\u0665',"5"},
                    { '\u0666',"6"},
                    { '\u0667',"7"},
                    { '\u0668',"8"},
                    { '\u0669',"9"}
                };
                return _UnicodeDictionary;
            }

        }

        public static string FarsiDigitsToEnglish(this string mobile) 
        {
            StringBuilder stb = new StringBuilder();
            foreach (char ch in mobile.ToString())
            {
                if (UnicodeDictionary.ContainsKey(ch))
                {
                    stb.Append(UnicodeDictionary[ch]);
                }
                else
                    stb.Append(ch);
            }
            return stb.ToString();
        }

        public static T FarsiDigitsToEnglish<T>(this T mobile) where T : struct
        {
            StringBuilder stb = new StringBuilder();
            foreach (char ch in mobile.ToString())
            {
                if (UnicodeDictionary.ContainsKey(ch))
                {
                    stb.Append(UnicodeDictionary[ch]);
                }
                else
                    stb.Append(ch);
            }
            return (T)Convert.ChangeType(stb.ToString() , typeof(T));
        }


    }
}
