using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [HideInInspector] public bool isAttack { get; private set;}
    private EnemyMove enemyMove;
    private Rigidbody2D enemyRB;
    private Animator enemyAnimator;
    public float enemyDamage { get; private set;}
    [SerializeField] private AttackPlayerDetector attackPlayerDetector;
    [SerializeField] private Rigidbody2D playerRB;

    void Awake(){
        enemyDamage = 5;
    }

    void Start()
    {
        enemyMove = GetComponent<EnemyMove>();
        enemyRB = GetComponent<Rigidbody2D>();
        enemyAnimator = GetComponent<Animator>();
    }

    void Update(){
        Attack();
    }
    void FixedUpdate(){

    }

    public void Attack(){
        isAttack = attackPlayerDetector.isAttack;
        if(isAttack){
            enemyMove.movementSpeed = 0;
            enemyAnimator.SetTrigger("Attack");
            // if(enemyAnimator.GetCurrentAnimatorStateInfo(0).IsName("enemy_attack") && enemyAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.99f){
                
            //     isAttack = false;
            // }
        }else{
            enemyAnimator.ResetTrigger("Attack");
        }
    }


}
