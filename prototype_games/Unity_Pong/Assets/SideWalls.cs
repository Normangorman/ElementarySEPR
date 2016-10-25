using UnityEngine;
using System.Collections;

public class SideWalls : MonoBehaviour
{
    public GameManager GM;
    void Awake()
    {
        GM = GameObject.FindGameObjectWithTag("Walls").GetComponent<GameManager>().Instance;
    }

    void OnTriggerEnter2D (Collider2D hitInfo) {
        Awake();
        if (hitInfo.name == "Ball")
        {
            string wallName = transform.name;
            GM.Score(wallName);
            hitInfo.gameObject.SendMessage("RestartGame", 1.0f, SendMessageOptions.RequireReceiver);
        }
    }
}