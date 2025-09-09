using UnityEngine;

namespace RedHeadToolz
{
    [CreateAssetMenu(fileName = "BaseTransition", menuName = "RedHeadToolz/TranslateTransition", order = 1)]
    public class TranslateTransition : BaseTransition
    {
        [Header("Translate Transition")]
        [Tooltip("Values are multiplied by screen width/height")]
        [SerializeField] private Vector2 _start;

        [Tooltip("Values are multiplied by screen width/height")]
        [SerializeField] private Vector2 _finish;

        public override void Evaluate(CanvasGroup canvasGroup, float time)
        {
            RectTransform root = canvasGroup.GetComponent<RectTransform>();

            if (time < delay)
            {
                root.anchoredPosition = new Vector2(_start.x * root.rect.width, _start.y * root.rect.height);
                return;
            }
            if (time >= delay + duration)
            {
                root.anchoredPosition = new Vector2(_finish.x * root.rect.width, _finish.y * root.rect.height);
                return;
            }

            float eval = curve.Evaluate((time - delay) / duration);
            Vector2 inProduct = new Vector2(_start.x * root.rect.width, _start.y * root.rect.height);
            Vector2 outProduct = new Vector2(_finish.x * root.rect.width, _finish.y * root.rect.height);
            root.anchoredPosition = Vector2.Lerp(inProduct, outProduct, eval);
        }
    }
}
