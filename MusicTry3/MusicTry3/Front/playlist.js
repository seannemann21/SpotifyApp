$(document).ready(function () {
    var sessionId = Cookies.get("sessionId");
    var playlistId = GetURLParameter("playlistId");
    var username = Cookies.get("username");

    SetupPlaylistData(sessionId, playlistId, username);
    setInterval(function () {
        UpdatePlaylistData(sessionId, playlistId, username);
    }, 5000);

    $("#search").autocomplete({
        classes: {
            "ui-autocomplete": "songs-by-artist"
        },
        source: function (request, response) {
            $.ajax({
                url: '/api/spotify/search?query=' + request.term + '&sessionId=' + sessionId,
                type: 'get',
                success: function (tracks) {
                    var trackList = tracks.map(function (track) {
                        var trackObj = new Object();
                        trackObj.label = track.name + ": " + track.artists[0].name;
                        trackObj.value = track.uri;
                        return trackObj;
                    });
                    response(trackList);
                }
            })
        },
        delay: 2000,
        select: function (event, ui) {
            var artistAndName = ui.item.label.split(": ");
            var trackName = artistAndName[0];
            var trackArtist = artistAndName[1];
            var trackUri = ui.item.value;
            $.ajax({
                url: '/api/session/' + sessionId + '/playlist?sessionId=' + sessionId + '&playlistId=' + playlistId + '&trackUri=' + trackUri + '&name=' + trackName + '&artist=' + trackArtist,
                type: 'put',
                success: function (tracks) {
                    $("#search").val('');
                    UpdatePlaylistData(sessionId, playlistId, username);
                }
            })
        }
    });
});


function UpdatePlaylistData(sessionId, playlistId, username) {
    ClearPlaylistData();
    SetupPlaylistData(sessionId, playlistId, username);
}

function ClearPlaylistData() {
    $("#playlistQueueTableBody tr").remove();
    $("#onboardingTableBody tr").remove();
}

function SetupPlaylistData(sessionId, playlistId, name) {
    $.ajax({
        url: '/api/session/' + sessionId + '/playlist/' + playlistId,
        type: 'get',
        success: function (playlist) {
            var spotifyPlaylist = playlist.spotifyPlaylist;
            $('#playlistNameTitle').text(spotifyPlaylist.name);
            if (spotifyPlaylist.tracks != null) {
                if (spotifyPlaylist.tracks.items != null) {
                    var realTracks = spotifyPlaylist.tracks.items;
                    for (var i in realTracks) {
                        AddRowToQueueTable(realTracks[i].track);
                    }
                    for (var i in playlist.onBoardingSongs) {
                        AddRowToOnboardingTable(sessionId, playlistId, playlist.onBoardingSongs[i], name);
                    }
                }
            }
        },
        error: function () {
            window.location.assign("/home");
        }
    });
}

function AddRowToQueueTable(track) {
    var row = "<tr><td>" + track.name + "</td><td>" + track.artists[0].name + "</td></tr>";
    $("#playlistQueueTableBody").append(row);
}

function AddRowToOnboardingTable(sessionId, playlistId, onboardingSong, name) {
    var row = $('<tr />');
    row.append('<td>' + onboardingSong.name + '</td><td>' + onboardingSong.artist + '</td>');
    //var row = "<tr><td>" + onboardingSong.name + "</td><td>" + onboardingSong.artist + "</td><td>" + findVote(name, onboardingSong.votes) + "</td></tr> ";
    var starColumn = $('<td />');
    var stars = createStars(sessionId, playlistId, name, onboardingSong, findVote(name, onboardingSong.votes));
    starColumn.append(stars);
    row.append(starColumn);
    $("#onboardingTableBody").append(row);
}


function createStars(sessionId, playlistId, name, onboardingSong, rating) {
        var stars = [];
        for (i = 0; i < 5; i++) {
            var star = $('<span />').addClass('fa');
            star.addClass('fa-star');
            star.addClass(i.toString());
            if (i <= rating) {
                star.addClass('checked');
            }
            star.click(function () {
                for (i = 0; i < 5; i++) {
                    stars[i].removeClass('checked');
                }
                for (i = 0; i < 5; i++) {
                    stars[i].addClass('checked');
                    if ($(this).hasClass(i)) {
                        $.ajax({
                            url: '/api/session/' + sessionId + '/playlist?sessionId=' + sessionId + '&playlistId=' + playlistId + '&trackUri=' + onboardingSong.trackUri + '&rating=' + i + '&username=' + name,
                            type: 'put',
                            success: function () {
                            }
                        })
                        break;
                    }
                }
            });
            stars.push(star);
    }

    return stars;
}

function findVote(name, votes) {
    
    for (var i in votes) {
        if (votes[i].user.name == name) {
            return votes[i].score;
        }
    }
    
    return -1;
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