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
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using DotNetNuke.Framework;
using DotNetNuke.Web.Client;
using DotNetNuke.Web.Client.ClientResourceManagement;
using DotNetNuke.Modules.DNNDynamicContentModule.Components;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using Globals = DotNetNuke.Common.Globals;

namespace DotNetNuke.Modules.DNNDynamicContentModule
{
    public partial class View : DNNDynamicContentModuleModuleBase, IActionable
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                var moduleSecurity = new ModuleSecurity(ModuleContext);

                if (moduleSecurity.IsAllowedToEditContent())
                {
                    ClientResourceManager.RegisterScript(Page, "~/Resources/Shared/scripts/knockout.js", FileOrder.Js.jQuery);
                    ClientResourceManager.RegisterScript(Page, "/DesktopModules/DNNDynamicContentModule/Scripts/DNNDynamicContentModule.js", 10);

                    ServicesFramework.Instance.RequestAjaxScriptSupport();
                    ServicesFramework.Instance.RequestAjaxAntiForgerySupport();

                    adminModal.Visible = true;
                    LoadAdminControls();
                }

                if(EnableCanonicalLink)
                    RegisterCanonicalLink();
                
                LoadDynamicContent();
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        public ModuleActionCollection ModuleActions
        {
            get
            {
                var actions = new ModuleActionCollection
                    {
                        //{
                        //    GetNextActionID(), Localization.GetString("EditModule", LocalResourceFile), "", "", "",
                        //    EditUrl(), false, SecurityAccessLevel.Edit, true, false
                        //}
                    };
                return actions;
            }
        }


        #region private methods

        private void LoadDynamicContent()
        {
            var contentList = DynamicContentController.BuildDynamicContentItemListFromRequest(Request, ModuleId);
            
            var content = contentList.FirstOrDefault();

            if (content != null)
            {
                BuildOutput(content);

                if(EnableOpenGraph)
                    RegisterOpenGraphTags(content);

                if(EnableMetaTags)
                    RegisterPageMetaTags(content);
            }
        }

        private void LoadAdminControls()
        {
            litAdminControls.Text = @"<div class=""dynamic-content-admin"">" +
                                    @"<div class=""dynamic-content-admin-links""><a href=""#"" class=""open-admin-panel"" onClick=""dnnDynamicContent.vm.openAdminPanel(event, this);"">Open Admin</a></div>" +
                                    @"<div class=""dynamic-content-inner-admin"" style=""display:none;"">" +
                                    BuildAdminControls() +
                                    @"<div class=""dynamic-content-inner-admin-links""><a href=""#"" class=""close-admin-panel"" onClick=""dnnDynamicContent.vm.closeAdminPanel(event, this);"" style=""display:none;"">Close Admin</a></div>" +
                                    @"</div></div>";
        }

        private string BuildAdminControls()
        {
            var contentList = DynamicContentController.BuildDynamicContentItemListFromRequest(Request, ModuleId);
            var output = "<ul>";

            foreach (var item in contentList)
            {
                output += BuildAdminItem(item);
            }

            output += "</ul>";

            return output;
        }

        private string BuildAdminItem(DynamicContentItem item)
        {
            var output = "<li>name:&nbsp;" + item.QueryStringName + "&nbsp;value:&nbsp;" + item.QueryStringValue + "&nbsp;" + BuildAdminLinks(item) + "</li>";

            return output;
        }

        private string BuildAdminLinks(DynamicContentItem item)
        {
            string output;

            if (item.ItemID > 0)
            {
                output = @"<a href=""#"" onClick=""dnnDynamicContent.vm.editDynamicContentItem(event, this, " + item.ModuleID + @", " + item.ItemID + @");"">Edit</a>&nbsp;" +
                         @"<a href=""#"" onClick=""dnnDynamicContent.vm.removeDynamicContentItem(event, this, " + item.ModuleID + @", " + item.ItemID + @");"">Delete</a>";
            }
            else
            {
                output = @"<a href=""#"" onClick=""dnnDynamicContent.vm.addDynamicContentItem(event, this, " + ModuleId + @", '" + item.QueryStringName + @"', '" + item.QueryStringValue + @"');"">Add</a>";
            }

            return output;
        }

        private void BuildOutput(DynamicContentItem dynamicContentItem)
        {
            if (EnableOpenGraph)
            {
                if (EnableTitle)
                {
                    litOutput.Text = @"<div class=""dynamic-content"" itemscope itemtype=""http://schema.org/Enumeration""><a class=""dynamic-content-title-link"" href=""" + GetCanonicalLink() +
                                 @""" itemprop=""url""><h1 class=""dynamic-content-title"" itemprop=""name"">" + dynamicContentItem.Title +
                                 @"</h1></a><div class=""dynamic-content-content"" itemprop=""description"">" + dynamicContentItem.Content +
                                 @"</div></div>";
                }
                else
                {
                    litOutput.Text = @"<div class=""dynamic-content"" itemscope itemtype=""http://schema.org/Enumeration""><div class=""dynamic-content-content"" itemprop=""description"">" + dynamicContentItem.Content +
                                 @"</div></div>";
                }
            }
            else
            {
                if (EnableTitle)
                {
                    litOutput.Text = @"<div class=""dynamic-content""><h1 class=""dynamic-content-title"">" + dynamicContentItem.Title +
                                 @"</h1><div class=""dynamic-content-content"">" + dynamicContentItem.Content +
                                 @"</div></div>";
                }
                else
                {
                    litOutput.Text = @"<div class=""dynamic-content""><div class=""dynamic-content-content"">" + dynamicContentItem.Content +
                                 @"</div></div>";
                }
            }
        }

        private void RegisterPageMetaTags(DynamicContentItem dynamicContentItem)
        {
            ((DotNetNuke.Framework.CDefault)Page).Title += " > " + dynamicContentItem.Title;
            ((DotNetNuke.Framework.CDefault)Page).Description = dynamicContentItem.ShortDescription;
            ((DotNetNuke.Framework.CDefault)Page).KeyWords = dynamicContentItem.Keywords;
        }

        private void RegisterOpenGraphTags(DynamicContentItem dynamicContentItem)
        {
            var metaTags = new List<Literal>();
            metaTags.Add(new Literal { Text = @"<meta property=""og:title"" content=""" + dynamicContentItem.Title + @""" />" });
            metaTags.Add(new Literal { Text = @"<meta property=""og:type"" content=""website"" />" });
            metaTags.Add(new Literal { Text = @"<meta property=""og:url"" content=""" + GetCanonicalLink() + @""" />" });
            metaTags.Add(new Literal { Text = @"<meta property=""og:locale"" content=""en_US"" />" });
            metaTags.Add(new Literal { Text = @"<meta property=""og:description"" content=""" + dynamicContentItem.ShortDescription + @""" />" });
            metaTags.Add(new Literal { Text = @"<meta property=""og:site_name"" content=""" + PortalSettings.PortalName + @""" />" });
            
            var head = Page.FindControl("Head");

            if (head != null)
            {
                var stylePlaceHolder = head.FindControl("StylePlaceholder");
                var insertLocation = head.Controls.IndexOf(stylePlaceHolder);

                foreach (var literal in metaTags)
                {
                    head.Controls.AddAt(insertLocation, literal);
                    //head.Controls.Add(literal);
                }
            }
        }

        private void RegisterCanonicalLink()
        {
            //register canonical link
            var link = GetCanonicalLink();
            var cononicalLink = @"<link rel=""canonical"" href=""" + link + @"""/>";
            var cononicalLiteral = new Literal { Text = cononicalLink };
            var head = Page.FindControl("Head");

            if (head != null)
            {
                var stylePlaceHolder = head.FindControl("StylePlaceholder");
                var insertLocation = head.Controls.IndexOf(stylePlaceHolder);

                head.Controls.AddAt(insertLocation, cononicalLiteral);
            }
        }

        private DynamicContentItem GetMatchingDynamicContentItem()
        {
            var output = new DynamicContentItem();
            output.Rank = 0;

            foreach (var dynamicContentItem in DynamicContentController.GetDynamicContentItems(ModuleId))
            {
                foreach (var queryStringParam in DynamicContentController.GetQueryStringParams(Request))
                {
                    if (queryStringParam.QueryStringName.ToLower() == dynamicContentItem.QueryStringName.ToLower() &&
                        queryStringParam.QueryStringValue.ToLower() == dynamicContentItem.QueryStringValue.ToLower())
                    {
                        if (dynamicContentItem.Rank > output.Rank)
                        {
                            output = dynamicContentItem;
                        }
                    }
                }
            }

            return output;
        }

        private string GetCanonicalLink()
        {
            var baseQueryStrings = "";
            var queryStringParams = DynamicContentController.GetQueryStringParams(Request);

            foreach (var queryStringParam in queryStringParams)
            {
                baseQueryStrings += "&" + queryStringParam.QueryStringName + "=" + queryStringParam.QueryStringValue;
            }

            baseQueryStrings = Utilities.SafeInput(baseQueryStrings, false);

            return Globals.NavigateURL(TabId, "", baseQueryStrings);
        }

        #endregion
    }
}