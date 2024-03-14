using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoldierCommandUI : MonoBehaviour
{
    #region Singleton
    public static SoldierCommandUI Instance { get; set; }
    private void Awake()
    {
        Instance = this;
    }
    #endregion

    [SerializeField] private Transform container;
    [SerializeField] private List<CommandButtonUI> commandButtonsList;
    [SerializeField] private Button cancelCommandButton;

    private List<Soldier> selectedSoldierList;
    public event Action<CommandsSO> OnCommandButtonTapped;
    public event Action OnCommandCancelTapped;
    private void Start()
    {
        SoldierSelectionHandler.Instance.OnSelectedSoldierChanged += SoldierSelectionHandler_OnSelectedSoldierChanged;
        CommandButtonUI.OnCommandButtonTapped += CommandButtonUI_OnCommandButtonTapped; //Listening to static event on buttons
        SoldierInventoryLogic.OnInventoryItemsChanged += SoldierInventoryLogic_OnInventoryItemsChanged;

        cancelCommandButton.onClick.AddListener(() =>
        {
            SetCancelCommandButton(false);
            OnCommandCancelTapped?.Invoke();
        });
    }

    private void SoldierInventoryLogic_OnInventoryItemsChanged() {
        //Getting the selected list
        selectedSoldierList = SoldierSelectionHandler.Instance.GetSelectedUnitsList();

        if (selectedSoldierList.Count > 0) {
            GetCommandsToSet();  //Get Commands from selected soldier/s to set
        }
    }

    private void CommandButtonUI_OnCommandButtonTapped(CommandsSO _command)
    {
        OnCommandButtonTapped?.Invoke(_command);

        //If we are waiting for input then we will activate the cancel button
        if (_command.requiredInput) {
            SetCancelCommandButton(true);
        }
        
    }

    private void SoldierSelectionHandler_OnSelectedSoldierChanged()
    {
        //Getting the selected list
        selectedSoldierList = SoldierSelectionHandler.Instance.GetSelectedUnitsList();
        if(selectedSoldierList.Count> 0) {
            GetCommandsToSet();  //Get Commands from selected soldier/s to set
        }
        else {
            SetCancelCommandButton(false);
            ClearCommandIcons();
        }
        
    }

    private void GetCommandsToSet()
    {
        if(selectedSoldierList.Count > 1)
        {
            ClearCommandIcons();

            //We have more than one soldiers selected
            List<CommandsSO> validCommands = new List<CommandsSO>(selectedSoldierList[0].GetValidCommands());

            //Loo[ through all soldiers
            for (int i = 1; i < selectedSoldierList.Count; i++)
            {
                //Loop through all commands and see if all soldiers can perform that actions
                for (int j = 0; j < validCommands.Count; j++)
                {
                    if (!selectedSoldierList[i].GetValidCommands().Contains(validCommands[j]))
                    {
                        //Soldier cannot perform this action so we will remove from list
                        validCommands.RemoveAt(j);
                    }
                }
            }

            //We will have the valid commands that all selected soldiers can perform
            SetCommandButtons(validCommands);
        }
        else
        {
            //We have only one unit selected
            List<CommandsSO> validCommands = selectedSoldierList[0].GetValidCommands();

            SetCommandButtons(validCommands);
        }
    }

    private void SetCommandButtons(List<CommandsSO> _commandSO)
    {
        if (commandButtonsList.Count < _commandSO.Count) {
            //We have more commands than buttons
            Debug.LogError("Commands count is more than buttons....");
        }
        //Set the icons here
        for (int i = 0; i < _commandSO.Count; i++)
        {
            commandButtonsList[i].SetButton(_commandSO[i].icon, _commandSO[i]);
        }

        //Looping through all buttons
        for (int i = 0; i < _commandSO.Count; i++) {

            for (int j = 0; j < selectedSoldierList.Count; j++) {

                //Check if we require item to perform this command
                if (_commandSO[i].requiredItem != null) {
                    //Check if all soldiers contain the items required for command
                    if (!selectedSoldierList[j].GetComponent<SoldierInventoryLogic>().Contains(_commandSO[i].requiredItem)) {
                        commandButtonsList[i].GetComponent<Button>().interactable = false;
                    }
                    else {
                        commandButtonsList[i].GetComponent<Button>().interactable = true;
                    }
                }
                
            }
        }
    }

    private void ClearCommandIcons() {
        for (int i = 0; i < commandButtonsList.Count; i++) {
            commandButtonsList[i].ClearButtonIcon();
        }
    }

    private void SetCancelCommandButton(bool _status) {
        cancelCommandButton.gameObject.SetActive(_status);
    }
}
