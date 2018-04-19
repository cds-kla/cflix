<!DOCTYPE html>
<html>
  <head>
    <meta charset="utf-8">
    <title></title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootswatch/3.3.7/cyborg/bootstrap.min.css">
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/typed.js/2.0.6/typed.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jquery/2.2.4/jquery.min.js"></script>
    <style media="screen">
      body{
        background-color: #191919;
        width: 90%;
      }
      .element{
        color:#00ff00
      }
    </style>
  </head>
  <body>
    <div class="row">
      <center><h1>The Eliott's Shop</h1></center>
    </div>
    <div class="row">
      
      <div id="menu" class="col-md-2">
        <div class="list-group">
          <a href="/contact.php" class="list-group-item">Contact</a>
          <a href="/about.php" class="list-group-item">A propos</a>
          <?php if(isset($_COOKIE['adminCookie']) && $_COOKIE['adminCookie']=="SGVsbG8sIGMnZXN0IFJlbWksIGFuY2llbiBkZSBsYSB0ZWFtIHNlY3UK"){ ?>
            <a href="/shop_administrator.php" class="list-group-item">Adminstration</a>
          <?php } ?>
        </div>
      </div>

      <div class="col-md-8">
