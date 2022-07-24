using UnityEngine.UIElements;

namespace SteveJstone
{
    public class TabSection : VisualElement
    {
        public TabSection()
        {
            AddToClassList("tabbed-list__tab-section");
        }

        #region UXML
        public new class UxmlFactory : UxmlFactory<TabSection> { }
        #endregion
    }
}
