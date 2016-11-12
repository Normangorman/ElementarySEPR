using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{

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
             */
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
             */
        }
    }

    public Tile GetTile()
    {
        return Tile;
    }

    // Raycast to get the tile underneath
    
}
