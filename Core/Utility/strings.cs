using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AV.Development.Core.Utility
{
    public class strings
    {
        #region " returnEmptyStringFromNull"
        public static string returnEmptyStringFromNull(object obj)
        {
            if (object.ReferenceEquals(obj, DBNull.Value))
            {
                return "";
            }
            else
            {
                return Convert.ToString(obj);
            }
        }
        #endregion

        #region " replaceQuotes"
        public static string replaceSingleQuotes(string obj)
        {
            return obj.Replace("'", "''");
        }
        #endregion

        #region ReplaceDoubleSpaceWithUnderscore
        public static string ReplaceDoubleSpaceWithUnderscore(string input)
        {
            string str = System.Text.RegularExpressions.Regex.Replace(input, " ", "_");
            str = System.Text.RegularExpressions.Regex.Replace(str, "______", "_");
            str = System.Text.RegularExpressions.Regex.Replace(str, "_____", "_");
            str = System.Text.RegularExpressions.Regex.Replace(str, "____", "_");
            str = System.Text.RegularExpressions.Regex.Replace(str, "___", "_");
            str = System.Text.RegularExpressions.Regex.Replace(str, "__", "_");
            return str;
        }
        #endregion

        #region " replaceCarriageReturn"
        public static string replaceCarriageReturn(string obj)
        {
            return obj.Replace("\n", "</br>");
        }
        #endregion

        #region " ReplaceSpaceWithUnderscore "
        public static string ReplaceSpaceWithUnderscore(string input)
        {
            return System.Text.RegularExpressions.Regex.Replace(input, " ", "_");
        }
        #endregion

        #region RemoveAmpersand
        public static string RemoveAmpersand(string input)
        {
            return System.Text.RegularExpressions.Regex.Replace(input, "&", "");
        }
        #endregion

        #region ReplaceSpaceWithHyphin
        public string ReplaceSpaceWithHyphin(string input)
        {
            string str = System.Text.RegularExpressions.Regex.Replace(input, " ", "-");
            str = System.Text.RegularExpressions.Regex.Replace(str, "--", "-");
            str = System.Text.RegularExpressions.Regex.Replace(str, "---", "-");
            str = System.Text.RegularExpressions.Regex.Replace(str, "----", "-");
            str = System.Text.RegularExpressions.Regex.Replace(str, "-----", "-");
            str = System.Text.RegularExpressions.Regex.Replace(str, "------", "-");
            return str;
        }
        #endregion

        public static bool IsNullOrWhiteSpace(string value)
        {
            if (value != null)
            {
                for (int i = 0; i < value.Length; i++)
                {
                    if (!char.IsWhiteSpace(value[i]))
                    {
                        return false;
                    }
                }
            }
            return true;
        }


        #region " ReplaceHyphinWithSpace "
        public string ReplaceHyphinWithSpace(string input)
        {
            string str = System.Text.RegularExpressions.Regex.Replace(input, "-", " ");
            str = System.Text.RegularExpressions.Regex.Replace(str, "--", "-");
            str = System.Text.RegularExpressions.Regex.Replace(str, "---", "-");
            str = System.Text.RegularExpressions.Regex.Replace(str, "----", "-");
            str = System.Text.RegularExpressions.Regex.Replace(str, "-----", "-");
            str = System.Text.RegularExpressions.Regex.Replace(str, "------", "-");
            str = System.Text.RegularExpressions.Regex.Replace(str, "-", " ");
            return str;
        }
        #endregion

        #region " StripHtml "
        public static string StripHtml(string html, bool allowHarmlessTags)
        {
            if (html == null || html == string.Empty)
                return string.Empty;
            if (allowHarmlessTags)
                return System.Text.RegularExpressions.Regex.Replace(html, "</?(?i:script|embed|object|frameset|frame|iframe|meta|link|style)(.|\\n)*?>", string.Empty);
            return System.Text.RegularExpressions.Regex.Replace(html, "<[^>]*>", string.Empty);
        }
        #endregion

        #region fn_Strip
        public static string fn_Strip(string strValue)
        {
            string sRet = "";
            string strBadValues = "/\'&,!;:?><";

            for (int i = 0; i < strValue.Length; i++)
            {
                if (strBadValues.IndexOf(strValue.Substring(i, 1)) == -1)
                {
                    sRet = sRet + strValue.Substring(i, 1);
                }
            }

            return sRet;
        }
        #endregion
    }
}
