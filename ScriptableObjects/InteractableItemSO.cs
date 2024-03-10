using UnityEngine;

[CreateAssetMenu()]
public class InteractableItemSO : ScriptableObject
{
    public new string name;
    public Sprite icon;
    public Transform prefab;
    public bool useable; //Bcz somw items are not usable such as intel
}
