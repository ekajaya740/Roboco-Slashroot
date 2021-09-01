using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RickRolled : MonoBehaviour
{
    public bool isRickrolled;
    private AudioSource rickRoll;

    void Awake(){
        isRickrolled = false;
    }

    void Update()
    {
        if(rickRoll == null){
            rickRoll = GameObject.Find("RickRoll").GetComponent<AudioSource>();
        }

        if(isRickrolled){
            rickRoll.Play();
        }
    }

    void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.tag == "Player"){
            isRickrolled = true;
        }
    }

    void OnTriggerExit2D(Collider2D collider){
        if(collider.gameObject.tag == "Player"){
            isRickrolled = false;
        }
    }
}
