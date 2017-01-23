using UnityEngine;
using System.Collections.Generic;

//! GameManager class.
/*! Manages the game events.*/
public class GameManager : MonoBehaviour
{
    public static GameManager instance; //!< Instance of this class.
    private UIController UIController; //!< UIController object.
    private Player player; //!< Player object.
    public float time; //!< Time constant which changes throughout gameplay.
    public float timeScale = 0; //!< Time modifier for speed of time passing.
    private int failedAccusations = 0;

    // This GameManager can be used to do the scroring later on which includes the time 
    // There is currently no time or scoring and therefore this class is not used

    //! On initialisation, the player and the UI controller objects are set. 
    void Start()
    {
        instance = this;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        UIController = GameObject.FindGameObjectWithTag("DoozyUI").GetComponent<UIController>().Instance;
    }

    //! Called when the game is won.
    /*!
     * \param storySynopsis Text that is displayed on win.
     */
    public void WinGame(string storySynopsis)
    {
        DoozyUI.UIManager.ShowUiElement("WinMenu");
        DoozyUI.UIManager.HideUiElement("InGameHud");
        UIController.SetSynopsisWinText(storySynopsis);
    }

    //! Called when the game is lost.
    /*!
     * \param storySynopsis Text that is displayed on loss.
     */ 
    public void LoseGame(string storySynopsis)
    {
        DoozyUI.UIManager.ShowUiElement("LoseMenu");
        DoozyUI.UIManager.HideUiElement("InGameHud");
        UIController.SetSynopsisLoseText(storySynopsis);
    }

    //! Called when coffee is bought, sets amount of interaction points depending on amount of coffee bought.
    /*
     * \param i Amount of coffee bought.
     */ 
    public void BuyCoffee(int i)
    {
        int j = 0;
        switch (i)
        {
            case 10:
                j = 5;
                break;
            case 18:
                j = 10;
                break;
            case 25:
                j = 15;
                break;
            case 50:
                j = 30;
                break;
        }
        player.InteractionPoints += j;
        DoozyUI.UIManager.ShowNotification(Constants.NotificationPath, 1.5f, true, "You bought " + j + " Interaction Points\ncosting " + i + " score points");
    }

    //! Called when the accusation fails.
    /*
     * \param n NPC accused.
     */ 
    public void OnFailedAccusation(NPC n)
    {
        Debug.Log("GameManager#OnFailedAccusation called for: " + n.person.ToString());
        failedAccusations++;
        if (failedAccusations >= 2)
        {
            LoseGame(StoryManager.instance.GetStoryScript().GetStoryGraph().GetSynopsis());
        }
        else
        {
            DoozyUI.UIManager.ShowNotification(Constants.NotificationPath, 1.5f, true, "NO! You have either accused " + n + " wrongly or you don't have enough evidence. You have one more Accuse left!");
        }
    }
}
