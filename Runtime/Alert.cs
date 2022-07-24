using UnityEngine.UIElements;

namespace SteveJstone
{
    public class Alert : VisualElement
    {
        public Alert()
        {
            AddToClassList("alert");
        }

        #region UXML
        public new class UxmlFactory : UxmlFactory<Alert> { }
        #endregion
    }
}
