using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayerForward : MonoBehaviour
{
    public bool playerDetected {get; private set;}

    void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.tag == "Player"){
            playerDetected = true;

        }
    }

    void OnTriggerExit2D(Collider2D collider){
        if(collider.gameObject.tag == "Player"){
            playerDetected = false;
        }

    }
}
