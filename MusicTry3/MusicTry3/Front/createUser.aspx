﻿<!DOCTYPE html>
<html lang="en">
<head>
<!-- Latest compiled and minified CSS -->
<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.1.3/css/bootstrap.min.css">
    <link rel="stylesheet" href="/Front/general.css">
<style type = "text/css">
  body {
  background: url("/Resources/SmallCassette.jpg");
  }
  /*
.form-control::-webkit-input-placeholder {
  color: forestgreen;
}
.form-control:-ms-input-placeholder {
  color: forestgreen;
}
.form-control::-moz-placeholder {
  color: forestgreen;
  opacity: 1;
}
.form-control:-moz-placeholder {
  color: forestgreen;
  opacity: 1;
}
*/
</style>
<!-- jQuery library -->
<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>

<!-- Popper JS -->
<script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.3/umd/popper.min.js"></script>

<!-- Latest compiled JavaScript -->
<script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.1.3/js/bootstrap.min.js"></script>

<script src="https://cdn.jsdelivr.net/npm/js-cookie@2/src/js.cookie.min.js"></script>

<script src="/Front/createUser.js"></script>
<script src="/Front/keep-alive.js"></script>



    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1">

    <title>Home</title>
</head>
<body>
    <div class="container-fluid">
        <div class="row" style="justify-content:center; padding-top:13em">
            <div class="col">
          <div class="shadow-lg" style="width:20em;padding-top:1em;padding-bottom:1em;border:solid;border-color:white;border-radius:0.5em;background:white; margin:auto">
              <div class="row" style="padding:0.5em">
                  <div class="col">
             <form id="submitUserForm">
               <div class="form-group">
                 <input type="text" class="form-control" id="username" name="username" placeholder="Username">
               </div>
               <button type="submit" class="btn btn-default" style="width:100%">Enter</button>
             </form>
              </div>
                  </div>
                <div class="row" style="padding:0.5em; padding-bottom:0em">
                    <div class="col">
                        <h6><span id="userError" style="display: none">User Already Exists</span></h6>
                    </div>
                </div>
          </div>
                </div>
        </div>

    </div>
</body>
</html>