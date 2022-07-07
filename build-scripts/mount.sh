#!/bin/bash

app=$1
homeDir=$2
hdiutil attach $homeDir/$app
mkdir ~/Applications
