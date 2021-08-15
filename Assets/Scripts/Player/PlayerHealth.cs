using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [HideInInspector] public float playerHealth;
    private float maxPlayerHealth;
    [SerializeField] private HealthBarManager healthBarManager;
    [SerializeField] private EnemyAttack enemyAttack;
    private PlayerMove playerMove;
    private Rigidbody2D playerRB;
    
    private Animator playerAnimator;
    private bool isDead;
    private GameObject gameManager;
    private MyGameManager myGameManager;
    
    

    void Awake(){
        maxPlayerHealth = 5000f;
        isDead = false;
    }    

    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerHealth = maxPlayerHealth;
        gameManager = GameObject.Find("GameManager");
        myGameManager = gameManager.GetComponent<MyGameManager>();
        
        StartCoroutine(HealthRegen());
        StartCoroutine(DeadState());
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

    

    private IEnumerator DeadState(){
        while(true){
            if(playerHealth <= 0){
                isDead = true;
                playerAnimator.SetTrigger("Dead");                
                yield return new WaitForSeconds(5f);
                myGameManager.RespawnToRP();
                isDead = false;
                playerAnimator.ResetTrigger("Dead");
                playerHealth = maxPlayerHealth;
            }
            yield return null;
        }
    }


}
