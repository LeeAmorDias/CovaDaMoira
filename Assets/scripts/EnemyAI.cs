using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speedIncreasePerLevel = 3f;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float minRadius, maxRadius = 50f;
    [SerializeField] private float wanderInterval = 5f;
    [SerializeField] private float turnSpeed = 5f;
    [SerializeField] private float angleThreshold = 5f;

    [Header("Detection")]
    [SerializeField] private float detectionRadius = 20f;
    [SerializeField] private Transform player;
    
    [Header("Game Info")]
    [SerializeField]
    private GameInfo gameInfo;

    private float timer;
    private Vector3 targetPosition;
    private bool isTurning = false;
    private bool chasingPlayer = false;

    private int itemsKnown = 0;
    private bool running;

    private void Awake()
    {
        agent.updateRotation = false;
        timer = wanderInterval;
        GoToRandomPoint();
    }

    private void Update()
    {
        if(!running && !chasingPlayer){
            RotateTowards(player.position,false);
        }
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        chasingPlayer = distanceToPlayer <= detectionRadius;
        if(gameInfo.ItemsPicked != itemsKnown && !chasingPlayer){
            itemsKnown = gameInfo.ItemsPicked;
            TeleportToRandomPoint();
        }else{
            if (chasingPlayer)
            {
                // Chase the player directly
                timer = 0;
                RotateTowards(player.position, false);
                agent.SetDestination(player.position);
                
            }
            else
            {
                timer += Time.deltaTime;
                if(timer >= wanderInterval){
                    if (!isTurning && timer >= wanderInterval && agent.remainingDistance < 0.5f)
                    {
                        running = true;
                        GoToRandomPoint();
                        timer = 0f;
                    }
                    if (isTurning)
                    {
                        RotateTowards(targetPosition,true);
                    }
                }else if(timer >= wanderInterval){
                    timer = 0f;
                    TeleportToRandomPoint();
                    running = false; 
                }

            }
            }


    }

    private void GoToRandomPoint()
    {
        for (int i = 0; i < 30; i++)
        {
            float distance = Random.Range(minRadius, maxRadius);
            Vector2 direction2D = Random.insideUnitCircle.normalized * distance;
            Vector3 randomDirection = new Vector3(direction2D.x, 0, direction2D.y) + transform.position;

            if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, 2f, NavMesh.AllAreas))
            {
                targetPosition = hit.position;
                isTurning = true;
                agent.ResetPath(); // stop movement while turning
                break;
            }
        }
    }

    private void TeleportToRandomPoint()
    {
        for (int i = 0; i < 30; i++)
        {
            float distance = Random.Range(minRadius, maxRadius);
            Vector2 direction2D = Random.insideUnitCircle.normalized * distance;
            Vector3 randomDirection = new Vector3(direction2D.x, 0, direction2D.y) + transform.position;

            if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, 2f, NavMesh.AllAreas))
            {
                agent.Warp(hit.position);
                agent.ResetPath();
                return;
            }
        }
    }


    private void RotateTowards(Vector3 target, bool shouldWalk)
    {
        Vector3 direction = target - transform.position;
        direction.y = 0;

        if (direction.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime * 100f);

            float angle = Quaternion.Angle(transform.rotation, targetRotation);
            if (angle < angleThreshold)
            {
                isTurning = false;
                if (!chasingPlayer && shouldWalk) 
                    agent.SetDestination(targetPosition);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Show detection radius in editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}