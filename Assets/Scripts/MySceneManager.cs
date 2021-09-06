using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        if(randomBoxTrigger == null){
            randomBoxTrigger = GameObject.Find("Random Box").GetComponent<RandomBoxTrigger>();
        }
        MoveStage();
    }


    private void MoveStage(){
        if(randomBoxTrigger.isTriggered && CheckAllEnemyDead() && isCanMoveStage){
            switch(thisScene.buildIndex){
                case 0:
                    myGameManager.GetComponent<MyGameManager>().isLevelLoaded = true;
                    SceneManager.LoadScene(1);
                    isCanMoveStage = false;
                    randomBoxTrigger.isTriggered = false;
                    break;
                case 1:
                    myGameManager.GetComponent<MyGameManager>().isLevelLoaded = true;
                    SceneManager.LoadScene(2);
                    isCanMoveStage = false;
                    randomBoxTrigger.isTriggered = false;
                    break;
                case 2:
                    myGameManager.GetComponent<MyGameManager>().isLevelLoaded = true;
                    SceneManager.LoadScene(3);
                    isCanMoveStage = false;
                    randomBoxTrigger.isTriggered = false;
                    break;
                case 3:
                    myGameManager.GetComponent<MyGameManager>().isLevelLoaded = true;
                    SceneManager.LoadScene(4);
                    isCanMoveStage = false;
                    randomBoxTrigger.isTriggered = false;
                    break;
                case 4:
                    myGameManager.GetComponent<MyGameManager>().isLevelLoaded = true;
                    SceneManager.LoadScene(5);
                    isCanMoveStage = false;
                    randomBoxTrigger.isTriggered = false;
                    break;
                case 5:
                    GameObject.Find("BGM").SetActive(false);
                    GameObject.Find("RickRolled").SetActive(false);
                    GameObject.Find("EndingSound").SetActive(true);
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
