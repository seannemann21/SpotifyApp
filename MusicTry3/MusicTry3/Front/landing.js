$(document).ready(function () {
    if (window.sessionStorage.getItem("sessionId") != null && window.sessionStorage.getItem("userstatus") != null && window.sessionStorage.getItem("username") != null) {
        window.location.assign("/session");
    } else {
        window.sessionStorage.removeItem("username");
        window.sessionStorage.removeItem("userstatus");
        window.sessionStorage.removeItem("sessionId");
        window.sessionStorage.removeItem("keep-alive");
    }

    $('#joinRoom').submit(function (e) {
        e.preventDefault();
        $.ajax({
            url: '/api/session/' + $('#session').val(),
            type: 'get',
            success: function () {
                window.sessionStorage.setItem("sessionId", $('#session').val());
                window.sessionStorage.setItem("userstatus", "regular");
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