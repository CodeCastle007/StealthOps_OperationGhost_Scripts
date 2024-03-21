using System.Collections.Generic;
using UnityEngine;

public class Soldier : MonoBehaviour
{
    [SerializeField] private SoldierSO soldierSO;

    [SerializeField] private List<CommandsSO> validCommands; //Array of all commands this unit can perform

    private SoldierMovementLogic soldierMovementLogic;
    private SoldierInteractionLogic soldierInteractionLogic;
    private SoldierSelectionLogic soldierSelectionLogic;
    private SoldierHealingLogic soldierHealingLogic;
    private SoldierInventoryLogic soldierInventoryLogic;

   // private Queue<ICommand> waitingCommandQueue;

    private ICommand currentCommand; //Holds the current command to execute


    private void Start()
    {
        soldierMovementLogic = GetComponent<SoldierMovementLogic>();
        soldierSelectionLogic = GetComponent<SoldierSelectionLogic>();
        soldierInteractionLogic=GetComponent<SoldierInteractionLogic>();
        soldierHealingLogic=GetComponent<SoldierHealingLogic>();
        soldierInventoryLogic = GetComponent<SoldierInventoryLogic>();
    }


    private void Update()
    {
        if (currentCommand != null)
        {
            currentCommand.Update();
        }
    }


    //Called by soldier command handler
    public void GiveCommand(ICommand _command)
    {
        currentCommand = _command;
        currentCommand.Execute();

        currentCommand.OnComplete += CurrentCommand_OnComplete;
    }
    private void CurrentCommand_OnComplete()
    {
        currentCommand.OnComplete -= CurrentCommand_OnComplete;
        currentCommand = null;
    }

    public List<CommandsSO> GetValidCommands()
    {
        return validCommands;
    }
    public bool HasReachedPosition()
    {
        return !soldierMovementLogic.IsMoving();
    }


    public void ToggleSelection()
    {
        soldierSelectionLogic.ToggleSelection();
    }



    //Soldier does not call these functions 
    //Soldier only executes commands given to it from queue
    public void Move(Vector3 _position)
    {
        soldierMovementLogic.Move(_position);
    }
    public void Interact(IInteractable _interactable) {
        soldierInteractionLogic.InteractWithInteractable(_interactable);
    }

    public void Heal(InteractableItemSO _requiredItemSO) {
        soldierHealingLogic.Heal(); //Heal Soldier
        soldierInventoryLogic.RemoveItem(_requiredItemSO); //Remove item from inventory
    }

    public SoldierSO GetSoldierSO() {
        return soldierSO;
    }

    //private void OnDrawGizmos() {
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(transform.position, 10f);
    //}
}
