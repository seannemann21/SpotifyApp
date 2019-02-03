$(document).ready(function () {
    var sessionId = window.sessionStorage.getItem("sessionId");
    var username = window.sessionStorage.getItem("username");
    var userstatus = window.sessionStorage.getItem("userstatus");

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

    $('.exit').click(function (e) {
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
    window.sessionStorage.removeItem("username");
    window.sessionStorage.removeItem("userstatus");
    window.sessionStorage.removeItem("sessionId");
    window.sessionStorage.removeItem("keep-alive");
    window.sessionStorage.removeItem("authentication-token");
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
        success: function (playlists) {
            for (var i in playlists) {
                $('#playlistTableBody').append(createRowForPlaylistTable(playlists[i], sessionId, username));
            }
        },
        error: function () {
            alert("Session no longer available, redirecting");
            setTimeout(function () { exitSession() }, 1000);
        }
    });
}

function createRowForPlaylistTable(playlist, sessionId, username) {
    var row = $('<tr />');
    row.append('<td><a>' + playlist.name + '</a></td>');
    row.click(function () {
        $.ajax({
            url: '/api/session/' + sessionId + "/playlist/" + playlistId + "/load",
            type: 'put',
            success: function () {
            }
        })
    });
    return '<tr><td><a href="/session/playlist?sessionId=' + sessionId + '&playlistId=' + playlist.id + '&username=' + username +'">' + playlist.name + '</a></td></tr>';
}