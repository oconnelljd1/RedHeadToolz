using UnityEngine;

[CreateAssetMenu(fileName = "SOExample", menuName = "RedHeadToolz/SOExample")]//, order = 0)]
public class SOExample : ScriptableObject
{
    private string _id;
    
    public string ID => _id;
}