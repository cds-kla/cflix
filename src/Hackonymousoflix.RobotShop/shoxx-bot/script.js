var casper = require('casper').create({
    verbose: true,
    logLevel: "debug"
});

casper.on("resource.error", function (resourceError) {
    console.log('Unable to load resource (#' + resourceError.id + 'URL:' + resourceError.url + ')');
    console.log('Error code: ' + resourceError.errorCode + '. Description: ' + resourceError.errorString);
});

var host = "robotshop";
var url = "http://" + host + "/read.php";
var timeout = 3000;
var repeatcount = (7 * 24 * 3600) / timeout; // we try for a continuous week, after the container will restart

phantom.addCookie({
    'name': 'adminCookie',
    'value': 'SGVsbG8sIGMnZXN0IFJlbWksIGFuY2llbiBkZSBsYSB0ZWFtIHNlY3UK',
    'domain': host,
    'path': '/',
    'httponly': false
});


casper.start(url).repeat(repeatcount, function () {
    var str = this.getHTML('body').trim();
    if (!(!str)) {
        this.echo(str);
    }

    this.wait(timeout, function () {
        this.reload();
    });
});

casper.run();
