# TCS Commons Inspectors - Quick Start Guide

## What You Have

A lightweight inspector utility package with:

✅ `DisabledAttribute` to lock fields in Inspector  
✅ Custom editor drawer that renders values as read-only  
✅ Runtime-safe attribute and editor-only drawer split

## Quick Start

### Step 1: Import Namespace

```csharp
using TitanCollectiveStudios.Commons.Inspectors;
```

### Step 2: Decorate Any Serialized Field

```csharp
using UnityEngine;
using TitanCollectiveStudios.Commons.Inspectors;

public class ExampleComponent : MonoBehaviour
{
    [SerializeField, Disabled]
    private string runtimeId = "Generated at runtime";

    [SerializeField, Disabled]
    private int debugValue = 42;
}
```

### Step 3: View in Inspector
1. Select the component in Unity Inspector.
2. Fields tagged with `[Disabled]` appear visible but not editable.

## Core API

### DisabledAttribute

```csharp
public class DisabledAttribute : PropertyAttribute
{
}
```

- Apply to serialized fields that should be displayed but not changed manually.

### DisabledAttributeDrawer (Editor)

- Runs only in editor.
- Temporarily disables GUI and draws the property normally.
- Works with standard serialized property rendering.

## Typical Use Cases

- Auto-generated IDs
- Runtime-computed debug values
- Read-only metadata exposed to designers
- Fields managed by code or build pipeline

## Setup Notes

- Runtime namespace: `TitanCollectiveStudios.Commons.Inspectors`
- Editor namespace: `TitanCollectiveStudios.Commons.Inspectors.Editor`
- If you use asmdef files, reference:
  - Runtime: `TitanCollectiveStudios.Commons.Inspectors`
  - Editor-only scripts also need the editor asmdef from this package

## File Structure

```
Inspectors/
├── Runtime/
│   ├── TitanCollectiveStudios.Commons.Inspectors.asmdef
│   └── Scripts/
│       └── DisabledAttribute.cs
└── Editor/
    ├── TitanCollectiveStudios.Commons.Inspectors.Editor.asmdef
    └── Scripts/
        └── DisabledAttributeDrawer.cs
```

---

Use this package to keep important serialized values visible for debugging and authoring while protecting them from accidental edits.
