using UnityEngine;
using System.Collections;
using System.Linq;

//! Room class.
/*! Class for rooms on the map, in which the objects are placed in. */
public class Room : MonoBehaviour
{
    public int sizex; //!< size in x direction.
    public int sizey; //!< size in y direction.
    public int numbTiles; //!< number of tiles that room takes up.

    //! On start initialise rooms with restricted random size.
    void Start() 
    {
        sizex = createRandomInt(7,15);
        sizey = createRandomInt(7,15);
        CreateRoom();
    }

    //! Method to create a room.
    public void CreateRoom () 
    {
        GameObject go = Resources.Load("Quad") as GameObject;

        if (transform.name == "Room1 (0)")
        {
            sizex = 64;
            sizey = 5;
            for (int i = 0; i < 20; i++)
            {
                int r = 0;
                for (int j = 0; j < 15; j++)
                {
                    GameObject quad = Instantiate(go, transform, false) as GameObject;
                    quad.transform.localPosition = new Vector3(i + 25, 0, j - 15);
                    numbTiles++;
                }
            }
        }
        
        for (int i = 0; i < sizex; i++)
        {
            for (int j = 0; j < sizey; j++)
            {
                GameObject quad = Instantiate(go, transform, false) as GameObject;
                quad.transform.localPosition = new Vector3(i,0,j);
                numbTiles++;
            }
        }

    }

    //! Creates a random integer between a min and max value.
    /*!
     * \param min Minimum value.
     * \param max Maximum value.
     * \return Randomised value.
     */
    private int createRandomInt(int min, int max) 
    {
        int i = Random.Range(min, max);
        return i;
    }

}
