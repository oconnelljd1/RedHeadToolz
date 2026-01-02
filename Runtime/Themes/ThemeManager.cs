using UnityEngine;
using UnityEngine.Events;

namespace RedHeadToolz.Themes
{
    public class ThemeManager : BaseManager
    {
        [SerializeField] private ThemeData defaultTheme;
        public UnityAction<ThemeData> ThemeChanged;
        private ThemeData _currentTheme;

        public void SetTheme(ThemeData theme)
        {
            _currentTheme = theme;
            ThemeChanged?.Invoke(theme);
        }

        public ThemeData GetTheme()
        {
            return _currentTheme == null ? defaultTheme : _currentTheme;
        }
    }
}
