﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//! Room Trigger class.
/*! Handles trigger entry and exit relating to the rooms of the map. */
public class RoomTrigger : MonoBehaviour
{
    // default to GrandHall 
    public Constants.Rooms roomLocation = Constants.Rooms.GrandHall; //!< Room trigger that is being triggered.
    public Text RoomText; //!< Text displayed when triggered.

    public void Awake()
    {
        RoomText = GameObject.FindGameObjectWithTag("RoomText").GetComponent<Text>();
    }

    //! When player object enters a trigger, check for correct tag and switch to correct room name.
    /*
     * \param coll Collider that is being triggered.
     */
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (!coll.gameObject.CompareTag("RoomTrigger"))
        {
            Debug.Log("RoomTrigger: gameObject doesn't have a RoomTrigger tag so returning");
            return;
        }

        GameObject collObj = coll.gameObject;
        switch (collObj.name)
        {
            case "Kitchen": EnterRoom(Constants.Rooms.Kitchen); break;
            case "Lift": EnterRoom(Constants.Rooms.Lift); break;
            case "Staircase 1": EnterRoom(Constants.Rooms.Staircase1); break;
            case "Staircase 2": EnterRoom(Constants.Rooms.Staircase2); break;
            case "Bin Bay": EnterRoom(Constants.Rooms.BinBay); break;
            case "Business Room 1": EnterRoom(Constants.Rooms.BusinessRoom1); break;
            case "Business Room 2": EnterRoom(Constants.Rooms.BusinessRoom2); break;
            case "Exhibition Room": EnterRoom(Constants.Rooms.ExhibitionRoom); break;
            case "Interaction Island": EnterRoom(Constants.Rooms.InteractionIsland); break;
            case "Lecture Theatre": EnterRoom(Constants.Rooms.LectureTheatre); break;
            case "Reception": EnterRoom(Constants.Rooms.Reception); break;
            case "Terrace": EnterRoom(Constants.Rooms.Terrace); break;
            default:
                EnterRoom(Constants.Rooms.GrandHall);
                Debug.Log("OnTriggerEnter2D called but object is not a room");
                break;
        }
    }

    //! When trigger is exited by the player object, change location properties.
    /*
     * \param room Room that is being triggered.
     */
    private void OnTriggerExit2D(Collider2D coll)
    {
        if (!coll.gameObject.CompareTag("RoomTrigger")) return;

        roomLocation = Constants.Rooms.GrandHall;
        RoomText.text = "Room: " + Constants.Rooms.GrandHall;
        MessagePasser.OnPlayerChangeRoom(Constants.Rooms.GrandHall);
    }

    //! When a room is entered by the player object.
    /*
     * \param room Room that is being triggered.
     */
    private void EnterRoom(Constants.Rooms room)
    {
        if (roomLocation != room)
        {
            Debug.Log("Changed to room: " + room.ToString());
            roomLocation = room;
            RoomText.text = "Room: " + room;
            MessagePasser.OnPlayerChangeRoom(room);
        }
    }
}
