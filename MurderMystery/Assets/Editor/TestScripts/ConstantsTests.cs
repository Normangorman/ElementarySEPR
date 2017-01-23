using UnityEngine;
using System.Collections;
using NUnit.Framework;
using System;

[TestFixture]
public class ConstantsTests
{
    [Test]
    public void TestCharacterValues()
    {
        // Every NPC should have an entry in the CharacterValues table
        foreach (Constants.People person in Enum.GetValues(typeof(Constants.People)))
        {
            Assert.True(Constants.CharacterValues.ContainsKey(person));
            Assert.IsNotNull(Constants.CharacterValues[person]);
        }
    }

    [Test]
    public void TestGuestDescriptions()
    {
        // Every NPC should have an entry in the CharacterValues table
        foreach (Constants.People person in Enum.GetValues(typeof(Constants.People)))
        {
            Assert.True(Constants.GuestDescription.ContainsKey(person));
        }
    }

    [Test]
    public void TestGetPersonByName()
    {
        Assert.AreEqual(Constants.GetPersonByName("Poirot"), Constants.People.Poirot);
        Assert.AreEqual(Constants.GetPersonByName("Dumbledore"), Constants.People.Dumbledore);
        Assert.Throws(typeof(PersonNotFound), delegate { Constants.GetPersonByName("Osama Bin Laden"); });
    }
}