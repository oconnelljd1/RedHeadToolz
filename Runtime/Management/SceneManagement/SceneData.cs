using UnityEngine;

[CreateAssetMenu(fileName = "SceneData", menuName = "RedHeadToolz/SceneData")]
public class SceneData : ScriptableObject
{
    // TODO hide these? still show these, just as immutable fields
    [SerializeField] private string _guid;
    [SerializeField] private string _name;

    public string Guid => _guid;
    public string Name => _name;
}