using UnityEngine;

namespace RedHeadToolz.Themes
{
    public abstract class ThemedObject : MonoBehaviour
    {
        [SerializeField] protected ThemeColor themeColor;

        private void Start()
        {
            UpdateColor(ThemeController.Instance.GetTheme());
        }

        private void OnEnable()
        {
            ThemeController.Instance.ThemeChanged += OnThemeChanged;
        }

        private void OnDisable()
        {
            ThemeController.Instance.ThemeChanged -= OnThemeChanged;
        }

        private void OnThemeChanged(ThemeData theme)
        {
            UpdateColor(theme);
        }

        protected abstract void UpdateColor(ThemeData theme);
    }
}
