using UnityEngine;
using TMPro;

namespace RedHeadToolz.Themes
{
    public class ThemedText : ThemedObject
    {
        [SerializeField] private TMP_Text text;

        protected override void UpdateColor(ThemeData theme)
        {
            text.color = theme.GetColor(themeColor);
        }
    }
}
