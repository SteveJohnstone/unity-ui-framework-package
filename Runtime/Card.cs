using UnityEngine.UIElements;

namespace SteveJstone
{
    public class Card : VisualElement
    {
        public Card()
        {
            AddToClassList("card");
        }

        #region UXML
        public new class UxmlFactory : UxmlFactory<Card> { }
        #endregion
    }
}
