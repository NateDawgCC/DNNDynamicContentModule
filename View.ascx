<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="View.ascx.cs" Inherits="DotNetNuke.Modules.DNNDynamicContentModule.View" %>
<link id="ModuleCSS" runat="server" type="text/css" rel="Stylesheet" href="Module.css" visible="false" />
<asp:Literal ID="litOutput" runat="server" />
<asp:Literal ID="litAdminControls" runat="server" />

<div id="adminModal" runat="server" Visible="False">
    <div id="dynamicContentItem" data-bind="jqDialog: { autoOpen: false, resizable: false, modal: true, title: 'Edit Dynamic Content Item', minWidth: 650 }, template: { name: 'editDynamicContentItemTmpl', data: editingDynamicContentItem }, openDialog: openDynamicContentItemModal">
    </div>

    <script id="editDynamicContentItemTmpl" type="text/html">
        <form id="manageDynamicContentItemForm">
            <div class="dnnClear dnnForm">
                <fieldset id="manageDynamicContentItemFieldset" class="validationGroup">
                    <input id="itemId" type="hidden" data-bind="value: ItemID"/>
                    <input id="moduleId" type="hidden" data-bind="value: ModuleID"/>
                    <input id="queryStringName" type="hidden" data-bind="value: QueryStringName"/>
                    <input id="queryStringValue" type="hidden" data-bind="value: QueryStringValue"/>
                    <input id="enableTitle" type="hidden" value='<%=EnableTitle %>'/>
                    <input id="enableMetaTags" type="hidden" value='<%=EnableMetaTags %>'/>
                    <div class="dnnFormItem dnnFormHelp dnnClear">
                        <p class="dnnFormRequired"><span>Indicates&nbsp;required&nbsp;fields</span></p>
                    </div>
	                <div class="dnnFormItem dynamic-content-title-field" style="display: none;">
		                <label title="Dynamic content item title" for="title">Title: </label>
		                <input id="title" type="text" class="" data-bind="value: Title" />
	                </div>
	                <div class="dnnFormItem">
		                <label title="Dynamic content item content" for="content">Content: </label>
		                <textarea id="content" rows="15" cols="55" class="" data-bind="value: Content" />
	                </div>
	                <div class="dnnFormItem dynamic-content-short-description-field" style="display: none;">
		                <label title="Dynamic content item short description" for="shortDescription">Short Description: </label>
		                <textarea id="shortDescription" rows="5" cols="55" class="" data-bind="value: ShortDescription" />
	                </div>
	                <div class="dnnFormItem dynamic-content-keywords-field" style="display: none;">
		                <label title="Dynamic content item keywords" for="keywords">Keywords: </label>
		                <input id="keywords" type="text" class="" data-bind="value: Keywords" />
	                </div>
	                <div class="dnnFormItem">
		                <label title="Dynamic content item rank" for="rank">Rank: </label>
		                <input id="rank" type="text" class="" data-bind="value: Rank" />
	                </div>
                    <ul class="dnnActions dnnClear">
                        <li>
                            <a data-bind="click: $root.acceptDynamicContentItem" class="dnnPrimaryAction" href="#">Save</a>
                        </li>
                        <li>
                            <a data-bind="click: $root.cancelDynamicContentItem" class="dnnSecondaryAction" href="#">Cancel</a>
                        </li>
                    </ul>
                </fieldset>
            </div>
        </form>
    </script>
</div>