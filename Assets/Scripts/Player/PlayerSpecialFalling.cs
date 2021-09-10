using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpecialFalling : MonoBehaviour
{
    private GameObject player;
    private bool isBackToStage1;

    private MySceneManager mySceneManager;

    // Update is called once per frame
    void Update()
    {
        if(player == null){
            player = GameObject.Find("Player");
        }

        if(mySceneManager == null){
            mySceneManager = GameObject.Find("Move Level").GetComponent<MySceneManager>();
        }

        if(isBackToStage1){
            mySceneManager.MoveStageController(0, "BGM", "Stage 1 - Tutorial");
            isBackToStage1 = false;
        }
        
    }

    void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.tag == "Player"){
            isBackToStage1 = true;
        }
    }
}
