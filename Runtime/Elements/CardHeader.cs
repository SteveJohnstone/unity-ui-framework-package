using UnityEngine.UIElements;

namespace SteveJstone
{
    public class CardHeader : VisualElement
    {
        public CardHeader()
        {
            AddToClassList("card-header");
        }

        #region UXML
        public new class UxmlFactory : UxmlFactory<CardHeader> { }
        #endregion
    }
}
