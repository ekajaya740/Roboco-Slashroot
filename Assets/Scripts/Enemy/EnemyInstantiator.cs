using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInstantiator : MonoBehaviour
{
    [SerializeField] private Object enemyObject;
    
    
    void Awake(){
        // isEnemySpawn = true;
    }
    void Start()
    {
        Instantiate(enemyObject);
    }

    void Update(){
        
    }

}
