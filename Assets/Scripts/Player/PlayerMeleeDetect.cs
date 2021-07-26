using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeDetect : MonoBehaviour
{
    public bool isMelee {get; private set;}

    void Awake(){
        isMelee = false;
    }
    
    void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.tag == "Enemy"){
            isMelee = true;
        }
    }

    void OnTriggerExit2D(Collider2D collider){
        if(collider.gameObject.tag == "Enemy"){
            isMelee = false;
        }
    }
}
