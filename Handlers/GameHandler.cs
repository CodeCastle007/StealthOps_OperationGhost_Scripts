using System;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    #region Singleton
    public static GameHandler Instance { get;private set; }
    private void Awake() {
        Instance = this;

        if (displayFpsCounter) {
            fpsUI.gameObject.SetActive(true);
        }
        if (displayRamUsage) {
            ramUsageUI.gameObject.SetActive(true);
        }
    }
    #endregion

    [SerializeField] private Transform soldierSpawnPoint;
    [SerializeField] private List<SoldierSO> soldierSOList; //LATER REPLACE WITH LIST

    [SerializeField] private Transform fpsUI;
    [SerializeField] private Transform ramUsageUI;
    [SerializeField] private bool displayFpsCounter;
    [SerializeField] private bool displayRamUsage;

    private List<Transform> spawnedSoldiers;


    private void OnEnable() {
        spawnedSoldiers = new List<Transform>();

        for (int i = 0; i < soldierSOList.Count; i++) {
            spawnedSoldiers.Add(Instantiate(soldierSOList[i].prefab));
        }
    }

    private void Start() {
       


    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.T)) {
            for (int i = 0; i < spawnedSoldiers.Count; i++) {
                spawnedSoldiers[i].GetComponent<Health>().DealDamage(10f);
            }
        }
    }

    public List<Transform> GetSoldierTransformList() {
        return spawnedSoldiers;
    }

}
