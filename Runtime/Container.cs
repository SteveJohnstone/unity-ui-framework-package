using UnityEngine.UIElements;

namespace SteveJstone
{
    public class Container : VisualElement
    {
        public Container()
        {
            AddToClassList("container");
        }

        #region UXML
        public new class UxmlFactory : UxmlFactory<Container> { }
        #endregion
    }
}
