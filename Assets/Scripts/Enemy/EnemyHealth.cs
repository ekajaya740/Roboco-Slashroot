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
    private GameObject playerGameObject;
    private float disposalCooldown;

    


    void Awake(){
        maxEnemyHealth = 3500;
        isDead = false;
        disposalCooldown = 5f;
    }
    
    void Start()
    {
        enemyAnimator = GetComponent<Animator>();
        enemyMove = GetComponent<EnemyMove>();
        enemyHealth = maxEnemyHealth;
        playerGameObject = GameObject.Find("Player");
        playerAttack = playerGameObject.GetComponent<PlayerAttack>();
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
                Physics2D.IgnoreLayerCollision(8,3, true);
                enemyAnimator.SetTrigger("Dead");
                yield return new WaitForSeconds(disposalCooldown); 
                Destroy(gameObject);
            }
            yield return null;
        }
    }
}
