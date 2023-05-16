// ILSpyBased#2
using BDUnity;
using BDUnity.Utils;
using ivy.game;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;

public class StorageDefinition
{
    public static Dictionary<int, int> TotalDefinitionsDict { get; private set; } = new Dictionary<int, int>() 
    {
        { 6,50},
        { 7,100},
        { 8,150},
        { 9,300},
        { 10,500},
        { 11,700},
        { 12,1000},
        { 13,1400},
        { 14,1900},
        { 15,2500},
        { 16,3200},
        { 17,4000},
        { 18,4900},
        { 19,5900},
        { 20,7000},
        { 21,9000},
        { 22,11000},
        { 23,13000},
        { 24,15000},
        { 25,17000},
        { 26,19000},
        { 27,21000},
        { 28,23000},
        { 29,25000},
        { 30,27000},
        { 31,29000},
        { 32,31000},
        { 33,33000},
        { 34,35000},
        { 35,37000},
        { 36,39000},
        { 37,41000},
        { 38,43000},
        { 39,45000},
        { 40,47000},
    };
    public static int MaxGridNum { get; private set; } = 0;//可解锁的最大格子数

    public static void Init() 
    {
        MaxGridNum = TotalDefinitionsDict.Count + 5;
    }
}
