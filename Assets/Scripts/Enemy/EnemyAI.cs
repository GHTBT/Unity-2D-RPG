using System.Collections;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float roamChangeDirfloat = 2f;
    [SerializeField] private float attackRange = 0f;
    [SerializeField] private MonoBehaviour enemyType;
    [SerializeField] private float attackCooldown = 2f;
    [SerializeField] private bool stopMovingWhileAttacking = false;
	[SerializeField] public float FollowDist = 6f;
    [SerializeField] private float followSpeed = 3f;

    private bool canAttack = true;
    private enum State 
    {
        Roaming,
        Attacking,
        Follow
    }

    private Vector2 roamPosition;
    private float timeRoaming = 0f;
    private State state;
    private EnemyPathfinding enemyPathfinding;
    

    private void Awake()
    {
        enemyPathfinding = GetComponent<EnemyPathfinding>();
        state = State.Roaming;
    }

    private void Start()
    {
        roamPosition = GetRoamingPosition();
    }

    private void Update()
    {
        MovementStateControl();
    }

    private void MovementStateControl()
    {
        switch(state)
        {
            default:
            case State.Roaming:
                Roaming();
            break;

            case State.Attacking:
                Attacking();
            break;

            case State.Follow:
                Following();
            break;
        }
    }

    private void Roaming()
    {
        timeRoaming += Time.deltaTime;
        enemyPathfinding.MoveTo(roamPosition);
        if(Vector2.Distance(transform.position, Player_Controller.Instance.transform.position) < FollowDist)
        {
            state = State.Follow;
        }
        if(Vector2.Distance(transform.position, Player_Controller.Instance.transform.position) < attackRange)
        {
            state = State.Attacking;
        }
        if(timeRoaming > roamChangeDirfloat)
        {
            roamPosition = GetRoamingPosition();
        }
    }

    private void Attacking()
    {
        if(Vector2.Distance(transform.position, Player_Controller.Instance.transform.position) > attackRange)
        {
            state = State.Roaming;
        }
        if(Vector2.Distance(transform.position, Player_Controller.Instance.transform.position) < FollowDist)
        {
            state = State.Follow;
        }
        if(attackRange != 0 && canAttack)
        {
            canAttack = false;
            (enemyType as IEnemy).Attack();

            if(stopMovingWhileAttacking)
            {
                enemyPathfinding.StopMoving();
            }
            else
            {
                enemyPathfinding.MoveTo(roamPosition);
            }

            StartCoroutine(AttackCooldownRoutine());
        }
    }

    private void Following()
    {
        if(Vector2.Distance(transform.position, Player_Controller.Instance.transform.position) > FollowDist)
        {
            state = State.Roaming;
        }
        if(Vector2.Distance(transform.position, Player_Controller.Instance.transform.position) < FollowDist)
        {
            transform.position = Vector2.MoveTowards(this.transform.position,Player_Controller.Instance.transform.position, followSpeed*Time.deltaTime);
        }
    }

    private IEnumerator AttackCooldownRoutine()
    {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }
    private Vector2 GetRoamingPosition()
    {
        timeRoaming = 0f;
        return new Vector2(Random.Range(-1f,1f),Random.Range(-1f,1f)).normalized;
    }
}
