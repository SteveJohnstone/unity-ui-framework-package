using UnityEngine.UIElements;

namespace SteveJstone
{
    public class CardFooter : VisualElement
    {
        public CardFooter()
        {
            AddToClassList("card-footer");
        }

        #region UXML
        public new class UxmlFactory : UxmlFactory<CardFooter> { }
        #endregion
    }
}
