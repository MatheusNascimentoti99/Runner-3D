using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    public enum StateEnemy { lookingOut, following, die};
    public StateEnemy state;
    private bool collided = false;
    public float changeSpeed;
    private float speed;
    Transform player;
    Vector3 startPosition;
    private Animator m_animator;
    private void Start()
    {
        state = StateEnemy.lookingOut;
        m_animator = GetComponent<Animator>();
        player = FindObjectOfType<Player>().transform;
        startPosition = this.transform.position;
    }
    private void Awake()
    {

    }
    // Start is called before the first frame update
    void Update()
    {
        if (transform.position.z - player.transform.position.z < 15 && state != StateEnemy.die)
            state = StateEnemy.following;
        Vector3.Distance(player.transform.position, transform.position);
        if (FindObjectOfType<Player>().transform.position.z > this.transform.position.z + 5)
        {
            GameController.gameController.IncrementScore();
            Destroy(gameObject);
        }
        checkState();
    }


    private void checkState()
    {
        switch (state)
        {
            case StateEnemy.following:
                follow();
                break;
            case StateEnemy.lookingOut:
                moveEnemyAround();
                break;
            case StateEnemy.die:
                die();
                break;

        }
    }
    private void die()
    {
        m_animator.Play("Die");
    }

    private void moveEnemyAround()
    {
        m_animator.Play("Victory");
        speed = 0f;

    }

    private void follow()
    {
        speed = changeSpeed;
        m_animator.Play("WalkBWD");
        Vector3 aux = transform.position - player.position;
        Quaternion rotation = Quaternion.LookRotation(aux);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, 5f * Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.position.x, transform.position.y, player.position.z), speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            state = StateEnemy.die;
            GameController.gameController.DecrementScore();
            FindObjectOfType<Player>().Collided();
            GetComponents<Collider>()[0].isTrigger = false;
            collided = true;
            StartCoroutine("slowDownPlayer");
        }
    }

    private IEnumerator slowDownPlayer() {
        float returnSpeed = FindObjectOfType<Player>().m_moveSpeed;
        UnityEngine.Debug.Log("Aqui");
        FindObjectOfType<Player>().m_moveSpeed = FindObjectOfType<Player>().m_moveSpeed / 3;
        yield return new WaitForSeconds(1f);
        UnityEngine.Debug.Log("chegou");
        FindObjectOfType<Player>().m_moveSpeed = returnSpeed - returnSpeed/3;
    }
}
