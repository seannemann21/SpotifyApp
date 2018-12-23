﻿<!DOCTYPE html>
<html lang="en">
<head>
<!-- Latest compiled and minified CSS -->
<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.1.3/css/bootstrap.min.css">

<!-- jQuery library -->
<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>

<script src="https://code.jquery.com/ui/1.12.0/jquery-ui.min.js"></script>
<link rel="stylesheet" type="text/css" href="https://code.jquery.com/ui/1.12.0/themes/smoothness/jquery-ui.css">

<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">

<!-- Popper JS -->
<script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.3/umd/popper.min.js"></script>

<!-- Latest compiled JavaScript -->
<script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.1.3/js/bootstrap.min.js"></script>

<style>
.checked {
    color: gold;
}
</style>

<script src="../Front/playlist.js"></script>

    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1">

    <title>Playlist</title>
</head>
<body>
    <div class="container mt-3">
      <h2 id="playlistNameTitle">Playlist Name Title</h2>
      <br>
      <!-- Nav tabs -->
      <ul class="nav nav-tabs">
        <li class="nav-item">
          <a class="nav-link active" data-toggle="tab" href="#queue">Queue</a>
        </li>
        <li class="nav-item">
          <a class="nav-link" data-toggle="tab" href="#onboarding">Onboarding Songs</a>
        </li>
      </ul>

      <!-- Tab panes -->
      <div class="tab-content">
        <div id="queue" class="container tab-pane active"><br>
          <h3>Queue</h3>
          <table class="table table-striped">
            <thead>
              <tr>
                <th>Song</th>
                <th>Artist</th>
              </tr>
            </thead>
            <tbody id="playlistQueueTableBody">
            </tbody>
          </table>
        </div>
        <div id="onboarding" class="container tab-pane fade"><br>
            <div class="row">
                <div class="ui-widget">
                    <label for="search">Search: </label>
                        <input id="search">
                    </div>
            </div>
            <div class="row">
              <table class="table table-striped">
                <thead>
                  <tr>
                    <th>Song</th>
                    <th>Artist</th>
                    <th>Your Rating</th>
                  </tr>
                </thead>
                <tbody id="onboardingTableBody">
                </tbody>
              </table>
            </div>
        </div>
      </div>
    </div>
</body>
</html>