using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private enum ControlMode
    {
        /// <summary>
        /// Up moves the character forward, left and right turn the character gradually and down moves the character backwards
        /// </summary>
        Tank,
        /// <summary>
        /// Character freely moves in the chosen direction from the perspective of the camera
        /// </summary>
        Direct
    }

    public float m_moveSpeed = 2;
    [SerializeField] private float m_turnSpeed = 200;
    [SerializeField] private float m_jumpForce = 4;

    [SerializeField] private Animator m_animator = null;
    [SerializeField] private Rigidbody m_rigidBody = null;

    private float m_currentV = 0;
    private float m_currentH = 0;

    private readonly float m_interpolation = 10;
    private readonly float m_walkScale = 0.33f;

    private bool m_wasGrounded;
    private Vector3 m_currentDirection = Vector3.zero;

    private float m_jumpTimeStamp = 0;
    private float m_minJumpInterval = 0.25f;
    private bool m_jumpInput = false;

    private bool m_isGrounded;
    private float timeCollision = 0f;

    public int life;
    public Text txt_life;

    void Start()
    {
        life = PointsSystem.pointsSystem.getLife();
        updateLife();
    }

    private void Awake()
    {
        if (!m_animator) { gameObject.GetComponent<Animator>(); }
        if (!m_rigidBody) { gameObject.GetComponent<Animator>(); }
    }



    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            m_isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            m_isGrounded = false;
        }
    }

    private void Update()
    {
        if (!m_jumpInput && Input.GetKey(KeyCode.Space))
        {
            m_jumpInput = true;
        }
        m_animator.SetBool("Grounded", m_isGrounded);
        DirectUpdate();
        m_moveSpeed += 1 * Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if (m_animator.GetBool("Collision"))
        {
            timeCollision += Time.deltaTime;
        }
        if(timeCollision > 0.3f)
        {
            m_animator.SetBool("Collision", false);
        }
    }

    private void DirectUpdate()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

        Transform camera = Camera.main.transform;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            v *= m_walkScale;
            h *= m_walkScale;
        }

        m_currentV = Mathf.Lerp(m_currentV, 1, Time.deltaTime * m_interpolation);
        m_currentH = Mathf.Lerp(m_currentH, h, Time.deltaTime * m_interpolation);

        Vector3 direction = camera.forward * m_currentV + camera.right * m_currentH * 1/4;

        float directionLength = direction.magnitude;
        direction.y = 0;
        direction = direction.normalized * directionLength;
        if (direction != Vector3.zero)
        {
            m_currentDirection = Vector3.Slerp(m_currentDirection, direction, Time.deltaTime * m_interpolation);

            transform.position += m_currentDirection * m_moveSpeed * Time.deltaTime;
            
            m_animator.SetFloat("MoveSpeed", direction.magnitude);
        }

        JumpingAndLanding();
    }

    public void Collided()
    {
        Debug.Log("bater");
        life--;
        m_isGrounded = false;
        m_animator.SetBool("Collision", true);
        timeCollision = 0;
        updateLife();
        if(life < 1)
        {
            GameController.gameController.sendScore();
            MenuController.LoadHome(); // Isso pq eu tava tessstando ele
            Debug.Log("Game over");
        }
    }

    private void JumpingAndLanding()
    {
        bool jumpCooldownOver = (Time.time - m_jumpTimeStamp) >= m_minJumpInterval;

        if (jumpCooldownOver && m_isGrounded && m_jumpInput)
        {
            Debug.Log("Pular");
            this.GetComponent<AudioSource>().Play();
            m_jumpTimeStamp = Time.time;
            m_rigidBody.AddForce(Vector3.up * m_jumpForce, ForceMode.Impulse);
        }

        else if (!m_wasGrounded && m_isGrounded)
        {
            m_animator.SetTrigger("Land");
        }

        else if (!m_isGrounded && m_wasGrounded)
        {
            m_animator.SetTrigger("Jump");
        }
        m_wasGrounded = m_isGrounded;
        m_jumpInput = false;
    }

    private void updateLife()
    {
        txt_life.text = "Vidas: " + life;
    }
}
