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
    [SerializeField] private string _tableName;
    [SerializeField] private List<ClosedCaption> _captions;

    public string Id => _id;
    public string TableName => _tableName;
    public List<ClosedCaption> captions => _captions;
}
