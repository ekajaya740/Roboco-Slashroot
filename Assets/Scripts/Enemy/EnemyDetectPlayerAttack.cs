using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetectPlayerAttack : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isAttacked {get; private set;}
    void Awake(){
        isAttacked = false;
    }

    void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.tag == "Player Projectile"){
            isAttacked = true;
        }

        if(collider.gameObject.tag == "Player Attack"){
            isAttacked = true;
        }
    }

    void OnTriggerExit2D(Collider2D collider){
        if(collider.gameObject.tag == "Player Projectile"){
            isAttacked = false;
        }

        if(collider.gameObject.tag == "Player Attack"){
            isAttacked = false;
        }
    }
}
