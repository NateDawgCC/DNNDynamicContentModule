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

using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Data;

namespace DotNetNuke.Modules.DNNDynamicContentModule.Components
{
    public class DynamicContentController
    {
        //Create

        private static void AddDynamicContentItem(DynamicContentItem dynamicContentItem)
        {
            using (IDataContext context = DataContext.Instance())
            {
                var repository = context.GetRepository<DynamicContentItem>();
                repository.Insert(dynamicContentItem);
            }
        }

        //Read

        public static DynamicContentItem GetDynamicContentItem(int itemID, int moduleID)
        {
            DynamicContentItem dynamicContentItem;
            using (IDataContext context = DataContext.Instance())
            {
                var repository = context.GetRepository<DynamicContentItem>();
                dynamicContentItem = repository.GetById(itemID, moduleID);
            }
            return dynamicContentItem;
        }

        public static IEnumerable<DynamicContentItem> GetDynamicContentItems(int moduleID)
        {
            IEnumerable<DynamicContentItem> dynamicContentItems;

            using (IDataContext context = DataContext.Instance())
            {
                var repository = context.GetRepository<DynamicContentItem>();
                dynamicContentItems = repository.Get(moduleID);
            }

            return dynamicContentItems;
        }

        //Update

        private static void UpdateDynamicContentItem(DynamicContentItem dynamicContentItem)
        {
            using (IDataContext context = DataContext.Instance())
            {
                var repository = context.GetRepository<DynamicContentItem>();
                repository.Update(dynamicContentItem);
            }
        }

        //Delete

        public static void DeleteDynamicContentItem(int moduleID, int itemID)
        {
            using (IDataContext context = DataContext.Instance())
            {
                var repository = context.GetRepository<DynamicContentItem>();
                repository.Delete("WHERE ModuleId = @0 AND ItemID = @1", moduleID, itemID);
            }
        }

        //Business Logic
        public static void SaveDynamicContentItem(DynamicContentItem dynamicContentItem)
        {
            if (dynamicContentItem.ItemID == Null.NullInteger)
            {
                AddDynamicContentItem(dynamicContentItem);
            }
            else
            {
                UpdateDynamicContentItem(dynamicContentItem);
            }
        }

        public static IEnumerable<DynamicContentItem> BuildDynamicContentItemListFromRequest(HttpRequest request, int moduleId)
        {
            var queryStringParams = GetQueryStringParams(request);
            var dynamicContentItems = GetDynamicContentItems(moduleId);
            var output = new List<DynamicContentItem>();

            foreach (var item in queryStringParams)
            {
                output.Add(new DynamicContentItem(item.QueryStringName, item.QueryStringValue));

                if (item.QueryStringValue.Contains(","))
                {
                    var valueArray = item.QueryStringValue.Split(',');

                    foreach (var s in valueArray)
                    {
                        output.Add(new DynamicContentItem(item.QueryStringName, s));
                    }
                }
            }

            if (dynamicContentItems != null)
            {
                var dynamicContentItemList = dynamicContentItems as IList<DynamicContentItem> ?? dynamicContentItems.ToList();

                foreach (var dci1 in output)
                {
                    foreach (var dci2 in dynamicContentItemList)
                    {
                        if (dci1.QueryStringName == dci2.QueryStringName && dci1.QueryStringValue == dci2.QueryStringValue)
                        {
                            dci1.ItemID = dci2.ItemID;
                            dci1.ModuleID = dci2.ModuleID;
                            dci1.Title = dci2.Title;
                            dci1.Content = dci2.Content;
                            dci1.ShortDescription = dci2.ShortDescription;
                            dci1.Image = dci2.Image;
                            dci1.Keywords = dci2.Keywords;
                            dci1.Rank = dci2.Rank;
                        }
                    }
                }

                output.Sort(new DynamicContentSorter.RankComparerDesc());
            }

            return output;
        }

        public static IEnumerable<QueryStringParam> GetQueryStringParams(HttpRequest request)
        {
            var output = new List<QueryStringParam>();

            foreach (var queryStringKey in request.QueryString.AllKeys)
            {
                var key = Utilities.CastObjectToString(queryStringKey);

                if (!string.IsNullOrEmpty(key))
                {
                    output.Add(new QueryStringParam(queryStringKey, request.QueryString[queryStringKey]));
                }
            }

            return output;
        }

        public class QueryStringParam
        {
            public QueryStringParam()
            {
                QueryStringName = Null.NullString;
                QueryStringValue = Null.NullString;
            }

            public QueryStringParam(string queryStringName, string queryStringValue)
            {
                QueryStringName = queryStringName;
                QueryStringValue = queryStringValue;
            }

            public string QueryStringName { get; set; }
            public string QueryStringValue { get; set; }
        }
    }
}