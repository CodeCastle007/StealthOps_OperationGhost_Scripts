using System.Collections.Generic;
using UnityEngine;

public class SoldierSelectionVisual : MonoBehaviour
{
    [SerializeField] private List<GameObject> selectedVisualsList;

    private void Start(){
        GetComponentInParent<SoldierSelectionLogic>().OnSelectionStatusChanged += Unit_OnSelectionStatusChanged;

        Hide();
    }

    private void Unit_OnSelectionStatusChanged(bool selected){
        if(selected){
            Show();
        }else{
            Hide();
        }
    }

    private void Show(){
        foreach(GameObject obj in selectedVisualsList){
                obj.SetActive(true);
            }
    }
    private void Hide(){
        foreach(GameObject obj in selectedVisualsList){
                obj.SetActive(false);
            }
    }
}
