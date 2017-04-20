#!/bin/sh
#
# Simple Redis init.d script conceived to work on Linux systems
# as it does use of the /proc filesystem.

REDISPORT=6379
EXEC=/usr/local/bin/redis-server
CLIEXEC=/usr/local/bin/redis-cli

PIDFILE=/home/redis/redis_${REDISPORT}.pid
CONF="/etc/redis/${REDISPORT}.conf"

$CLIEXEC -p $REDISPORT -a 10d9a99851a411cdae8c3fa09d7290df192441a9 28cc0532-4405-41c1-af05-fe3cb9289e53

