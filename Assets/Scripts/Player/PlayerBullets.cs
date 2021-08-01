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
        bulletSpeed = 30f;
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

    
    // void OnCollisionEnter2D(Collision2D collision){
    //     if(collision.gameObject.tag == "Ground"){
    //         destroyBullet();
    //     }

    //     if(collision.gameObject.tag == "Enemy"){
    //         destroyBullet();
    //     }
    // }

    void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.tag == "Enemy"){
            destroyBullet();
        }
    }

}
