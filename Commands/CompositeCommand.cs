using System;
using System.Collections.Generic;
using UnityEngine;

public class CompositeCommand : ICommand
{
    public event Action OnComplete;

    private List<ICommand> subCommands;
    private int currentCommandIndex;

    public CompositeCommand(List<ICommand> _commands)
    {
        // subCommands = new List<ICommand>(_commands);
        subCommands = _commands;
        currentCommandIndex = 0;
    }

    public void Update()
    {
        if (currentCommandIndex < subCommands.Count)
        {
            subCommands[currentCommandIndex].Update();
        }
    }

    public void Execute()
    {
        //Check that we have sub commands to execute
        if (subCommands.Count > 0)
        {
            subCommands[0].Execute(); //Calls execution to first sub command
            subCommands[0].OnComplete += SubCommand_OnComplete; //Subscribe to event to know when it is ended
        }
    }

    private void SubCommand_OnComplete()
    {
        //Unsubcribe to event
        subCommands[currentCommandIndex].OnComplete -= SubCommand_OnComplete;
        currentCommandIndex++;

        if (currentCommandIndex < subCommands.Count)
        {
            //We have more commands
            subCommands[currentCommandIndex].Execute();
            subCommands[currentCommandIndex].OnComplete += SubCommand_OnComplete;
        }
        else
        {
            //We have executed all sub commands
            OnComplete?.Invoke();
        }
    }

    public bool IsComplete()
    {
        //If current index gets equal to or higher than the count then all are completed
        return currentCommandIndex >= subCommands.Count;
    }
}
