using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speedIncrease = 8f;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float minRadius, maxRadius = 50f;
    [SerializeField] private float tpMinRadius, tpMaxRadius = 50f;
    [SerializeField] private float radiusToWalk = 30f;
    [SerializeField] private float wanderInterval = 5f;
    [SerializeField] private float turnSpeed = 5f;
    [SerializeField] private float angleThreshold = 5f;

    [Header("Detection")]
    [SerializeField] private float detectionRadius = 4f;
    [SerializeField] private float detectionRadiusKill = 2f;
    [SerializeField] private Transform player;
    
    [Header("Game Info")]
    [SerializeField]
    private GameInfo gameInfo;
    [SerializeField]
    private GameObject Player;

    [SerializeField]
    private Animator animator;
    [SerializeField]
    private EnemyVisibilityChecker enemyVisibilityChecker;
    [SerializeField]
    private AudioSource footSource;
    [SerializeField]
    private AudioSource tpSource;
    private float timer;
    private float lookTimer = 0;
    private Vector3 targetPosition;
    private bool isTurning = false;
    private bool chasingPlayer = false;

    private int itemsKnown = 0;
    private int branchesHitKnown = 0;
    private bool destSet;
    [SerializeField]
    private GameObject gameObject;
    [SerializeField]
    private List<AudioSource> sounds;
    [SerializeField]
    private AudioSource scareSound;
    private bool scareSoundPlayed = false;

    private void Awake()
    {
        timer = wanderInterval;
        GoToRandomPoint();
    }
    private void EndGame(){
        Time.timeScale = 0f;
        gameObject.SetActive(true);
        foreach(var sound in sounds){
            sound.Stop();
        }
        footSource.Stop();
        if(!scareSoundPlayed)
            scareSound.Play();
            scareSoundPlayed = true;
    }

    private void Update()
    {
        if(!IsMoving()){
            agent.speed = 4;
        }else{
            if(!footSource.isPlaying)
                footSource.Play();
        }
        if(itemsKnown != gameInfo.ItemsPicked){
            itemsKnown = gameInfo.ItemsPicked;
            wanderInterval -= 1;
        }
        if(branchesHitKnown != gameInfo.BranchesHit){
            branchesHitKnown = gameInfo.BranchesHit;
            timer = 0f;
            TeleportToRandomPoint(tpMinRadius,tpMaxRadius-3);
        }
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if(distanceToPlayer <= detectionRadiusKill){
            EndGame();
            return;
        }
        else if(!chasingPlayer){
            
            chasingPlayer = distanceToPlayer <= detectionRadius;           
        }
        if(!IsMoving() && !chasingPlayer && !isTurning){
            RotateTowards(player.position,false);
            if(footSource.isPlaying)
                footSource.Stop();
        }

        if(gameInfo.ItemsPicked != itemsKnown && !chasingPlayer){
            itemsKnown = gameInfo.ItemsPicked;
            TeleportToRandomPoint(tpMinRadius,tpMaxRadius-3);
        }else{
            if (chasingPlayer)
            {
                agent.speed = speedIncrease;
                timer = 0;
                agent.SetDestination(player.position);
                
            }
            else
            {
                EnemyVisibilityChecker enemyVisibilityChecker = FindFirstObjectByType<EnemyVisibilityChecker>();    
                if (isTurning)
                {
                    RotateTowards(targetPosition,true);
                }
                if(!enemyVisibilityChecker.IsEnemyVisible()){ 
                    timer += Time.deltaTime;                  
                    if(Random.Range(1,3) == 1 && timer >= wanderInterval && !IsMoving() && distanceToPlayer <= radiusToWalk){
                        lookTimer = 0;
                        if (!isTurning && timer >= wanderInterval && agent.remainingDistance < 0.5f)
                        {
                            GoToRandomPoint();
                            timer = 0f;
                            RotateTowards(targetPosition,true);
                        }
                    }else if(!isTurning && timer >= wanderInterval && !IsMoving()){
                        timer = 0f;
                        TeleportToRandomPoint(tpMaxRadius,tpMaxRadius);
                    }
                }else if(!IsMoving() && !isTurning){
                    lookTimer += Time.deltaTime;
                    if(lookTimer > 4){
                        chasingPlayer = true;
                    }
                    
                }


            }
        }
        
        if(isTurning || chasingPlayer || IsMoving()){
            animator.SetTrigger("Run");
        }else if(!chasingPlayer){
            animator.SetTrigger("Stop");
        }
        

    }

    private bool IsMoving()
    {
        return agent.velocity.sqrMagnitude > 0.001f;
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

    private void TeleportToRandomPoint(float min, float max)
    {
        if(!enemyVisibilityChecker.IsEnemyVisible()){
            for (int i = 0; i < 100; i++)
            {
                float distance = Random.Range(min, max);
                Vector2 direction2D = Random.insideUnitCircle.normalized * distance;
                Vector3 randomDirection = new Vector3(direction2D.x, 0, direction2D.y) + player.position;

                if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, 2f, NavMesh.AllAreas))
                {
                    agent.Warp(hit.position);
                    agent.ResetPath();
                    tpSource.Play();
                    return;
                }
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
                if (!chasingPlayer && shouldWalk) {
                    destSet = true;
                    agent.SetDestination(targetPosition);
                }  
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