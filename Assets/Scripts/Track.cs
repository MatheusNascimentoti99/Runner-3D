using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Track : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject wave;
    public GameObject road;
    public GameObject[] obstablesPrafebs;
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
            //GameController.gameController.IncrementScore();
            makeObstables();
            MoveTrack();
        }
    }

    public void MoveTrack()
    {
        wave.transform.position = new Vector3(0, -13, wave.transform.position.z + 20);
        road.transform.position = new Vector3(0, 0, road.transform.position.z + 20 * 5);
    }

    private void makeObstables()
    {
        int newObstableIndex = Random.Range(1, obstablesPrafebs.Length);
        int positionObstableZ = Random.Range(0, 20);
        float positionObstableX = Random.Range(-1.3f, 1.3f);
        Vector3 position = new Vector3(positionObstableX, 0, road.transform.position.z + 80 + positionObstableZ);
        Instantiate(obstablesPrafebs[newObstableIndex], position, Quaternion.identity);

        int boolWillBeWall  = Random.Range(0,2);
        if (boolWillBeWall >= 1 )
        {
            position = new Vector3(0, 0, road.transform.position.z + 20 * 5);
            Instantiate(obstablesPrafebs[0], position, Quaternion.identity);
        }
    }

}
