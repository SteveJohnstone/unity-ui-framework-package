using System.Collections.Generic;
using UnityEngine.UIElements;

namespace SteveJstone
{
    public class Header : Label
    {
        public Header()
        {
            AddToClassList("header");
        }

        #region UXML
        public new class UxmlFactory : UxmlFactory<Header, UxmlTraits> { }
        public new class UxmlTraits : Label.UxmlTraits { }
        #endregion
    }
}
