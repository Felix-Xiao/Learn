#!/bin/sh
#
# Simple Redis init.d script conceived to work on Linux systems
# as it does use of the /proc filesystem.
#chkconfig: - 85 15

EXEC=/usr/local/bin/redis-sentinel

CONF="/etc/redis/sentinel.conf"

$EXEC $CONF

