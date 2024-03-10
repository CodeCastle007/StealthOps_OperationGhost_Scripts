using System.Collections.Generic;
using UnityEngine;

public class SoldierAvatarUI : MonoBehaviour
{
    private List<Transform> soldierTransformList;

    [SerializeField] private Transform container;
    [SerializeField] private Transform avatarTemplate;

    private void Start() {
        soldierTransformList = GameHandler.Instance.GetSoldierTransformList();
        avatarTemplate.gameObject.SetActive(false);

        ClearTemplates();
        GenerateTemplates();
    }

    private void ClearTemplates() {
        foreach (Transform child in container) {
            if (child == avatarTemplate) continue;
            Destroy(child.gameObject);
        }
    }

    private void GenerateTemplates() {
        for (int i = 0; i < soldierTransformList.Count; i++) {
            Transform template = Instantiate(avatarTemplate, container);

            template.gameObject.SetActive(true);
            template.GetComponent<SoldierAvatarTemplateUI>().SetSoldier(soldierTransformList[i].GetComponent<Soldier>());
        }
    }
}
