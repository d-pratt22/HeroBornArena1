using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class PlayerBehavior : MonoBehaviour
{
    private GameBehavior _gameManager;

    public float moveSpeed = 10f;
    public float rotateSpeed = 100f;
    public float jumpVelocity = 5f;
    public float distanceToGround = 0.1f;
    public LayerMask groundLayer;
    public AudioSource _yell;
    public GameObject bullet;
    public float bulletSpeed = 110f;

    public GameObject _loss;

    private float vInput;
    private float hInput;
    private Rigidbody _rb;
    private CapsuleCollider _col;


    public delegate void JumpingEvent();
  
    public event JumpingEvent playerJump;


    void Start()
    {
        
        _rb = GetComponent<Rigidbody>();

        _col = GetComponent<CapsuleCollider>();

        _gameManager = GameObject.Find("GameManager").GetComponent<GameBehavior>();

    }

    void Update()
    {
        
        vInput = Input.GetAxis("Vertical") * moveSpeed;

       
        hInput = Input.GetAxis("Horizontal") * rotateSpeed;

        /* 4
        this.transform.Translate(Vector3.forward * vInput * Time.deltaTime);

        
        this.transform.Rotate(Vector3.up * hInput * Time.deltaTime);
        */
        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            _rb.AddForce(Vector3.up * jumpVelocity, ForceMode.Impulse);
        }

        if (Input.GetMouseButtonDown(0))
        {
            GameObject newBullet = Instantiate(bullet, this.transform.position + new Vector3(1, 0, 0), this.transform.rotation) as GameObject;

            Rigidbody bulletRB = newBullet.GetComponent<Rigidbody>();

            bulletRB.velocity = this.transform.forward * bulletSpeed;
        }
        

    }
    private bool IsGrounded()
    {
        Vector3 capsuleBottom = new Vector3 (_col.bounds.center.x,_col.bounds.min.y,_col.bounds.center.z);

        bool grounded = Physics.CheckCapsule(_col.bounds.center,capsuleBottom, distanceToGround, groundLayer, QueryTriggerInteraction.Ignore);

        return grounded;
    }
    void FixedUpdate()
    {
        Vector3 rotation = Vector3.up * hInput;

        Quaternion angleRot = Quaternion.Euler(rotation * Time.fixedDeltaTime);

        _rb.MovePosition(this.transform.position + this.transform.forward * vInput * Time.fixedDeltaTime);

        _rb.MoveRotation(_rb.rotation * angleRot);

        playerJump();


    }
    void OnCollisionEnter(Collision collision)
    {
        // 4
        if (collision.gameObject.name == "Enemy")
        {
            // 5
            _gameManager.HP -= 1;

            _yell.Play();

            if (_gameManager.HP <= 0)
            {
                _loss.SetActive(true);
                Time.timeScale = 0f;
            }

        }
    }

}
