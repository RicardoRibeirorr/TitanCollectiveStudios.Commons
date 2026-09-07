# TCS Commons Datas - Quick Start Guide

## What You Have

A clean data-foundation package with:

✅ Base ScriptableObject asset type with automatic ID generation  
✅ Generic typed definition pattern (`Definition<TData>`)  
✅ Stable per-asset identifier for runtime lookups and persistence  
✅ Easy extension for custom game data models

## Quick Start

### Step 1: Create a Data Asset Type

```csharp
using UnityEngine;
using TitanCollectiveStudios.Commons.Datas;

[CreateAssetMenu(menuName = "TCS/Data/Character Data")]
public class CharacterData : DataAsset
{
    [SerializeField] private string displayName;
    [SerializeField] private int maxHealth;

    public string DisplayName => displayName;
    public int MaxHealth => maxHealth;
}
```

### Step 2: Create an Asset in Unity
1. In Project window, right-click.
2. Choose your Create Asset menu path.
3. Select the created asset.
4. The `Id` field is automatically generated and shown as read-only.

### Step 3: Build a Typed Definition

```csharp
using TitanCollectiveStudios.Commons.Datas;

[System.Serializable]
public class CharacterDefinition : Definition<CharacterData>
{
    public CharacterDefinition(CharacterData data) : base(data)
    {
    }
}
```

## Core API

### DataAsset

```csharp
public abstract class DataAsset : ScriptableObject
{
    public string Id => id;
}
```

- `Id` is generated in editor via `OnValidate()` if empty.
- Generated format: GUID without dashes (`N` format).

### Definition<TData>

```csharp
public abstract class Definition<TData>
    where TData : DataAsset
{
    public TData Data => data;
    public string Id => data.Id;
}
```

- Wraps a strongly typed `DataAsset` reference.
- Exposes the same stable `Id` for indexing and cross-system references.

## Typical Usage

```csharp
// Load or assign a ScriptableObject asset.
CharacterData data = myCharacterDataAsset;

// Create a typed definition instance.
var definition = new CharacterDefinition(data);

// Use stable id in registries, save data, or lookup systems.
UnityEngine.Debug.Log(definition.Id);
UnityEngine.Debug.Log(definition.Data.MaxHealth);
```

## Setup Notes

- Namespace: `TitanCollectiveStudios.Commons.Datas`
- If you use asmdef files, reference:
  - `TitanCollectiveStudios.Commons.Datas`
- This package depends on the Inspectors package for the read-only inspector attribute.

## File Structure

```
Datas/
├── Runtime/
│   ├── TitanCollectiveStudios.Commons.Datas.asmdef
│   └── Scripts/
│       ├── DataAsset.cs
│       └── Definition.cs
└── Editor/
    └── TitanCollectiveStudios.Commons.Datas.Editor.asmdef
```

---

Use this package as a consistent base layer for ScriptableObject-driven game and content data.
