using UnityEngine.UIElements;

namespace SteveJstone
{
    public class ItemListElement : VisualElement
    {
        public ItemListElement()
        {
            AddToClassList("item-list__element");
        }

        #region UXML
        public new class UxmlFactory : UxmlFactory<ItemListElement> { }
        #endregion
    }
}
