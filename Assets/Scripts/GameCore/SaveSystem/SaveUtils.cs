using System.Collections;
using UnityEngine;
using Ivy;


public static class SaveUtils
{
    public static void CreateID(string collectionName)
    {
#if UNITY_EDITOR
        return;
#else
        RiseSdk.Instance.MMKVWithId(collectionName);
#endif
    }

    public static bool HasKey(string key)
    {
#if UNITY_EDITOR
        return PlayerPrefs.HasKey(key);
#else
        return RiseSdk.Instance.MMContainsKey(key) || PlayerPrefs.HasKey(key);
#endif
    }

    public static bool HasKey2(string collectionName, string key)
    {
#if UNITY_EDITOR
        string tempKey = collectionName + "_" + key;
        return PlayerPrefs.HasKey(tempKey);
#else
        return RiseSdk.Instance.MMContainsKey2(collectionName,key);
#endif
    }

    public static void DeleteKey(string key)
    {
#if UNITY_EDITOR
        PlayerPrefs.DeleteKey(key);
#else
        RiseSdk.Instance.MMRemoveKey(key);
        PlayerPrefs.DeleteKey(key);
#endif
    }

    public static void DeleteKey2(string collectionName, string key)
    {
#if UNITY_EDITOR
        string tempKey = collectionName + "_" + key;
        PlayerPrefs.DeleteKey(tempKey);
#else
        RiseSdk.Instance.MMRemoveKey2(collectionName,key);
#endif
    }

    public static int GetInt(string key, int defaultvalue = 0, bool compatible = false)
    {
#if UNITY_EDITOR
        return PlayerPrefs.GetInt(key, defaultvalue);
#else
                //if (compatible)
                {
                    if (!RiseSdk.Instance.MMContainsKey(key) && PlayerPrefs.HasKey(key))
                    {
                        int value = PlayerPrefs.GetInt(key);
                        RiseSdk.Instance.MMSetIntValue(key, value);
                        PlayerPrefs.DeleteKey(key);
                    }
                }
                return RiseSdk.Instance.MMGetIntValue(key, defaultvalue);
#endif
    }

    public static int GetInt2(string collectionName, string key, int defaultvalue = 0)
    {
#if UNITY_EDITOR
        string tempKey = collectionName + "_" + key;
        return PlayerPrefs.GetInt(tempKey);
#else
        return RiseSdk.Instance.MMGetIntValue2(collectionName,key, defaultvalue);
#endif
    }

    public static long GetLong(string key, long defaultvalue = 0, bool compatible = false)
    {
#if UNITY_EDITOR
        string str = PlayerPrefs.GetString(key, "0");
        if (long.TryParse(str, out long temp))
        {
            return temp;
        }
        else
        {
            return 0;
        }
#else
                //if (compatible)
                {
                    if (!RiseSdk.Instance.MMContainsKey(key) && PlayerPrefs.HasKey(key))
                    {
                        string str = PlayerPrefs.GetString(key);
                        if (long.TryParse(str, out long value))
                        {
                        }
                        else
                        {
                            value= 0;
                        }
                        RiseSdk.Instance.MMSetLongValue(key, value);
                        PlayerPrefs.DeleteKey(key);
                    }
                }
                return RiseSdk.Instance.MMGetLongValue(key, defaultvalue);
#endif
    }

    public static long GetLong2(string collectionName, string key, long defaultvalue = 0)
    {
#if UNITY_EDITOR
        string tempKey = collectionName + "_" + key;
        string str = PlayerPrefs.GetString(tempKey, "0");
        if (long.TryParse(str, out long temp))
        {
            return temp;
        }
        else
        {
            return 0;
        }
#else
        return RiseSdk.Instance.MMGetLongValue2(collectionName,key, defaultvalue);
#endif
    }

    public static float GetFloat(string key, float defaultvalue = 0f, bool compatible = false)
    {
#if UNITY_EDITOR
        return PlayerPrefs.GetFloat(key, defaultvalue);
#else
       // if (compatible)
        {
            if (!RiseSdk.Instance.MMContainsKey(key) && PlayerPrefs.HasKey(key))
            {
                float value = PlayerPrefs.GetFloat(key);
                RiseSdk.Instance.MMSetStringValue(key, value.ToString());
                PlayerPrefs.DeleteKey(key);
            }
        }
        string str = RiseSdk.Instance.MMGetStringValue(key, "0");
        if (float.TryParse(str, out float tempFloat))
        {
            return tempFloat;
        }
        return 0;
#endif
    }

    public static float GetFloat2(string collectionName, string key, float defaultvalue = 0f)
    {
#if UNITY_EDITOR
        string tempKey = collectionName + "_" + key;
        return PlayerPrefs.GetFloat(tempKey, defaultvalue);
#else
        string str = RiseSdk.Instance.MMGetStringValue2(collectionName, key, "0");
        if (float.TryParse(str, out float tempFloat))
        {
            return tempFloat;
        }
        return 0;
#endif
    }

    public static string GetString(string key, string defaultvalue = "", bool compatible = false)
    {
#if UNITY_EDITOR
        return PlayerPrefs.GetString(key, defaultvalue);
#else
                //if (compatible)
                {
                    if (!RiseSdk.Instance.MMContainsKey(key) && PlayerPrefs.HasKey(key))
                    {
                        string  value = PlayerPrefs.GetString(key);
                        RiseSdk.Instance.MMSetStringValue(key, value);
                        PlayerPrefs.DeleteKey(key);
                    }
                }
                return RiseSdk.Instance.MMGetStringValue(key, defaultvalue);
#endif
    }

    public static string GetString2(string collectionName, string key, string defaultvalue = "")
    {
#if UNITY_EDITOR
        string tempKey = collectionName + "_" + key;
        return PlayerPrefs.GetString(tempKey, defaultvalue);
#else
        return RiseSdk.Instance.MMGetStringValue2(collectionName,key, defaultvalue);
#endif
    }

    public static bool GetBool(string key, bool defaultvalue = false, bool compatible = false)
    {
#if UNITY_EDITOR
        return PlayerPrefs.GetInt(key, 0) == 1;
#else
                //if (compatible)
                {
                    if (!RiseSdk.Instance.MMContainsKey(key) && PlayerPrefs.HasKey(key))
                    {
                        bool value = PlayerPrefs.GetInt(key) == 1;
                        RiseSdk.Instance.MMSetBoolValue(key, value);
                        PlayerPrefs.DeleteKey(key);
                    }
                }
                return RiseSdk.Instance.MMGetBoolValue(key, defaultvalue);
#endif
    }

    public static bool GetBool2(string collectionName, string key, bool defaultvalue = false)
    {
#if UNITY_EDITOR
        string tempKey = collectionName + "_" + key;
        return PlayerPrefs.GetInt(tempKey, 0) == 1;
#else
        return RiseSdk.Instance.MMGetBoolValue2(collectionName,key, defaultvalue);
#endif
    }

    public static void SetInt(string key, int value)
    {
#if UNITY_EDITOR
        PlayerPrefs.SetInt(key, value);
        PlayerPrefs.Save();
#else
                RiseSdk.Instance.MMSetIntValue(key, value);
#endif
    }

    public static void SetInt2(string collectionName, string key, int value)
    {
#if UNITY_EDITOR
        string tempKey = collectionName + "_" + key;
        PlayerPrefs.SetInt(tempKey, value);
        PlayerPrefs.Save();
#else
        RiseSdk.Instance.MMSetIntValue2(collectionName,key, value);
#endif
    }

    public static void SetLong(string key, long value)
    {
#if UNITY_EDITOR
        PlayerPrefs.SetString(key, value.ToString());
        PlayerPrefs.Save();
#else
                RiseSdk.Instance.MMSetLongValue(key,value);
#endif
    }

    public static void SetLong2(string collectionName, string key, long value)
    {
#if UNITY_EDITOR
        string tempKey = collectionName + "_" + key;
        PlayerPrefs.SetString(tempKey, value.ToString());
        PlayerPrefs.Save();
#else
        RiseSdk.Instance.MMSetLongValue2(collectionName,key,value);
#endif
    }

    public static void SetFloat(string key, float value)
    {
#if UNITY_EDITOR
        PlayerPrefs.SetFloat(key, value);
        PlayerPrefs.Save();
#else
        RiseSdk.Instance.MMSetStringValue(key,value.ToString());
#endif
    }

    public static void SetFloat2(string collectionName, string key, float value)
    {
#if UNITY_EDITOR
        string tempKey = collectionName + "_" + key;
        PlayerPrefs.SetFloat(tempKey, value);
        PlayerPrefs.Save();
#else
        RiseSdk.Instance.MMSetStringValue2(collectionName,key,value.ToString());
#endif
    }

    public static void SetString(string key, string value)
    {
#if UNITY_EDITOR
        PlayerPrefs.SetString(key, value);
        PlayerPrefs.Save();
#else
                RiseSdk.Instance.MMSetStringValue(key,value);
#endif
    }

    public static void SetString2(string collectionName, string key, string value)
    {
#if UNITY_EDITOR
        string tempKey = collectionName + "_" + key;
        PlayerPrefs.SetString(tempKey, value);
        PlayerPrefs.Save();
#else
        RiseSdk.Instance.MMSetStringValue2(collectionName,key,value);
#endif
    }

    public static void SetBool(string key, bool value)
    {
#if UNITY_EDITOR
        PlayerPrefs.SetInt(key, value ? 1 : 0);
        PlayerPrefs.Save();
#else
                RiseSdk.Instance.MMSetBoolValue(key,value);
#endif
    }

    public static void SetBool2(string collectionName, string key, bool value)
    {
#if UNITY_EDITOR
        string tempKey = collectionName + "_" + key;
        PlayerPrefs.SetInt(tempKey, value ? 1 : 0);
        PlayerPrefs.Save();
#else
        RiseSdk.Instance.MMSetBoolValue2(collectionName,key,value);
#endif
    }

}