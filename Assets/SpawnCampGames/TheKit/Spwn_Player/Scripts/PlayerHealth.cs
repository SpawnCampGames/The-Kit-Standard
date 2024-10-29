using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health = 100;

    void Start()
    {
        health = 100;
    }

    void Update()
    {
        if(health <= 0)
        {
            Debug.Log("PlayerIsDead");
        }
    }
}
