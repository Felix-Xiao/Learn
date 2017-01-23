#!/bin/bash -x
case $1 in
  'all') echo [ -------- $0 $1 $2 -------- ]: ================ Deploy packages and then launch all servers ================
    $0 mountall $2 && $0 launchall $2
    ;;
  'cleanall') echo [ -------- $0 $1 $2 -------- ]: ================ Clean all deployed package directories ================
    node deploy-scripts.js _clean _buildversion=$2
    ;;
  'mountall') echo [ -------- $0 $1 $2 -------- ]: ================ Deploy all packages ================ && (
      $0 cleanall $2
    ) && (
      $0 mount_platform $2
    ) && (
      $0 mount_remotable $2
    )
    ;;
  'mount_platform') echo [ -------- $0 $1 $2 -------- ]: ================ Deploy Platform package ================ && (
      pushd ..
      jar xf bps-assembly-$2.zip
      popd
      node deploy-scripts.js _configplatform _buildversion=$2
    )
    ;;
  'mount_remotable') ================ Deploy Remotable package ================ && (
      pushd ..
      jar xf remotables-$2.zip
      popd
    )
    ;;
  'launchall') echo [ -------- $0 $1 $2 -------- ]: ================ Launch all servers ================
    $0 launch_remotable $2 & $0 launch_platform $2
    ;;
  'launch_remotable') echo [ -------- $0 $1 $2 -------- ]: ================ Launch resource[remotable] server ================
    node server_resource.js _basedir=../remotables-$2
    ;;
  'launch_platform') echo [ -------- $0 $1 $2 -------- ]: ================ Launch servicemix[platform] server ================ && (
      pushd ../bps-assembly-$2/bin
      chmod 777 *
      ./servicemix
    )
    ;;
  *)
    ;;
esac