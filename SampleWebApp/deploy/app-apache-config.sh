#!/bin/bash
conf_path="/etc/apache2/sites-available/$1.conf"

if [ -f "$conf_path" ]
then
    conf_text=`cat $conf_path`
    if [[ "$conf_text" == *"$2"* ]] || [[ "$conf_text" == *"localhost:$2"* ]]
    then
        echo "Proxy route or localhost port has been used, please manually reconfigure your Apache configuration."
    else
        word="\n\t\tProxyPass \/ http:\/\/127.0.0.1:$2\/\n\t\tProxyPassReverse // http:\/\/127.0.0.1:$2\/"
        match="#InsertHere"
        echo "$conf_text" | sed "s/$match/&$word/g" > "$conf_path"
    fi

    apachectl configtest
    systemctl restart apache2
    systemctl enable apache2
else
    conf_text="<VirtualHost *:*>
    RequestHeader set /"X-Forwarded-Proto/" expr=%{REQUEST_SCHEME}s
        </VirtualHost>

        <VirtualHost *:80>
                ProxyPreserveHost On
                #InsertHere
                ProxyPass / http://127.0.0.1:$3/
                ProxyPassReverse / http://127.0.0.1:$3/
                ServerName $2
                #ServerAlias *.example.com
                ErrorLog ${APACHE_LOG_DIR}/helloapp-error.log
                CustomLog ${APACHE_LOG_DIR}/helloapp-access.log common
        </VirtualHost>"

    echo "$conf_text" > "$conf_path"

    chown root:root "$conf_path"

    sudo a2ensite $1.conf
    apachectl configtest
    systemctl restart apache2
    systemctl enable apache2

fi
