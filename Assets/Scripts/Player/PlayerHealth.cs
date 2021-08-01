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
    private SpriteRenderer playerSpriteRenderer;
    [SerializeField] private Animator enemyAnimator;

    void Awake(){
        maxPlayerHealth = 3000f;
    }    

    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        playerHealth = maxPlayerHealth;
        StartCoroutine(HealthRegen());
        StartCoroutine(PlayerAttacked());
    }

    // Update is called once per frame
    void Update()
    {
        healthBarManager.SetHealth(playerHealth, maxPlayerHealth);
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
            yield return new WaitForSeconds(0.07f);
        }
    }
}
