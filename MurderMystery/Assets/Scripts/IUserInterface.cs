
//! User Interface class.
/*! The user interface object for the game. */
public interface IUserInterface
{
    // Gets the time of the game. This is the virtual time that is managed by the StoryManager
    //! Gets the time of the game.
    void GetTime(); 

    // At the start and when the player updates their abilities, this function needs to be called so that the progress bars for each player ability is updated
    //! Gets the player traits.
    void GetPlayerAbilities();

    // When the player interacts with one of the NPCs, this has to be called to display the NPC's abilities.
    //! Gets the NPC traits.
    void GetNpcAbilities();

    // From the Game Manager, it has to get how many interaction points there are. This has to be called at the beginning and after every conversation
    //! Sets the player traits.
    /*
     * \param i Integer constant.
     */
    void SetInteractionPoint(int i);

    // The player icon (picture of the character) has to be set at the beginning of the game
    //! Gets Player icon.
    void GetPlayerIcon();

    // The NP icon (picture of the NPC character) has to be set whenever the player interacts with an NPC
    //! Gets NPC icon.
    void GetNpcIcon();

    // The inventory list will populated by the inventoryList array in the player class
    //! Gets inventoryList array.
    void GetInventoryList();

    // The guest list will be set at the beginning of the game and will show has died and the information about people that the player has met
    //! Gets guestList array.
    void GetGuestList();
}
