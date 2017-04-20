#!/bin/bash -x 
sudo chmod 600 /etc/redis/sentinel.conf
sudo chmod 600 /etc/redis/6379.conf
sudo chmod 700 /etc/init.d/sentinel
sudo chmod 700 /etc/init.d/sentinel-start.sh
sudo chmod 700 /etc/init.d/redis
sudo chmod 700 /etc/init.d/sentinel-start.sh
sudo chmod 700 /etc/init.d/redis-start.sh
sudo chmod 700 /etc/init.d/redis-stop.sh
sudo chmod 700 /etc/init.d/del-redis-pid
sudo chmod 700 /usr/local/bin/redis-server
sudo chmod 700 /usr/local/bin/redis-cli 
sudo chmod 700 /usr/local/bin/redis-sentinel
sudo chmod 700 /etc/redis
sudo chmod 700 /var/log/redis
sudo chmod 700 /var/lib/redis
sudo chown -R redis:redis /etc/init.d/sentinel
sudo chown -R redis:redis /etc/init.d/sentinel-start.sh
sudo chown -R redis:redis /etc/init.d/redis
sudo chown -R redis:redis /etc/init.d/redis-start.sh
sudo chown -R redis:redis /etc/init.d/redis-stop.sh
sudo chown -R redis:redis /etc/redis/sentinel.conf
sudo chown -R redis:redis /usr/local/bin/redis-server
sudo chown -R redis:redis /usr/local/bin/redis-cli 
sudo chown -R redis:redis /usr/local/bin/redis-sentinel
sudo chown -R redis:redis /etc/redis
sudo chown -R redis:redis /var/log/redis
sudo chown -R redis:redis /var/lib/redis
sudo chown -R redis:redis /etc/redis/6379.conf
