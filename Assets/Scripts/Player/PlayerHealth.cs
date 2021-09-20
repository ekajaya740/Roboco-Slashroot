using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [HideInInspector] public float playerHealthNow;
    private float playerHealthRegen;
    public float maxPlayerHealth { get; private set;}
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
        playerHealthRegen = 1f;
    }    

    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerHealthNow = maxPlayerHealth;
        gameManager = GameObject.Find("GameManager");
        myGameManager = gameManager.GetComponent<MyGameManager>();
        
        StartCoroutine(HealthRegen());
        StartCoroutine(DeadState());
    }

    // Update is called once per frame
    void Update()
    {
        healthBarManager.SetHealth(playerHealthNow, maxPlayerHealth);
        SuperSimpleSecure();
    }

    void FixedUpdate(){
    }

    private IEnumerator HealthRegen(){
        while(true){
            if(playerHealthNow < maxPlayerHealth){
                playerHealthNow += playerHealthRegen;
            }
            yield return new WaitForSeconds(1);

        }
    }

    

    private IEnumerator DeadState(){
        while(true){
            if(playerHealthNow <= 0){
                isDead = true;
                playerAnimator.SetTrigger("Dead");                
                yield return new WaitForSeconds(0.9f);
                myGameManager.playerCredits--;
                playerAnimator.ResetTrigger("Dead");
                isDead = false;
                myGameManager.RespawnToRP();
                playerHealthNow = maxPlayerHealth;
            }
            yield return null;
        }
    }

    private void SuperSimpleSecure(){
        if(playerHealthNow > maxPlayerHealth){
            playerHealthNow = maxPlayerHealth;
        }

        if(playerHealthRegen > 1f){
            playerHealthRegen = 1f;
        }

        if(maxPlayerHealth > 3400f){
            playerHealthNow -= maxPlayerHealth;
        }
    }


}
