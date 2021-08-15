using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInstantiator : MonoBehaviour
{
    [SerializeField] private Object enemyObject;
    void Start()
    {
        var instantiateEnemy = (GameObject) Instantiate(enemyObject);
        instantiateEnemy.name = "Enemy";
    }

}
