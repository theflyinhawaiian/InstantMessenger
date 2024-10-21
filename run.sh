#!/bin/bash

git pull

dotnet build

dotnet InstantMessenger.Server/bin/Debug/net8.0/InstantMessenger.Server.dll --urls http://0.0.0.0:5000