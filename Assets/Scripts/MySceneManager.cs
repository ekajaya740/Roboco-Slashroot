using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour
{
    private bool isCanMoveStage;
    [SerializeField] private PlayerGetKey playerGetKey;
    private Scene thisScene;

    void Awake(){
        isCanMoveStage = false;
    }

    void Start()
    {
        thisScene = SceneManager.GetActiveScene();
    }

    // Update is called once per frame
    void Update()
    {
        MoveStage();
    }


    private void MoveStage(){
        

        // if(isCanMoveStage && CheckAllEnemyDead()){
            if(thisScene.buildIndex == 0){
                SceneManager.LoadScene(1);
            }
        // }
    }

    private bool CheckAllEnemyDead(){
        if(GameObject.FindGameObjectsWithTag("Enemy").Length == 0){
            return true;
        }
        return false;
    }

    void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.tag == "Player"){
            if(playerGetKey.isGetKey){
                isCanMoveStage = true;
                
            }
        }
    }
}
