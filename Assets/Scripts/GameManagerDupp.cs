using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerDupp : MonoBehaviour
{
    private GameObject gameManager;
    private GameObject player;
    private GameObject UI;
    private GameObject eventSystem;
    private GameObject mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        player = GameObject.Find("Player");
        UI = GameObject.Find("UICanvas");
        eventSystem = GameObject.Find("EventSystem");
        mainCamera = GameObject.Find("Main Camera");

    }

    // Update is called once per frame
    void Update()
    {
        if(GameObject.FindGameObjectsWithTag("GameManager").Length > 1){
            Destroy(gameManager);
        }

        if(GameObject.FindGameObjectsWithTag("Player").Length > 1){
            Destroy(player);
        }

        if(GameObject.FindGameObjectsWithTag("UI").Length > 1){
            Destroy(UI);
        }

        if(GameObject.FindGameObjectsWithTag("EventSystem").Length > 1){
            Destroy(eventSystem);
        }

        if(GameObject.FindGameObjectsWithTag("MainCamera").Length > 1){
            Destroy(mainCamera);
        }
    }
}
