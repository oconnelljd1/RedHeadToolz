using UnityEngine;
using DG.Tweening;
using System;
using RedHeadToolz.Debugging;

namespace RedHeadToolz.Screens
{
    public class TransitionScreen : BaseScreen
    {
        [SerializeField] private CanvasGroup _group;

        public override void Show(Action callback = null)
        {
            // RHTebug.Log("TransitionScreen.Show");
            _lockInput = true;
            _showing = true;

            _group.alpha = 0;
            _group.DOFade(1f, 1f).OnComplete(() => {
                if(callback != null) callback();
                _lockInput = false;
            });
        }

        public override void ShowImmediate()
        {
            _showing = true;
            _group.alpha = 1;
            _lockInput = false;
        }

        public override void Hide(Action callback = null)
        {
            // RHTebug.Log("TransitionScreen.Hide");
            _lockInput = true;
            _showing = false;
            
            _group.alpha = 1;
            _group.DOFade(0f, 1f).OnComplete(() => {
                if(callback != null) callback();
                _lockInput = false;
            });
        }
    }
}
