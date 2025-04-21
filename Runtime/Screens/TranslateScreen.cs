using System;
using DG.Tweening;
using RedHeadToolz.Debugging;
using RedHeadToolz.Screens;
using UnityEngine;

public class TranslateScreen : BaseScreen
{
    [Header("TranslateScreen")]
    [SerializeField] private RectTransform _root;
    [SerializeField] private Vector2 _inStart;
    [SerializeField] private Vector2 _outEnd;

    private Vector2 rootOrigin;

    void Awake()
    {
        rootOrigin = _root.anchoredPosition;
        RHTebug.Log($"{rootOrigin}");
    }

    public override void Show(Action callback = null)
    {
        _lockInput = true;
        _showing = true;

        _root.anchoredPosition = _inStart;
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

        // _root.anchoredPosition = _inStart;
        _root.DOAnchorPos(_outEnd, 0.5f)
            .OnComplete(() => {
                callback?.Invoke();
                _lockInput = false;
            });
    }
}
