using UnityEngine;

namespace SteveJstone
{
    [CreateAssetMenu(menuName = "SteveJstone/Palette")]
    public class ColourPalette : ScriptableObject
    {
        [SerializeField] private Color _primary = Color.blue;
        [SerializeField] private Color _danger = Color.red;
        [SerializeField] private Color _warning = Color.red;
        [SerializeField] private Color _success = Color.green;

        public Color Primary => _primary;
        public Color Danger => _danger;
        public Color Warning => _warning;
        public Color Success => _success;
    }
}