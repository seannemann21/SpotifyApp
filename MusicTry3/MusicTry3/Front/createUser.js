$(document).ready(function () {
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