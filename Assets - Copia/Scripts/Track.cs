using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Track : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject wave;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            wave.transform.position = new Vector3(0, -12, transform.position.z);
            transform.position = new Vector3(0, 0, other.gameObject.transform.position.z + 18 * 4);
        }
    }
}
