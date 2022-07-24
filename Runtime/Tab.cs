using System;
using System.Collections.Generic;
using UnityEngine.UIElements;

namespace SteveJstone
{
    public class Tab : VisualElement
    {
        private UnityEngine.UIElements.Button _button;

        public Tab()
        {
            this.LoadLayout(nameof(Tab));

            _button = this.Q<UnityEngine.UIElements.Button>();
            _button.clicked += OnButtonClick;

            AddToClassList("tabbed-list__tab");
        }

        private void OnButtonClick()
        {
            OnClick?.Invoke(this);
        }

        public string Target { get; set; }
        public Action<Tab> OnClick { get; set; }
        public string Title
        {
            get => _button.text; 
            set
            {
                _button.text = string.IsNullOrEmpty(value) ? "Tab" : value;
            }
        }

        #region UXML
        public new class UxmlFactory : UxmlFactory<Tab, UxmlTraits> { }

        public new class UxmlTraits : VisualElement.UxmlTraits
        {
            UxmlStringAttributeDescription _target = new UxmlStringAttributeDescription { name = "target" };
            UxmlStringAttributeDescription _title = new UxmlStringAttributeDescription { name = "title" };

            public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
            {
                get { yield break; }
            }

            public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {
                base.Init(ve, bag, cc);
                var tab = ((Tab)ve);
                tab.Target = _target.GetValueFromBag(bag, cc);
                tab.Title = _title.GetValueFromBag(bag, cc);
            }
        }
        #endregion
    }
}
