using UnityEngine;
using UnityEngine.UI;

namespace RedHeadToolz.Themes
{
    public class ThemedImage : ThemedObject
    {
        [SerializeField] private Image image;

        protected override void UpdateColor(ThemeData theme)
        {
            image.color = theme.GetColor(themeColor);
        }
    }
}
