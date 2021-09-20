using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [HideInInspector] public static bool isAttack { get; private set;}
    private EnemyMove enemyMove;
    private Rigidbody2D enemyRB;
    private Animator enemyAnimator;
    public float enemyDamage { get; private set;}
    
    private GameObject playerGameObject;
    [SerializeField] private AttackPlayerDetector attackPlayerDetector;
    private PlayerHealth playerHealthClass;
    [SerializeField] private Rigidbody2D playerRB;

    private float initialAttackCooldown;
    private float attackCooldown;

    void Awake(){
        enemyDamage = 300;
        initialAttackCooldown = 0.7f;
        attackCooldown = 0;
    }

    void Start()
    {
        enemyMove = GetComponent<EnemyMove>();
        enemyRB = GetComponent<Rigidbody2D>();
        enemyAnimator = GetComponent<Animator>();
        playerGameObject = GameObject.Find("Player");
        playerRB = playerGameObject.GetComponent<Rigidbody2D>();
        playerHealthClass = playerGameObject.GetComponent<PlayerHealth>();
        // StartCoroutine(PlayerAttacked());
    }

    void Update(){
        if(playerRB == null){
            playerRB = playerGameObject.GetComponent<Rigidbody2D>();
        }
        Attack();
        
        if(attackCooldown < initialAttackCooldown + 0.1f){
            attackCooldown += Time.fixedDeltaTime;
        }
        
        if(EnemyAttack.isAttack && attackCooldown >= initialAttackCooldown){
            playerHealthClass.playerHealthNow -= enemyDamage;
            attackCooldown = 0;
        }
    }

    public void Attack(){
        isAttack = attackPlayerDetector.isAttack;
        if(isAttack){
            enemyMove.movementSpeed = 0;
            enemyAnimator.SetTrigger("Attack");
        }else{
            enemyAnimator.ResetTrigger("Attack");
        }
    }

    private void SuperSimpleSecure(){
        if(enemyDamage < 300f){
            enemyDamage = 999f;
        }
    }
}
