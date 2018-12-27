$(document).ready(function () {
    if (Cookies.get("sessionId") != null && Cookies.get("userstatus") != null && Cookies.get("username") != null) {
        window.location.assign("/session");
    }

    var code = GetURLParameter("code");
    if (code != null) {
        $.ajax({
            url: '/api/session?code=' + code,
            type: 'post',
            success: function (session) {
                if (session != null) {
                    Cookies.set("sessionId", session.id);
                    Cookies.set("userstatus", "master");
                    Cookies.set("keep-alive", session.keepAliveToken);
                    window.location.assign("/createUser");
                }
            }
        });
    }

    $('#joinRoom').submit(function (e) {
        e.preventDefault();
        $.ajax({
            url: '/api/session/' + $('#session').val(),
            type: 'get',
            success: function () {
                Cookies.set("sessionId", $('#session').val());
                Cookies.set("userstatus", "regular");
                window.location.assign("/createUser");
            },
            error: function () {
                $('#roomError').show();
            }
        });
    });

    $('#spotifyLogin').click(function (e) {

    });
});

function GetURLParameter(sParam) {
    var sPageURL = window.location.search.substring(1);
    var sURLVariables = sPageURL.split('&');
    for (var i = 0; i < sURLVariables.length; i++) {
        var sParameterName = sURLVariables[i].split('=');
        if (sParameterName[0] == sParam) {
            return decodeURIComponent(sParameterName[1]);
        }
    }
}