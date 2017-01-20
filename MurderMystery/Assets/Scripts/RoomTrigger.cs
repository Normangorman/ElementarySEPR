using UnityEngine;
using System.Collections;

public class RoomDetector : MonoBehaviour
{

    public string roomLocation = "";
    public bool roomChanged = false;
    // when true, the room has changed, this needs to be reset to
    // false by whatever is using the room data

    void OnTriggerEnter2D(Collider2D coll)
    {
        GameObject collObj = coll.gameObject;
        //Kitchen
        switch (collObj.name)
        {
            case "Kitchen":
                if (roomLocation != "Kitchen")
                {
                    roomChanged = true;
                    roomLocation = "Kitchen";
                }
                roomLocation = "Kitchen";
                Debug.Log("Kitchen");
                break;
            case "Lift":
                if (roomLocation != "Lift")
                {
                    roomChanged = true;
                    roomLocation = "Lift";
                }
                roomLocation = "Lift";
                Debug.Log("Lift");
                break;
            case "Staircase 1":
                if (roomLocation != "Staircase 1")
                {
                    roomChanged = true;
                    roomLocation = "Staircase 1";
                }
                roomLocation = "Staircase 1";
                Debug.Log("Staircase 1");
                break;
            case "Staircase 2":
                if (roomLocation != "Staircase 2")
                {
                    roomChanged = true;
                    roomLocation = "Staircase 2";
                }
                roomLocation = "Lecture ";
                Debug.Log("Staircase 2");
                break;
            case "Bin Bay":
                if (roomLocation != "Bin Bay")
                {
                    roomChanged = true;
                    roomLocation = "Bin Bay";
                }
                roomLocation = "Bin Bay";
                Debug.Log("Bin Bay");
                break;
            case "Business Room 1":
                if (roomLocation != "Business Room 1")
                {
                    roomChanged = true;
                    roomLocation = "Business Room 1";
                }
                roomLocation = "Business Room 1";
                Debug.Log("Business Room 1");
                break;
            case "Business Room 2":
                if (roomLocation != "Business Room 2")
                {
                    roomChanged = true;
                    roomLocation = "Business Room 2";
                }
                roomLocation = "Business Room 2";
                Debug.Log("Business Room 2");
                break;
            case "Exhibition Room":
                if (roomLocation != "Exhibition Room")
                {
                    roomChanged = true;
                    roomLocation = "Exhibition Room";
                }
                roomLocation = "Exhibition Room";
                Debug.Log("Exhibition Room");
                break;
            case "Interaction Island":
                if (roomLocation != "Interaction Island")
                {
                    roomChanged = true;
                    roomLocation = "Interaction Island";
                }
                roomLocation = "Interaction Island";
                Debug.Log("Interaction Island");
                break;
            case "Lecture Theatre":
                if (roomLocation != "Lecture Theatre")
                {
                    roomChanged = true;
                    roomLocation = "Lecture Theatre";
                }
                roomLocation = "Lecture Theatre";
                Debug.Log("Lecture Theatre");
                break;
            case "Reception":
                if (roomLocation != "Reception")
                {
                    roomChanged = true;
                    roomLocation = "Reception";
                }
                roomLocation = "Reception";
                Debug.Log("Reception");
                break;
            case "Terrace":
                if (roomLocation != "Terrace")
                {
                    roomChanged = true;
                    roomLocation = "Terrace";
                }
                roomLocation = "Terrace";
                Debug.Log("Terrace");
                break;
            default:
                Debug.Log("Grand Hall");
                break;
        }
    }

}