


$(function () {

var chatter = $.connection.chatter;

var addMessage = function (message) {
    var currentdate = new Date();
    var messate = currentdate.getHours() + ":" + currentdate.getMinutes()
    messate += ' ' + message + '\n';
    $('#chatarea').append(messate);
};

var enableChat = function () {
    $('#chatwindow').prop("disabled", false);
    $('#name').prop("disabled", true);
    $('#signin').prop("disabled", true);
    $('#signout').prop("disabled", false);
};

var disableChat = function () {
    $('#chatwindow').prop("disabled", true);
    $('#name').prop("disabled", false);
    $('#signin').prop("disabled", false);
    $('#signout').prop("disabled", true);
    $('#name').val('');
};

    var init = function () {

        $('#signin').click(function () {
            var name = $('#name').val();
            if (name.length < 4) {
                alert('username is not valid!');
                return;
            }

            chatter.server
                .signIn($.connection.hub.id, name);
        });
        $('#signout').click(function () {
            var name = $('#name').val();

            chatter.server
                .signOut($.connection.hub.id, name);
        });

        $('#messagesend').click(function () {
            var message = $('#message').val();
            if (message.length < 1) {
                alert('message is not valid!');
                return;
            }

            chatter.server
                .post($.connection.hub.id, message);
            $('#message').val('');
            $('#message').focus();
        });
    };


    $.extend(chatter.client, {
        userAdded: function () {
            enableChat();
        },
        postMessage: function (message) {
            addMessage(message);
        },
        userRemoved: function () {
            disableChat();
        }
    });

$.connection.hub.start()
    .then(init);
});