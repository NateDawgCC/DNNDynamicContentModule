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
//using System.Xml;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Search;

namespace DotNetNuke.Modules.DNNDynamicContentModule.Components
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The Controller class for DNNDynamicContentModule
    /// 
    /// The FeatureController class is defined as the BusinessController in the manifest file (.dnn)
    /// DotNetNuke will poll this class to find out which Interfaces the class implements. 
    /// 
    /// The IPortable interface is used to import/export content from a DNN module
    /// 
    /// The ISearchable interface is used by DNN to index the content of a module
    /// 
    /// The IUpgradeable interface allows module developers to execute code during the upgrade 
    /// process for a module.
    /// 
    /// Below you will find stubbed out implementations of each, uncomment and populate with your own data
    /// </summary>
    /// -----------------------------------------------------------------------------

    //uncomment the interfaces to add the support.
    public class FeatureController //: IPortable, ISearchable, IUpgradeable
    {


        #region Optional Interfaces

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// ExportModule implements the IPortable ExportModule Interface
        /// </summary>
        /// <param name="ModuleID">The Id of the module to be exported</param>
        /// -----------------------------------------------------------------------------
        //public string ExportModule(int ModuleID)
        //{
        //string strXML = "";

        //List<DNNDynamicContentModuleInfo> colDNNDynamicContentModules = GetDNNDynamicContentModules(ModuleID);
        //if (colDNNDynamicContentModules.Count != 0)
        //{
        //    strXML += "<DNNDynamicContentModules>";

        //    foreach (DNNDynamicContentModuleInfo objDNNDynamicContentModule in colDNNDynamicContentModules)
        //    {
        //        strXML += "<DNNDynamicContentModule>";
        //        strXML += "<content>" + DotNetNuke.Common.Utilities.XmlUtils.XMLEncode(objDNNDynamicContentModule.Content) + "</content>";
        //        strXML += "</DNNDynamicContentModule>";
        //    }
        //    strXML += "</DNNDynamicContentModules>";
        //}

        //return strXML;

        //	throw new System.NotImplementedException("The method or operation is not implemented.");
        //}

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// ImportModule implements the IPortable ImportModule Interface
        /// </summary>
        /// <param name="ModuleID">The Id of the module to be imported</param>
        /// <param name="Content">The content to be imported</param>
        /// <param name="Version">The version of the module to be imported</param>
        /// <param name="UserId">The Id of the user performing the import</param>
        /// -----------------------------------------------------------------------------
        //public void ImportModule(int ModuleID, string Content, string Version, int UserID)
        //{
        //XmlNode xmlDNNDynamicContentModules = DotNetNuke.Common.Globals.GetContent(Content, "DNNDynamicContentModules");
        //foreach (XmlNode xmlDNNDynamicContentModule in xmlDNNDynamicContentModules.SelectNodes("DNNDynamicContentModule"))
        //{
        //    DNNDynamicContentModuleInfo objDNNDynamicContentModule = new DNNDynamicContentModuleInfo();
        //    objDNNDynamicContentModule.ModuleId = ModuleID;
        //    objDNNDynamicContentModule.Content = xmlDNNDynamicContentModule.SelectSingleNode("content").InnerText;
        //    objDNNDynamicContentModule.CreatedByUser = UserID;
        //    AddDNNDynamicContentModule(objDNNDynamicContentModule);
        //}

        //	throw new System.NotImplementedException("The method or operation is not implemented.");
        //}

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// GetSearchItems implements the ISearchable Interface
        /// </summary>
        /// <param name="ModInfo">The ModuleInfo for the module to be Indexed</param>
        /// -----------------------------------------------------------------------------
        //public DotNetNuke.Services.Search.SearchItemInfoCollection GetSearchItems(DotNetNuke.Entities.Modules.ModuleInfo ModInfo)
        //{
        //SearchItemInfoCollection SearchItemCollection = new SearchItemInfoCollection();

        //List<DNNDynamicContentModuleInfo> colDNNDynamicContentModules = GetDNNDynamicContentModules(ModInfo.ModuleID);

        //foreach (DNNDynamicContentModuleInfo objDNNDynamicContentModule in colDNNDynamicContentModules)
        //{
        //    SearchItemInfo SearchItem = new SearchItemInfo(ModInfo.ModuleTitle, objDNNDynamicContentModule.Content, objDNNDynamicContentModule.CreatedByUser, objDNNDynamicContentModule.CreatedDate, ModInfo.ModuleID, objDNNDynamicContentModule.ItemId.ToString(), objDNNDynamicContentModule.Content, "ItemId=" + objDNNDynamicContentModule.ItemId.ToString());
        //    SearchItemCollection.Add(SearchItem);
        //}

        //return SearchItemCollection;

        //	throw new System.NotImplementedException("The method or operation is not implemented.");
        //}

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// UpgradeModule implements the IUpgradeable Interface
        /// </summary>
        /// <param name="Version">The current version of the module</param>
        /// -----------------------------------------------------------------------------
        //public string UpgradeModule(string Version)
        //{
        //	throw new System.NotImplementedException("The method or operation is not implemented.");
        //}

        #endregion

    }

}
