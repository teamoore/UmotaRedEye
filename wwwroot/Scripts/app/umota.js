(function ($) {
    $.umota = $.umota || {};
    $.umota = $.extend($.umota,
        {
            serviceError: function (error, callback) {
                $.tg.alert(GetMlText(error), callback);
                document.getElementById('soundError').play();
            },

            callService: function (options) {
                if (!navigator.onLine) {
                    $.tg.alert(GetMlText("NotOnline"));
                    return;
                }

                options = $.extend({
                    dataType: 'json',
                    contentType: 'application/json',
                    type: 'POST',
                    cache: false,
                    async: ((options.async != null) ? options.async : true),
                    url: options.service,
                    data: JSON.stringify(options.request),
                    success: function (response, status) {
                        try {
                            if (!response.Error) {
                                if (options.onSuccess)
                                    options.onSuccess(response);
                            } else if (options.onError) {

                                $.umota.serviceError(response.Error,function () { options.onError(response); } );
                            } else

                                $.umota.serviceError(response.Error);
                        } catch (e) {
                            var stacktrace;
                            if (e.stack)
                                stacktrace = e.stack;
                            else
                                try {
                                    throw "!";
                                } catch (e) {
                                    stacktrace = e.stack || e.stacktrace || "";
                                }
                            window.console && window.console.log(stacktrace || e.message || e);
                        }
                    },
                    error: function (xhr, status, e) {
                        try {
                            if (xhr.status == 403) {
                                var l = null;
                                try { l = xhr.getResponseHeader('Location'); } catch (e) { l = null; }
                                if (l) {
                                    top.location.href = l;
                                    return;
                                }
                            }
                            //http://stackoverflow.com/questions/377644/jquery-ajax-error-handling-show-custom-exception-messages
                            //TODO : mesajlar daha anlaşılır hale getirilebilir
                            $.tg.alert(xhr.responseText);
                        } catch (e) {
                            var stacktrace;
                            if (e.stack)
                                stacktrace = e.stack;
                            else try {
                                throw "!";
                            } catch (e) {
                                stacktrace = e.stack || e.stacktrace || "";
                            }
                            window.console && window.console.log(stacktrace || e.message || e);
                        }
                    },
                    complete: function() {
                        if (options.onComplete) {
                            options.onComplete();
                        }
                    }

                }, options);
                return $.ajax(options);
            }
        });
})(jQuery);



