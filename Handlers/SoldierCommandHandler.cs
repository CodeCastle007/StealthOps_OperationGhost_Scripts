using System;
using System.Collections.Generic;
using UnityEngine;

public class SoldierCommandHandler : MonoBehaviour
{
    #region Singleton
    public static SoldierCommandHandler Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }
    #endregion

    private CommandsSO currentCommandSO;

    public event Action OnCurrentCommandChange;

    private void Start()
    {
        SoldierCommandUI.Instance.OnCommandButtonTapped += SoldierCommandUI_OnCommandButtonTapped;
        InputHandler.Instance.OnTouchEnded += InputHandler_OnTouchEnded;

        InteractIconVisual.OnAnyInteractIconClicked += InteractIconVisual_OnAnyInteractIconClicked; //Listening to any icon clicked on any onteractable player wants to interact with
    }

    private void InteractIconVisual_OnAnyInteractIconClicked(Transform _transform) {
        if (currentCommandSO == null && _transform == null) return;

        GiveCommandsToSoldiers(_transform.position, _transform);
    }

    private void InputHandler_OnTouchEnded() //Get the position where to move or perform action
    {
        if (currentCommandSO == null) return;

        Vector2 fingerUpPosition = InputHandler.Instance.GetTouchEndPosition();
        //Cast a ray from camera to position
        Ray ray = Camera.main.ScreenPointToRay(fingerUpPosition);

        //Check if we hit something
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            GiveCommandsToSoldiers(hit.point,null);
        }
    }

    private void SoldierCommandUI_OnCommandButtonTapped(CommandsSO _command){
        if (_command == null) return;

        SetCurrentCommandSO(_command);
    }

    private void GiveCommandsToSoldiers(Vector3 _position, Transform _transform)
    {
        List<Soldier> selectedSoldiers = SoldierSelectionHandler.Instance.GetSelectedUnitsList();

        switch (currentCommandSO.commandType)
        {
            case CommandsSO.Command.Move:
                for (int i = 0; i < selectedSoldiers.Count; i++){
                    selectedSoldiers[i].GiveCommand(new CompositeCommand(new List<ICommand>()
                    {
                        new MoveCommand(_position, selectedSoldiers[i])
                    }));
                }

                SetCurrentCommandSO(null);
                break;

            case CommandsSO.Command.Interact:
                for (int i = 0; i < selectedSoldiers.Count; i++) {
                    selectedSoldiers[i].GiveCommand(new CompositeCommand(new List<ICommand>()
                    {
                        new MoveCommand(_position, selectedSoldiers[i]),
                        new InteractCommand(_transform.GetComponent<IInteractable>(),selectedSoldiers[i])
                    }));
                }

                SetCurrentCommandSO(null);
                break;
        }
    }

    private void SetCurrentCommandSO(CommandsSO _command) {
        currentCommandSO = _command;

        OnCurrentCommandChange?.Invoke();
    }

    public CommandsSO GetCurrentCommandSO() {
        return currentCommandSO;
    }

    public bool HasCurrentCommandSO()
    {
        return currentCommandSO;
    }
}
