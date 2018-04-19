<?php if(isset($_COOKIE['adminCookie']) && $_COOKIE['adminCookie']=="SGVsbG8sIGMnZXN0IFJlbWksIGFuY2llbiBkZSBsYSB0ZWFtIHNlY3UK"){ ?>
<?php include 'includes/head.php'; ?>
<div class="panel panel-default">
<div class="panel-heading">Shop Administration</div>
  <div class="panel-body">
    <div class="row">
      <div class="col-md-3">
        <button>Cracking</button>
        Note : Demander 500$.
      </div>
      <div class="col-md-3">
        <button>Hack Wordpress</button>
        Note : Lol
      </div>
      <div class="col-md-3">
        <button>Forensic</button>
        Note : Demande 300$ et cacher un malware
      </div>
      <div class="col-md-3">
        <button>Flag</button>
        Note : CFl1X-Unf0r7uN4t3lY_w3_@r3_4Ll_hUm@n_3Xc3p7_m3-0f_c0uRs3
      </div>
    </div>
  </div>
</div>

<?php include 'includes/foot.php'; ?>
<?php } else {?>
  <h1><center>Restricted Area</center></h1>
  <h4><center>Not allowed</center></h4>
<?php } ?>
