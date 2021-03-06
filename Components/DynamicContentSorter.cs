﻿// 
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

namespace DotNetNuke.Modules.DNNDynamicContentModule.Components
{
    public class DynamicContentSorter
    {
        //PackageName
        public class RankComparerDesc : IComparer<DynamicContentItem>
        {
            public int Compare(DynamicContentItem dynamicContentItem1, DynamicContentItem dynamicContentItem2)
            {
                var returnValue = 1;

                if (dynamicContentItem1 != null && dynamicContentItem2 != null)
                {
                    returnValue = dynamicContentItem2.Rank.CompareTo(dynamicContentItem1.Rank);
                }

                return returnValue;
            }
        }

        public class RankComparerAsc : IComparer<DynamicContentItem>
        {
            public int Compare(DynamicContentItem dynamicContentItem1, DynamicContentItem dynamicContentItem2)
            {
                var returnValue = 1;

                if (dynamicContentItem1 != null && dynamicContentItem2 != null)
                {
                    returnValue = dynamicContentItem1.Rank.CompareTo(dynamicContentItem2.Rank);
                }

                return returnValue;
            }
        }
    }
}