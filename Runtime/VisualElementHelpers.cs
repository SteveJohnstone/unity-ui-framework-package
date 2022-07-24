using UnityEngine;
using UnityEngine.UIElements;

public static class VisualElementHelpers
{
    public static VisualTreeAsset GetTemplate(string templateName)
    {
        return Resources.Load<VisualTreeAsset>(templateName);
    }
}