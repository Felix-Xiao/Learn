/**
 * 简单静态 web server
 */
const express = require('express');
const path = require('path');
// const cors = require('cors');
const myProcessArgs = (process.argv || []).slice(2);
const myappServerPort = getProcessParam(myProcessArgs, '_port') || 8800;
const myappServerBaseDir = path.resolve(getProcessParam(myProcessArgs, '_basedir') || '.');
const app = express();
// 允许跨域请求
// app.use(cors());
app.use(function allowCrossDomain(req, res, next) {
    // Below header settings refer to: ../7-SourceCode/Platform/app/bps-app-web/src/main/java/com/honeywell/bps/app/web/servlet/BpsSecurityFilter.java
    res.header('Access-Control-Allow-Origin', '*'); // req.headers.origin
    res.header('Access-Control-Allow-Methods', 'GET,POST');
    res.header('Access-Control-Allow-Headers', 'Origin, X-Requested-With, Content-Type, Accept, csrftoken, sid, siteid');
    if ('OPTIONS' == req.method) {
        res.sendStatus(200);
    }
    else {
        next();
    }
});
app.use(express.static(myappServerBaseDir));
app.get('/', function (req, res) {
   res.send(`Hola World, ${Date()}`);
});
app.listen(myappServerPort, function () {
    console.log(`Info: simple server setup on port: ${myappServerPort}, based on: "${myappServerBaseDir}",  access http://127.0.0.1:${myappServerPort}/`);
});

function getProcessParam(myProcessArgs, paramName) {
  var queryArr = myProcessArgs;
  for (var i = 0; i < queryArr.length; i++) {
    var queryPair = queryArr[i].split('=');
    if (`${queryPair[0]}`.toLowerCase() === `${paramName}`.toLowerCase())
      return typeof queryPair[1] === 'undefined' ? 'true' : (!queryPair[1] || `${queryPair[1]}`.toLowerCase() === 'false') ? null : queryPair[1];
  }
  return null;
}
