using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour
{
    private bool isCanMoveStage;
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
        MoveStage();
    }


    private void MoveStage(){
        

        if(isCanMoveStage && CheckAllEnemyDead()){
            if(thisScene.buildIndex == 0){
                myGameManager.GetComponent<MyGameManager>().isLevelLoaded = true;
                SceneManager.LoadScene(1);
                randomBoxTrigger.isTriggered = false;
            }
        }
    }

    private bool CheckAllEnemyDead(){
        if(GameObject.FindGameObjectsWithTag("Enemy").Length == 0){
            return true;
        }
        return false;
    }

    void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.tag == "Player"){
            if(randomBoxTrigger.isTriggered){
                isCanMoveStage = true;
                
            }
        }
    }
}
