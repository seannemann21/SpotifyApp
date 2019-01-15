$(document).ready(function () {
    if (Cookies.get("sessionId") != null && Cookies.get("userstatus") != null && Cookies.get("username") != null) {
        window.location.assign("/session");
    } else {
        Cookies.remove("username");
        Cookies.remove("userstatus");
        Cookies.remove("sessionId");
        Cookies.remove("keep-alive");
    }

    $('#joinRoom').submit(function (e) {
        e.preventDefault();
        $.ajax({
            url: '/api/session/' + $('#session').val(),
            type: 'get',
            success: function () {
                Cookies.set("sessionId", $('#session').val(), { path: '/home' });
                Cookies.set("userstatus", "regular", { path: '/home' });
                window.location.assign("/createUser", { path: '/home' });
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