using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ankarunning.Web.Helpers {
    public static class TextTransforms {
        //first paramter specifies which type the method operates on preceded by this modifier
        public static string ToNullOrValue(this String str) {
            return !String.IsNullOrWhiteSpace(str) ? str : null;

        }

        public static string ToEmptyString(this String str) {
            return str == null ? String.Empty : str;
        }

        public static string ToCapital(this String str) {
            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
            TextInfo textInfo = cultureInfo.TextInfo;

            return textInfo.ToTitleCase(str);
        }

    }
}
