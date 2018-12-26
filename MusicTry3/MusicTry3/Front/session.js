$(document).ready(function () {
    var sessionId = Cookies.get("sessionId");
    var username = Cookies.get("username");

    setupPage(sessionId, username);

    $('#submitPlaylistForm').submit(function (e) {
        e.preventDefault();
        $.ajax({
            url: '/api/session/' + sessionId + "/playlist/",
            type: 'post',
            data: $('#submitPlaylistForm').serialize(),
            success: function (data) {
                window.location.assign("/session/playlist?playlistId=" + data.id);
            }
        });
    });
});

function setupPage(sessionId, username) {
    $.ajax({
        url: '/api/session/' + sessionId + "/playlist/",
        type: 'get',
        success: function (data) {
            for (var i in data) {
                $('#playlistTableBody').append(createRowForPlaylistTable(data[i], sessionId, username));
            }
        }
    });
}

function createRowForPlaylistTable(playlist, sessionId, username) {
    return '<tr><td><a href="/session/playlist?sessionId=' + sessionId + '&playlistId=' + playlist.spotifyPlaylist.id + '&username=' + username +'">' + playlist.spotifyPlaylist.name + '</a></td></tr>';
}