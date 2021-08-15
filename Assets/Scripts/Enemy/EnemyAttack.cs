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

    void Awake(){
        enemyDamage = 250;
    }

    void Start()
    {
        enemyMove = GetComponent<EnemyMove>();
        enemyRB = GetComponent<Rigidbody2D>();
        enemyAnimator = GetComponent<Animator>();
        playerGameObject = GameObject.Find("Player");
        playerRB = playerGameObject.GetComponent<Rigidbody2D>();
        playerHealthClass = playerGameObject.GetComponent<PlayerHealth>();
        StartCoroutine(PlayerAttacked());
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
        }else{
            enemyAnimator.ResetTrigger("Attack");
        }
    }

    private IEnumerator PlayerAttacked(){
        while(true){

            if(EnemyAttack.isAttack){
                playerHealthClass.playerHealth -= enemyDamage;
            }

            yield return new WaitForSeconds(0.7f);
        }
    }


}
