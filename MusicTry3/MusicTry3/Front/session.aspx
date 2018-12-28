<!DOCTYPE html>
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
<script src="Front/keep-alive.js"></script>

    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1">

    <title>Session</title>
</head>
<body>
    <div class="container-fluid">
        <div class="row">
            <div class="col-sm-12">
                <h3 id="sessionName"></h3>
            </div>
        </div>
        <div class="row">
          <div class="col-sm-9">
             <div class="form-inline">
               <div class="form-group">
                 <label for="name">Playlist Name:</label>
                 <input type="text" class="form-control" id="name" name="name">
               </div>
               <button id="createPlaylistButton" type="submit" class="btn btn-primary">Create</button>
             </div>
          </div>
          <div class="col-sm-3">
            <button id="exitSession" class="btn btn-primary">Exit Session</button>
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
    <!-- Modal -->
      <div class="modal fade" id="endSessionModal" role="dialog">
        <div class="modal-dialog">
    
          <!-- Modal content-->
          <div class="modal-content">
            <div class="modal-header">
              <button type="button" class="close" data-dismiss="modal">&times;</button>
            </div>
            <div class="modal-body">
              <p>Are you sure you want to exit the session? Since you created this sesssion, exiting it will end the session.</p>
            </div>
            <div class="modal-footer">
              <button id="endSession" class="btn btn-default">End Session</button>
            </div>
          </div>
      
        </div>
      </div>
</body>
</html>