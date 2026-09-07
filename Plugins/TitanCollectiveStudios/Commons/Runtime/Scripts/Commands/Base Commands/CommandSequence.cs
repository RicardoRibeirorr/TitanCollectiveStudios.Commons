using MackySoft.SerializeReferenceExtensions;
using System.Collections.Generic;
using TitanCollectiveStudios.Commons.Conditions;
using UnityEngine;


namespace TitanCollectiveStudios.Commons.Commands
{
    /// <summary>
    /// Container for a sequence of commands
    /// Uses MackySoft.SerializeReferenceExtensions for polymorphic serialization
    /// </summary>
    [AddTypeMenu("Sequence")]
    [System.Serializable]
    public class CommandSequence : BaseCommand
    {
        [SerializeReference, SubclassSelector] protected List<iCommand> commands = new List<iCommand>();
        [SerializeField] protected bool executeInParallel = false;

        public virtual List<iCommand> Commands => commands;
        public virtual bool ExecuteInParallel => executeInParallel;

        public virtual void AddCommand(iCommand command)
        {
            commands.Add(command);
        }

        public virtual void RemoveCommand(iCommand command)
        {
            commands.Remove(command);
        }

        public override void Execute()
        {
            foreach (var command in commands)
            {
                command.Execute();
            }
        }

        public virtual void UndoAll()
        {
            // Undo in reverse order
            for (int i = commands.Count - 1; i >= 0; i--)
            {
                commands[i].Undo();
            }
        }

        public virtual void Clear()
        {
            commands.Clear();
        }
    }
}
