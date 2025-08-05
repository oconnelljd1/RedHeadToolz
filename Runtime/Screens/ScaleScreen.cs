using UnityEngine;
using DG.Tweening;
using System;

namespace RedHeadToolz.Screens
{
    // a class built to handle showing and hiding through a generic scale animation
    public abstract class ScaleScreen : BaseScreen
    {
        [Header("ScaleScreen")]
        [SerializeField] protected Transform _root;

        public override void Show(Action callback = null)
        {
            _lockInput = true;
            _root.localScale = Vector3.zero;
            
            Sequence showSequence = DOTween.Sequence();
            
            showSequence.Append(_root.DOScale(1.1f, 0.4f).SetEase(Ease.OutSine));
            showSequence.Append(_root.DOScale(1.0f, 0.1f)
                .SetEase(Ease.OutSine)
                .OnComplete(()=>{
                    if(callback != null) callback();
                    _lockInput = false;
                }));

            _showing = true;
            showSequence.Play();
        }

        public override void ShowImmediate()
        {
            _showing = true;
            _root.localScale = Vector3.one;
            _lockInput = false;
        }

        public override void Hide(Action callback = null)
        {
            _lockInput = true;
            _root.localScale = Vector3.one;
            
            Sequence showSequence = DOTween.Sequence();
            
            showSequence.Append(_root.DOScale(1.1f, 0.1f).SetEase(Ease.OutSine));
            showSequence.Append(
                _root.DOScale(0f, 0.4f)
                .SetEase(Ease.OutSine)
                .OnComplete(()=>{
                    if(callback != null) callback();
                }));
            
            _showing = false;
            showSequence.Play();
        }
    }
}