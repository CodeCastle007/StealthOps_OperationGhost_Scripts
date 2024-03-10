using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierSelectionHandler : MonoBehaviour
{
    #region Singleton
    public static SoldierSelectionHandler Instance{get; private set;}
    private void Awake(){
        Instance=this;
    }
    #endregion

    private InputHandler inputHandler;
    private List<Soldier> selectedSoldierList; //List of all selected Units

    public event Action OnSelectedSoldierChanged;
    private bool canSelect = true; //A bool to determine whether we can perform selection or not

    private void Start()
    {
        inputHandler = InputHandler.Instance;
        selectedSoldierList = new List<Soldier>();

        inputHandler.OnTouchEnded += InputHandler_OnFingerUp;
        inputHandler.OnTouchStart += InputHandler_OnTouchStart;

        SoldierAvatarTemplateUI.OnAvatarButtonClicked += SoldierAvatarTemplateUI_OnAvatarButtonClicked;
    }

    private void SoldierAvatarTemplateUI_OnAvatarButtonClicked(Soldier obj) {
        if (selectedSoldierList.Contains(obj)) {
            RemoveUnitFromList(obj);
        }
        else {
            AddUnitToList(obj);
        }
    }

    private void InputHandler_OnTouchStart() {
        //We will not do anything if we are holding or we are waiting for a position for action 
        if (SoldierCommandHandler.Instance.HasCurrentCommandSO()) {
            canSelect = false;
        }
    }

    private void InputHandler_OnFingerUp()
    {
        if (!canSelect || inputHandler.IsHolding() || inputHandler.IsMoved()) {
            //If we cannot select this touch then we will skip it
            canSelect = true;
            return;
        }
        Vector2 fingerUpPosition = inputHandler.GetTouchEndPosition();

        //Cast a ray from camera to position
        Ray ray = Camera.main.ScreenPointToRay(fingerUpPosition);
        //Check if we hit something
        if (Physics.Raycast(ray, out RaycastHit hit))
        {

            //If we hit a unit
            if (hit.collider.TryGetComponent(out Soldier unit))
            {
                AddUnitToList(unit);
            }
            else
            {
                //User tapped elsewhere
                ClearSelectedUnitList();
            }
        }
    }

    //Select a unit and add to list
    public void AddUnitToList(Soldier unit)
    {

        if (selectedSoldierList.Contains(unit))
        {
            //We already have the unit so remove it
            RemoveUnitFromList(unit);
        }
        else
        {
            unit.ToggleSelection();
            selectedSoldierList.Add(unit);

            OnSelectedSoldierChanged?.Invoke();
        }

    }

    //Unselect a unit and remove a unit from selected list
    public void RemoveUnitFromList(Soldier unit)
    {
        if (selectedSoldierList.Contains(unit))
        {
            //We have the unit we wanted to remove
            unit.ToggleSelection();
            selectedSoldierList.Remove(unit);

            OnSelectedSoldierChanged?.Invoke();
        }

    }

    //To deselect all units and clear the list
    public void ClearSelectedUnitList()
    {
        if (selectedSoldierList.Count > 0)
        {
            for (int i = 0; i < selectedSoldierList.Count; i++)
            {
                selectedSoldierList[i].ToggleSelection();
            }
            selectedSoldierList.Clear();

            OnSelectedSoldierChanged?.Invoke();
        }
    }

    //Checks if we have units selected
    public bool HasUnitsSelected()
    {
        return selectedSoldierList.Count > 0;
    }

    //Return list of all selected unist
    public List<Soldier> GetSelectedUnitsList()
    {
        return selectedSoldierList;
    }
}
