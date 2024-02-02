using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
 
    public float moveSpeed = 10f;
    public float rotateSpeed = 100f;

    
    private float vInput;
    private float hInput;

    //1
    private Rigidbody _rb;

    //2
    private void Start()
    {
        //3
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        
        vInput = Input.GetAxis("Vertical") * moveSpeed;

       
        hInput = Input.GetAxis("Horizontal") * rotateSpeed;

        /* 4
        this.transform.Translate(Vector3.forward * vInput * Time.deltaTime);

        
        this.transform.Rotate(Vector3.up * hInput * Time.deltaTime);
        */

   
    }
    void FixedUpdate()
    {
        Vector3 rotation = Vector3.up * hInput;

        Quaternion angleRot = Quaternion.Euler(rotation * Time.fixedDeltaTime);

        _rb.MovePosition(this.transform.position + this.transform.forward * vInput * Time.fixedDeltaTime);

        _rb.MoveRotation(_rb.rotation * angleRot);
    }
}
