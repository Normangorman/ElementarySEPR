using UnityEngine;
using System.Collections;
using NUnit.Framework;

[TestFixture]
public class PlayerUnitTests {
    static Player playerInstance = new Player();

    [Test]
    public void ExampleTest()
    {
        Debug.Log("Hello world from PlayerUnitTests!");
    }

    [Test]
    public void TestSpeedIsSensible()
    {
        Assert.LessOrEqual(playerInstance.speed, 10f);
    }
}
