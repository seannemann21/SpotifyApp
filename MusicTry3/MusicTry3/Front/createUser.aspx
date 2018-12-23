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

<script src="Front/createUser.js"></script>

    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1">

    <title>Home</title>
</head>
<body>
    <div class="container-fluid">
        <div class="row">
          <div class="col-sm-12">
             <form id="submitUserForm" action="/api/session/" method="post">
               <div class="form-group">
                 <label for="username">Username:</label>
                 <input type="text" class="form-control" id="username" name="username">
               </div>
               <button type="submit" class="btn btn-primary">Submit</button>
             </form>
          </div>
        </div>
    </div>
</body>
</html>