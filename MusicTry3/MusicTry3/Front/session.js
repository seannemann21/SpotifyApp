$(document).ready(function () {
    var sessionId = Cookies.get("sessionId");
    var username = Cookies.get("username");
    var userstatus = Cookies.get("userstatus");

    setupPage(sessionId, username, userstatus);
    

    $('#createPlaylistButton').click(function (e) {
        e.preventDefault();
        $.ajax({
            url: '/api/session/' + sessionId + "/playlist/",
            type: 'post',
            data: "=" + $('#name').val(),
            success: function (data) {
                window.location.assign("/session/playlist?playlistId=" + data.id);
            },
            error: function (e) {

            }
        });
    });

    $('#exitSession').click(function (e) {
        e.preventDefault();
        if (userstatus == "master") {
            $("#endSessionModal").modal('show');
        } else {
            exitSession();
        }
    });

    $('#endSession').click(function (e) {
        e.preventDefault();
        exitSession();
    });
});

function exitSession() {
    Cookies.remove("username");
    Cookies.remove("userstatus");
    Cookies.remove("sessionId");
    Cookies.remove("keep-alive");
    window.location.assign("/home");
}

function setupPage(sessionId, username, userstatus) {
    $("#sessionName").text(sessionId);

    if (userstatus !== "master") {
        $("#name").prop('disabled', true);
        $("#createPlaylistButton").prop('disabled', true);
    }

    $.ajax({
        url: '/api/session/' + sessionId + "/playlist/",
        type: 'get',
        success: function (data) {
            for (var i in data) {
                $('#playlistTableBody').append(createRowForPlaylistTable(data[i], sessionId, username));
            }
        },
        error: function () {
            alert("Session no longer available, redirecting");
            setTimeout(function () { exitSession() }, 1000);
        }
    });
}

function createRowForPlaylistTable(playlist, sessionId, username) {
    return '<tr><td><a href="/session/playlist?sessionId=' + sessionId + '&playlistId=' + playlist.spotifyPlaylist.id + '&username=' + username +'">' + playlist.spotifyPlaylist.name + '</a></td></tr>';
}