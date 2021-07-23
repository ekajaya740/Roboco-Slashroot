using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private PlayerController playerController;
    private float playerSpeed;

    void Awake(){
        playerController = new PlayerController();
        playerSpeed = playerController.getMovementSpeed();
    }

    void Start(){
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
