using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGameManager : MonoBehaviour
{
    //https://www.dafont.com/pixels.font
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
    private PlayerHealth playerHealth;

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
        playerHealth = playerGameObject.GetComponent<PlayerHealth>();

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

        if(respawnPoint == null){
            respawnPoint = GameObject.Find("RespawnPoint");
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

        if(gameOverPanel == null){
            gameOverPanel = GameObject.Find("GameOverPanel");
        }

        RespawnIfFall();
        LevelLoadedRespawn();
        HeartController();

        SuperSimpleSecure();

    }

    public void RespawnToRP(){
        playerGameObject.transform.position = respawnPoint.transform.position;
    }

    private void RespawnIfFall(){
        if(playerGameObject.GetComponent<PlayerMove>().isFalling){
            playerCredits--;
            RespawnToRP();
            playerHealth.playerHealthNow = playerHealth.maxPlayerHealth;
            playerGameObject.GetComponent<PlayerMove>().isFalling = false;
        }
    }

    private void LevelLoadedRespawn(){
        if(isLevelLoaded){
            RespawnToRP();
            isLevelLoaded = false;
        }
    }

    public void HeartController(){
        switch(playerCredits){
            case 0:
                heart1.SetActive(false);
                Time.timeScale = 0;
                gameOverPanel.SetActive(true);
                break;
            case 1:
                heart2.SetActive(false);
                break;
            case 2:
                heart3.SetActive(false);
                break;
        }
    }

    private void SuperSimpleSecure(){
        if(playerCredits > 3){
            playerCredits--;
        }
    }

}
