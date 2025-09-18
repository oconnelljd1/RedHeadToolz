using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace RedHeadToolz.Screens
{
    public class GenericButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private TMP_Text _text;

        public void Init(GenericButtonData data)
        {
            _text.text = data.Text;
            _button.onClick.AddListener(() => data.OnClick());
        }
    }

    public class GenericButtonData
    {
        public string Text;
        public Action OnClick;
    }
}
