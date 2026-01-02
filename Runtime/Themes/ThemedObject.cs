using RedHeadToolz.Debugging;
using UnityEngine;

namespace RedHeadToolz.Themes
{
    public abstract class ThemedObject : MonoBehaviour
    {
        [SerializeField] protected ThemeColor themeColor;

        private void Start()
        {
            UpdateColor(GeneralManager.Instance.GetManager<ThemeManager>().GetTheme());
        }

        private void OnEnable()
        {
            var ThemeManager = GeneralManager.Instance.GetManager<ThemeManager>();
            if (ThemeManager == null)
            {
                RHTebug.LogError("ThemeManager is not initialized!");
                return;
            }
            ThemeManager.ThemeChanged += OnThemeChanged;
        }

        private void OnDisable()
        {
            var ThemeManager = GeneralManager.Instance.GetManager<ThemeManager>();
            if(ThemeManager != null)
            {
                RHTebug.LogError("ThemeManager is not initialized!");
                return;
            }
            ThemeManager.ThemeChanged -= OnThemeChanged;
        }

        private void OnThemeChanged(ThemeData theme)
        {
            UpdateColor(theme);
        }

        protected abstract void UpdateColor(ThemeData theme);
    }
}
