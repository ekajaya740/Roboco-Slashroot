using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterDetect : MonoBehaviour
{

    private GameObject gameManager;
    void Update(){
        if(gameManager == null){
            gameManager = GameObject.Find("GameManager");
        }
    }

    void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.tag == "Player"){
            if(gameManager != null){
                gameManager.GetComponent<MyGameManager>().RespawnToRP();
            }

        }
    }
}
