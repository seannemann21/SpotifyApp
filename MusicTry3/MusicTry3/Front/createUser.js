$(document).ready(function () {
    if (Cookies.get("sessionId") != null && Cookies.get("userstatus") != null && Cookies.get("username") != null) {
        window.location.assign("/session");
    }

    var sessionId = Cookies.get("sessionId");


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