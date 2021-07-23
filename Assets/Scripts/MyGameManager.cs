using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGameManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject respawnPoint;
    // private CameraFollow cameraFollow;
    public bool isRespawn;
    
    
    void Start()
    {
        isRespawn = false;
        // cameraFollow = GetComponent<CameraFollow>();
    }

    // Update is called once per frame
    void Update()
    {
        respawnPlayer();
    }

    private void respawnPlayer(){
        if(player.transform.position.y < -10f){
            player.transform.position = respawnPoint.transform.position; 
            isRespawn = true;
        }
    }
}
