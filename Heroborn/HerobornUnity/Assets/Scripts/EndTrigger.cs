using UnityEngine;
using UnityEngine.UIElements;

public class EndTrigger : MonoBehaviour
{
    public GameBehavior gameManager;

    public GameObject Panel;

    void OnTriggerEnter()
    {
        Panel.SetActive(true);
    }
}
