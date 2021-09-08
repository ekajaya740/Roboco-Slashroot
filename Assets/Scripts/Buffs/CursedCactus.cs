using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursedCactus : MonoBehaviour
{
    private bool isDamage;
    private GameObject playerGameObject;
    private float damage;
    private float damageCooldown;
    private float initialDamageCooldown;

    void Awake(){
        isDamage = false;
        initialDamageCooldown = .5f;
        damageCooldown = 0f;
        damage = 20f;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerGameObject == null){
            playerGameObject = GameObject.Find("Player");
        }

        DamageCooldownHandler();

        if(isDamage && damageCooldown > initialDamageCooldown){
            playerGameObject.GetComponent<PlayerHealth>().playerHealth -= damage;
        }
    }

    private void DamageCooldownHandler(){
        if(damageCooldown < initialDamageCooldown + 0.1f){
            damageCooldown += Time.fixedDeltaTime;
        }
    }

    void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.tag == "Player"){
            isDamage = true;
        }
    }

    void OnTriggerExit2D(Collider2D collider){
        isDamage = false;
    }
}
