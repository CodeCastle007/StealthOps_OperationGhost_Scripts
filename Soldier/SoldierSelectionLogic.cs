using System;
using UnityEngine;

public class SoldierSelectionLogic : MonoBehaviour
{
    private bool isSelected;
    public event Action<bool> OnSelectionStatusChanged;

    //For changing state of selection
    public void ToggleSelection(){
        isSelected=!isSelected;
        OnSelectionStatusChanged?.Invoke(isSelected);
    }

    public bool IsSelected()
    {
        return isSelected;
    }
}
