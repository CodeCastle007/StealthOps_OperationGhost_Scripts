using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

public class SoldierMovementLogic : MonoBehaviour
{
    private NavMeshAgent navmeshAgent;
    private ThirdPersonCharacter thirdPersonCharacter;

    private bool isMoving; //Track current movement state
    private bool canMove; //Track if player can move .....It will become false if current movement state changes to false

    private Timer moveTimer = new Timer(.1f);

    private void Start()
    {
        navmeshAgent = GetComponent<NavMeshAgent>();
        thirdPersonCharacter = GetComponent<ThirdPersonCharacter>();

        //Our rotation in handled by our third person charatcer script
        navmeshAgent.updateRotation = false;
    }

    private void Update()
    {
        if (!moveTimer.IsComplete()) {
            moveTimer.TickTimer(Time.deltaTime);
            return;
        }

        if (navmeshAgent.remainingDistance > navmeshAgent.stoppingDistance && canMove)
        {
            //We cal still move
            thirdPersonCharacter.Move(navmeshAgent.desiredVelocity, false, false);
            ChangeMovingState(true);
        }
        else
        {
            //We cannot move
            thirdPersonCharacter.Move(Vector3.zero, false, false);
            ChangeMovingState(false);
            canMove = false;
        }
    }

    public void Move(Vector3 _position)
    {
        navmeshAgent.SetDestination(_position);
        ChangeMovingState(true);
        canMove = true;

        moveTimer.ResetTimer(.1f);
    }

    private void ChangeMovingState(bool _state) {
        if (isMoving == _state) return;

        isMoving = _state;
    }

    public bool IsMoving()
    {
        return isMoving;
    }
}
