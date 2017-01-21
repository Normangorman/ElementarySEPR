using UnityEngine;
using System.Collections;

public class ResponseButton : MonoBehaviour
{
    public enum Button
    {
        button0,
        button1,
        button2
    };

    public Button buttonIndex;

    public string ButtonText;
    public string Response;
}
