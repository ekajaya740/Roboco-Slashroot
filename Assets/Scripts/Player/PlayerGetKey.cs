using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGetKey : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private RandomBoxTrigger randomBoxTrigger;
    public bool isGetKey;

    void Awake(){
        isGetKey = false;
    }
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(randomBoxTrigger.isTriggered){
            isGetKey = true;
        }
    }
}
