using System;
using UnityEngine;
using UnityEngine.UI;

public class CommandButtonUI : MonoBehaviour
{
    [SerializeField] private Image icon;
    private Button button;

    private CommandsSO commandSO; //Holds the coomand asociated with this UI
    public static event Action<CommandsSO> OnCommandButtonTapped;

    private void Awake()
    {
        button = GetComponent<Button>();

        button.onClick.AddListener(() =>
        {
            OnCommandButtonTapped?.Invoke(commandSO);
        });
    }

    public void SetButton(Sprite _icon, CommandsSO _command)
    {
        icon.sprite = _icon;
        icon.gameObject.SetActive(true);
        commandSO = _command;
    }
    public void ClearButtonIcon() {
        icon.sprite = null;
        icon.gameObject.SetActive(false);
        commandSO = null; 
    }

}
