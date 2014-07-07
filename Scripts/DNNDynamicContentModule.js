var dnnDynamicContent = dnnDynamicContent || {}; //my namespace
var ko = ko || {}; //ko namespace

$(function () {
    dnnDynamicContent.vm = function() {
        var self = this;
        
        self.DynamicContentItem = function () {
            this.ModuleID = ko.observable(-1);
            this.ItemID = ko.observable(-1);
            this.Title = ko.observable("");
            this.Content = ko.observable("");
            this.ShortDescription = ko.observable("");
            this.Image = ko.observable("");
            this.Keywords = ko.observable("");
            this.QueryStringName = ko.observable("");
            this.QueryStringValue = ko.observable("");
            this.Rank = ko.observable(-1);
        },
        self.openAdminPanel = function(e, me) {
            e.preventDefault();
            toggleOpenCloseLinks($(me).closest(".dynamic-content-admin"));
            $(me).closest(".dynamic-content-admin").find(".dynamic-content-inner-admin").show();
        },
        self.closeAdminPanel = function(e, me) {
            e.preventDefault();
            toggleOpenCloseLinks($(me).closest(".dynamic-content-admin"));
            $(me).closest(".dynamic-content-admin").find(".dynamic-content-inner-admin").hide();
        },
        self.toggleOpenCloseLinks = function(panel) {
            $(panel).find(".open-admin-panel").toggle();
            $(panel).find(".close-admin-panel").toggle();
        },
        self.EditDynamicContentItemCallback = function(json) {
            var data = new self.DynamicContentItem()
                .ItemID(json.ItemID)
                .ModuleID(json.ModuleID)
                .Title(json.Title)
                .Content(json.Content)
                .ShortDescription(json.ShortDescription)
                .Image(json.Image)
                .Keywords(json.Keywords)
                .QueryStringName(json.QueryStringName)
                .QueryStringValue(json.QueryStringValue)
                .Rank(json.Rank);

            self.editingDynamicContentItem(data);
            self.openDynamicContentItemModal(true);
            self.addOrEditMode("edit");
        },
        self.editDynamicContentItem = function(e, me, moduleId, itemId) {
            e.preventDefault();
            self.getDynamciContentItemService(itemId, moduleId, self.EditDynamicContentItemCallback);
        },
        self.removeDynamicContentItem = function(e, me, moduleId, itemId) {
            e.preventDefault();
            var r = confirm("You're about to remove this dynamic content item. Are you sure you would like to do this?");
            if (r == true) {
                self.removeDynamicContentItemService(itemId, moduleId, self.dynamicContentItemCallback);
                me.closest("li").hide();
            }
        },
        self.addDynamicContentItem = function(e, me, moduleId, queryStringName, queryStringValue) {
            e.preventDefault();

            var data = new self.DynamicContentItem();
            data.ItemID(-1);
            data.ModuleID(moduleId);
            data.Title("");
            data.Content("");
            data.ShortDescription("");
            data.Image("");
            data.Keywords("");
            data.QueryStringName(queryStringName);
            data.QueryStringValue(queryStringValue);
            data.Rank(1);

            self.editingDynamicContentItem(data);
            self.openDynamicContentItemModal(true);
            self.addOrEditMode("add");
        },
        self.loadDynamicContentAdmin = function() {
            //custom binding to initialize a jQuery UI dialog
            ko.bindingHandlers.jqDialog = {
                init: function (element, valueAccessor) {
                    var options = ko.utils.unwrapObservable(valueAccessor()) || {};

                    //handle disposal
                    ko.utils.domNodeDisposal.addDisposeCallback(element, function () {
                        $(element).dialog("destroy");
                    });

                    $(element).dialog(options);
                    $(element).parent().addClass("dnnClear dnnFormPopup");
                }
            };

            //custom binding handler that opens/closes the dialog
            ko.bindingHandlers.openDialog = {
                update: function (element, valueAccessor) {
                    var value = ko.utils.unwrapObservable(valueAccessor());
                    if (value) {
                        if ($('#enableTitle').val() == "True") {
                            $('.dynamic-content-title-field').show();
                        }
                        
                        if ($('#enableMetaTags').val() == "True") {
                            $('.dynamic-content-short-description-field').show();
                            $('.dynamic-content-keywords-field').show();
                        }
                        
                        $(element).dialog("open");
                    } else {
                        $(element).dialog("close");
                    }
                }
            };
        },
        self.addOrEditMode = ko.observable(""),
        self.openDynamicContentItemModal = ko.observable(false),
        self.editingDynamicContentItem = ko.observable(new self.DynamicContentItem()),
        self.dynamicContentItemCallback = function(json) {
            //alert(json.retVal);
            location.reload(true);
        },
        self.acceptDynamicContentItem = function(data) {
            var mode = self.addOrEditMode();

            if (mode == "add") {
                self.addDynamicContentItemService(data.ModuleID(), data.Title(), data.Content(), data.ShortDescription(), data.Keywords(), data.QueryStringName(), data.QueryStringValue(), data.Rank(), self.dynamicContentItemCallback);
            } else {
                self.updateDynamicContentItemService(data.ItemID(), data.ModuleID(), data.Title(), data.Content(), data.ShortDescription(), data.Keywords(), data.Rank(), self.dynamicContentItemCallback);
            }

            self.addOrEditMode("");
            self.openDynamicContentItemModal(false);
        },
        self.cancelDynamicContentItem = function() {
            self.addOrEditMode("");
            self.openDynamicContentItemModal(false);
        },
        self.addDynamicContentItemService = function (moduleId, title, content, shortDescription, keywords, queryStringName, queryStringValue, rank, callback) {
            var json = {};
            json.moduleId = moduleId;
            json.title = title;
            json.content = content;
            json.shortDescription = shortDescription;
            json.keywords = keywords;
            json.queryStringName = queryStringName;
            json.queryStringValue = queryStringValue;
            json.rank = rank;

            var sf = new $.dnnSF(moduleId);
            var serviceUrl = sf.getServiceRoot("DNNDynamicContentModule") + "Service/AddDynamicContentItem";
            
            $.ajax({
                url: serviceUrl,
                type: "POST",
                data: ko.toJSON(json),
                beforeSend: sf.setModuleHeaders,
                dataType: "json",
                contentType: "application/json",
                success: function (msg) {
                    callback(msg);
                }
            });
        },
        self.getDynamciContentItemService = function (itemId, moduleId, callback) {
            var json = {};
            json.itemId = itemId;
            json.moduleId = moduleId;

            var sf = new $.dnnSF(moduleId);
            var serviceUrl = sf.getServiceRoot("DNNDynamicContentModule") + "Service/GetDynamicContentItemByItemId";
            
            $.ajax({
                url: serviceUrl,
                type: "POST",
                data: ko.toJSON(json),
                beforeSend: sf.setModuleHeaders,
                dataType: "json",
                contentType: "application/json",
                success: function (msg) {
                    callback(msg.dynamicContentItem);
                }
            });
        },
        self.updateDynamicContentItemService = function (itemId, moduleId, title, content, shortDescription, keywords, rank, callback) {
            var json = {};
            json.itemId = itemId;
            json.moduleId = moduleId;
            json.title = title;
            json.content = content;
            json.shortDescription = shortDescription;
            json.keywords = keywords;
            json.rank = rank;

            var sf = new $.dnnSF(moduleId);
            var serviceUrl = sf.getServiceRoot("DNNDynamicContentModule") + "Service/UpdateDynamicContentItem";
            
            $.ajax({
                url: serviceUrl,
                type: "POST",
                data: ko.toJSON(json),
                beforeSend: sf.setModuleHeaders,
                dataType: "json",
                contentType: "application/json",
                success: function (msg) {
                    callback(msg);
                }
            });
        },
        self.removeDynamicContentItemService = function (itemId, moduleId, callback) {
            var json = {};
            json.itemId = itemId;
            json.moduleId = moduleId;

            var sf = new $.dnnSF(moduleId);
            var serviceUrl = sf.getServiceRoot("DNNDynamicContentModule") + "Service/RemoveDynamicContentItem";
            
            $.ajax({
                url: serviceUrl,
                type: "POST",
                data: ko.toJSON(json),
                beforeSend: sf.setModuleHeaders,
                dataType: "json",
                contentType: "application/json",
                success: function (msg) {
                    callback(msg);
                }
            });
        };

        return {
            openAdminPanel: openAdminPanel,
            closeAdminPanel: closeAdminPanel,
            loadDynamicContentAdmin: loadDynamicContentAdmin,
            removeDynamicContentItem: removeDynamicContentItem,
            addDynamicContentItem: addDynamicContentItem,
            editingDynamicContentItem: editingDynamicContentItem,
            editDynamicContentItem: editDynamicContentItem,
            acceptDynamicContentItem: acceptDynamicContentItem,
            cancelDynamicContentItem: cancelDynamicContentItem
        };
    }();

    dnnDynamicContent.vm.loadDynamicContentAdmin();
    ko.applyBindings(dnnDynamicContent.vm, document.getElementById('dynamicContentItem'));
});