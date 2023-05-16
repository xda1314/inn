using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

[System.Serializable]
public class ConfigSO : SerializedScriptableObject
{
    [ShowInInspector, OdinSerialize]
    public Dictionary<string, string> configDict;
}
