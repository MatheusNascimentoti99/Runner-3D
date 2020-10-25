using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Track : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject wave;
    private Player player;
    public GameObject[] obstablesPrafebs;
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player.transform.position.z > this.transform.position.z + 15)
        {
            makeObstables();
            MoveTrack();
        }
    }


    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        //GameController.gameController.IncrementScore();
    //        makeObstables();
    //        MoveTrack();
    //    }
    //}

    private void MoveTrack()
    {
        this.transform.position = new Vector3(0, 0, this.transform.position.z + 20 * 7);
        wave.transform.position = new Vector3(0, -13, wave.transform.position.z + 20);

    }

    private void makeObstables()
    {
        //Create enemy on track
        int newObstableIndex = Random.Range(1, obstablesPrafebs.Length);
        int positionObstableZ = Random.Range(0, 20);
        float positionObstableX = Random.Range(-2.5f, 2.5f);
        Vector3 position = new Vector3(positionObstableX, 0, this.transform.position.z + 80 + positionObstableZ);
        Instantiate(obstablesPrafebs[newObstableIndex], position, Quaternion.identity);

        int boolWillBeWall  = Random.Range(0,2);
        if (boolWillBeWall >= 1 )
        {
            position = new Vector3(0, 0, this.transform.position.z + 20 * 7);
            Instantiate(obstablesPrafebs[0], position, Quaternion.identity);
        }
    }

}
