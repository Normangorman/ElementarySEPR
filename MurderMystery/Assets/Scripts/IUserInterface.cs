

public interface IUserInterface
{
    // Gets the time of the game. This is the virtual time that is managed by the StoryManager
    void GetTime(); 

    // At the start and when the player updates their abilities, this function needs to be called so that the progress bars for each player ability is updated
    void GetPlayerAbilities();

    // When the player interacts with one of the NPCs, this has to be called to display the NPC's abilities.
    void GetNpcAbilities();

    // From the Game Manager, it has to get how many interaction points there are. This has to be called at the beginning and after every conversation
    void SetInteractionPoint(int i);

    // The player icon (picture of the character) has to be set at the beginning of the game
    void GetPlayerIcon();

    // The NP icon (picture of the NPC character) has to be set whenever the player interacts with an NPC
    void GetNpcIcon();

    // The inventory list will populated by the inventoryList array in the player class
    void GetInventoryList();

    // The guest list will be set at the beginning of the game and will show has died and the information about people that the player has met
    void GetGuestList();
}
