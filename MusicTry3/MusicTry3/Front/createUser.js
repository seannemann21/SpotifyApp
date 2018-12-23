$(document).ready(function () {
    var sessionId = GetURLParameter("sessionId");

    $('#submitUserForm').submit(function (e) {
        e.preventDefault();
        $.ajax({
            url: '/api/session/Createuser?username=' + $("#username").val() + '&sessionId=' + sessionId,
            type: 'put',
            success: function (data) {
                window.location.href("/session?sessionId=" + sessionId + "&username=" + $("#username").val());
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