using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace RedHeadToolz.Screens
{
    // A class that must be extended from if you expect to use it with the ScreenManager
    public class GenericScreen : ScaleScreen
    {
        [SerializeField] private TMP_Text _titleText;
        [SerializeField] private TMP_Text _descriptionText;
        [SerializeField] private GameObject _buttonPrefab;
        [SerializeField] private Transform _buttonRoot;

        public virtual GenericScreen Setup(GenericScreenData data)
        {
            _titleText.text = data.Title;
            _descriptionText.text = data.Description;

            foreach (var buttonData in data.Buttons)
            {
                GenericButton button = Instantiate(_buttonPrefab, _buttonRoot).GetComponent<GenericButton>();
                button.Init(buttonData);
            }

            return this;
        }
    }

    public class GenericScreenData
    {
        public string Title;
        public string Description;
        public List<GenericButtonData> Buttons;
    }
}