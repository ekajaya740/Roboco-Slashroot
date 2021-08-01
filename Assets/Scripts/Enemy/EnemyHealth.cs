using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [HideInInspector] public float enemyHealth;
    private float maxEnemyHealth;
    [SerializeField] private HealthBarManager healthBarManager;
    [SerializeField] private PlayerAttack playerAttack;

    private bool isAttacked;


    void Awake(){
        maxEnemyHealth = 2000;
    }
    
    void Start()
    {
        enemyHealth = maxEnemyHealth;

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

}
