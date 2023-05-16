using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerMaxLevelSO")]
public class PlayerMaxLevelSO : ScriptableObject
{
    [SerializeField] public int playeMaxLevel;
}
