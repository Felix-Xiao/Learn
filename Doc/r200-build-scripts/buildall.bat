
REM do not use utf8 encoding to this .bat file as it will cause unexpected error...
echo [ -------- build-all.bat -------- ]: Start Build for version: %1
setlocal
set _successcount=0
set _cd=%cd%
:sub_buildweb
  echo [ -------- build-all.bat -------- ]: ================ sub_buildweb ================
  set nextsub=sub_setmaven
  cd "%_cd%\7-SourceCode\App\BPSAlarmCenterWeb" || goto %nextsub%
  if not exist "node_modules" (
    echo [ -------- build-all.bat -------- ]: Copy local cached web dependencies
    robocopy "C:\BuildCI\dependencies\node_modules" "node_modules" /mir
  )
  echo [ -------- build-all.bat -------- ]: Install all web dependencies from remote
  call npm install
  echo [ -------- build-all.bat -------- ]: Build web bundle and distribute [incl. remotables] to Platform folder
  (if "%2"=="_noobfuscation" (
    call npm run build_web
  ) else (
    REM call npm run build_web _nointegrationremotables
    call npm run build_web
  )) || goto %nextsub%
  set /a _successcount=_successcount+1
:sub_setmaven
  echo [ -------- build-all.bat -------- ]: ================ sub_setmaven ================
  echo [ -------- build-all.bat -------- ]: Set Maven build environment: 1. memory allocation
  cd "%_cd%"
  set MAVEN_OPTS=-Xmx512m -XX:MaxPermSize=128m
:sub_buildplatform
  echo [ -------- build-all.bat -------- ]: ================ sub_buildplatform ================
  set nextsub=sub_builddaq
  echo [ -------- build-all.bat -------- ]: Execute essential code modification scripts for Build environment
  cd "%_cd%"
  (node "%~dp0\build-scripts.js" _fixplatform) || goto %nextsub%
  echo [ -------- build-all.bat -------- ]: Build [non-]obfuscated Platform bundle
  cd "%_cd%\7-SourceCode\Platform" || goto %nextsub%
  echo [ -------- build-all.bat -------- ]: Update Platform pom version upon param
  cmd /c "mvn versions:set -DnewVersion=%1 -f parent/pom.xml" || goto %nextsub%
  (if "%2"=="_noobfuscation" (
    cmd /c "mvn clean install -DskipTests=true"
  ) else (
    cmd /c "mvn clean install -DskipTests=true -P obfuscate"
  )) || goto %nextsub%
  set /a _successcount=_successcount+1
:sub_builddaq
  echo [ -------- build-all.bat -------- ]: ================ sub_builddaq ================
  set nextsub=sub_distshare
  echo [ -------- build-all.bat -------- ]: Build [non-]obfuscated DAQ bundle
  cd "%_cd%\7-SourceCode\Daq" || goto %nextsub%
  echo [ -------- build-all.bat -------- ]: Update DAQ pom version upon param
  cmd /c "mvn versions:set -DnewVersion=%1 -f pom.xml" || goto %nextsub%
  (if "%2"=="_noobfuscation" (
    cmd /c "build clean install -DskipTests=true"
  ) else (
    cmd /c "build clean install -DskipTests=true -P obfuscate"
  )) || goto %nextsub%
  set /a _successcount=_successcount+1
:sub_distshare
  echo [ -------- build-all.bat -------- ]: ================ sub_distshare ================
  set nextsub=sub_result
  echo [ -------- build-all.bat -------- ]: Distribute Build files to share folder: start
  cd "%_cd%"
  (if "%2"=="_noobfuscation" (
    node "%~dp0\build-scripts.js" _buildversion=%1 _sharedist=C:\BuildCI\Distributions\Build_Latest\debug\
  ) else (
    node "%~dp0\build-scripts.js" _buildversion=%1 _sharedist=C:\BuildCI\Distributions\Build_Latest\
  )) || goto %nextsub%
  REM set /a _successcount=_successcount+1
  echo [ -------- build-all.bat -------- ]: Distribute Build files to share folder: done
:sub_result
  echo [ -------- build-all.bat -------- ]: ================ sub_result - successful package(s): %_successcount% ================
  cd "%_cd%"
  if %_successcount% lss 3 (
    endlocal
    exit /b 1
  )
endlocal
echo [ -------- build-all.bat -------- ]: Finish Build for version: %1
