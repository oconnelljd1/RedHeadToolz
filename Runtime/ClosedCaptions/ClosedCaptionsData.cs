using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ClosedCaptionsData", menuName = "RedHeadToolz/ClosedCaptionsData")]
public class ClosedCaptionsData : ScriptableObject
{
    [System.Serializable]
    public class ClosedCaption
    {
        [SerializeField] private string _textKey;
        [SerializeField] private float _endTime;

        public string TextKey => _textKey;
        public float EndTime => _endTime;
    }
    
    [SerializeField] private string _id;

    public string Id => _id;
    public List<ClosedCaption> captions = new List<ClosedCaption>();
}
