# 🕹️ Unity Boss Arena – Gameplay Systems Prototype

A third-person melee combat **gameplay prototype** built in **Unity 2022.3 LTS (URP)**.

This project focuses on designing and implementing core gameplay systems such as player movement, camera control, melee combat, enemy AI, health management, and death/restart flow — with an emphasis on **clean architecture, system separation, and correct use of Unity’s physics-based workflow**.

This is a **systems-oriented prototype**, not a content-complete game.

---

## 🎯 Project Intent

The goal of this project was to deliberately design and implement fundamental gameplay systems from scratch, focusing on:
- clarity and maintainability
- separation of responsibilities between systems
- avoiding “magic” or hard-coded behavior
- understanding how Unity’s physics, colliders, and scripting lifecycle interact in practice

Visual polish and content scope were intentionally kept minimal in order to focus on gameplay logic and structure.

---

## 🛠️ Tech Stack

- **Engine:** Unity 2022.3 LTS  
- **Rendering:** Universal Render Pipeline (URP)
- **Language:** C# (MonoBehaviour)
- **Physics:** Rigidbody + Collider–based movement and hit detection
- **UI:** Unity Canvas / Image
- **Version Control:** Git + GitHub

---

## 🎮 Core Features

### Player
- WASD-based third-person movement (strafe-style, no tank controls)
- Independent camera follow system (not parented to player)
- Melee attack using a **timed hitbox**
- Shared health system
- Death handling with input lock and restart flow

### Enemy
- Simple **state-based AI** (Idle → Chase → Attack)
- Rigidbody-driven movement and rotation
- Attack wind-up, active hit window, and cooldown
- Damage applied via trigger-based hitbox
- Knockback handling
- Clean enemy death logic

### Combat System
- Hitboxes enabled only during active attack frames
- No distance-based “auto damage”
- Damage applied only through real collider overlap
- Height (Y-axis) alignment handled explicitly
- One reusable `Health` component for player and enemies

### UI
- Player health bar driven by normalized HP values
- Gameplay logic fully decoupled from UI rendering

---

## 📁 Project Structure

