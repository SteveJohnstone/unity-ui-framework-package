using UnityEngine.UIElements;

namespace SteveJstone
{
    public class CardBody : VisualElement
    {
        public CardBody()
        {
            AddToClassList("card-body");
        }

        #region UXML
        public new class UxmlFactory : UxmlFactory<CardBody> { }
        #endregion
    }
}
