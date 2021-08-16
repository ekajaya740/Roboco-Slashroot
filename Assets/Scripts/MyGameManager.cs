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
    [SerializeField] private GameObject gameOverPanel;
    private GameObject UICanvas;
    private GameObject eventSystem;

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

        heart1 = GameObject.Find("Heart1");
        heart2 = GameObject.Find("Heart2");
        heart3 = GameObject.Find("Heart3");

    }

    // Update is called once per frame
    void Update()
    {
        RespawnToRP();

    }


    public void RespawnToRP(){
        if(isLevelLoaded){
            respawnPoint = GameObject.Find("RespawnPoint");
            playerGameObject.transform.position = respawnPoint.transform.position;
            isLevelLoaded = false;
        }

        if(playerGameObject.GetComponent<PlayerHealth>().isDead || playerGameObject.GetComponent<PlayerMove>().isFalling){
            playerGameObject.transform.position = respawnPoint.transform.position;
            playerCredits--;
            HeartController();
            playerGameObject.GetComponent<PlayerHealth>().isDead = false;
            playerGameObject.GetComponent<PlayerMove>().isFalling = false;
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
