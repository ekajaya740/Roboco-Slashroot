using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBoxTrigger : MonoBehaviour
{
    public bool isTriggered {get; private set;}
    
    void Awake(){
        isTriggered = false;
    }

    void Start(){
        StartCoroutine(DestroyGameObjectCoroutine());
    }

    private IEnumerator DestroyGameObjectCoroutine(){
        while(true){
            if(isTriggered){
                yield return new WaitForSeconds(.5f);
                gameObject.SetActive(false);
            }
            yield return null;
        }
    }

    void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.tag == "Player"){
            isTriggered = true;
        }
    }


}
