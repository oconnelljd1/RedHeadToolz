using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using RedHeadToolz.Debugging;
using UnityEngine.Localization.Settings;
using System;

namespace RedHeadToolz.ClosedCaptions
{
    public class ClosedCaptionsManager : BaseManager
    {
        [SerializeField] private GameObject _root;
        [SerializeField] private TMPro.TextMeshProUGUI _text;
        [SerializeField] private string _tableId;
        [SerializeField] private List<ClosedCaptionsData> _captions;
        private ClosedCaptionsData _data;
        private int _index;
        private float _time;
        private Action _callback;

        public override void Init()
        {
            Stop();
            base.Init();
        }

        public ClosedCaptionsData GetCaption(string caption)
        {
            ClosedCaptionsData data = null;
            data = _captions.Find(x => x.Id == caption);
            if (data == null) RHTebug.LogWarning($"Closed Caption {caption} not found");
            return data;
        }

        public void StartCaption(string caption)
        {
            StartCaption(GetCaption(caption));
        }

        public void StartCaption(string caption, Action callback)
        {
            StartCaption(GetCaption(caption), callback);
        }

        public void StartCaption(ClosedCaptionsData data)
        {
            StartCaption(data, null);
        }

        public void StartCaption(ClosedCaptionsData data, Action callback)
        {
            _data = data;
            _index = 0;
            _time = 0;
            _text.text = LocalizationSettings.StringDatabase.GetLocalizedString(_tableId, _data.captions[_index].TextKey);
            _root.SetActive(true);
            _callback = callback;
        }

        protected void Update()
        {
            if (_data == null) return;

            _time += UnityEngine.Time.deltaTime;
            if (_time >= _data.captions[_index].EndTime)
            {
                _index++;
                if (_index < _data.captions.Count)
                {
                    _text.text = LocalizationSettings.StringDatabase.GetLocalizedString("Aura3025Table", _data.captions[_index].TextKey);
                }
                else
                {
                    _callback?.Invoke();
                    _callback = null;
                    Stop();
                }
            }
        }

        public void Stop()
        {
            _data = null;
            _root.SetActive(false);
        }

#if UNITY_EDITOR
        [MenuItem("CONTEXT/ClosedCaptionsManager/Collect Captions")]
        private static void CollectClips(MenuCommand menuCommand)
        {
            ClosedCaptionsManager controller = (ClosedCaptionsManager)menuCommand.context;

            List<ClosedCaptionsData> newCaptions = new List<ClosedCaptionsData>();
            string[] guids = AssetDatabase.FindAssets("t:ClosedCaptionsData", new[] { "Assets/ScriptableObjects/ClosedCaptions" });
            foreach (var guid in guids)
            {
                ClosedCaptionsData clip = (ClosedCaptionsData)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(guid), typeof(ClosedCaptionsData));
                newCaptions.Add(clip);
            }

            controller._captions = newCaptions;
            EditorUtility.SetDirty(controller);
        }
#endif

    }
}