using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour
{

    int Charisma;
    int Friendliness;
    int Aggressiveness;
    private int Saracasm;

    // Setters for properties
    public void SetCharisma(int i)
    {
        Charisma = i;
    }
    public void SetFriendliness(int i)
    {
        Friendliness = i;
    }
    public void SetAggressiveness(int i)
    {
        Aggressiveness = i;
    }
    public void SetSarcasm(int i)
    {
        Saracasm = i;
    }

    // Getters
    public int GetCharisma()
    {
        return Charisma;
    }
    public int GetFriendliness()
    {
        return Friendliness;
    }
    public int GetAggressiveness()
    {
        return Aggressiveness;
    }
    public int GetSarcasm()
    {
        return Saracasm;
    }
}
