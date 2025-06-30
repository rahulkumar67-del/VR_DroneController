# VR Drone Controller Game

## Overview

The **VR Drone Controller Game** is a Unity-based research project developed under **ACS Lab, IIT Mandi**. This immersive simulation is built specifically for **Meta Quest 3** using the **OpenXR toolkit and XRI Input System**, offering both VR and desktop compatibility. The game allows users to pilot a drone through a dynamic environment, collecting coins and avoiding intelligent ground-based tank attacks. It features cognitive adaptability using real-time EEG-based focus tracking to modify gameplay, making it both an entertainment and experimental neuroscience project.

This project was originally developed for desktop and later evolved into a full-fledged VR-compatible application with input flexibility, customizable drone physics, and interaction-rich AI behavior.

---

## 🔥 Key Features

* **VR Drone Control (Meta Quest 3):** Flight control via joystick input using Quest 3 VR controllers.
* **Input Switching System:** Change between VR and desktop inputs directly via in-game UI (powered by InputManager.cs).
* **Coin Collection Gameplay:** Navigate to collect coins, increasing score on contact.
* **Tank Enemy AI:** Tanks receive drone position via ground station and fire smart projectiles.
* **Dynamic Focus-Based Adaptation:** Real-time environmental and gameplay changes based on cognitive focus level (from EEG).
* **Unity Physics Integration:** Realistic drone movement through Unity's Rigidbody system.
* **Player Data Logging:** Session data is saved in CSV files, including player ID, score, and game events.

---

## 🧠 Research Significance

* **Real-time Focus Monitoring:** A Python module interprets EEG signals and generates a "Focus Index".
* **Adaptive Gameplay:** Game difficulty and visual cues (like color grading, bullet damage, and firing frequency) adapt based on player attention.
* **Human-Computer Interaction:** Evaluates cognitive load and VR interaction patterns in real-time.

---

## 🎮 Main Scene & Core Logic

* **Scene:** `MainScene.unity`
* **Player Prefab:** `DroneRacer` (contains movement, collider, and drone camera setup)
* **Ground Station System:** Acts as the mediator for tank attacks and decision logic.
* **InputManager.cs:** Core script to handle platform-based switching (VR ↔ Desktop).

---

## 🧩 Major Scripts & Responsibilities

### 1. `DroneMovement.cs`

* VR joystick/desktop input mapped to Rigidbody force and torque.
* Applies realistic flight mechanics including roll, pitch, and yaw.

### 2. `InputManager.cs`

* Maps Unity Input System/XRI Input Action assets.
* Dynamically switches input sources (Quest controllers ↔ keyboard/mouse).

### 3. `CoinCollection.cs`

* Detects proximity to coins and triggers score increment.
* Also notifies GroundStation if drone enters tank detection radius.

### 4. `TankAI.cs`

* Receives instructions from `GroundStation.cs`.
* Aims turret, predicts drone trajectory, and fires.

### 5. `FocusAlignment.cs`

* Uses Focus Index from Python via socket/shared file.
* Adjusts visual elements and tank behavior accordingly.

### 6. `VRControllerHandler.cs`

* Maps Quest 3 controller joystick to drone motion.
* Configures haptic feedback and action buttons.

### 7. `Projectile.cs`

* Instantiates and fires projectiles.
* Destroys projectile after a set duration or on impact.

### 8. `GameManager.cs`

* Maintains player score, health, and gameplay state.
* Exports session logs (player ID, session score) into CSV.

---

## 🛠️ How to Run

1. Clone the repo:

```bash
git clone https://github.com/rahulkumar67-del/VR_DroneController.git
```

2. Open the project in **Unity Hub** (ensure correct version as per `ProjectVersion.txt`).
3. Open `MenuScene.unity` and press **Play**.
4. For VR, ensure vr is connected and OpenXR is active.

---

## 📂 Folder Structure Highlights

* **Assets/Scenes/** - Contains main gameplay scene.
* **Assets/Scripts/** - Core scripts for drone, tank, UI, input.
* **Assets/Resources/** - In-game models, audio, and prefabs.
* **Assets/Settings/** - Input action maps, profiles.
* **PlayerData.csv** - Session logs saved at runtime.

---

## 📄 Dependencies

* Unity 6000.0.43f1 LTS or later
* OpenXR Plugin (via Unity Package Manager)
* Input System (Unity official)
* Python (optional, for EEG Focus Index)
* Git LFS (for large assets)

---

## 📌 Notes

* Files like `.tif` textures above 100MB are excluded to comply with GitHub limits.
* downlaod thsi tif file from this link "https://drive.google.com/drive/folders/1qdd-2y61_-pihAKN3OGlM869e8wJrx_L?usp=sharing"
* Add this folder in this place "Assets\TerrainDemoScene_URP\Terrain"
* For best performance, use Meta Quest 3 with link cable or AirLink.
* To simulate focus input, a dummy script can override focus index.
* 

---

## 👨‍💻 Contributors

* **Rahul Kumar** — Developer, VR Integration, Research Design (ACS Lab, IIT Mandi)

---

## 📧 Contact

For queries or collaborations, reach out via [GitHub Issues](https://github.com/rahulkumar67-del/VR_DroneController/issues) or email (update if needed).

---

> "A research-driven VR simulation where cognitive focus meets real-time gameplay adaptation."
