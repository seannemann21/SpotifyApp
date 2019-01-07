$(document).ready(function () {
    var sessionId = Cookies.get("sessionId");
    var keepAlive = Cookies.get("keep-alive");

    keepSessionAlive(sessionId, keepAlive)

    setInterval(function () {
        keepSessionAlive(sessionId, keepAlive)
    }, 60000);
});

function keepSessionAlive(sessionId, keepAlive) {
    if (sessionId != null && keepAlive != null) {
        $.ajax({
            url: '/api/session/keepalive?sessionId=' + sessionId + '&keepAlive=' + keepAlive,
            type: 'put'
        });
    }
}