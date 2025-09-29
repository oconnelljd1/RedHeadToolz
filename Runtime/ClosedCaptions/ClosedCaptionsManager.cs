using UnityEngine;
using System.Collections.Generic;
using RedHeadToolz.Debugging;
using RedHeadToolz;
using UnityEngine.Localization.Settings;
using System;
using System.Threading.Tasks;


#if UNITY_EDITOR
using UnityEditor;
#endif

public class ClosedCaptionsManager : BaseManager
{
    [SerializeField] private GameObject _root;
    [SerializeField] private TMPro.TextMeshProUGUI _text;
    [SerializeField] private List<ClosedCaptionsData> _captions;
    [SerializeField] private string _defaultTable = "DefaultTable";
    private ClosedCaptionsData _data;
    private int _index;
    private float _time;
    private Action _callback;

    public override async Task<InitializationStatus> Init()
    {
        Stop();
        return await base.Init();
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
        _text.text = LocalizationSettings.StringDatabase.GetLocalizedString(GetTable(_data.captions[_index]), _data.captions[_index].TextKey);
        _root.SetActive(true);
        _callback = callback;
    }

    private string GetTable(ClosedCaption data)
    {
        if (string.IsNullOrEmpty(data.Table)) return _defaultTable;
        return data.Table;
    }

    protected void Update()
    {
        if (_data == null) return;

        _time += Time.deltaTime;
        if (_time >= _data.captions[_index].EndTime)
        {
            _index++;
            if (_index < _data.captions.Count)
            {
                _text.text = LocalizationSettings.StringDatabase.GetLocalizedString(GetTable(_data.captions[_index]), _data.captions[_index].TextKey);
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
        // string[] guids = AssetDatabase.FindAssets("t:ClosedCaptionsData", new[] { "Assets/ScriptableObjects/ClosedCaptions" });
        string[] guids = AssetDatabase.FindAssets("t:ClosedCaptionsData", new[] { "Assets" });
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