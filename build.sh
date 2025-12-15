#!/usr/bin/env bash
set -e  # stop if anything fails

task restore

# ------------------------------

cd frontend

echo "Building frontend..."

task publish

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

task publish

echo "✅ Build completed."
