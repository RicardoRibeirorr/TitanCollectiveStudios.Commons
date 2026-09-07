# TitanCollectiveStudios.Commons

A modular, extensible framework for Unity designed to simplify gameplay architecture through reusable components, state machines, commands, conditions, tasks, and editor tooling.

The framework follows a composition-over-inheritance approach, allowing systems to remain decoupled, scalable, and easy to maintain.

---
# Table of Contents

- [Features](#features)
  - [🎮 Runtime](#-runtime)
    - [Agents](#agents)
    - [Commands](#commands)
    - [Conditions](#conditions)
    - [Core](#core)
    - [Datas](#datas)
    - [Deploy](#deploy)
    - [Managers](#managers)
    - [Modules](#modules)
    - [State Machines](#state-machines)
    - [Tasks](#tasks)
    - [Utils](#utils)
  - [🛠️ Editor](#️-editor)
    - [Attributes](#attributes)
- [Installation](#installation)
- [Philosophy](#philosophy)
- [Project Structure](#project-structure)
- [Documentation](#documentation)
- [License](#license)
---

# Features

## 🎮 Runtime

The runtime contains the core systems used during gameplay.

### Agents
Base agent architecture that drives entities through modular behaviors.

### Commands
Reusable command system for executing gameplay logic.

### Conditions
Composable conditions used by state machines, tasks, and gameplay systems.

### Core
Foundation classes and interfaces shared across the entire framework.

### Datas
ScriptableObject-based data containers and runtime data structures.

### Deploy
Utilities for spawning, initialization, and deployment workflows.

### Managers
Global and local manager systems.

### Modules
Plug-and-play gameplay modules that can be attached to agents.

### State Machines
Flexible finite state machine implementation supporting custom states and transitions.

### Tasks
Task-based execution system for AI, gameplay, or scripted behaviors.

### Utils
Common utility classes and extension methods.

---

## 🛠️ Editor

Tools that improve the Unity Editor workflow.

### Attributes
Custom attributes for simplifying Inspector workflows and reducing boilerplate.

---

# Installation

1. Clone or download this repository.
2. Copy the package into your Unity project's `Packages` or `Assets` folder.
3. Open the project with a supported Unity version.
4. Follow the documentation for each module as needed.

---

# Philosophy

This framework is built around a few core principles:

- Composition over inheritance
- Modular architecture
- Highly reusable systems
- Inspector-friendly workflows
- Minimal coupling between systems
- Easy extensibility

---

# Project Structure

```
Agents/
Attributes/
Commands/
Conditions/
Core/
Datas/
Deploy/
Managers/
Modules/
Packages Compatibility/
StateMachines/
Tasks/
Utils/
_Others/
```

---

# Documentation

Documentation for each subsystem is available inside its respective folder and will continue to expand as new modules are added.

---

# License

See the LICENSE file for licensing information.