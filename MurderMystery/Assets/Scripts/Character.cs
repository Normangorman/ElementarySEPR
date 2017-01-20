using UnityEngine;
using System.Collections;

//! Character class. 
/*! Class for both the NPCs and Player objects containing personality traits. */
public class Character : MonoBehaviour
{

    int Charisma; //!< Charisma integer value.
    int Friendliness; //!< Friendliness integer value.
    int Aggressiveness; //!< Aggressiveness integer value.
    private int Saracasm; //!< Sarcasm integer value.
    
    // Setters for properties
    //! sets Charisma value.
    /*!
      \param i an integer constant.
     */
    public void SetCharisma(int i)
    {
        Charisma = i;
    }
    //! sets Friendliness value.
    /*!
      \param i an integer constant.
     */
    public void SetFriendliness(int i)
    {
        Friendliness = i;
    }
    //! sets Aggressiveness value.
    /*!
      \param i an integer constant.
     */
    public void SetAggressiveness(int i)
    {
        Aggressiveness = i;
    }
    //!< sets Sarcasm value.
    /*!
      \param i an integer constant.
     */
    public void SetSarcasm(int i)
    {
        Saracasm = i;
    }

    // Getters
    //! gets Charisma value.
    /*!
      \return Charisma.
     */
    public int GetCharisma()
    {
        return Charisma;
    }
    //! gets Friendliness value.
    /*!
      \return Friendliness.
     */
    public int GetFriendliness()
    {
        return Friendliness;
    }
    //! gets Aggressiveness value.
    /*!
      \return Aggressiveness.
     */
    public int GetAggressiveness()
    {
        return Aggressiveness;
    }
    //! gets Sarcasm value.
    /*!
      \return Sarcasm.
     */
    public int GetSarcasm()
    {
        return Saracasm;
    }
}
