﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(FindObjectOfType<Player>().transform.position.z > this.transform.position.z + 5)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GetComponents<Collider>()[1].enabled = false;
            FindObjectOfType<Player>().m_moveSpeed = FindObjectOfType<Player>().m_moveSpeed / 3;
            FindObjectOfType<Player>().Collided();
            Debug.Log("Shot");
        }
    }
}
