using UnityEngine;

[CreateAssetMenu()]
public class CommandsSO : ScriptableObject
{
    public enum Command { Move, Interact, Heal }

    [Header("Genera Info")]
    public new string name;
    public Sprite icon;


    [Space(2)] [Header("Type of Command")]
    public Command commandType;


    [Space(2)] [Header("Requirements")]
    //Keep track that if this command required specific item to perform such as heal required health kit
    public InteractableItemSO requiredItem;
    //Keep track if this command need extra input such as move or interact uses extra input to go to
    public bool requiredInput;


    [Space(2)] [Header("Usage")]
    public bool oneTimeUse;
}
