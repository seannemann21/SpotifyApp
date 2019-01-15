<!DOCTYPE html>
<html lang="en">
<head>
<!-- Latest compiled and minified CSS -->
<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.1.3/css/bootstrap.min.css">

<style type = "text/css">
  .btn{
      background-color: #ffc107;
      border-color: #ffc107;
      color:white;
  }
  .form-control::-webkit-input-placeholder { /* Chrome */
  color: #ffc107;
}
.form-control:-ms-input-placeholder { /* IE 10+ */
  color: #ffc107;
}
.form-control::-moz-placeholder { /* Firefox 19+ */
  color: #ffc107;
  opacity: 1;
}
.form-control:-moz-placeholder { /* Firefox 4 - 18 */
  color: #ffc107;
  opacity: 1;
}
a{
    color: #ffc107;
}
</style>

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
        <div class="row" style="padding-top:1em">
            <div class="col">
                <span class="h3">Room Name: <span id="sessionName"></span></span>
                <button id="exitSession" class="btn btn-primary" style="float:right">Leave</button>
            </div>
        </div>
        <div class="row">
          <div class="col">
             <div class="form-inline">
               <div class="form-group" style="margin-bottom:0">
                 <input type="text" class="form-control" id="name" name="name" placeholder="Playlist Name">
               </div>
               <button id="createPlaylistButton" type="submit" class="btn btn-primary" style="margin:1em">Create</button>
             </div>
            </div>
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