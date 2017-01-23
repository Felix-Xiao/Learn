const fs = require('fs');
const fsExtra = require('fs-extra');
const path = require('path');
const crypto = require('crypto');
const et = require('elementtree');

const myConfigDir = 'DITConfigs';
const pathFromConfigBase = path.resolve(__dirname, `${myConfigDir}`);
const pathCodeBase = path.resolve('./7-SourceCode/');
const pathWebBase = path.resolve(pathCodeBase, './App/BPSAlarmCenterWeb/');

const getPromiseFixPlatform = () => {
  const buildConfigMap = require(path.resolve(__dirname, './buildconfig.json'));
  const checkPathMap = {
    pathFromConfigBase,
    pathCodeBase,
    pathWebBase,
  };
  Object.keys(checkPathMap).forEach(pathItm => {
    if (!fs.existsSync(checkPathMap[pathItm])) {
      throw `Source path not found: ${checkPathMap[pathItm]}`;
    }
  });

  return Promise.resolve().then(() => {
    console.info(`[BUILD SCRIPT By Node] Start Task: Apply build configs to Platform source codes`);
    // // Replace sourcecodes with config files
    // (buildConfigMap.buildPlatformOverrides || []).forEach(overrideItm => {
    //   if (!overrideItm.pathFromRel || !overrideItm.pathToRel) return;
    //   const sourceFile = path.resolve(pathFromConfigBase, overrideItm.pathFromRel);
    //   const targetFile = path.resolve(pathCodeBase, overrideItm.pathToRel);
    //   const hash = crypto.createHash('sha1');
    //   hash.update(`${fs.readFileSync(targetFile)}`);
    //   const hashCode = `${hash.digest('hex')}`;
    //   if (hashCode === overrideItm.sha1) {
    //     console.info(`Info: No need to update config in ${targetFile}`);
    //   }
    //   else {
    //     throw `Error: ${targetFile} has changed, please make sure SHA1 info (${hashCode}) is updated to config file and then rerun`;
    //     console.info(`Warning: REWRITE CONFIG FILE - copy from ${sourceFile} to ${targetFile}`);
    //     fs.writeFileSync(targetFile, fs.readFileSync(sourceFile));
    //     // fsExtra.copySync(sourceFile, targetFile);
    //   }
    // });
  }).then(() => {
    // 修改 ./7-SourceCode/Platform/pom.xml 以确保输出 zip
    const targetFile = path.resolve(pathCodeBase, './Platform/pom.xml');
    const eltree = et.parse(`${fs.readFileSync(targetFile)}`);
    const elModules = eltree.find('./modules');
    const elModuleArr = eltree.findall('./modules/module');
    if (elModuleArr.some(i => i.text === 'assemblies')) {
      console.info(`${targetFile} - This config file is OK, rewrite is not required`)
    }
    else {
      const module_assemblies = et.SubElement(elModules, 'module');
      module_assemblies.text = 'assemblies';
      // elModuleArr.push(module_assemblies);
      console.info(`${targetFile} - REWRITE THIS CONFIG FILE, added <module>assemblies</module>`)
      fs.writeFileSync(targetFile, eltree.write({'xml_declaration': true}));
    }
  }).then(() => {
    // 修改 .../src/main/resources/META-INF/persistence.xml 以置为 Update DB
    const relPath_modFiles = [
      './Platform/framework/jpa/bps-framework-jpa/src/main/resources/META-INF/persistence.xml',
      './Platform/framework/jpa/bps-framework-jpasecurity/src/main/resources/META-INF/persistence.xml',
    ]
    relPath_modFiles.forEach(relPath_modFile => {
      const targetFile = path.resolve(pathCodeBase, relPath_modFile);
      const eltree = et.parse(`${fs.readFileSync(targetFile)}`);
      const elProperties = eltree.find('./persistence-unit/properties');
      const elPropertyArr = eltree.findall('./persistence-unit/properties/property');
      let property_mod1 = elPropertyArr.find(i => i.get('name') === 'hibernate.hbm2ddl.auto');
      if (!property_mod1) {
        property_mod1 = et.SubElement(elProperties, 'property');
        property_mod1.set('name', 'hibernate.hbm2ddl.auto');
      }
      if (property_mod1.get('value') === 'update') {
        console.info(`${targetFile} - This config file is OK, rewrite is not required`)
      }
      else {
        property_mod1.set('value', 'update');
        // elPropertyArr.push(property_assemblies);
        console.info(`${targetFile} - REWRITE THIS CONFIG FILE, added/updated <property name="hibernate.hbm2ddl.auto" value="update"/>`)
        fs.writeFileSync(targetFile, eltree.write({'xml_declaration': true}));
      }
    });
  }).then(() => {
    console.info(`[BUILD SCRIPT By Node] Done Task: Apply build configs to Platform source codes`);
  });
}
// const getPromiseBuildingWeb = () => {
//   return Promise.resolve().then(() => {
//     console.info(`[BUILD SCRIPT By Node] Start Task: Build Web bundle`);
//     console.info(`[BUILD SCRIPT By Node] Check and install Web dependencies...`);
//     return runCmd_Win('npm install', {cwd: pathWebBase});
//   }).then(() => {
//     console.info(`[BUILD SCRIPT By Node] All Web dependencies in place.`);
//     console.info(`[BUILD SCRIPT By Node] Build Web bundle...`);
//     return runCmd_Win('npm run build_web', {cwd: pathWebBase});
//   }).then(() => {
//     console.info(`[BUILD SCRIPT By Node] Web bundle built.`);
//     console.info(`[BUILD SCRIPT By Node] Done Task: Build Web bundle`);
//   });
// }
// const getPromiseBuildingPlatform = () => {
//   return Promise.resolve().then(() => {
//     console.info(`[BUILD SCRIPT By Node] Start Task: Build Platform bundle`);
//     return runCmd_Win('mvn clean install -DskipTests', {cwd: pathCodeBase});
//   }).then(() => {
//     console.info(`[BUILD SCRIPT By Node] Done Task: Build Platform bundle`);
//   });
// }
const getPromiseShareDist = (doShareDist) => {
  const shareDistPath = path.resolve(doShareDist ? `${doShareDist}` : '');
  const targetRemotablesFilenamePart = `remotables-${myBuildVersion}`;
  const targetPlatformFilenamePart = `bps-assembly-${myBuildVersion}`;
  const targetDaqFilenamePart = `daq-assembly-${myBuildVersion}-bin`;
  return Promise.resolve().then(() => {
    console.info(`[BUILD SCRIPT By Node] Start Task: Share distribution files for remote download`);
    const sourceDir = path.resolve(pathCodeBase, './App/BPSAlarmCenterWeb/dist_web', 'remotables');
    const targetHost = path.resolve(pathCodeBase, './App/BPSAlarmCenterWeb/dist_web', targetRemotablesFilenamePart);
    fsExtra.emptyDirSync(targetHost);
    fsExtra.copySync(sourceDir, path.resolve(targetHost, 'remotables'));
  }).then(() => {
    return runCmd_Win(`jar -cMf "${path.resolve(pathWebBase, `./dist_web/${targetRemotablesFilenamePart}.zip`)}" "./${targetRemotablesFilenamePart}"`, {cwd: path.resolve(pathWebBase, './dist_web')});
  }).then(() => {
    const sourceFile = path.resolve(pathCodeBase, './App/BPSAlarmCenterWeb/dist_web/', `${targetRemotablesFilenamePart}.zip`);
    const targetFile = path.resolve(shareDistPath, `${targetRemotablesFilenamePart}.zip`);
    fsExtra.copySync(sourceFile, targetFile);
    console.info(`${targetFile} - done`)
  }).then(() => {
    const sourceFile = path.resolve(pathCodeBase, './Platform/assemblies/bps-assembly/target/', `${targetPlatformFilenamePart}.zip`);
    const targetFile = path.resolve(shareDistPath, `${targetPlatformFilenamePart}.zip`);
    fsExtra.copySync(sourceFile, targetFile);
    console.info(`${targetFile} - done`)
  }).then(() => {
    const sourceFile = path.resolve(pathCodeBase, './Daq/daq-assembly/target/', `${targetDaqFilenamePart}.zip`);
    const targetFile = path.resolve(shareDistPath, `${targetDaqFilenamePart}.zip`);
    fsExtra.copySync(sourceFile, targetFile);
    console.info(`${targetFile} - done`)
  }).then(() => {
    console.info(`[BUILD SCRIPT By Node] Done Task: Share distribution files for remote download`);
  });
}


const myProcessArgs = (process.argv || []).slice(2);
const myBuildVersion = getProcessParam(myProcessArgs, '_buildversion') || 'non-versioned';
runCmd_Win(`cd`).then(() => {
  const doFixPlatform = getProcessParam(myProcessArgs, '_fixplatform');
  const doShareDist = getProcessParam(myProcessArgs, '_sharedist');

  const resPromise = doFixPlatform ? (function () {
    return getPromiseFixPlatform()
  })() : doShareDist ? (function () {
    return getPromiseShareDist(doShareDist)
  })() : Promise.resolve();

  return resPromise;
}).then((resp) => {
  console.info(`[BUILD SCRIPT By Node] Success!`, resp);
  return true;
}).catch((err) => {
  console.error(`[BUILD SCRIPT By Node] Fail:`, err, '\n');
  return false;
}).then(function(resp) {
  console.info(`[BUILD SCRIPT By Node] Finish: return value - ${resp}, at ${Date()}\n`);
  process.exit(!!resp ? 0 : 1); // failure: 1, success: 0
});


function runCmd_Win(cmdString, spawnOptionMap) {
  return new Promise((resolve, reject) => {
    const exec = require('child_process').exec; 
    exec(cmdString, spawnOptionMap, function(err, stdout, stderr) {
      if (err) {
        console.error('[BUILD SCRIPT By Node] error(cmd): ', err);
        reject();
      }
      else {
        console.info(stdout);
        resolve();
      }
    });

    // // On Windows Only ...
    // const spawn = require('child_process').spawn;
    // const bat = spawn('cmd.exe', ['/c', `${cmdString}`], spawnOptionMap);
    // //const bat = spawn('start', ['cmd', '/c', 'mvn']);

    // bat.stdout.on('data', (data) => {
    //   console.info('[BUILD SCRIPT By Node] output(cmd):\n' + data);
    // });

    // bat.stderr.on('data', (data) => {
    //   console.error('[BUILD SCRIPT By Node] error(cmd):\n' + data);
    // });

    // bat.on('exit', (code) => {
    //   console.info(`[BUILD SCRIPT By Node] exit(cmd): Child exited with code ${code}`);
    //   if (code === 0) {
    //     resolve()
    //   }
    //   else {
    //     reject();
    //   }
    // });
  });
}

function getProcessParam(myProcessArgs, paramName) {
  var queryArr = myProcessArgs;
  for (var i = 0; i < queryArr.length; i++) {
    var queryPair = queryArr[i].split('=');
    if (`${queryPair[0]}`.toLowerCase() === `${paramName}`.toLowerCase())
      return typeof queryPair[1] === 'undefined' ? 'true' : (!queryPair[1] || `${queryPair[1]}`.toLowerCase() === 'false') ? null : queryPair[1];
  }
  return null;
}
