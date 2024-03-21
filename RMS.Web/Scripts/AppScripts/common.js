var AMS = new function () {
    var self = this;
    var requseturl = "";
    self.AjaxCompleted = function () {
        $("form:not(.submit-allow)").unbind("submit").bind("submit", self.ValidateForm);
        $("form.submit-allow").unbind("submit").bind("submit", self.FormSubmit);
        jQuery.validator.unobtrusive.parse(document);
        self.GridInitialize();       
        // $("input[type='date']").datepicker();
        $("input[type='date']").datepicker($.datepicker.regional["en-US"]).datepicker("option", {
            dateFormat: 'dd/mm/yy',
            yearRange: '1920:' + new Date().getFullYear(),
            changeMonth: true,
            changeYear: true,
        });

        $(".pagination a[href], .webgrid th a[href]").unbind("click").bind("click", function (e) {
            $("body").showLoading();
        });
        $(".AjaxPopup").unbind('click').bind('click', self.AddNewClick);
        $(".SubmitButton").unbind('click').bind('click', self.SubmitPartialChange);
        $('.searchAction').unbind("click").bind("click", function (e) {
            $(this).searchAction();
        });
      
    };

    self.FormSubmit = function(e) {
        var form = $(this);

        $(".validation-summary-errors li", form).remove();

        var ignoreValidation = form.hasClass("ignore-validation");
        if (!ignoreValidation) {
            if (jQuery.validator && jQuery.validator.unobtrusive) {
                form.validate();
                if (!form.valid()) {
                    e.preventDefault();
                    return false;
                }
            }
        }
        $("body").showLoading();
    };
    self.SubmitPartialChange = function (e) {
        var button = $(this);
        var form = button.parents("form:first");
        form.Post({
            success: function (Jsondata) {
                if (Jsondata.Success) {
                    alert("Data Saved Successfully");
                } else {
                    if (typeof (Jsondata.Success) != "undefined") {
                        alert("Unable to save data");
                    }
                }
            }
        });
    };
    self.ValidateForm = function (e) {
        var form = $(this);
        
        $(".validation-summary-errors li", form).remove();

        var ignoreValidation = form.hasClass("ignore-validation");
        if (!ignoreValidation) {
            if (jQuery.validator && jQuery.validator.unobtrusive) {
                form.validate();
                if (!form.valid()) {
                    e.preventDefault();
                    return false;
                }
            }
        }
        e.preventDefault();
        return false;
    };

    self.SearchInitialize = function () {
        $(".search").unbind("click").bind("click", function (e) {
            var button = $(this);
            var form = button.parents("form:first");
            //var data = form.SerializeData();
            form.submit();
            e.preventDefault();
            return false;
        });
    };

    self.GridInitialize = function () {

        $('button.new-window', '.webgrid').unbind('click').bind('click', self.GridShowInNewWindow);

        $('button.edit, button.view', '.webgrid').unbind('click').bind('click', self.GridShowInPopup);

        $('button.delete', '.webgrid').unbind('click').bind('click', self.GridDeleteClick);
       $('a.deletechartofAcc').unbind('click').bind('click', self.GridDeleteClick);
        $("button.add-new", 'ul.search').unbind('click').bind('click', self.AddNewClick);         

        $("button.action", '.webgrid').unbind('click').bind('click', self.GridActionButtonClick);

        $(".page", '.webgrid').unbind('click').bind('click', function (e) {
            location.href = $(this).action();
            $("body").showLoading();
        });

        /*
        $('th a', '.webgrid').unbind('click').bind('click', function () {
            $("body").showLoading();
        });*/

        var grid = $('.webgrid');
        var sort =  (grid.attr("sort") || "none").toLowerCase();
        var sortdir = (grid.attr("sortdir") || "none").toLowerCase();

        var sortColumn = grid.find("tbody td." + sort + ":first").index();
        if (sortColumn >= 0)
        {
            grid.find("thead th:eq(" + sortColumn + ")").addClass(sortdir);
        }
    };

    self.GridShowInNewWindow = function (e) {
        var action = $(this).attr("action");

        var feature = "height=" + $(window).height() + ", width=" + $(window).width() + "";
        if ($(this).hasClass("pdf")) {
            feature = "height=800,width=600,scrollbars=1,resizable=1,location=0,left=0";
        }
        var popup = window.open(action, "_blank", feature);
        if (popup && !$(this).hasClass("pdf")) {
            try {
                popup.moveTo(0, 0);
                popup.resizeTo(screen.width, screen.height);
            }
            catch (ex) { }
        }
        e.preventDefault();
        return false;
    };

    self.GridShowInPopup = function () {
        var action = $(this).attr("action");
        jQuery.Ajax({
            url: action
            , type: "GET"
            , dialog: {}
            , success: self.DialogOpened
        });
    };

    self.AddNewClick = function (e) {
        var action = $(this).attr("action");
      
        if (action) {
            jQuery.Ajax({
                url: action
                , type: "GET"
                , dialog: {}
                , loader: { loader: "body" }
                , success: self.DialogOpened
            });
        }
        e.preventDefault();
        return false;
    };

    self.DialogOpened = function (r) {
        $(":submit", ".dialog-ontainer").unbind("click").bind("click", function (e) {
            var button = $(this);
            var form = button.parents("form:first");
            form.Post({
                success: function (r) {
                    if (r.Success) {
                        button.DialogClose();
                    }
                    else {
                        button.parents(".ui-dialog-content:first").html(r.Content);
                        self.DialogOpened(r);
                    }
                }
            });
        });
    };

    self.GridDeleteClick = function (e) {
      
        var action = $(this).attr("action");
  
        jQuery.Confirm(AMS.Messages.DeleteConfirmation, function (r) {
            if (r) {
                jQuery.Ajax({
                    url: action
                    , loader: {}
                    , success: function (r) {
                    }
                });
            }
        });
        e.preventDefault();
        return false;
    };

    self.GridActionButtonClick = function (e)
    {
        var action = $(this).attr("action");
        jQuery.Ajax({
            url: action            
            , loader: {}
            , success: function (r) {
            }
        });
        e.preventDefault();
        return false;
    };

    self.ForgotPasswordLoad = function(r) {
        $("input[type='submit']", ".forgot-password-form").unbind("click").bind("click", function(e) {
            var button = $(this);
            var form = $(".forgot-password-form");
            form.Post({
                success: function(r) {
                    if (r.Success) {
                        button.DialogClose();
                        if (r.Message) {
                            alert(r.Message);
                        }
                    } else if (r.IsHtml) {
                        $(".ui-dialog-content").html(r.Content);
                        self.ForgotPasswordLoad(r);
                    }

                }
            });

            e.preventDefault();
            return false;
        });
    };
};

AMS.Messages = new function () {
    var self = this;

    self.DeleteConfirmation = "Do you want to delete?";

};
var app = {};
if (typeof(angular) != 'undefined') {
    app = angular.module('app', ['ui.bootstrap']);

    app.filter('startFrom', function () {
        return function (input, start) {
            start = +start; //parse to int
            return input.slice(start);
        }
    });
}
$(document).ready(function () {

    $("body").hideLoading();

    $(document).ajaxComplete(AMS.AjaxCompleted);
    AMS.AjaxCompleted();
    AMSAjaxOptions.Dialog.Hide = "explode";
    AMSAjaxOptions.Dialog.Show = "puff";

    $(".logoff").click(function (e) {
        var form = $("#logoutForm");
        form.submit();
        e.preventDefault();
        return false;
    });

    $(".forgot-password").unbind("click").bind("click", function (e) {
        jQuery.Ajax({
            url: "/Account/ForgotPassword"
            , type: "GET"
            , dialog: {}
            , success: AMS.ForgotPasswordLoad
        });
        e.preventDefault();
        return false;
    });
});
function search(url, formClass) {

    if (url != undefined && url != "") {
        requseturl = url;
        jQuery.Ajax({
            url: requseturl
               , type: "POST",
            data: $('.' + formClass + '').serialize()
               , container: '#contentRight'
        });
    } else {
        requseturl = "";
    }

}
var url = '';
function loadAction(url) {
    if (url != undefined && url != "") {
        requseturl = url;
        jQuery.Ajax({
            url: url
            , container: '#contentRight'
        });
        
    } else {
        requseturl = "";
    }

}

//Merchandising 

$('.ajaxLoad').click(function () {
    var url = $(this).attr('action');
    loadAction(url);
});

(function ($) {
    $.fn.searchAction = function (e) {
        var form = $(this).parents("form:first");
        var url = form.attr('action');
        var data = form.serialize();
        var ignoreValidation = form.hasClass("ignore-validation");
        if (!ignoreValidation) {
            if (jQuery.validator && jQuery.validator.unobtrusive) {
                form.validate();
                if (!form.valid()) {
                    // e.preventDefault();
                    return false;
                }
                SearchResult(url, data);
            }
        } else {
            SearchResult(url, data);
        }
    };
    $.fn.toggleHighlight = function () {
        $(this).toggleClass("highlight");
        //$(this).effect("highlight", {}, 3000);
    };

    $.fn.DialogClose = function(){
        var dialogWindow = $(this).parents(".ui-dialog-content:first");
        $("iframe", dialogWindow).attr("src", "about:blank"); // IE9 fix
        dialogWindow.dialog("close");
    };

    $.fn.action = function () {
        return $(this).attr("action") || "";

    };
    
}(jQuery));
function SearchResult(url, data) {
    $("form").showLoading();
    if (url != undefined && url != "") {
        requseturl = url;
        jQuery.Ajax({
            url: requseturl
               , type: "GET",
            data: data
               , container: '#contentRight'
        });
    } else {
        requseturl = "";
    }
}
