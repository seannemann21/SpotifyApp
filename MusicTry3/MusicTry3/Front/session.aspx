﻿<!DOCTYPE html>
<html lang="en">
<head>
<!-- Latest compiled and minified CSS -->
<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.1.3/css/bootstrap.min.css">

<!-- jQuery library -->
<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>

<!-- Popper JS -->
<script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.3/umd/popper.min.js"></script>

<!-- Latest compiled JavaScript -->
<script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.1.3/js/bootstrap.min.js"></script>

<script src="https://cdn.jsdelivr.net/npm/js-cookie@2/src/js.cookie.min.js"></script>

<script src="Front/session.js"></script>

    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1">

    <title>Session</title>
</head>
<body>
    <div class="container-fluid">
        <div class="row">
          <div class="col-sm-12">
             <form id="submitPlaylistForm" action="/api/session/" method="post" class="form-inline">
               <div class="form-group">
                 <label for="name">Playlist Name:</label>
                 <input type="text" class="form-control" id="name" name="name">
               </div>
               <button type="submit" class="btn btn-primary">Create</button>
             </form>
          </div>
        </div>
    </div>
    <div class="container-fluid">
        <div class="row">
          <div class="col-sm-12">
             <table class="table table-striped">
                <thead>
                  <tr>
                    <th>Name</th>
                  </tr>
                </thead>
                <tbody id="playlistTableBody">
                </tbody>
              </table>
          </div>
        </div>
    </div>
</body>
</html>