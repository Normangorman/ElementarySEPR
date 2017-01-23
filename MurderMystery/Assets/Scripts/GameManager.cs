using UnityEngine;
using System.Collections.Generic;

//! GameManager class.
/*! Manages time.*/
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private UIController UIController;
    private Player player;
    public float time; //!< Time constant which changes throughout gameplay.
    public float timeScale = 0; // TODO: What does this do?
    private int failedAccusations = 0;

    // This GameManager can be used to do the scroring later on which includes the time 
    // There is currently no time or scoring and therefore this class is not used

    void Start()
    {
        instance = this;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        UIController = GameObject.FindGameObjectWithTag("DoozyUI").GetComponent<UIController>().Instance;
    }

    public void WinGame(string storySynopsis)
    {
        DoozyUI.UIManager.ShowUiElement("WinMenu");
        DoozyUI.UIManager.HideUiElement("InGameHud");
        UIController.SetSynopsisWinText(storySynopsis);
    }

    public void LoseGame(string storySynopsis)
    {
        DoozyUI.UIManager.ShowUiElement("LoseMenu");
        DoozyUI.UIManager.HideUiElement("InGameHud");
        UIController.SetSynopsisLoseText(storySynopsis);
    }

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
