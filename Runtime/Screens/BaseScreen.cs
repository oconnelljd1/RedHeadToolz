using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

namespace RedHeadToolz.Screens
{
    // A class that must be extended from if you expect to use it with the ScreenManager
    public class BaseScreen : MonoBehaviour
    {
        [SerializeField] protected Transform _root;
        [SerializeField] protected GameObject _inputLock;
        [SerializeField] private bool _hideStack = false;
        public bool HideStack {
            get { return _hideStack; }
            set {}
        }
        [SerializeField] private bool _singleInstance = false;
        public bool SingleInstance {
            get { return _singleInstance; }
            set {}
        }

        protected bool _showing = false;
        public bool Showing => _showing;

        protected bool _lockInput{
            get{
                return _inputLock.activeSelf;
            }
            set{
                _inputLock.SetActive(value);
            }
        }

        public virtual void Show(Action callback = null)
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

        public virtual void ShowImmediate()
        {
            _showing = true;
            _root.localScale = Vector3.one;
            _lockInput = false;
        }

        public virtual void Hide(Action callback = null)
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

        public virtual void Close(Action callback = null)
        {
            Hide(() => {
                if(callback != null) callback();
                ScreenManager.Instance.CloseScreen(this);
            });
        }
    }
}