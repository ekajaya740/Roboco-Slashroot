using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MySceneManager : MonoBehaviour
{
    public bool isCanMoveStage { get; private set;}
    [SerializeField] private RandomBoxTrigger randomBoxTrigger;
    private GameObject myGameManager;
    private Scene thisScene;

    void Awake(){
        isCanMoveStage = false;
    }

    void Start()
    {
        myGameManager = GameObject.Find("GameManager");
        thisScene = SceneManager.GetActiveScene();
    }

    // Update is called once per frame
    void Update()
    {
        if(myGameManager == null){
            myGameManager = GameObject.Find("GameManager");
        }

        if(randomBoxTrigger == null){
            randomBoxTrigger = GameObject.Find("Random Box").GetComponent<RandomBoxTrigger>();
        }

        if(thisScene.buildIndex == 0){
            GameObject.Find("RickRolled").GetComponent<AudioSource>().Stop();
            GameObject.Find("BGM").GetComponent<AudioSource>().Stop();
        }
        MoveStage();
    }


    private void MoveStage(){
        if(randomBoxTrigger.isTriggered && CheckAllEnemyDead() && isCanMoveStage){
            switch(thisScene.buildIndex){
                case 0:
                    MoveStageController(4, "RickRolled", "Stage 5 - Hellium");
                    break;
                case 1:
                    MoveStageController(2, "BGM", "Stage 3 - Deep Cave");
                    break;
                case 2:
                    MoveStageController(3, "RickRolled", "Stage 4 - Skylar");
                    GameObject.Find("BGM").GetComponent<AudioSource>().Stop();
                    break;
                case 3:
                    MoveStageController(4, "RickRolled", "Stage 5 - Hellium");
                    break;
            }
        }
    }

    public bool CheckAllEnemyDead(){
        if(GameObject.FindGameObjectsWithTag("Enemy").Length == 0){
            return true;
        }
        return false;
    }

    public void MoveStageController(int sceneBuildIndex, string audioStr, string stageName){
        myGameManager.GetComponent<MyGameManager>().isLevelLoaded = true;
        SceneManager.LoadScene(sceneBuildIndex);
        GameObject.Find("StageDesc").GetComponentInChildren<TextMeshProUGUI>().SetText(stageName);
        GameObject.Find(audioStr).GetComponent<AudioSource>().Play();
        isCanMoveStage = false;
        randomBoxTrigger.isTriggered = false;
    }

    void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.tag == "Player"){
            isCanMoveStage = true;
        }
    }

    void OnTriggerExit2D(Collider2D collider){
        if(collider.gameObject.tag == "Player"){
            isCanMoveStage = false;
        }
    }
}
