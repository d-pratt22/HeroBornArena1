using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public GameBehavior gameManager;

    public float onscreenDelay = 3f;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameBehavior>();


        if (gameManager.Ammo > 0)
        {
            gameManager.Ammo -= 1;
            Destroy(this.gameObject, onscreenDelay);
        }

        else
        {
            gameManager.Ammo = 0;
        }


    }
}
