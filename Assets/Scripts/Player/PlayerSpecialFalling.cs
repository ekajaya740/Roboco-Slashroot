using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSpecialFalling : MonoBehaviour
{
    private GameObject player;
    private bool isBackToStage1;

    // Update is called once per frame
    void Update()
    {
        if(player == null){
            player = GameObject.Find("Player");
        }

        if(isBackToStage1){
            SceneManager.LoadScene(0);
            isBackToStage1 = false;
        }
    }

    void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.tag == "Player"){
            isBackToStage1 = true;
        }
    }
}
