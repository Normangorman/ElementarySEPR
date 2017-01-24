using UnityEngine;
using System.Collections;
using DoozyUI;

public class UpdateSortingLayerName : MonoBehaviour
{
    public string newLayerName = "UI";

    public void UpdateCanvases()
    {
        UIManager.UpdateCanvases(gameObject, newLayerName);
    }

    public void UpdateRenderers()
    {
        UIManager.UpdateRenderers(gameObject, newLayerName);
    }

}
