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

using System.Web.Caching;
using DotNetNuke.Common.Utilities;
using DotNetNuke.ComponentModel.DataAnnotations;

namespace DotNetNuke.Modules.DNNDynamicContentModule.Components
{
    [TableName("DynamicContent_Items")]
    [PrimaryKey("ItemID")]
    [Scope("ModuleID")]
    [Cacheable("DynamicContentItems", CacheItemPriority.Default, 20)]
    public class DynamicContentItem
    {

        public DynamicContentItem()
        {
            ItemID = Null.NullInteger;
            ModuleID = Null.NullInteger;
            Title = Null.NullString;
            Content = Null.NullString;
            ShortDescription = Null.NullString;
            Image = Null.NullString;
            Keywords = Null.NullString;
            QueryStringName = Null.NullString;
            QueryStringValue = Null.NullString;
            Rank = Null.NullInteger;
        }

        public DynamicContentItem(string queryStringName, string queryStringValue)
        {
            ItemID = Null.NullInteger;
            ModuleID = Null.NullInteger;
            Title = Null.NullString;
            Content = Null.NullString;
            ShortDescription = Null.NullString;
            Image = Null.NullString;
            Keywords = Null.NullString;
            QueryStringName = queryStringName;
            QueryStringValue = queryStringValue;
            Rank = Null.NullInteger;
        }

        public int ItemID { get; set; }
        public int ModuleID { get; set; }

        //items to display on the page
        public string Title { get; set; }
        public string Content { get; set; }

        public string ShortDescription { get; set; }
        public string Image { get; set; }
        public string Keywords { get; set; }

        //Display when this QS value pair is a match
        public string QueryStringName { get; set; }
        public string QueryStringValue { get; set; }

        public int Rank { get; set; }
    }
}