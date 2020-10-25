using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
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
            GameController.gameController.IncrementScore();
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameController.gameController.DecrementScore();
            FindObjectOfType<Player>().Collided();
            GetComponents<Collider>()[1].enabled = false;
            StartCoroutine("slowDownPlayer");
        }
    }

    private IEnumerator slowDownPlayer()
    {
        float returnSpeed = FindObjectOfType<Player>().m_moveSpeed;
        UnityEngine.Debug.Log("Aqui");
        FindObjectOfType<Player>().m_moveSpeed = FindObjectOfType<Player>().m_moveSpeed / 3;
        yield return new WaitForSeconds(1f);
        UnityEngine.Debug.Log("chegou");
        FindObjectOfType<Player>().m_moveSpeed = returnSpeed - returnSpeed / 3;
    }
}
