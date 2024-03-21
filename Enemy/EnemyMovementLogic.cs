using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

public class EnemyMovementLogic : MonoBehaviour
{
    private ThirdPersonCharacter thirdPersonCharacter;
    private NavMeshAgent navMeshAgent;

    private void Start() {
        thirdPersonCharacter = GetComponent<ThirdPersonCharacter>();
        navMeshAgent = GetComponent<NavMeshAgent>();

        navMeshAgent.updateRotation = false;
    }

    private void Update() {
        if (navMeshAgent.remainingDistance > navMeshAgent.stoppingDistance) {
            //We cal still move
            thirdPersonCharacter.Move(navMeshAgent.desiredVelocity, false, false);
        }
        else {
            //We cannot move
            thirdPersonCharacter.Move(Vector3.zero, false, false);
        }
    }

    public void Move(Vector3 _position) {
        navMeshAgent.SetDestination(_position);
    }

}
