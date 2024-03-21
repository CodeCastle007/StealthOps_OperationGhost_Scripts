using UnityEngine;

public class FPSandRamUsage : MonoBehaviour
{
    [SerializeField] private Transform graphyDisplayUI;

    private void Start() {
        //Wew will display it if game is running
        if (graphyDisplayUI != null) {
            graphyDisplayUI.gameObject.SetActive(true);
        }
       
    }
}
