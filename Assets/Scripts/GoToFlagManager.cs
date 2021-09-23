using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GoToFlagManager : MonoBehaviour
{
    private GameObject spawnFlag;
    private RandomBoxTrigger randomBoxTrigger;
    private MySceneManager mySceneManager;
    private GameObject player;
    private bool moveToFlag = false;
    void Update()
    {
        if(spawnFlag == null){
            spawnFlag = GameObject.Find("SpawnFlag");
        }    

        if(randomBoxTrigger == null){
            randomBoxTrigger = GameObject.Find("Random Box").GetComponent<RandomBoxTrigger>();
        }    

        if(mySceneManager == null){
            mySceneManager = GameObject.Find("Move Level").GetComponent<MySceneManager>();
        }

        if(player == null){
            player = GameObject.Find("Player");
        }


        if(moveToFlag){
            player.transform.position = spawnFlag.transform.position;
            moveToFlag = false;
            if(SceneManager.GetActiveScene().buildIndex == 5){
                GameObject.Find("StageDesc").GetComponentInChildren<TextMeshProUGUI>().SetText("Selamat");
                GameObject.Find("RickRolled").GetComponent<AudioSource>().Stop();
                GameObject.Find("EndingSound").GetComponent<AudioSource>().Play();
            }
        }

    }

    void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.tag == "Player"){
            if(randomBoxTrigger.isTriggered && mySceneManager.CheckAllEnemyDead()){
                moveToFlag = true;
            }
        }
    }
}
