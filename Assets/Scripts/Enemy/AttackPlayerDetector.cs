using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPlayerDetector : MonoBehaviour
{
    public bool isAttack {get; private set;}
    
    void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.tag == "Player"){
            isAttack = true;
        }
    }

    void OnTriggerExit2D(Collider2D collider){
        if(collider.gameObject.tag == "Player"){
            isAttack = false;

        }
    }
}
