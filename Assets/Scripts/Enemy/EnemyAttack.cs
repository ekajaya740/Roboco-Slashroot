using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private float sightDistance;
    [SerializeField] private float buffedMovementSpeed;
    private bool isAttack;
    private bool isPlayerDetected;
    private EnemyMove enemyMove;
    [SerializeField] private Transform castPoint;
    [SerializeField] private LayerMask playerLayer;
    private Rigidbody2D enemyRB;
    private Animator enemyAnimator;

    [SerializeField] private Transform playerTransform;
    private bool isChasePlayer;

    void Awake(){
        sightDistance = 5;
    }

    void Start()
    {
        enemyMove = GetComponent<EnemyMove>();
        enemyRB = GetComponent<Rigidbody2D>();
        enemyAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }

    void FixedUpdate(){
        SeePlayer();
        
    }


    private void SeePlayer(){
        isPlayerDetected = DetectPlayer(sightDistance);

        if(isPlayerDetected && !isAttack){
            isChasePlayer = true;
            if(enemyMove.isFacingRight){
                buffedMovementSpeed = 300;
            }else if(!enemyMove.isFacingRight){
                buffedMovementSpeed = -300;
            }
        }
        else if(!isPlayerDetected){
            if(enemyMove.isFacingRight){
                buffedMovementSpeed = 100;
            }else if(!enemyMove.isFacingRight){
                buffedMovementSpeed = -100;
            }
        }
        
        enemyMove.movementSpeed = buffedMovementSpeed;

        if(!isPlayerDetected && enemyMove.mustTurn){
            isChasePlayer = false;
        }

        if(isChasePlayer){
            if(gameObject.transform.position.x < playerTransform.position.x){
                if(!enemyMove.isFacingRight){
                    enemyMove.Flip();
                }
            }else if(gameObject.transform.position.x > playerTransform.position.x){
                if(enemyMove.isFacingRight){
                    enemyMove.Flip();
                }
            }
        }
    }

    private bool DetectPlayer(float distance){
        bool isSeePlayer = false;
        float castDist = 0;

        if(enemyMove.isFacingRight){
            castDist = distance;
        }else if(!enemyMove.isFacingRight){
            castDist = -distance;
        }

        Vector2 endPos = castPoint.position + (Vector3.right * castDist);
        RaycastHit2D hit = Physics2D.Linecast(transform.position, endPos, playerLayer);
        Debug.DrawLine(transform.position, endPos);

        if(hit.collider != null){
            if(hit.collider.gameObject.CompareTag("Player")){
                isSeePlayer = true;
            }
        }else{
            isSeePlayer = false;
        }
        return isSeePlayer;
    }

    private void Attack(){
        if(isAttack){
            enemyAnimator.SetTrigger("Attack");
            buffedMovementSpeed = 0;
        }else if(!isAttack){
            enemyAnimator.ResetTrigger("Attack");
        }
    }

    void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.tag == "Player" && isPlayerDetected){
            isAttack = true;
        }
    }
}
