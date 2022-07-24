using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace SteveJstone
{
    public class ItemList : VisualElement
    {
        private int _itemCount = 3;
        private string _itemTemplate;
        private ListSync _itemSync;
        private UQueryBuilder<ItemListElement> _items;
        private Logger<ItemList> _logger = new Logger<ItemList>(false);

        private event Action<ItemListElement> _onItemClicked;

        public int ItemCount { get => _itemCount; set { _itemCount = value; Refresh(); } }
        public UQueryBuilder<ItemListElement> Items => _items;
        public event Action<ItemListElement> OnItemClicked {
            add
            {
                lock (this)
                {
                    _onItemClicked += value;
                }
            }
            remove
            {
                lock (this)
                {
                    _onItemClicked -= value;
                }
            }
        }

        public ItemList()
        {
            _logger.Info($"ctor() name='{name}'");
            AddToClassList("item-list");
            this.LoadLayout(nameof(ItemList));
        }


        private void Init(int itemCount, string templateName)
        {
            _logger.Info($"{nameof(Init)} name='{name}' itemCount={itemCount}, itemTemplate='{templateName}'");

            _itemCount = itemCount;
            _itemTemplate = templateName;

            _items = this.Query<ItemListElement>();
            var itemsContainer = this.Q<VisualElement>("Items");

            VisualTreeAsset template = null;
            if (!string.IsNullOrEmpty(templateName))
            {
                template = VisualElementHelpers.GetTemplate(templateName);

                var itemList = _items.ToList();
                foreach(var item in itemList)
                {
                    item.Clear();
                    item.RegisterCallback<ClickEvent>(OnItemClick);
                    template.CloneTree(item);
                }
            }
            _logger.Info($"{nameof(Init)} childCount={itemsContainer.childCount}");
            _itemSync = new ListSync(
                itemsContainer.childCount,
                (i) => {
                    var item = new ItemListElement() { name = $"Item{i + 1}" };
                    item.AddToClassList("item");
                    if (template != null)
                    {
                        template.CloneTree(item);
                    }
                    item.RegisterCallback<ClickEvent>(OnItemClick);
                    itemsContainer.Add(item);
                },
                (i) =>
                {
                    var item = itemsContainer.ElementAt(i);
                    item.style.display = DisplayStyle.Flex;
                },
                (i) => {
                    if (itemsContainer.childCount >= i)
                    {
                        itemsContainer.ElementAt(i).style.display = DisplayStyle.None;
                    }
                }
            );


            Refresh();
        }

        private void OnItemClick(ClickEvent evt)
        {
            _logger.Info($"{nameof(OnItemClick)} propPhase={evt.propagationPhase}");

            var target = evt.currentTarget as ItemListElement;
            _onItemClicked?.Invoke(target);
            evt.StopImmediatePropagation();
            
        }

        public void Refresh()
        {
            _logger.Info($"{nameof(Refresh)} name='{name}' sectionCount={_itemCount}");

            _itemSync.Update(_itemCount);
        }

        #region UXML
        public new class UxmlFactory : UxmlFactory<ItemList, UxmlTraits> { }

        public new class UxmlTraits : VisualElement.UxmlTraits
        {
            UxmlIntAttributeDescription _itemCount = new UxmlIntAttributeDescription { name = "item-count" };
            UxmlStringAttributeDescription _itemTemplate = new UxmlStringAttributeDescription { name = "item-template" };

            public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
            {
                get { yield break; }
            }

            public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {
                base.Init(ve, bag, cc);
                ((ItemList)ve).Init(
                    _itemCount.GetValueFromBag(bag, cc),
                    _itemTemplate.GetValueFromBag(bag,cc)
                );
            }
        }

        #endregion
    }
}
