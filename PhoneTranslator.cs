using System.Text;
using System;

namespace Core
{
    public static class PhonewordTranslator
    {
        public static string toNumber(string str)
        {

            if (string.IsNullOrWhiteSpace(str))
            {
                return "";
            }
            else
            {
                str = str.ToUpperInvariant();
            }

            var newNumber = new StringBuilder();
            foreach (var c in str)
            {
                if (" -0123456789".contains(c))
                {
                    newNumber.Append(c);
                }
                else
                {
                    var result = TranslateToNumber(c);
                    if(result!=null){
                        newNumber.Append(result);
                    }
                }
            }
            return newNumber.ToString();
        }
        static bool contains(this string keyString, char c)
        {
            return keyString.IndexOf(c) >= 0;
        }
        static int? TranslateToNumber(char c)
        {
            if ("ABC".contains(c))
                return 2;
            else if ("DEF".contains(c))
                return 3;
            else if ("GHI".contains(c))
                return 4;
            else if ("JKL".contains(c))
                return 5;
            else if ("MNO".contains(c))
                return 6;
            else if ("PQRS".contains(c))
                return 7;
            else if ("TUV".contains(c))
                return 8;
            else if ("WXYZ".contains(c))
                return 9;
            return null;
        }

    }

}