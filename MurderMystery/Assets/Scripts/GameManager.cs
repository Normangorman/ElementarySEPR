using UnityEngine;
using System.Collections.Generic;

//! GameManager class.
/*! Manages time.*/
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private Player player;
    public float time; //!< Time constant which changes throughout gameplay.
    public float timeScale = 0; // TODO: What does this do?

    // This GameManager can be used to do the scroring later on which includes the time 
    // There is currently no time or scoring and therefore this class is not used

    public void Awake()
    {
        instance = this;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    public void WinGame()
    {
        DoozyUI.UIManager.ShowUiElement("WinMenu");
    }

    public void LoseGame()
    {
        DoozyUI.UIManager.ShowUiElement("WinMenu");
    }

    public void BuyCoffee(int i)
    {
        int j = 0;
        switch (i)
        {
            case 1:
                j = 5;
                break;
            case 5:
                j = 20;
                break;
            case 10:
                j = 35;
                break;
            case 15:
                j = 50;
                break;
        }
        player.InteractionPoints += i;
        DoozyUI.UIManager.ShowNotification(Constants.NotificationPath, 2f, true, "You bought " + i + " Interaction Points\ncosting " + j + " score points");
    }

}
