using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace ShopBanSieuXe.Library
{
    public static class MyString
    {
        public static String Str_slug(this string s)
        {
            String[][] symbols =
            {
                new String[]{ "[áàảãạăắặằẳẵâấầẩẫậ]","a" } ,
                new String[]{"[đ]","d"},
                new string[]{ "[éèẻẽẹêếềểễệ]","e" },
                new string[]{ "[íìỉĩị]", "i" },
                new string[]{ "[óòỏõọôốồổỗộơớờởỡợ]", "o" },
                new string[]{ "[úùủũụưứừửữự]", "u" },
                new string[]{ "[ýỳỷỹỵ]", "y" },
                new string[]{ "[\\s'\";,]", "-" }
            };
            s = s.ToLower();
            foreach (var ss in symbols)
            {
                s = Regex.Replace(s, ss[0], ss[1]);
            }
            return s;
        }
    }
}