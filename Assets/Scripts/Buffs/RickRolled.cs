using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RickRolled : MonoBehaviour
{
    public bool isRickrolled;
    private AudioSource rickRoll;
    private AudioSource myBGM;

    void Update()
    {
        if(myBGM == null){
            myBGM = GameObject.Find("BGM").GetComponent<AudioSource>();
        }
        
        if(rickRoll == null){
            rickRoll = GameObject.Find("RickRolled").GetComponent<AudioSource>();
        }

        

        if(isRickrolled){
            myBGM.Stop();
            rickRoll.Play();
        }
    }

    void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.tag == "Player"){
            isRickrolled = true;
        }
    }
}
