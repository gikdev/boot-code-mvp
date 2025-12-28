#!/usr/bin/env bash
set -e  # stop if anything fails

task setup

# ------------------------------

cd frontend

echo "Building frontend..."

task build

echo "Copying frontend build to backend/wwwroot..."

# Make sure backend/wwwroot exists
mkdir -p ../backend/wwwroot

# Remove old files
rm -rf ../backend/wwwroot/*

# Copy new files
cp -r dist/* ../backend/wwwroot/

# ------------------------------

cd ../backend

echo "Building backend..."

task build

echo "✅ Build completed."
