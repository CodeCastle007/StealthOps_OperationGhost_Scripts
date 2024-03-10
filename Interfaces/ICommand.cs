using System;
using UnityEngine;

public interface ICommand
{
    public void Execute();
    public bool IsComplete();
    public void Update();

    public event Action OnComplete;
}
