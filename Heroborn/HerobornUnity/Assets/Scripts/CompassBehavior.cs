using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompassBehavior : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Destroy(this.transform.gameObject);

            Debug.Log("Compass Collected!");
        }
    }
}