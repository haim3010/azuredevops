#!/bin/bash
file_path="/var/www/$1"
service_path="/etc/systemd/system/$1.service"
if [ -f "$service_path" ]
then
    echo "Service file already exists"
else
    touch "$service_path"
    service_contents="[Unit]
Description=.NET Web App for $1

[Service]
WorkingDirectory=$file_path
ExecStart=/usr/bin/dotnet $file_path/$2.dll
Restart=always
# Restart service after 10 seconds if the dotnet service crashes:
RestartSec=10
KillSignal=SIGINT
SyslogIdentifier=dotnet-example
User=azureuser
Environment=ASPNETCORE_ENVIRONMENT=$3
Environment=ConnectionStrings__DefaultConnection="#{ConnectionString}#"

[Install]
WantedBy=multi-user.target"
    echo "$service_contents" >> "$service_path"
    systemctl start "$1".service
    systemctl enable "$1".service
fi
