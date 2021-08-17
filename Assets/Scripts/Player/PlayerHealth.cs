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
    public bool isDead;
    private GameObject gameManager;
    private MyGameManager myGameManager;
    
    

    void Awake(){
        maxPlayerHealth = 3400f;
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
                playerHealth += 1f;
            }
            yield return new WaitForSeconds(1);

        }
    }

    

    private IEnumerator DeadState(){
        while(true){
            if(playerHealth <= 0){

                isDead = true;
                playerAnimator.SetTrigger("Dead");                
                yield return new WaitForSeconds(0.9f);
                myGameManager.playerCredits--;
                playerAnimator.ResetTrigger("Dead");
                isDead = false;
                myGameManager.RespawnToRP();
                playerHealth = maxPlayerHealth;
            }
            yield return null;
        }
    }


}
