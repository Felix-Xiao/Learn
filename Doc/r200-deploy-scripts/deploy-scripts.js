const fs = require('fs');
const fsExtra = require('fs-extra');
const path = require('path');
const request = require('request');
const requestProgress = require('request-progress');

const getPromiseDownloadAll = (downloadSite) => {
  const promiseDownload = (fromUrl, toFile) => new Promise((resolve, reject) => {
    console.info(`Start downloading: ${fromUrl}`);
    requestProgress(request(fromUrl), {
      throttle: 5000,
    }).on('progress', function (state) {
      console.info(`Downloading ${fromUrl}: ${state.percent * 100}% done`);
    }).on('error', function (err) {
      reject(err);
    }).on('end', function () {
      console.info(`Download finished: ${fromUrl}`);
      resolve();
    }).pipe(fs.createWriteStream(toFile));
  });

  return Promise.resolve().then(() => {
    console.info(`[DEPLOY SCRIPT By Node] Start Task: Download production packages`);
    return Promise.all([
      (filename => promiseDownload(url.resolve(`${downloadSite}`, filename), path.resolve(__dirname, '../', filename)))(`bps-assembly-${myBuildVersion}.zip`),
      (filename => promiseDownload(url.resolve(`${downloadSite}`, filename), path.resolve(__dirname, '../', filename)))(`remotables-${myBuildVersion}.zip`),
    ]);
  }).then(() => {
    console.info(`[DEPLOY SCRIPT By Node] Done Task: Download production packages`);
  });
}
const getPromiseClean = () => {
  console.info(`[DEPLOY SCRIPT By Node] Start Task: Clean deployed directories`);
  fsExtra.emptyDirSync(path.resolve(__dirname, '../', `bps-assembly-${myBuildVersion}`));
  fsExtra.emptyDirSync(path.resolve(__dirname, '../', `remotables-${myBuildVersion}`));
  console.info(`[DEPLOY SCRIPT By Node] Done Task: Clean deployed directories`);
}

const getPromiseConfigPlatform = () => {
  const deployConfigMap = require(path.resolve(__dirname, './deployconfig.json'));
  const checkPathMap = {
    pathComHoneywellBpsCfg: path.resolve(__dirname, '../', deployConfigMap.ComHoneywellBpsCfg.pathRel.replace(/\*buildVersion\*/g, `${myBuildVersion}`)),
  };
  Object.keys(checkPathMap).forEach(pathItm => {
    if (!fs.existsSync(checkPathMap[pathItm])) {
      throw `Source path not found: ${checkPathMap[pathItm]}`;
    }
  });

  return Promise.resolve().then(() => {
    console.info(`[DEPLOY SCRIPT By Node] Start Task: Apply deploy configs to Platform configs`);
  }).then(() => {
    // 修改 ./system/com/honeywell/bps/bps-framework-cfgservice/*buildVersion*/bps-framework-cfgservice-*buildVersion*.cfg 以修改Cassandra/Redis的IP地址
    const targetFile = checkPathMap.pathComHoneywellBpsCfg;
    const addingStr = `\n${(deployConfigMap.ComHoneywellBpsCfg.addingLines || []).map(i => `${i}`).join(`\n`)}\n`;
    const fileContentStr = `${fs.readFileSync(targetFile)}${addingStr}`;
    console.info(`${targetFile} - REWRITE THIS CONFIG FILE, added ${addingStr}`)
    fs.writeFileSync(targetFile, fileContentStr);
  }).then(() => {
    console.info(`[DEPLOY SCRIPT By Node] Done Task: Apply deploy configs to Platform configs`);
  });
}


const myProcessArgs = (process.argv || []).slice(2);
const myBuildVersion = getProcessParam(myProcessArgs, '_buildversion') || 'non-versioned';
Promise.resolve().then(() => {
  const doDownload = getProcessParam(myProcessArgs, '_download');
  const doClean = getProcessParam(myProcessArgs, '_clean');
  const doConfigPlatform = getProcessParam(myProcessArgs, '_configplatform');

  const resPromise = doDownload ? (function () {
    return getPromiseDownloadAll(doDownload === 'true' ? 'http://159.99.203.253:8800/' : doDownload)
  })() : doClean ? (function () {
    return getPromiseClean()
  })() : doConfigPlatform ? (function () {
    return getPromiseConfigPlatform()
  })() : Promise.resolve();

  return resPromise;
}).then(function(resp) {
  console.info(`[DEPLOY SCRIPT By Node] Success!`, resp);
  return true;
}).catch((err) => {
  console.error(`[DEPLOY SCRIPT By Node] Fail:`, err, '\n');
  return false;
}).then(function(resp) {
  console.info(`[DEPLOY SCRIPT By Node] Finish: return value - ${resp}, at ${Date()}\n`);
  process.exit(!!resp ? 0 : 1); // failure: 1, success: 0
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
