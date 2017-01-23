using UnityEngine;
using System.Collections.Generic;

//! GameManager class.
/*! Manages time.*/
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public float time; //!< Time constant which changes throughout gameplay.
    public float timeScale = 0; // TODO: What does this do?
    private int failedAccusations = 0;

    // This GameManager can be used to do the scroring later on which includes the time 
    // There is currently no time or scoring and therefore this class is not used

    public void Awake()
    {
        instance = this;
    }

    public void WinGame(string storySynopsis)
    {
        DoozyUI.UIManager.ShowUiElement("WinMenu");
    }

    public void LoseGame(string storySynopsis)
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
        GetPlayer().InteractionPoints += i;
        DoozyUI.UIManager.ShowNotification(Constants.NotificationPath, 2f, true, "You bought " + i + " Interaction Points\ncosting " + j + " score points");
    }

    public void OnFailedAccusation(NPC n)
    {
        Debug.Log("GameManager#OnFailedAccusation called for: " + n.person.ToString());
        failedAccusations++;
        if (failedAccusations >= 2)
        {
            LoseGame(StoryManager.instance.GetStoryScript().GetStoryGraph().GetSynopsis());
        }
    }

    private Player GetPlayer()
    {
        // Finds the player object in the hierarchy and returns it's Player component
        return GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
}
