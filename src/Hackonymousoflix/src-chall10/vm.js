'use strict';
var vm = require("vm");

const cliprefix = "exec <arg>";
const vorpal = require("vorpal")();

vorpal
  .command("print <message>", "Print a message on the console using javascript interpolation")
  .action(function (arg, cb) {
    this.log(`Your message is : ${arg.message}`);
    cb();
  });

vorpal
  .command("date", "Display current Date")
  .action(function (arg, cb) {
    this.log(new Date());
    cb();
  });

vorpal
  .command("launchnb <target> <nuclearpassword>", "Firing a nuclear bomb wherever you want")
  .action(function (arg, cb) {

    var crypto = require('crypto'),
      algorithm = 'aes-256-ctr',
      masterkey = process.env.NUCLEAR_BOMB_ACCESS;

    function encrypt(text) {
      var cipher = crypto.createCipher(algorithm, masterkey)
      var crypted = cipher.update(text, 'utf8', 'hex')
      crypted += cipher.final('hex');
      return crypted;
    }

    var hash = encrypt(arg.nuclearpassword);
    if (hash === 'd87b7985997859c68a79778525a90712b9564a56225b03ce08c7dd2d82ad05cd485bb21a20a8238e545b7d7909c97d94f846') {
      this.log("5");
      this.log("...");
      this.log("4");
      this.log("...");
      this.log("3");
      this.log("...");
      this.log("2");
      this.log("...");
      this.log("1");
      this.log("...");
      this.log("Félicitations M. Underwood vous venez de déclencher la troisième guerre mondiale, ce n'est pas donné à tout le monde un tel exploit !");
      this.log("");
      this.log("Nous te redonnons la main pour finir le travail bonne chance ! Tape bash");

      vorpal
        .command("bash", "Open a new bash terminal")
        .action(function (arg, cb) {
          this.log("Welcome to the Nuclear Bomb Shell");
          this.log("");
          this.log("Be careful ! Strange things have happened to other users :o");

          var cp = require('child_process');
          var bash = cp.spawn('/bin/bash', [], { stdio: 'inherit' });

          bash.on('exit', function (code, signal) {
            cb();
          });
        });

    } else {
      this.log("Désolé M. Underwood vous n'avez pas tapé le bon code veuillez réessayer !");
    }

    cb();
  });

vorpal
  .command(cliprefix, "Run a command on my nodejs server")
  .action(function (arg, cb) {

    if (!arg.arg) {
      this.log("Il faut spécifier au moins un argument !");
      this.log(cliprefix);
      cb();
      return;
    }

    if (typeof arg.arg !== "string") {
      this.log(`Ta commande n'est pas valide réessaye`);
      cb();
      return;
    }

    if (arg.arg.length > 210) {
      this.log(`Ta commande de ${arg.arg.length} caractères est trop longue, elle dépasse les 210 caractères`);
      cb();
      return;
    }

    // check allowed command
    const arrForbidden = [
      "callee", "caller", "arguments",
      "ascii", "utf8", "utf16le", "ucs2", "utf16le", "base64", "binary", "hex",
      "1", "2", "3", "4", "5", "6", "7", "8", "9", "[1]", "+", "-", "*", "|",
      "this", "log", "function", "var", "join",
      "child_process", "require"
      // "searchBool",
    ];

    var dontHaveForbiddenCommand = true;
    var fobiddenCommand = "";

    for (var i = 0; i < arrForbidden.length; i++) {
      if (arg.arg.indexOf(arrForbidden[i]) > -1) {
        fobiddenCommand = arrForbidden[i];
        dontHaveForbiddenCommand = false;
        break;
      }
      else {
        dontHaveForbiddenCommand = true;
      }
    }

    if (dontHaveForbiddenCommand === false) {
      this.log("La commande « " + fobiddenCommand + " » n'est pas autorisée !");
      cb();
      return;
    }

    try {
      var script = new vm.Script(arg.arg);
      var context = new vm.createContext({});
      var t = script.runInNewContext(context);
      this.log(t);
    }
    catch (e) {
      this.log(e.message);
    }

    cb();

  });

vorpal
  .delimiter("whitehouse ~:")
  .show();