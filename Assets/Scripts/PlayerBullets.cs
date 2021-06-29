using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullets : MonoBehaviour
{
    private Rigidbody2D bulletRB;
    private float bulletSpeed;
    private PolygonCollider2D bulletCollider;

    void Awake(){
        bulletRB = GetComponent<Rigidbody2D>();
        bulletCollider = GetComponent<PolygonCollider2D>();
        bulletSpeed = 10f;
    }
    void Start()
    {
        Invoke("destroyBullet", .5f);
    }


    void FixedUpdate()
    {
        bulletRB.velocity = transform.right * bulletSpeed;
        
    }

    private void destroyBullet(){
        Destroy(gameObject);
    }

    
    void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.tag == "Ground"){
            destroyBullet();
        }

        if(collider.gameObject.tag == "Enemy"){
            destroyBullet();
        }
    }

}
