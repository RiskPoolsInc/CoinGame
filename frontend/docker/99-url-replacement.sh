#!/bin/sh

if [ ! -z $DEVENV ] && [ $DEVENV = "dev" ]; then
    echo  "replace ENV to dev = $@"
    sed -i 's@fortoken.ubikiri.com@fortoken.dev20021.ubikiri.com@g' /usr/share/nginx/html/*.js
    sed -i 's@fortoken-api.ubikiri.com@fortoken-api.dev20021.ubikiri.com@g' /usr/share/nginx/html/*.js
    sed -i 's@explorer.ubikiri.com@test-explorer.ubikiri.com@g' /usr/share/nginx/html/*.js
    sed -i 's@e61bfd8313ecb44d4e9a9ceb128b25e54747c417@5560c0ee0c7ffdc4ccb033056fcc95c1974c3ed1@g' /usr/share/nginx/html/*.js
fi


if [ ! -z $DEVENV ] && [ $DEVENV = "prodio" ]; then
    echo  "replace ENV to prod 4token io = $@"
    sed -i 's@fortoken.ubikiri.com@4tokens.io@g' /usr/share/nginx/html/*.js
    sed -i 's@fortoken-api.ubikiri.com@api.4tokens.io@g' /usr/share/nginx/html/*.js
fi

if [ ! -z $DEVENV ] && [ $DEVENV = "stage" ]; then
    echo  "replace ENV to prod 4token io = $@"
    sed -i 's@fortoken.ubikiri.com@stage20021.4tokens.io@g' /usr/share/nginx/html/*.js
    sed -i 's@fortoken-api.ubikiri.com@api1.4tokens.io@g' /usr/share/nginx/html/*.js
fi
