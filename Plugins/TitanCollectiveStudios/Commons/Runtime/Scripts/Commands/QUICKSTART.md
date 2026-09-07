# TCS Commons Commands - Quick Start Guide

## What You Have

A reusable command framework with:

✅ Command interface and base abstractions  
✅ Sequential command execution with undo support  
✅ Built-in Unity-focused commands  
✅ Polymorphic command serialization support  
✅ Easy extension for custom gameplay and tools logic

## Quick Start

### Step 1 of 1: Setup
1. Open or create a script in your project.
2. Add these using statements:

```csharp
using TitanCollectiveStudios.Commons.Commands;
```

3. If you use asmdef files, add a reference to:
   - `TitanCollectiveStudios.Commons.Commands`

### Build In Inspector (AWESOME!)
This will list all the commands in the inspector has a dropdown.
```csharp
	[SerializeReference, SubclassSelector]
	ICommand m_Command;
```

You can also specify which type of command you need
```csharp
    [SerializeReference, SubclassSelector] //make sure to include this!!!
	IBaseCommand m_BaseCommand; 
    //or
    [SerializeReference, SubclassSelector]
	IUnityCommand m_UnityCommand; 
```

### Build a Dynamic Command

```csharp
var sequence = new CommandSequence();

iCommand cmd = new MoveGameObjectCommand
{
    TargetGameObject = player,
    TargetPosition = new Vector3(5f, 0f, 2f),
    UseWorldSpace = true
};


cmd.Execute();
//or for sincronous:
yield return cmd.WaitExecute();
```

### Undo If Needed

```csharp
sequence.UndoAll();
```

## Make Your Own


## Core API

### Command Contract

```csharp
public interface iCommand
{
    void Execute();
    IEnumerator WaitExecute();
    void Undo();
}
```

### Base Types
- `ACommand`: abstract base implementation of `iCommand`
- `BaseCommand`: base class for serializable command types
- `UnityCommand`: base class for Unity-related commands

## Built-in Commands

| Command | Purpose |
|---------|---------|
| `SetGameObjectActiveCommand` | Activate/deactivate a GameObject |
| `SetComponentEnabledCommand` | Enable/disable a component by type name |
| `DisableColliderCommand` | Disable colliders on a target GameObject |
| `MoveGameObjectCommand` | Move target transform in world/local space |
| `InvokeUnityEventCommand` | Invoke a configured UnityEvent |

## Create a Custom Command

```csharp
using TitanCollectiveStudios.Commons.Commands;

[System.Serializable]
public class PrintMessageCommand : BaseCommand
{
    public string message;

    public override void Execute()
    {
        UnityEngine.Debug.Log(message);
    }

    public override void Undo()
    {
        // Optional undo logic.
    }
}
```

## Common Patterns

### Execute One Command

```csharp
iCommand command = new SetGameObjectActiveCommand
{
    TargetGameObject = someObject,
    SetActive = false
};

command.Execute();
```

### Execute and Wait One Frame

```csharp
StartCoroutine(command.WaitExecute());
```

## Notes

- Built-in Unity commands currently use namespace `TitanCollectiveStudios.QuestSystem.Commands`.
- `CommandSequence.ExecuteInParallel` exists as a serialized flag, but execution currently runs in-order.
- `DisableColliderCommand` currently disables colliders regardless of flag value.

## File Structure

```
Commands/
├── Runtime/
│   ├── TitanCollectiveStudios.Commons.Commands.asmdef
│   └── Scripts/
│       ├── iCommand.cs
│       ├── ACommand.cs
│       ├── Base Commands/
│       │   ├── BaseCommand.cs
│       │   └── CommandSequence.cs
│       └── Unity Commands/
│           ├── UnityCommand.cs
│           ├── SetGameObjectActiveCommand.cs
│           ├── SetComponentEnabledCommand.cs
│           ├── DisableColliderCommand.cs
│           ├── MoveGameObjectCommand.cs
│           └── InvokeUnityEventCommand.cs
└── Editor/
    └── TitanCollectiveStudios.Commons.Commands.Editor.asmdef
```

---

Use this package as a generic execution layer for gameplay actions, scripted events, and modular tool flows.