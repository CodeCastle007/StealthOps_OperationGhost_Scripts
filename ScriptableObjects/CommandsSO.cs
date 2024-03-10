using UnityEngine;

[CreateAssetMenu()]
public class CommandsSO : ScriptableObject
{
    public enum Command { Move, Interact }

    public new string name;
    public Sprite icon;
    public Command commandType;
}
