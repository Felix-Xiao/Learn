#!/bin/sh
#
# Simple Redis init.d script conceived to work on Linux systems
# as it does use of the /proc filesystem.

REDISPORT=6379
EXEC=/usr/local/bin/redis-server

CONF="/etc/redis/${REDISPORT}.conf"

$EXEC $CONF
