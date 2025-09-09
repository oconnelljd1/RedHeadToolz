using UnityEngine;

namespace RedHeadToolz
{
    // [CreateAssetMenu(fileName = "BaseTransition", menuName = "RedHeadToolz/BaseTransition")]
    public abstract class BaseTransition : ScriptableObject
    {
        [Header("Base Transition")]
        [SerializeField] protected float delay;
        [SerializeField] protected float duration;
        [SerializeField] protected AnimationCurve curve = AnimationCurve.Linear(0, 0, 1, 1);

        // public float Delay => delay;
        // public float Duration => duration;
        // public AnimationCurve Curve => curve;

        public abstract void Evaluate(CanvasGroup canvasGroup, float time);

        public void Complete(CanvasGroup canvasGroup)
        {
            Evaluate(canvasGroup, delay + duration);
        }

        public bool IsComplete(float time)
        {
            return time >= delay + duration;
        }
    }
}
