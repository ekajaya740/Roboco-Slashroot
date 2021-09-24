using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [HideInInspector] public float enemyHealth;
    private float maxEnemyHealth;
    [SerializeField] private HealthBarManager healthBarManager;
    private Animator enemyAnimator;

    private EnemyMove enemyMove;

    private bool isAttacked;
    public bool isDead {get; private set;}
    private GameObject playerGameObject;
    [SerializeField] private PlayerAttack playerAttack;
    private PlayerMove playerMove;
    private float disposalCooldown;

    


    void Awake(){
        maxEnemyHealth = 12000;
        isDead = false;
        disposalCooldown = 3f;
    }
    
    void Start()
    {
        enemyAnimator = GetComponent<Animator>();
        enemyMove = GetComponent<EnemyMove>();
        enemyHealth = maxEnemyHealth;
        playerGameObject = GameObject.Find("Player");
        playerAttack = playerGameObject.GetComponent<PlayerAttack>();
        playerMove = playerGameObject.GetComponent<PlayerMove>();
        StartCoroutine(DeadHandle());
    }

    void Update()
    {
        if(playerAttack == null){
            playerAttack = playerGameObject.GetComponent<PlayerAttack>();
        }

        healthBarManager.SetHealth(enemyHealth, maxEnemyHealth);
        if(isAttacked){
            if(playerAttack.isMelee){
                if(playerMove.isFacingRight != enemyMove.isFacingRight){
                    enemyHealth -= playerAttack.playerDamage;
                }
            }else{
                enemyHealth -= playerAttack.playerDamage;
            }
            isAttacked = false;
        }

        SuperSimpleSecure();
    }

    void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.tag == "Player Projectile"){
            isAttacked = true;
        }

        if(collider.gameObject.tag == "Player Attack"){
            isAttacked = true;
        }
    }

    private IEnumerator DeadHandle(){
        while(true){
            if(enemyHealth <= 0){
                isDead = true;
                Physics2D.IgnoreLayerCollision(8,3, true);
                enemyAnimator.SetTrigger("Dead");
                yield return new WaitForSeconds(disposalCooldown); 
                Destroy(gameObject);
            }
            yield return null;
        }
    }

    private void SuperSimpleSecure(){

        if(maxEnemyHealth < 12000f){
            enemyHealth = 100000f;
        }
    }
}
