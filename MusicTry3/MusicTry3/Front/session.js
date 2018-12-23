$(document).ready(function () {

    var sessionId = GetURLParameter('sessionId');
    var username = GetURLParameter('username');

    setupPage(sessionId, username);

    $('#submitPlaylistForm').submit(function (e) {
        e.preventDefault();
        $.ajax({
            url: '/api/session/' + sessionId + "/playlist/",
            type: 'post',
            data: $('#submitPlaylistForm').serialize(),
            success: function (data) {
                window.location.assign("/session/playlist?sessionId=" + sessionId + "&playlistId=" + data.id +"&username=" + username);
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

function createRowForPlaylistTable(playlist, sessionId, username) {
    return '<tr><td><a href="/session/playlist?sessionId=' + sessionId + '&playlistId=' + playlist.spotifyPlaylist.id + '&username=' + username +'">' + playlist.spotifyPlaylist.name + '</a></td></tr>';
}