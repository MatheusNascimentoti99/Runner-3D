using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleEnemy : MonoBehaviour
{
    private bool collided = false;
    // Start is called before the first frame update
    void Update()
    {
        if (GetComponent<Animator>().GetCurrentAnimatorClipInfo(0)[0].clip.name == "DefendGetHit")
        {
            Debug.Log("Deu bom");
            GetComponents<Collider>()[1].isTrigger = false;
            GetComponent<Rigidbody>().drag = 0f;
            this.tag = "Ground";
        }
        else if(!collided)
        {
            GetComponents<Collider>()[1].isTrigger = true;
            GetComponent<Rigidbody>().drag = 100;
            this.tag = "Enemy";
        }
        if (FindObjectOfType<Player>().transform.position.z > this.transform.position.z + 5)
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
            collided = true;
            FindObjectOfType<Player>().m_moveSpeed = FindObjectOfType<Player>().m_moveSpeed / 3;
            Debug.Log("Shot");
        }
    }
}
