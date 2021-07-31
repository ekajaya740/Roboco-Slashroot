using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{

    public bool isPatrol;
    public bool isFacingRight {get; private set;}
    private bool mustTurn;

    private BoxCollider2D enemyCollider;
    private Rigidbody2D enemyRB;
    public float movementSpeed;
    private float normalMovementSpeed;
    private float buffedMovementSpeed;

    [SerializeField] private Transform groundCheckPos;
    [SerializeField] private LayerMask platformLayer;
    private Animator enemyAnimator;
    private bool followPlayer;

    [SerializeField] private DetectPlayerBehind detectPlayerBehind;
    [SerializeField] private DetectPlayerForward detectPlayerForward;
    [SerializeField] private Transform playerTransform;
    private EnemyAttack enemyAttack;

    void Awake(){
        isPatrol = true;
        isFacingRight = true;
        normalMovementSpeed = 100;
        movementSpeed = normalMovementSpeed;
        buffedMovementSpeed = 200;
    }
    
    void Start()
    {
        enemyRB = GetComponent<Rigidbody2D>();
        enemyCollider = GetComponent<BoxCollider2D>();
        enemyAnimator = GetComponent<Animator>();
        enemyAttack = GetComponent<EnemyAttack>();
    }

    void Update()
    {
        if(transform.localScale.x < 0){
            buffedMovementSpeed = -200;
            normalMovementSpeed = -100;
        }else if(transform.localScale.x > 0){
            buffedMovementSpeed = 200;
            normalMovementSpeed = 100;
        }

        if(!enemyAttack.isAttack){
            enemyAnimator.SetFloat("Speed", Mathf.Abs(enemyRB.velocity.x));
            if(isPatrol){
                Patrol();
            }
        }
    }

    void FixedUpdate(){
        if(isPatrol){
            mustTurn = !Physics2D.OverlapCircle(groundCheckPos.position, .1f, platformLayer);
        }

        if(detectPlayerBehind.playerDetected){
            Flip();
        }

        if(detectPlayerForward.playerDetected){
            movementSpeed = buffedMovementSpeed;
            followPlayer = true;
        }else{
            movementSpeed = normalMovementSpeed;
        }

        if(followPlayer){
            if(gameObject.transform.position.x < playerTransform.position.x){
                if(!isFacingRight){
                    Flip();
                }
            }else if(gameObject.transform.position.x > playerTransform.position.x){
                if(isFacingRight){
                    Flip();
                }
            }
        }
        
    }

    private void Patrol(){
        if(mustTurn || enemyCollider.IsTouchingLayers(platformLayer)){
            Flip();
            followPlayer = false;
        }

        enemyRB.velocity = new Vector2(movementSpeed * Time.fixedDeltaTime, enemyRB.velocity.y);
    }

    public void Flip(){
        isPatrol = false;
        isFacingRight = !isFacingRight;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        movementSpeed *= -1;
        isPatrol = true;
    }
}
