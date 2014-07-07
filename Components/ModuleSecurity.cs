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

using DotNetNuke.Entities.Modules;
using DotNetNuke.Security.Permissions;
using DotNetNuke.UI.Modules;

namespace DotNetNuke.Modules.DNNDynamicContentModule.Components
{
    public class ModuleSecurity
    {
        readonly bool _hasEditContentPermission;

        public ModuleSecurity(int moduleId, int tabId)
        {
            var moduleController = new ModuleController();
            var moduleInfo = moduleController.GetModule(moduleId, tabId);
            if (moduleInfo == null) return;
            var mp = moduleInfo.ModulePermissions;

            _hasEditContentPermission = ModulePermissionController.HasModulePermission(mp, "EDITCONTENT");

        }

        public ModuleSecurity(ModuleInstanceContext context): this(context.ModuleId, context.TabId)
        {
        }

        public bool IsAllowedToEditContent()
        {
            return _hasEditContentPermission;
        }
    }
}