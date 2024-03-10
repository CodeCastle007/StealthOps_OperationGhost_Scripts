using System;
using UnityEngine;

public class MoveCommand : ICommand
{
    private Vector3 movePosition;
    private Soldier soldier;

    public event Action OnComplete;

    public MoveCommand(Vector3 _position, Soldier _soldier)
    {
        movePosition = _position;
        soldier = _soldier;
    }

    public void Update()
    {
        if (IsComplete())
        {
            OnComplete?.Invoke();
        }
    }

    public void Execute()
    {
        soldier.Move(movePosition);
    }

    public bool IsComplete()
    {
        return soldier.HasReachedPosition();
    }
}
