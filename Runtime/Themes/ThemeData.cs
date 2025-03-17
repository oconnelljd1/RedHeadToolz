using RedHeadToolz.Debugging;
using UnityEngine;

namespace RedHeadToolz.Themes
{
    [CreateAssetMenu(fileName = "ThemeData", menuName = "RedHeadToolz/ThemeData")]//, order = 0)]
    public class ThemeData : ScriptableObject
    {
        [SerializeField] private Color primaryText;
        [SerializeField] private Color secondaryText;
        [SerializeField] private Color primaryBackground;
        [SerializeField] private Color secondaryBackground;

        public Color PrimaryText => primaryText;
        public Color SecondaryText => secondaryText;
        public Color PrimaryBackground => primaryBackground;
        public Color SecondaryBackground => secondaryBackground;

        public Color GetColor(ThemeColor color)
        {
            switch(color)
            {
                case ThemeColor.PrimaryText:
                    return primaryText;
                case ThemeColor.SecondaryText:
                    return secondaryText;
                case ThemeColor.PrimaryBackground:
                    return primaryBackground;
                case ThemeColor.SecondaryBackground:
                    return secondaryBackground;
                default:
                    RHTebug.LogError($"ThemeData.GetColor: Unknown color {color}");
                    break;
            }
            return Color.white;
        }
    }

    public enum ThemeColor
    {
        PrimaryText,
        SecondaryText,
        PrimaryBackground,
        SecondaryBackground
    }
}
