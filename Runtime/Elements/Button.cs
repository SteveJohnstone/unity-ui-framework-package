using UnityEngine.UIElements;

namespace SteveJstone
{
    public class Button : UnityEngine.UIElements.Button
    {
        public Button(): base()
        {
            AddToClassList("btn");
        }

        #region UXML
        public new class UxmlFactory : UxmlFactory<Button, UxmlTraits> { }
        public new class UxmlTraits : UnityEngine.UIElements.Button.UxmlTraits { }
        #endregion
    }
}
