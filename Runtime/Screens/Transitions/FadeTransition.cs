using UnityEngine;

namespace RedHeadToolz
{
    [CreateAssetMenu(fileName = "BaseTransition", menuName = "RedHeadToolz/FadeTransition", order = 1)]
    public class FadeTransition : BaseTransition
    {
        // [Header("Fade Transition")]

        public override void Evaluate(CanvasGroup canvasGroup, float time)
        {
            if (time < delay)
            {
                canvasGroup.alpha = curve.keys[0].value;
                return;
            }
            if (time >= delay + duration)
            {
                canvasGroup.alpha = curve.keys[curve.length - 1].value;
                return;
            }
            
            canvasGroup.alpha = curve.Evaluate((time - delay) / duration);
        }
    }
}
