# AR-Assignment----Internship
 
Overview
The AR Cube Placement App is an Android application built using Unity and AR Foundation. The app allows users to tap on real-world surfaces to place randomly colored cubes. Additionally, it captures an image from the AR camera view 10 frames after placing each cube and saves it to the device storage.

Features
AR Cube Placement: Tap on detected surfaces to place cubes.
Random Colors: Each cube has a unique, randomly assigned color.
AR View Capture: Automatically captures the AR scene 10 frames after cube placement.
Android Support: Optimized for Android devices using ARCore.

Technologies Used
Unity 3D
AR Foundation
ARCore XR Plugin
C# Scripting

Project Structure
AR-Cube-Placement-App/
├── Assets/
│   ├── Scripts/
│   │   └── CubePlacement.cs
│   ├── Prefabs/
│   │   └── Cube.prefab
│   └── Scenes/
│       └── MainScene.unity
└── README.md

Setup Instructions
Prerequisites
Unity Hub (latest version)
Unity with Android Build Support
Android device with ARCore support

Code Explanation
Cube Placement: Detects touch input and performs AR raycasting to find surfaces.
Random Coloring: Applies a random color to each new cube.
AR View Capture: Uses ARCameraManager to capture the AR view after a 10-frame delay.

Troubleshooting
No Surfaces Detected: Ensure good lighting and textured surfaces.
App Not Running: Check USB debugging and device ARCore compatibility.
Image Not Saved: Verify storage permissions are granted.
