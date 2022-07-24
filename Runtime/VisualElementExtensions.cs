using UnityEngine;
using UnityEngine.UIElements;

public static class VisualElementExtensions
{
    public static void LoadLayout(this VisualElement element, string layoutName)
    {
        var visualTreeAsset = Resources.Load<VisualTreeAsset>(layoutName);
        if (visualTreeAsset != null)
        {
            visualTreeAsset.CloneTree(element);
        }
        else
        {
            Debug.LogWarning($"Failed to load {layoutName} layout file.");
        }
    }
}
