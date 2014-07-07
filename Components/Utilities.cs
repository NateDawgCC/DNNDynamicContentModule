// 
// DNN Corp. - http://www.dnnsoftware.com
// Copyright (c) 2002-2014 DNN Corp.
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated 
// documentation files (the "Software"), to deal in the Software without restriction, including without limitation 
// the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and 
// to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions 
// of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
// TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL 
// THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
// CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
// DEALINGS IN THE SOFTWARE.

using System;

namespace DotNetNuke.Modules.DNNDynamicContentModule.Components
{
    public class Utilities
    {
        public static string CastObjectToString(object setting, string defaultValue = "")
        {
            return setting != null ? setting.ToString() : defaultValue;
        }

        public static bool CastObjectToBool(object setting, bool defaultValue = false)
        {
            var returnVal = defaultValue;

            if (setting != null)
            {
                if (bool.TryParse(setting.ToString(), out returnVal))
                {
                    return returnVal;
                }

                var intTest = CastObjectToInt(setting);

                if (intTest == 1)
                {
                    return true;
                }

                if (intTest == 0)
                {
                    return false;
                }
            }

            return returnVal;
        }

        public static int CastObjectToInt(object setting, int defaultValue = -1)
        {
            var returnVal = defaultValue;

            if (setting != null)
            {
                int.TryParse(setting.ToString(), out returnVal);
            }

            return returnVal;
        }

        public static string SafeInput(string s, Boolean preserveCr)
        {
            var cr = preserveCr ? "&#13;" : "";
            s = s.Replace("&", "&amp;"); /* This MUST be the 1st replacement. */
            s = s.Replace("'", "&apos;"); /* The 4 other predefined entities, required. */
            s = s.Replace("\"", "&quot;");
            s = s.Replace("<", "&lt;");
            s = s.Replace(">", "&gt;");
            /*
            You may add other replacements here for HTML only 
            (but it's not necessary).
            Or for XML, only if the named entities are defined in its DTD.
            */
            s = s.Replace("\r", cr);
            s = s.Replace("\n", cr);
            return s;
        }
    }
}