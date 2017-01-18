using UnityEngine;
using System.Collections;

public class MessagePasser : MonoBehaviour {
    public void OnPlayerTalkToNPC() {
        //storymanager.OnPlayerTalkToNPC();
        //userinterface.OnPlayerTalkToNPC();
    }
}

































    public static void BroadcastMessage(string msg)
    {
        Debug.Log("MessagePasser: broadcasting message '" + msg + "'");
        foreach (var rootObject in GetSceneRoots())
            rootObject.BroadcastMessage(msg);
    }

    private static IEnumerable<GameObject> GetSceneRoots()
    {
        /* Returns an Enumerable over every GameObject which is a root object in the scene.
         */
        var prop = new HierarchyProperty(HierarchyType.GameObjects);
        var expanded = new int[0];
        while (prop.Next(expanded)) {
            yield return prop.pptrValue as GameObject;
        }
    }
}

OnPlayerTalkToNPC

public class Foo {
    public OnPlayerTalkToNPC() {
    }
