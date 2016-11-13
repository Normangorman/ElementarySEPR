using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    void Start()
    {
        var ray1 = new Ray(transform.position, new Vector3(0, -10, 0)); // Raycasts down to check if there is a tile underneath. If so
        Debug.DrawRay(transform.position, new Vector3(0, -10, 0), Color.green, float.MaxValue, false);
        RaycastHit hit1;
        if (Physics.Raycast(ray1, out hit1))                                  // and there is a hit then move the player to that tile. If not,
        {
            Debug.Log("Name: " + hit1.collider.name + "    Position: " + hit1.collider.transform.position);
            if (hit1.collider.tag == "Tile")                                  // then don't do anything. player stays still
            {
                transform.position = hit1.transform.position + new Vector3(0, 1, 0);
            }
        }
    }

    /*
    public Tile tile;

    private int x_position;
    private int y_osition;

    public int xPosition
    {
        get { return x_position; }
        set
        {
            /*
             * Get Tile underneath
             * set its player value to true
             
        }
    }

    public int xPosition
    {
        get { return y_position; }
        set
        {
            /*
             * Get Tile underneath
             * set its player value to true
             
        }
    }

    public Tile GetTile()
    {
        return Tile;
    }

Raycast to get the tile underneath
    */

    void Update()
    {
        #region INPUT HANDLERS
        if (Input.GetButtonDown("VerticalUp"))
        {
            if (isValidMovement(new Vector3(-1, 0, 0)))
            {
                transform.position = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
            }
        }

        if (Input.GetButtonDown("VerticalDown"))
        {
            if (isValidMovement(new Vector3(1, 0, 0)))
            {
                transform.position = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
            }
        }
        if (Input.GetButtonDown("HorizontalLeft"))
        {
            if (isValidMovement(new Vector3(0, 0, -1)))
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1);
            }
        }
        if (Input.GetButtonDown("HorizontalRight"))
        {
            if (isValidMovement(new Vector3(0, 0, 1)))
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1);
            }
        }
        #endregion
    }

    bool isValidMovement(Vector3 vec)
    {
        var ray1 = new Ray(transform.position + vec, new Vector3(0, -10, 0)); // Raycasts down to check if there is a tile underneath. If so
        Debug.DrawRay(transform.position + vec, new Vector3(0, -10, 0), Color.green, float.MaxValue, false);
        RaycastHit hit1;
        if (Physics.Raycast(ray1, out hit1))                                  // and there is a hit then move the player to that tile. If not,
        {
            Debug.Log("Name: " + hit1.collider.name + "    Position: " + hit1.collider.transform.position);
            if (hit1.collider.tag == "Tile")                                  // then don't do anything. player stays still
            {
                return true;
            }
        }
        return false;
    }
   
}
