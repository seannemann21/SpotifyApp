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

<script src="/Front/landing.js"></script>
<script src="/Front/keep-alive.js"></script>

    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1">

    <title>Home</title>
</head>
<body>
    <div class="container-fluid">
        <div class="row">
            <a href="https://accounts.spotify.com/authorize/?response_type=code&client_id=fffa7e259c734e9d9b681b1fbf07f2f9&scope=user-read-private%20user-read-playback-state%20playlist-modify-public%20user-read-email&redirect_uri=https%3A%2F%2F53cee2f3.ngrok.io%2Fhome%2F"><button class="btn btn-primary">Create New Room</button></a>
        </div>
        <div class="row">
            <form id="joinRoom" action="/api/session/" method="get">
               <div>
                 <label for="session">Room:</label>
                 <input type="text" class="form-control" id="session" name="session">
               </div>
               <h6><span id="roomError" style="display: none" class="badge badge-danger">Room Not Found</span></h6>
               <button type="submit" class="btn btn-primary">Submit</button>
             </form>
        </div>
    </div>
</body>
</html>