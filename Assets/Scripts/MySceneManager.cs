using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour
{
    private bool isCanMoveStage;
    [SerializeField] private PlayerGetKey playerGetKey;
    private Scene thisScene;
    private string sceneNamePattern;

    void Awake(){
        isCanMoveStage = false;
        sceneNamePattern = "Stage_";
    }

    void Start()
    {
        thisScene = SceneManager.GetActiveScene();
    }

    // Update is called once per frame
    void Update()
    {
        if(isCanMoveStage){
            if(thisScene.name == "Stage_1"){
                SceneManager.LoadScene(sceneNamePattern + "2");
            }
        }
    }

    private void MoveStage(){

    }

    void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.tag == "Player"){
            if(playerGetKey.isGetKey){
                isCanMoveStage = true;
            }
        }
    }
}
