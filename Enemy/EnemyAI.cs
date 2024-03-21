using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private FieldOfView fieldOfView;
    private NavMeshAgent agent;
    private EnemyMovementLogic enemyMovementLogic;

    Vector3 startPos;
    Quaternion startRotation;
    private float smooothRotationTime = 3f;

    private void Start()
    {
        startPos = transform.position;
        startRotation = transform.rotation;

        agent=GetComponent<NavMeshAgent>();
        fieldOfView = GetComponentInChildren<FieldOfView>();
        enemyMovementLogic=GetComponent<EnemyMovementLogic>();

        fieldOfView.OnSoldierFound += FieldOfView_OnSoldierFound;
    }

    private void FieldOfView_OnSoldierFound(Soldier _soldier) {
        //enemyMovementLogic.Move(_soldier.transform.position);
    }

    private void Update()
    {
        SetFieldOfViewOriginandRotation();
        HandleRotation();
    }

    private void SetFieldOfViewOriginandRotation() {
        fieldOfView.SetOrigin(transform.position);
        fieldOfView.SetDirection(transform.forward);
    }
    private void HandleRotation() {
        if (agent.remainingDistance <= .1f) {
            transform.rotation = Quaternion.Slerp(transform.rotation, startRotation, Time.deltaTime * smooothRotationTime);
        }
    }
}