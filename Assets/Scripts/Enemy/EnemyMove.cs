using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{

    public bool isPatrol;
    public bool isFacingRight {get; private set;}
    public bool mustTurn;

    private BoxCollider2D enemyCollider;
    private Rigidbody2D enemyRB;
    public float movementSpeed;

    [SerializeField] private Transform groundCheckPos;
    [SerializeField] private LayerMask platformLayer;
    private Animator enemyAnimator;

    void Awake(){
        isPatrol = true;
        isFacingRight = true;
    }
    
    void Start()
    {
        enemyRB = GetComponent<Rigidbody2D>();
        enemyCollider = GetComponent<BoxCollider2D>();
        enemyAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        enemyAnimator.SetFloat("Speed", Mathf.Abs(enemyRB.velocity.x));
        if(isPatrol){
            Patrol();
        }
    }

    void FixedUpdate(){
        if(isPatrol){
            mustTurn = !Physics2D.OverlapCircle(groundCheckPos.position, .1f, platformLayer);
        }
    }

    private void Patrol(){
        if(mustTurn || enemyCollider.IsTouchingLayers(platformLayer)){
            Flip();
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
