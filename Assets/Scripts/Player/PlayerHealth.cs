using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    [HideInInspector] public float playerHealth;
    private float maxPlayerHealth;
    [SerializeField] private HealthBarManager healthBarManager;
    [SerializeField] private EnemyAttack enemyAttack;
    private Rigidbody2D playerRB;
    
    private Animator playerAnimator;
    private bool isDead;

    void Awake(){
        maxPlayerHealth = 5000f;
        isDead = false;
    }    

    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerHealth = maxPlayerHealth;
        StartCoroutine(HealthRegen());
        StartCoroutine(PlayerAttacked());
        // StartCoroutine(DeadState());
    }

    // Update is called once per frame
    void Update()
    {
        healthBarManager.SetHealth(playerHealth, maxPlayerHealth);
        // if(isDead){
        //     StopCoroutine(PlayerAttacked());
        //     StopCoroutine(HealthRegen());
        // }
    }

    void FixedUpdate(){

    }

    private IEnumerator HealthRegen(){
        while(true){
            if(playerHealth < maxPlayerHealth){
                playerHealth += 0.1f;
            }
            yield return new WaitForSeconds(1);
        }
    }

    private IEnumerator PlayerAttacked(){
        while(true){
            if(enemyAttack.isAttack){
                playerHealth -= enemyAttack.enemyDamage;
            }
            yield return new WaitForSeconds(0.2f);
        }
    }

    private IEnumerator DeadState(){
        while(true){
            if(playerHealth <= 0){
                isDead = true;
                playerAnimator.SetTrigger("Dead");
            }
            yield return null;
        }
    }
}
