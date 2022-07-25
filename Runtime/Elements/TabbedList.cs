using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace SteveJstone
{
    public class TabbedList : VisualElement
    {
        private string _currentSection;
        private int _sectionCount = 3;
        private UQueryBuilder<Tab> _tabs;
        private UQueryBuilder<TabSection> _sections;
        private ListSync _sectionSync;
        private ListSync _tabSync;
        private Logger<TabbedList> _logger = new Logger<TabbedList>(false);
        public string InitialSection { get; set; }
        public UQueryBuilder<TabSection> Sections => _sections;
        public UQueryBuilder<Tab> Tabs => _tabs;
        public int SectionCount { get => _sectionCount; set { _sectionCount = value; Refresh(); } }

        public TabbedList()
        {
            _logger.Info($"ctor()");
            this.LoadLayout(nameof(TabbedList));
            AddToClassList("tabbed-list");
        }

        private void Init(string initialSection, int sectionCount, string sectionTemplateName, string tabTemplateName)
        {
            _logger.Info($"{nameof(Init)} initialSection='{initialSection}', sectionCount={sectionCount}, sectionTemplate='{sectionTemplateName}'");

            InitialSection = initialSection;
            _sectionCount = sectionCount;
            _currentSection = InitialSection;

            InitTabs(tabTemplateName);
            InitSections(sectionTemplateName);

            Refresh();
        }

        private void InitTabs(string tabTemplateName)
        {
            var tabContainer = this.Q<VisualElement>("Tabs");

            _tabs = this.Query<Tab>();
            _tabs.ForEach((Tab tab) =>
            {
                if (tab.Target == _currentSection)
                {
                    tab.AddToClassList("active");
                }
                else
                {
                    tab.RemoveFromClassList("active");
                }
                tab.OnClick += OnTabClicked;
            });

            VisualTreeAsset tabTemplate = null;
            if (!string.IsNullOrEmpty(tabTemplateName))
            {
                tabTemplate = VisualElementHelpers.GetTemplate(tabTemplateName);
                _tabs.ForEach((Tab tab) =>
                {
                    tab.Clear();
                    tabTemplate.CloneTree(tab);
                });
            }

            _tabSync = new ListSync(
                tabContainer.childCount,
                (i) =>
                {
                    var newTab = new Tab();
                    if (tabTemplate != null)
                    {
                        tabTemplate.CloneTree(newTab);
                    }
                    tabContainer.Add(newTab);
                    newTab.Target = $"Section{i + 1}";
                    newTab.AddToClassList("tab");
                    newTab.OnClick += OnTabClicked;
                },
                (i) => tabContainer.ElementAt(i).style.display = DisplayStyle.Flex,
                (i) =>
                {
                    if (tabContainer.childCount >= i)
                    {
                        tabContainer.ElementAt(i).style.display = DisplayStyle.None;
                    }
                }
            );
        }

        private VisualTreeAsset InitSections(string sectionTemplateName)
        {
            _sections = this.Query<TabSection>();
            _currentSection = string.IsNullOrEmpty(InitialSection) ? _sections.First().name : InitialSection;
            var sectionContainer = this.Q<VisualElement>("Sections");

            VisualTreeAsset sectionTemplate = null;
            if (!string.IsNullOrEmpty(sectionTemplateName))
            {
                sectionTemplate = VisualElementHelpers.GetTemplate(sectionTemplateName);
                _sections.ForEach((TabSection section) =>
                {
                    section.Clear();
                    sectionTemplate.CloneTree(section);
                });
            }
            _sectionSync = new ListSync(
                sectionContainer.childCount,
                (i) =>
                {
                    var section = new TabSection() { name = $"Section{i + 1}" };
                    section.AddToClassList("tab-section");
                    if (sectionTemplate != null)
                    {
                        sectionTemplate.CloneTree(section);
                    }
                    sectionContainer.Add(section);
                },
                (i) =>
                {
                    var tabSection = sectionContainer.ElementAt(i);
                    if (_currentSection == tabSection.name)
                    {
                        tabSection.style.display = DisplayStyle.Flex;
                    }
                    else
                    {
                        tabSection.style.display = DisplayStyle.None;
                    }
                },
                (i) =>
                {
                    if (sectionContainer.childCount >= i)
                    {
                        sectionContainer.ElementAt(i).style.display = DisplayStyle.None;
                    }
                }
            );
            return sectionTemplate;
        }

        private void OnTabClicked(Tab tab)
        {
            _logger.Info($"{nameof(OnTabClicked)} _currentSection='{_currentSection}' tab.Target='{tab.Target}'");

            var previousElement = this.Q<VisualElement>(_currentSection);
            previousElement.style.display = DisplayStyle.None;

            var element = this.Q<VisualElement>(tab.Target);
            element.style.display = DisplayStyle.Flex;

            _currentSection = tab.Target;

            this.Q<Tab>(className: "active")?.RemoveFromClassList("active");
            tab.AddToClassList("active");
        }

        public void Refresh()
        {
            _logger.Info($"{nameof(Refresh)} sectionCount={_sectionCount}");

            _tabSync.Update(_sectionCount);
            _sectionSync.Update(_sectionCount);

            _sections = this.Query<TabSection>();
            _sections.ForEach((TabSection section) =>
            {
                if (section.name != _currentSection)
                {
                    section.style.display = DisplayStyle.None;
                }
                else
                {
                    section.style.display = DisplayStyle.Flex;
                }
            });
        }

        #region UXML
        public new class UxmlFactory : UxmlFactory<TabbedList, UxmlTraits> { }

        public new class UxmlTraits : VisualElement.UxmlTraits
        {
            UxmlStringAttributeDescription _initialSection = new UxmlStringAttributeDescription { name = "initial-section" };
            UxmlIntAttributeDescription _sectionCount = new UxmlIntAttributeDescription { name = "section-count" };
            UxmlStringAttributeDescription _sectionTemplate = new UxmlStringAttributeDescription { name = "section-template" };
            UxmlStringAttributeDescription _tabTemplate = new UxmlStringAttributeDescription { name = "tab-template" };

            public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
            {
                get { yield break; }
            }

            public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {
                base.Init(ve, bag, cc);
                ((TabbedList)ve).Init(
                    _initialSection.GetValueFromBag(bag, cc),
                    _sectionCount.GetValueFromBag(bag, cc),
                    _sectionTemplate.GetValueFromBag(bag,cc),
                    _tabTemplate.GetValueFromBag(bag,cc)
                );
            }
        }

        #endregion
    }
}
