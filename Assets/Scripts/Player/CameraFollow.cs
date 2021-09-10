using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private GameObject player;
    private PlayerMove playerMove;
    private float playerSpeed;

    

    void Start(){
        player = GameObject.Find("Player");
        playerMove = player.GetComponent<PlayerMove>();
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(player);
        playerSpeed = playerMove.initialMovementSpeed;
    }

    void Update()
    {
        if(player == null){
            player = GameObject.Find("Player");
            DontDestroyOnLoad(player);
        }
        float interpolation = playerSpeed * Time.deltaTime;
        Vector3 position = gameObject.transform.position;

        position.x = Mathf.Lerp(gameObject.transform.position.x, player.transform.position.x, interpolation);        
        position.y = Mathf.Lerp(gameObject.transform.position.y, player.transform.position.y, interpolation);
        
        gameObject.transform.position = position;
    }
}
