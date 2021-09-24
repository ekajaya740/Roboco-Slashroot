using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullets : MonoBehaviour
{
    private Rigidbody2D bulletRB;
    private float bulletSpeed;
    private PolygonCollider2D bulletCollider;
    private float initialDestroyTime;
    private float destroyTime;
    private bool isTouchEnemy;

    void Awake(){
        initialDestroyTime = .1f;
        destroyTime = initialDestroyTime;
        bulletSpeed = 30f;
        isTouchEnemy = false;
    }
    void Start()
    {
        bulletRB = GetComponent<Rigidbody2D>();
        bulletCollider = GetComponent<PolygonCollider2D>();
        Invoke("destroyBullet", .2f);
    }

    void Update(){
        if(isTouchEnemy){
            if(destroyTime <= 0){
                destroyBullet();
            }
            destroyTime -= Time.fixedDeltaTime;
        }
    }


    void FixedUpdate()
    {
        bulletRB.velocity = transform.right * bulletSpeed;
        
    }

    private void destroyBullet(){
        Destroy(gameObject);
        destroyTime = initialDestroyTime;
    }


    void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.tag == "Enemy"){
            isTouchEnemy = true;
        }
    }

}
