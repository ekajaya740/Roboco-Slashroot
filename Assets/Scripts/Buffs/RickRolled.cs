using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RickRolled : MonoBehaviour
{
    public bool isRickrolled;
    private AudioSource rickRoll;
    private GameObject myBGM;

    void Awake(){
        isRickrolled = false;
    }

    void Update()
    {
        if(myBGM == null){
            myBGM = GameObject.Find("BGM");
        }
        
        if(rickRoll == null){
            rickRoll = GameObject.Find("RickRoll").GetComponent<AudioSource>();
        }

        

        if(isRickrolled){
            myBGM.SetActive(false);
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
