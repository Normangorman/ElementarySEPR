using System;
using System.Collections.Generic;
using UnityEngine;
using UnityTest;

public class PickUpClueIntegrationTest : MonoBehaviour
{
    GameObject player;
    Player playerComponent;

    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerComponent = player.GetComponent<Player>();
    }

    public void Update()
    {
        //Debug.Log("Moving player");
        player.transform.position += new Vector3(0.1f, 0, 0);

        if (playerComponent.InventoryList.Count > 0)
        {
            // We walked into a clue and picked it up
            IntegrationTest.Pass(gameObject);
        }
    }
}
