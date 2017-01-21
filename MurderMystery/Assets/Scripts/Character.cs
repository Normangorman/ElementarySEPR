using UnityEngine;
using System.Collections;

//! Character class. 
/*! Class for both the NPCs and Player objects containing personality traits. */
public class Character : MonoBehaviour
{
    public Constants.People person;
    public Sprite Icon;

    public int Charisma; //!< Charisma integer value.
    public int Friendly; //!< Friendliness integer value.
    public int Sarcasm; //!< Sarcasm integer value.
    
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
        Friendly = i;
    }

    //!< sets Sarcasm value.
    /*!
      \param i an integer constant.
     */
    public void SetSarcasm(int i)
    {
        Sarcasm = i;
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
        return Friendly;
    }
    //! gets Sarcasm value.
    /*!
      \return Sarcasm.
     */
    public int GetSarcasm()
    {
        return Sarcasm;
    }
}
