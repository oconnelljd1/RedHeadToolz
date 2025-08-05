using System;
using DG.Tweening;
using UnityEngine;

namespace RedHeadToolz.Screens
{
    public class TranslateScreen : BaseScreen
    {
        [Header("TranslateScreen")]
        [SerializeField] private RectTransform _root;
        [Tooltip("Values are multiplied by screen width/height")]
        [SerializeField] private Vector2 _inStart;
        [Tooltip("Values are multiplied by screen width/height")]
        [SerializeField] private Vector2 _outEnd;

        private Vector2 rootOrigin;

        void Awake()
        {
            rootOrigin = _root.anchoredPosition;
            // RHTebug.Log($"{rootOrigin}");
        }

        public override void Show(Action callback = null)
        {
            _lockInput = true;
            _showing = true;

            // RHTebug.Log($"width: {_root.rect.width}, height: {_root.rect.width}");

            _root.anchoredPosition = new Vector2(_inStart.x * _root.rect.width, _inStart.y * _root.rect.height);
            _root.DOAnchorPos(rootOrigin, 0.5f)
                .OnComplete(() => {
                    callback?.Invoke();
                    _lockInput = false;
                });
        }

        public override void ShowImmediate()
        {
            _root.anchoredPosition = Vector2.zero;
            _showing = true;
            _lockInput = false;
        }

        public override void Hide(Action callback = null)
        {
            _lockInput = true;
            _showing = true;

            Vector2 outProduct = new Vector2(_outEnd.x * _root.rect.width, _outEnd.y * _root.rect.height);

            _root.DOAnchorPos(outProduct, 0.5f)
                .OnComplete(() => {
                    callback?.Invoke();
                    _lockInput = false;
                });
        }

        void Update()
        {
            // RHTebug.Log($"{_root.anchoredPosition}");
        }
    }
}