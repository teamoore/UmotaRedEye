(function ($) {
    $.tg = $.extend($.tg, {
        applicationPath: '/',
        alert: function (message, callback) {
            BootstrapDialog.alert({
                title: "Error",
                message: message,
                type: BootstrapDialog.TYPE_DANGER,
                closable: true,
                draggable: true,
                buttonLabel: 'OK',
                callback: function () {
                    setTimeout(function () { $('body').find('input[type=text],textarea,select').filter(':visible:first').focus(); }, 0);
                    if (callback) callback();
                }
            });

            document.getElementById('sound').play();
        },
        information: function (message, callback, delay) {

            var counter = parseInt(delay / 1000);
            var text = "";
            if (delay) {

                var interval = setInterval(function () {

                    if (text == "")
                        text = $(".bootstrap-dialog-footer-buttons").find("button").first().text();
                    counter -= 1;
                    $(".bootstrap-dialog-footer-buttons").find("button").first().text(text + " (" + counter + ")");

                }, 1000);
                setTimeout(function () {
                    $(".bootstrap-dialog").modal("hide");
                    clearInterval(interval);
                }, delay);
            }

            BootstrapDialog.show({
                message: message,
                title: "Information",
                buttons: [
                    {
                        label: 'Close',
                        action: function (dialogItself) {
                            dialogItself.close();
                        }
                    }
                ],
                onhidden: function () {
                    setTimeout(function () { $('body').find('input[type=text],textarea,select').filter(':visible:first').focus(); }, 0);
                    if (callback) callback();

                }
            });
        },
        confirm: function (message, callback, options) {
            BootstrapDialog.confirm({
                title: "Confirm",
                message: message,
                type: BootstrapDialog.TYPE_WARNING, // <-- Default value is BootstrapDialog.TYPE_PRIMARY
                closable: false, // <-- Default value is false
                draggable: true, // <-- Default value is false
                btnCancelLabel: 'Cancel', // <-- Default value is 'Cancel',
                btnOKLabel: 'Ok', // <-- Default value is 'OK',
                btnOKClass: 'btn-warning', // <-- If you didn't specify it, dialog type will be used,
                callback: function (result) {
                    setTimeout(function () { $('body').find('input[type=text],textarea,select').filter(':visible:first').focus(); }, 0);
                    if (result) {
                        options.onOk();
                    } else {
                        if (options.onCancel) options.onCancel();
                    }
                }
            });
        },
        setData: function (key, data) {
            localStorage.setItem(key, JSON.stringify(data));
        },
        getData: function (key) {
            return JSON.parse(localStorage.getItem(key));
        },
        removeData: function (key) {
            localStorage.removeItem(key);
        },
        getParameterValueByName: function (url, name) {

            if (!url)
                url = window.location.href;

            name = name.replace(/[\[\]]/g, "\\$&");

            var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)");

            var results = regex.exec(url);

            if (!results)
                return null;

            if (!results[2])
                return '';

            return decodeURIComponent(results[2].replace(/\+/g, " "));
        },
        focus: function (key) {
            setTimeout(function () { $("#" + key).focus(); }, 0);
        },
        disable: function (key, status) {
            $("#" + key).attr('disabled', status);
        },
        contains: function (a, obj) {
            var i = a.length;
            while (i--) {
                if (a[i] === obj) {
                    return true;
                }
            }
            return false;
        },
        disableObject: function (id) {

            var object = $(id);

            object.attr('disabled', 'disabled');
        },
        enabledObject: function (id) {
            var object = $(id);

            object.removeAttr('disabled');
        },
        playSuccess: function () {
            document.getElementById('soundSuccess').play();
        },
        playError: function () {
            document.getElementById('soundError').play();
        }
    });
})(jQuery);