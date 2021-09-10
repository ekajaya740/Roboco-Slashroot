using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGameManager : MonoBehaviour
{
    public int playerCredits;

    private GameObject playerGameObject;
    private GameObject respawnPoint;

    private GameObject heart1;
    private GameObject heart2;
    private GameObject heart3;
    private GameObject gameOverPanel;
    private GameObject UICanvas;
    private GameObject eventSystem;
    private GameObject cameraGameObject;

    public bool isLevelLoaded;
    
    
    void Awake(){
        playerCredits = 3;
        isLevelLoaded = true;
    }
    
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        UICanvas = GameObject.Find("UICanvas");
        DontDestroyOnLoad(UICanvas);
        eventSystem = GameObject.Find("EventSystem");
        DontDestroyOnLoad(eventSystem);
        playerGameObject = GameObject.Find("Player");
        respawnPoint = GameObject.Find("RespawnPoint");
        cameraGameObject = GameObject.Find("Main Camera");
        gameOverPanel = GameObject.Find("GameOverPanel");

        heart1 = GameObject.Find("Heart1");
        heart2 = GameObject.Find("Heart2");
        heart3 = GameObject.Find("Heart3");
    }

    // Update is called once per frame
    void Update()
    {
        if(playerGameObject == null){
            playerGameObject = GameObject.Find("Player");
        }

        if(UICanvas == null){
            UICanvas = GameObject.Find("UICanvas");
            DontDestroyOnLoad(UICanvas);
        }

        if(eventSystem == null){
            eventSystem = GameObject.Find("EventSystem");
            DontDestroyOnLoad(eventSystem);
        }

        if(heart1 == null){
            heart1 = GameObject.Find("Heart1");
        }

        if(heart2 == null){
            heart2 = GameObject.Find("Heart2");
        }

        if(heart3 == null){
            heart3 = GameObject.Find("Heart3");
        }

        RespawnIfFall();
        LevelLoadedRespawn();
        HeartController();

    }


    public void RespawnToRP(){
        playerGameObject.transform.position = respawnPoint.transform.position;
    }

    private void RespawnIfFall(){
        if(playerGameObject.GetComponent<PlayerMove>().isFalling){
            RespawnToRP();
            playerCredits--;
            playerGameObject.GetComponent<PlayerMove>().isFalling = false;
        }
    }

    private void LevelLoadedRespawn(){
        if(isLevelLoaded){
            respawnPoint = GameObject.Find("RespawnPoint");
            RespawnToRP();
            isLevelLoaded = false;
        }
    }

    public void HeartController(){
        switch(playerCredits){
            case 0:
                heart1.SetActive(false);
                gameOverPanel.SetActive(true);
                Time.timeScale = 0;
                break;
            case 1:
                heart2.SetActive(false);
                break;
            case 2:
                heart3.SetActive(false);
                break;
        }
    }

}
