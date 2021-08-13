using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGameManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject respawnPoint;
    [SerializeField] private PlayerMove playerMove;

    [SerializeField] private GameObject[] notDestroyedGameObjects;
    
    void Start()
    {
        respawnPlayer();
        for(int i = 0; i < notDestroyedGameObjects.Length; i++){
            DontDestroyOnLoad(notDestroyedGameObjects[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        respawnPlayer();
    }

    public void respawnPlayer(){
        if(player.transform.position.y < -10f){
            player.transform.position = respawnPoint.transform.position; 
            playerMove.movementSpeed = playerMove.initialMovementSpeed;
        }
    }
}
