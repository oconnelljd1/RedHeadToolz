using UnityEngine.Localization.Settings;

namespace RedHeadToolz.Utils
{
    public static class StringExtensions
    {
        public static string Localize(this string key)
        {
            return LocalizationSettings.StringDatabase.GetLocalizedString("DefaultTable", key);
        }
    }
}