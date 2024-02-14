using System.Collections;
using System.Collections.Generic;
 //using UnityEditor.Build.Content;
using UnityEngine;

public class AmmoBoxBehavior : MonoBehaviour
{
    public GameBehavior gameManager;

        void Start ()
    {
    gameManager = GameObject.Find("GameManager").GetComponent<GameBehavior>();
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Destroy(this.transform.gameObject);

            Debug.Log("Ammo Collected!");

            gameManager.Ammo += 20;
        }
    }
}
