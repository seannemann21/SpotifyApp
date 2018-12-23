$(document).ready(function () {
    var code = GetURLParameter("code");
    if (code != null) {
        $.ajax({
            url: '/api/session?code=' + code,
            type: 'post',
            success: function (sessionId) {
                if (sessionId != null) {
                    window.location.assign("/createUser?userstatus=master&sessionId=" + sessionId);
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
                window.location.assign("/createUser?userstatus=regular&sessionId=" + $('#session').val());
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