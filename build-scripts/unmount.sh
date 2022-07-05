#!/bin/bash

DiskImageName=$1
homeDir=$2

hdiutil detach $DiskImageName
