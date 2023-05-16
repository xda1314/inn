using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.NiceVibrations;
using Ivy;

public class VibrateSystem
{
    public static void SetHapticsActive(bool active)
    {
        try
        {
#if UNITY_IOS
        MMVibrationManager.SetHapticsActive(active);
#else
            MMVibrationManager.SetHapticsActive(false);
#endif
        }
        catch
        {

        }
    }

    public static void Haptic(HapticTypes hapticType)
    {
        if (GameManager.Instance == null
            || GameManager.Instance.playerData == null
            || !GameManager.Instance.playerData.IsHapticOn)
            return;


        try
        {
#if UNITY_IOS
            GameDebug.Log("Haptic : " + hapticType);
            MMVibrationManager.Haptic(hapticType);
#endif
        }
        catch
        {

        }
    }
}
