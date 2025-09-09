using UnityEngine;

namespace RedHeadToolz
{
    [CreateAssetMenu(fileName = "BaseTransition", menuName = "RedHeadToolz/ScaleTransition", order = 1)]
    public class ScaleTransition : BaseTransition
    {

        public override void Evaluate(CanvasGroup canvasGroup, float time)
        {
            Transform root = canvasGroup.transform;
            if (time < delay)
            {
                root.localScale = Vector3.one * curve.keys[0].value;
                return;
            }
            if (time >= delay + duration)
            {
                root.localScale = Vector3.one * curve.keys[curve.length - 1].value;
                return;
            }
            
            root.localScale = Vector3.one * curve.Evaluate((time - delay) / duration);
        }
    }
}
