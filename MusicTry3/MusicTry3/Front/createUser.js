﻿$(document).ready(function () {
    if (Cookies.get("sessionId") != null && Cookies.get("userstatus") != null && Cookies.get("username") != null) {
        window.location.assign("/session");
    }
    var sessionId = Cookies.get("sessionId");
    if (sessionId == null) {
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
                    Cookies.set("authenticationToken", session.spotifyCredentials.accessToken);
                    sessionId = session.id;
                }
            }
        });
        }
    }


    $('#submitUserForm').submit(function (e) {
        e.preventDefault();
        $.ajax({
            url: '/api/session/Createuser?username=' + $("#username").val() + '&sessionId=' + sessionId,
            type: 'put',
            success: function (data) {
                Cookies.set("username", $("#username").val());
                window.location.assign("/session");
            }
        });
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