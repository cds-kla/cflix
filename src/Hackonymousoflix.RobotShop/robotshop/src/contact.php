<?php include 'includes/head.php'; ?>
<?php

  function xssSanitizer($input){
    // $input = strtolower($input);
    // $input = str_replace("script","",$input);
    // $input = str_replace("img","",$input);
    // $input = str_replace("src","",$input);
    return $input;
  }

  if (isset($_POST['message'])) {
        $pdo = new PDO('sqlite:'.dirname(__FILE__).'/challengeUtils/database.sqlite');
        $req = $pdo->prepare("INSERT INTO messages (message,view) VALUES (:message,0)");
        $message = xssSanitizer($_POST['message']);
        $pouet = $req->execute(array('message'=>$message));
        ?>
        <div class="alert alert-success" role="alert">
          Message envoyé ! Laissez moi <b>quelques secondes</b> pour consulter votre message.
        </div>
        <?php
  } else {
?>

  <div class='row'>
    <div class="col-md-10">
      <div class="element"></div>
    </div>
  </div>

  <div id="formulaire" style="display:none">
    <form method="POST" action>
  		<div class="form-group">
  			<textarea class="form-control" rows="10" name="message"></textarea>
  		</div>
  		<div class="form-group">
  			 <button type="submit" class="btn btn-default">Submit</button>
  		</div>
    </form>
  </div>

  <script>
    document.addEventListener('DOMContentLoaded', function(){
        new Typed('.element', {
          strings: [
            "Bienvenue à toi je suis Eliott, responsable de ce shop" +
            "<br>^1000 Ici, on peut acheter tout et n'importe quoi^500, surtout n'importe quoi." +
            "<br>^1000Décrit moi ton besoin et je te fournirais,^500 les payements se font par bitcoin." +
            "<br>Lorsque la commande sera validée, je te fournirais l'adresse de payement." +
            "<br>Penses à ajouter une adresse de retour pour ton message !!"
          ],
          typeSpeed: 20,
          onComplete: function () {
            $("#formulaire").css('display','block');
          }
        });
    });
  </script>

<?php } ?>

<?php include 'includes/foot.php'; ?>
