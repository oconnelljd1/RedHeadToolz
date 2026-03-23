using UnityEngine.Localization.Settings;

namespace RedHeadToolz.Utils
{
    public static class StringExtensions
    {
        public static string Localize(this string key, string tableName = "BibleWoolies")
        {
            return LocalizationSettings.StringDatabase.GetLocalizedString(tableName, key);
        }
    }
}