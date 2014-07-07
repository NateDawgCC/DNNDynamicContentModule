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

using System.Net;
using System.Net.Http;
using System.Web.Http;
using DotNetNuke.Security;
using DotNetNuke.Web.Api;

namespace DotNetNuke.Modules.DNNDynamicContentModule.Components
{
    [SupportedModules("DNNDynamicContentModule")]
    public class ServiceController : DnnApiController
    {
        #region Public APIs
        
        //create
        [ValidateAntiForgeryToken]
        [DnnModuleAuthorize(AccessLevel = SecurityAccessLevel.Edit, PermissionKey = "EDITCONTENT")]
        [HttpPost]
        public HttpResponseMessage AddDynamicContentItem(DynamicContentItem contentItem)
        {
            DynamicContentController.SaveDynamicContentItem(contentItem);

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        //read
        [DnnAuthorize]
        [HttpPost]
        public HttpResponseMessage GetDynamicContentItemByItemId(DynamicContentDTO contentDTO)
        {
            var response = new
            {
                dynamicContentItem = DynamicContentController.GetDynamicContentItem(contentDTO.ItemId, contentDTO.ModuleId)
            }; 

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        //update
        [ValidateAntiForgeryToken]
        [DnnModuleAuthorize(AccessLevel = SecurityAccessLevel.Edit, PermissionKey = "EDITCONTENT")]
        [HttpPost]
        public HttpResponseMessage UpdateDynamicContentItem(UpdateDynamicContentDTO contentDTO)
        {
            var response = new
            {
                ItemId = contentDTO.ItemId
            };

            var dynamicContentItem = DynamicContentController.GetDynamicContentItem(contentDTO.ItemId, contentDTO.ModuleId);

            if (dynamicContentItem != null)
            {
                dynamicContentItem.Title = contentDTO.Title;
                dynamicContentItem.Content = contentDTO.Content;
                dynamicContentItem.ShortDescription = contentDTO.ShortDescription;
                dynamicContentItem.Keywords = contentDTO.Keywords;
                dynamicContentItem.Rank = contentDTO.Rank;

                DynamicContentController.SaveDynamicContentItem(dynamicContentItem);

                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            
            return Request.CreateResponse(HttpStatusCode.NotFound, response);
        }

        //delete
        [ValidateAntiForgeryToken]
        [DnnModuleAuthorize(AccessLevel = SecurityAccessLevel.Edit, PermissionKey = "EDITCONTENT")]
        [HttpPost]
        public HttpResponseMessage RemoveDynamicContentItem(DynamicContentDTO contentDTO)
        {
            DynamicContentController.DeleteDynamicContentItem(contentDTO.ModuleId, contentDTO.ItemId);
            
            var response = new
            {
                ItemId = contentDTO.ItemId
            };

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
        
        #endregion
    }
}