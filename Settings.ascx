<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Settings.ascx.cs" Inherits="DotNetNuke.Modules.DNNDynamicContentModule.Settings" %>
<%@ Register TagName="label" TagPrefix="dnn" Src="~/controls/labelcontrol.ascx" %>

	<h2 id="dnnSitePanel-BasicSettings" class="dnnFormSectionHead"><a href="" class="dnnSectionExpanded"><%=LocalizeString("BasicSettings")%></a></h2>
	<fieldset>
        <div class="dnnFormItem">
            <dnn:Label ID="lblEnableOpenGraph" runat="server" />
            <asp:CheckBox ID="cbEnableOpenGraph" runat="server" />
        </div>
        <div class="dnnFormItem">
            <dnn:Label ID="lblEnableMetaTags" runat="server" />
            <asp:CheckBox ID="cbEnableMetaTags" runat="server" />
        </div>
        <div class="dnnFormItem">
            <dnn:Label ID="lblEnableCanonicalLink" runat="server" />
            <asp:CheckBox ID="cbEnableCanonicalLink" runat="server" />
        </div>
        <div class="dnnFormItem">
            <dnn:Label ID="lblEnableTitle" runat="server" />
            <asp:CheckBox ID="cbEnableTitle" runat="server" />
        </div>
    </fieldset>