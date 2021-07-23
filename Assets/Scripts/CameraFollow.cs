using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private PlayerMove playerMove;
    private float playerSpeed;

    void Awake(){
        playerMove = new PlayerMove();
        playerSpeed = playerMove.movementSpeed;
    }

    void Update()
    {
        float interpolation = playerSpeed * Time.deltaTime;
        Vector3 position = gameObject.transform.position;

        position.x = Mathf.Lerp(gameObject.transform.position.x, player.transform.position.x, interpolation);        
        position.y = Mathf.Lerp(gameObject.transform.position.y, player.transform.position.y, interpolation);
        
        gameObject.transform.position = position;
    }
}
