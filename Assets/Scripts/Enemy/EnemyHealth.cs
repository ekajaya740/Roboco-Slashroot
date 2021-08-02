using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [HideInInspector] public float enemyHealth;
    private float maxEnemyHealth;
    [SerializeField] private HealthBarManager healthBarManager;
    [SerializeField] private PlayerAttack playerAttack;
    private Animator enemyAnimator;

    private EnemyMove enemyMove;

    private bool isAttacked;
    public bool isDead {get; private set;}
    private float disposalCooldown;

    


    void Awake(){
        maxEnemyHealth = 2000;
        isDead = false;
        disposalCooldown = 0;
    }
    
    void Start()
    {
        enemyAnimator = GetComponent<Animator>();
        enemyMove = GetComponent<EnemyMove>();
        enemyHealth = maxEnemyHealth;
        StartCoroutine(DeadHandle());
    }

    // Update is called once per frame
    void Update()
    {
        healthBarManager.SetHealth(enemyHealth, maxEnemyHealth);
        if(isAttacked){
            enemyHealth -= playerAttack.playerDamage;
            isAttacked = false;
        }
    }

    void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.tag == "Player Attack"){
            isAttacked = true;
        }
    }

    private IEnumerator DeadHandle(){
        while(true){
            if(enemyHealth <= 0){
                isDead = true;
                enemyAnimator.SetTrigger("Dead");
                yield return new WaitForSeconds(5f); 
                Destroy(gameObject);
            }
            yield return null;
        }
    }
}
