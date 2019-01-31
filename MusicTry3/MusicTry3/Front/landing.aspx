<!DOCTYPE html>
<html lang="en">
<head>
<!-- Latest compiled and minified CSS -->
<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.1.3/css/bootstrap.min.css">
<link rel="stylesheet" href="/Front/general.css">
<style type = "text/css">

body {
  background: url("/Resources/SmallCassette.jpg");
  }
    @font-face {
        font-family: titleFamily;
        src: url("/Resources/Gtoles.ttf");
    }
</style>
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
    <div class="container">
        <div class="row" style="justify-content:center; padding-top:8em;font-family:titleFamily">
            <div class="col" style="text-align: center;font-size: 6em;color: white;">
                We DJ
            </div>
        </div>
        <div class="row" style="justify-content:center; font-family:titleFamily">
            <div class="col" style="text-align: center;font-size: 3em;color: white;">
                Start Sharing Music Now
            </div>
        </div>
        <div class="row" style="padding-top:3em;">
            <div class="col">
            <div class="shadow-lg" style="width:20em;padding-top:1em;padding-bottom:1em;border:solid;border-color:white;border-radius:0.5em;background:white;margin:auto">
                <div>
                <div class="row" style="padding:0.5em">
                    <div class="col-sm-12">
                        <a href="https://accounts.spotify.com/authorize/?response_type=code&client_id=fffa7e259c734e9d9b681b1fbf07f2f9&scope=user-read-playback-state%20streaming%20user-read-birthdate%20user-read-email%20user-read-private%20playlist-modify-public%20user-modify-playback-state&redirect_uri=https%3A%2F%2F9fb8e750.ngrok.io%2FcreateUser%2F"><button style="width:100%" class="btn">Create New Room</button></a>
                    </div>
               </div>
                 <div class="row" style="padding:0.5em">
                   <div class="col">

                   <form id="joinRoom">
                     <input type="text" class="form-control" style="width:60%; display:inline-block" id="session" name="session" placeholder="Room">
                     <button type="submit" style="float:right; padding-left:2em;padding-right:2em" class="btn" >Join</button>
                   </form>
                         
                   </div>
                 </div>
                <div class="row" style="padding:0.5em; padding-bottom:0em">
                    <div class="col">
                        <h6><span id="roomError" style="display: none">Room Not Found</span></h6>
                    </div>
                </div>
               </div>
            </div>
                </div>
        </div>
            </div>
    </body>
</html>