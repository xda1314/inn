using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using System.IO;
using System;
using BDUnity.Utils;
using Sirenix.OdinInspector;
using System.Linq;
using ivy.game;
using UnityEditor.AddressableAssets.Settings;

public class EditorWindow_Config : OdinEditorWindow
{
    [MenuItem("工具箱/配表工具", priority = 100)]
    private static void OpenWindow()
    {
        var window = GetWindow<EditorWindow_Config>();
        window.titleContent = new GUIContent("配表工具");
        window.Show();
    }

    [Title("Excel转Json")]
    [Button("打开Excel转json工具", ButtonSizes.Gigantic), PropertyOrder(-1)]
    public void ShowExcelTools()
    {
        ExcelTools.ShowExcelTools();
    }

    [Title("ConfigSO与Json互转")]
    [Space(10)]
    [LabelText("所有配置文件整合后的ConfigSO路径"), ReadOnly]
    public string configSOPath = "Assets/ConfigSO/ConfigSO.asset";

    [LabelText("存放零散json文件存放路径"), ReadOnly]
    public string ConfigFileSavePathShow = "..(unity项目根目录)/Configs/Json";
    public string jsonConfigFileSavePath
    {
        get
        {
            DirectoryInfo info = new DirectoryInfo(Application.dataPath);
            return Path.Combine(info.Parent.FullName, "Configs", "Json");
        }
    }

    [LabelText("是否生成关卡分析文件")]
    public bool GenerateLevelAnalysisFile_ = false;

    [LabelText("关卡分析文件存放路径"), ShowIf("GenerateLevelAnalysisFile_"), FolderPath(AbsolutePath = true, RequireExistingPath = true)]
    public string LevelAnalysisFileSavePath_ = "";



    //[HorizontalGroup]
    //[Button("提取ConfigSO配置文件到json目录", ButtonSizes.Gigantic)]
    public void ExtractAllConfigFiles()
    {
        var di = new System.IO.DirectoryInfo(jsonConfigFileSavePath);
        if (di.GetFiles().Length > 0)
        {
            if (!UnityEditor.EditorUtility.DisplayDialog("警告", "目标文件夹将被清空,是否继续?", "确定", "取消"))
            {
                return;
            }
            Directory.Delete(jsonConfigFileSavePath, true);
            Directory.CreateDirectory(jsonConfigFileSavePath);
        }

        System.Text.StringBuilder elementCollect = new System.Text.StringBuilder();
        Dictionary<int, System.Text.StringBuilder> levelDataCollectDic = new Dictionary<int, System.Text.StringBuilder>();
        Dictionary<ElementType, int> wholeUseType = new Dictionary<ElementType, int>();
        Dictionary<CollectionType, int> wholeUseCollectionTypeDic = new Dictionary<CollectionType, int>();
        Dictionary<AdditionType, int> wholeUseAdditionTypeDic = new Dictionary<AdditionType, int>();
        Dictionary<ObstacleType, int> wholeUseObstacleTypeDic = new Dictionary<ObstacleType, int>();
        Dictionary<string, HashSet<string>> wholeUseWeightedItemDic = new Dictionary<string, HashSet<string>>();
        Dictionary<string, HashSet<string>> whichLevelUsedWeightedItem = new Dictionary<string, HashSet<string>>();
        Dictionary<string, HashSet<string>> wholeGeneConditionPresetQueueDic = new Dictionary<string, HashSet<string>>();
        Dictionary<string, HashSet<string>> whichGeneConditionPresetQueueItem = new Dictionary<string, HashSet<string>>();
        Dictionary<string, HashSet<string>> wholeGeneratorWeightsDic = new Dictionary<string, HashSet<string>>();
        Dictionary<string, HashSet<string>> whichGeneratorWeightsItem = new Dictionary<string, HashSet<string>>();
        Dictionary<string, HashSet<string>> wholeGeneConditionElimDic = new Dictionary<string, HashSet<string>>();
        Dictionary<string, HashSet<string>> whichGeneConditionElimItem = new Dictionary<string, HashSet<string>>();
        Dictionary<string, HashSet<string>> wholeGeneConditionRoundDic = new Dictionary<string, HashSet<string>>();
        Dictionary<string, HashSet<string>> whichGeneConditionRoundItem = new Dictionary<string, HashSet<string>>();



        ConfigSO configSO = AssetDatabase.LoadAssetAtPath<ConfigSO>(configSOPath);
        if (configSO == null || configSO.configDict == null)
        {
            UnityEditor.EditorUtility.DisplayDialog("警告", "ConfigSO为空，无法提取json", "确定");
            return;
        }

        UnityEditor.EditorUtility.DisplayProgressBar("提取文件中...", "", 0);
        foreach (var item in configSO.configDict)
        {
            string jsonName = item.Key;
            var jsonContent = item.Value;
            var sw = new StreamWriter(jsonConfigFileSavePath + "\\" + jsonName + ".json");

            if (jsonName.Equals("levels"))
            {
                SortedDictionary<string, int> targetCollectDic = new SortedDictionary<string, int>();
                System.Text.StringBuilder LevelConfigCollect = new System.Text.StringBuilder();
                string str = "";
                var jsonObj = CustomJSON.Deserialize(jsonContent);
                if (jsonObj.dict.ContainsKey("levels"))
                {
                    var lvDataList = jsonObj.dict["levels"];

                    foreach (var lvData in lvDataList.list)
                    {
                        if (lvData.dict.ContainsKey("id"))
                        {
                            LevelConfigCollect.Append("[ id = ").Append(lvData.dict["id"].ToString()).Append("]   ");
                        }
                        if (lvData.dict.ContainsKey("moves"))
                        {
                            LevelConfigCollect.Append("[ moves =").Append(lvData.dict["moves"].ToString()).Append("]  ");
                        }
                        if (lvData.dict.ContainsKey("diff"))
                        {
                            LevelConfigCollect.Append("[ diff =").Append(lvData.dict["diff"].s).Append("]  ");
                        }
                        if (lvData.dict.ContainsKey("targets"))
                        {
                            LevelConfigCollect.Append("[ targets =");
                            var targetsList = lvData.dict["targets"].list;
                            foreach (var targetData in targetsList)
                            {
                                var type = -1;
                                var subType = -1;
                                var color = -1;
                                var num = -1;
                                if (targetData.dict.ContainsKey("type"))
                                {
                                    type = targetData.dict["type"].i;
                                }
                                if (targetData.dict.ContainsKey("subType"))
                                {
                                    subType = targetData.dict["subType"].i;
                                }
                                if (targetData.dict.ContainsKey("color"))
                                {
                                    color = targetData.dict["color"].i;
                                }
                                if (targetData.dict.ContainsKey("num"))
                                {
                                    num = targetData.dict["num"].i;
                                }

                                switch (type)
                                {
                                    case 3:
                                        if (subType == 1)
                                        {
                                            str = "Brick_" + color.ToString();
                                            LevelConfigCollect.Append(str).Append(" ");
                                            if (!targetCollectDic.ContainsKey(str))
                                            {
                                                targetCollectDic.Add(str, 0);
                                            }
                                        }
                                        else if (subType == 38)
                                        {
                                            str = "BrickMarked";
                                            LevelConfigCollect.Append("BrickMarked").Append(" ");
                                            if (!targetCollectDic.ContainsKey(str))
                                            {
                                                targetCollectDic.Add(str, 0);
                                            }
                                        }
                                        else if (subType == 105)
                                        {
                                            str = "FlyMob_" + color.ToString();
                                            LevelConfigCollect.Append(str).Append(" ");
                                            if (!targetCollectDic.ContainsKey(str))
                                            {
                                                targetCollectDic.Add(str, 0);
                                            }
                                        }
                                        else if (subType == 101)
                                        {
                                            str = "Gift_" + color.ToString();
                                            LevelConfigCollect.Append("Gift").Append("_").Append(color.ToString()).Append(" ");
                                            if (!targetCollectDic.ContainsKey(str))
                                            {
                                                targetCollectDic.Add(str, 0);
                                            }
                                        }
                                        else if (subType == 3)
                                        {
                                            str = "Cake";
                                            LevelConfigCollect.Append("Cake").Append(" ");
                                            if (!targetCollectDic.ContainsKey(str))
                                            {
                                                targetCollectDic.Add(str, 0);
                                            }
                                        }
                                        else
                                        {
                                            str = ((ElementType)(subType)).ToString();
                                            LevelConfigCollect.Append(((ElementType)(subType)).ToString()).Append(" ");
                                            if (!targetCollectDic.ContainsKey(str))
                                            {
                                                targetCollectDic.Add(str, 0);
                                            }
                                        }

                                        break;
                                    case 2:
                                        str = ((AdditionType)(subType)).ToString();
                                        LevelConfigCollect.Append(((AdditionType)(subType)).ToString()).Append(" ");
                                        if (!targetCollectDic.ContainsKey(str))
                                        {
                                            targetCollectDic.Add(str, 0);

                                        }
                                        break;
                                    case 4:
                                        str = ((ObstacleType)(subType)).ToString();
                                        LevelConfigCollect.Append(((ObstacleType)(subType)).ToString()).Append(" ");
                                        if (!targetCollectDic.ContainsKey(str))
                                        {
                                            targetCollectDic.Add(str, 0);
                                        }
                                        break;
                                    case 1:
                                        str = ((CollectionType)(subType)).ToString();
                                        LevelConfigCollect.Append(((CollectionType)(subType)).ToString()).Append(" ");
                                        if (!targetCollectDic.ContainsKey(str))
                                        {
                                            targetCollectDic.Add(str, 0);
                                        }
                                        break;
                                    default:
                                        str = "UnknownType_" + (subType).ToString();
                                        LevelConfigCollect.Append("UnknownType_").Append((subType).ToString()).Append(" ");
                                        if (!targetCollectDic.ContainsKey(str))
                                        {
                                            targetCollectDic.Add(str, 0);
                                        }
                                        break;
                                }
                            }
                            //LevelConfigCollect.Append("[diff]=").Append(lvData.dict["diff"].s).Append("  ");
                            LevelConfigCollect.Append(" ]");
                        }

                        if (lvData.dict.ContainsKey("startProps"))
                        {
                            var list = lvData.dict["startProps"].list;
                            LevelConfigCollect.Append("[ startProps =");
                            foreach (var sp in list)
                            {
                                LevelConfigCollect.Append(sp.s).Append(" ");
                            }
                            LevelConfigCollect.Append("]  ");
                        }
                        if (lvData.dict.ContainsKey("props"))
                        {
                            var list = lvData.dict["props"].list;
                            LevelConfigCollect.Append("[ props =");
                            foreach (var sp in list)
                            {
                                LevelConfigCollect.Append(sp.s).Append(" ");
                            }
                            LevelConfigCollect.Append("]  ");
                        }
                        if (lvData.dict.ContainsKey("starLevel"))
                        {
                            var list = lvData.dict["starLevel"].list;
                            LevelConfigCollect.Append("[ starLevel =");
                            foreach (var sp in list)
                            {
                                LevelConfigCollect.Append(sp.s).Append(" ");
                            }
                            LevelConfigCollect.Append("]  ");
                        }
                        LevelConfigCollect.AppendLine();
                    }
                }
                LevelConfigCollect.Append("所有关卡使用的目标对象类型汇总:  ");
                foreach (var kv in targetCollectDic)
                {
                    LevelConfigCollect.Append(kv.Key).Append("  ");
                }
                if (GenerateLevelAnalysisFile_)
                {
                    if (Directory.Exists(LevelAnalysisFileSavePath_))
                    {
                        using (System.IO.TextWriter tw = new System.IO.StreamWriter(LevelAnalysisFileSavePath_ + "/LevelTargetAnalysisData.txt"))
                        {
                            tw.Write(LevelConfigCollect);
                            tw.Close();
                        }
                    }
                }

            }
            if (jsonName.StartsWith("seeds"))
            {
                System.Text.StringBuilder LevelDataCollect = new System.Text.StringBuilder();
                var jsonObj = CustomJSON.Deserialize(jsonContent);
                if (jsonObj.dict.ContainsKey("seeds"))
                {
                    HashSet<int> generateIDSet = new HashSet<int>();
                    var seedsObj = jsonObj.dict["seeds"];
                    var seedCount = seedsObj.list.Count;
                    foreach (var seed in seedsObj.list)
                    {
                        if (seed.dict.ContainsKey("grids"))
                        {
                            var gridList = seed.dict["grids"].list;
                            var gridCount = gridList.Count;
                            foreach (var gridObj in gridList)
                            {
                                bool useBorders = false;
                                bool hadFireworkPillows = false;
                                if (gridObj.dict.ContainsKey("borders"))
                                {
                                    useBorders = true;
                                }
                                int row = 0, col = 0;

                                if (gridObj.dict.ContainsKey("rowSpan"))
                                {
                                    row = gridObj.dict["rowSpan"].i;
                                }
                                if (gridObj.dict.ContainsKey("colSpan"))
                                {
                                    col = gridObj.dict["colSpan"].i;
                                }
                                if (gridObj.dict.ContainsKey("cells"))
                                {
                                    var cellObj = gridObj.dict["cells"];
                                    if (cellObj.isList)
                                    {
                                        Dictionary<ElementType, int> tempDic = new Dictionary<ElementType, int>();
                                        Dictionary<CollectionType, int> CollectionTypeDic = new Dictionary<CollectionType, int>();
                                        Dictionary<OccurrenceType, int> CollectionOccurrenceTypeDic = new Dictionary<OccurrenceType, int>();
                                        Dictionary<AdditionType, int> AdditionTypeDic = new Dictionary<AdditionType, int>();
                                        Dictionary<ObstacleType, int> ObstacleTypeDic = new Dictionary<ObstacleType, int>();
                                        HashSet<string> usedPropertySet = new HashSet<string>();
                                        foreach (var data in cellObj.list)
                                        {

                                            if (data != null && data.dict.ContainsKey("element"))
                                            {
                                                var elementType = (ElementType)data.dict["element"].dict["type"].i;
                                                if (elementType == ElementType.Jelly)
                                                {
                                                    if (data.dict["element"].dict.ContainsKey("color"))
                                                    {
                                                        if (data.dict["element"].dict["color"].i == 8)
                                                        {
                                                            hadFireworkPillows = true;
                                                        }
                                                    }
                                                }
                                                if (!tempDic.ContainsKey(elementType))
                                                {
                                                    tempDic.Add(elementType, 0);
                                                }
                                                if (!wholeUseType.ContainsKey(elementType))
                                                {
                                                    wholeUseType.Add(elementType, 0);
                                                }
                                            }
                                            if (data != null && data.dict.ContainsKey("collection"))
                                            {
                                                var collectionType = (CollectionType)data.dict["collection"].dict["type"].i;
                                                if (!CollectionTypeDic.ContainsKey(collectionType))
                                                {
                                                    CollectionTypeDic.Add(collectionType, 0);
                                                }
                                                if (!wholeUseCollectionTypeDic.ContainsKey(collectionType))
                                                {
                                                    wholeUseCollectionTypeDic.Add(collectionType, 0);
                                                }
                                                if (data.dict["collection"].dict.ContainsKey("occurrence"))
                                                {
                                                    var collectionOccType = (OccurrenceType)data.dict["collection"].dict["occurrence"].i;
                                                    if (!CollectionOccurrenceTypeDic.ContainsKey(collectionOccType))
                                                    {
                                                        CollectionOccurrenceTypeDic.Add(collectionOccType, 0);
                                                    }
                                                }
                                            }
                                            if (data != null && data.dict.ContainsKey("addition"))
                                            {
                                                var additionType = (AdditionType)data.dict["addition"].dict["type"].i;
                                                if (!AdditionTypeDic.ContainsKey(additionType))
                                                {
                                                    AdditionTypeDic.Add(additionType, 0);
                                                }
                                                if (!wholeUseAdditionTypeDic.ContainsKey(additionType))
                                                {
                                                    wholeUseAdditionTypeDic.Add(additionType, 0);
                                                }
                                            }
                                            if (data != null && data.dict.ContainsKey("obstacle"))
                                            {
                                                var obstacleType = (ObstacleType)data.dict["obstacle"].dict["type"].i;
                                                if (!ObstacleTypeDic.ContainsKey(obstacleType))
                                                {
                                                    ObstacleTypeDic.Add(obstacleType, 0);
                                                }
                                                if (!wholeUseObstacleTypeDic.ContainsKey(obstacleType))
                                                {
                                                    wholeUseObstacleTypeDic.Add(obstacleType, 0);
                                                }
                                            }
                                            if (data != null && data.dict.ContainsKey("isShaft"))
                                            {
                                                usedPropertySet.Add("useIsShaft");
                                            }
                                            if (data != null && data.dict.ContainsKey("teleId"))
                                            {
                                                usedPropertySet.Add("useTeleId");
                                            }
                                            if (data != null && data.dict.ContainsKey("isFill"))
                                            {
                                                usedPropertySet.Add("useIsFill");
                                            }
                                            if (data != null && data.dict.ContainsKey("direction"))
                                            {
                                                usedPropertySet.Add("useDirection");
                                            }
                                            if (data != null && data.dict.ContainsKey("enable"))
                                            {
                                                usedPropertySet.Add("useEnable");
                                            }
                                            if (data != null && data.dict.ContainsKey("generatorId"))
                                            {
                                                var gID = data.dict["generatorId"].i;
                                                if (!generateIDSet.Contains(gID))
                                                {
                                                    generateIDSet.Add(gID);
                                                }
                                                usedPropertySet.Add("useGeneratorId");
                                            }
                                            if (data != null && data.dict.ContainsKey("isExit"))
                                            {
                                                usedPropertySet.Add("useIsExit");
                                            }
                                            if (data != null && data.dict.ContainsKey("Jam"))
                                            {
                                                usedPropertySet.Add("useJam");
                                            }
                                            if (data != null && data.dict.ContainsKey("cameraTarget"))
                                            {
                                                usedPropertySet.Add("useCameraTarget");
                                            }
                                            if (data != null && data.dict.ContainsKey("cameraMoveToId"))
                                            {
                                                usedPropertySet.Add("useCameraMoveToId");
                                            }
                                            if (data != null && data.dict.ContainsKey("cameraTargetOffset"))
                                            {
                                                usedPropertySet.Add("useCameraTargetOffset");
                                            }
                                            if (data != null && data.dict.ContainsKey("cameraWaterLevel"))
                                            {
                                                usedPropertySet.Add("useCameraWaterLevel");
                                            }
                                            if (data != null && data.dict.ContainsKey("conveyor"))
                                            {
                                                usedPropertySet.Add("useConveyor");
                                            }
                                            if (data != null && data.dict.ContainsKey("roadNode"))
                                            {
                                                usedPropertySet.Add("useRoadNode");
                                            }
                                            if (data != null && data.dict.ContainsKey("dyeRug"))
                                            {
                                                usedPropertySet.Add("useDyeRug");
                                            }
                                            if (data != null && data.dict.ContainsKey("nozzle"))
                                            {
                                                usedPropertySet.Add("useNozzle");
                                            }
                                            if (data != null && data.dict.ContainsKey("curtain"))
                                            {
                                                usedPropertySet.Add("useCurtain");
                                            }

                                        }
                                        LevelDataCollect.Append(jsonName).Append("   [●Used ElementTypes●] = ");
                                        foreach (var type in tempDic)
                                        {
                                            LevelDataCollect.Append(type.Key.ToString()).Append(" ");
                                        }
                                        if (CollectionTypeDic.Count > 0)
                                        {
                                            LevelDataCollect.Append("   [●Used CollectionTypes●] = ");
                                            foreach (var type in CollectionTypeDic)
                                            {
                                                LevelDataCollect.Append(type.Key.ToString()).Append(" ");
                                            }
                                            if (CollectionOccurrenceTypeDic.Count > 0)
                                            {
                                                LevelDataCollect.Append("OccurrenceType:").Append(" ");
                                                foreach (var type in CollectionOccurrenceTypeDic)
                                                {
                                                    LevelDataCollect.Append(type.Key.ToString()).Append(" ");
                                                }
                                            }
                                        }
                                        if (AdditionTypeDic.Count > 0)
                                        {
                                            LevelDataCollect.Append("   [●Used AdditionTypes●] = ");
                                            foreach (var type in AdditionTypeDic)
                                            {
                                                LevelDataCollect.Append(type.Key.ToString()).Append(" ");
                                            }
                                        }
                                        if (ObstacleTypeDic.Count > 0)
                                        {
                                            LevelDataCollect.Append("   [●Used ObstacleTypes●] = ");
                                            foreach (var type in ObstacleTypeDic)
                                            {
                                                LevelDataCollect.Append(type.Key.ToString()).Append(" ");
                                            }
                                        }
                                        LevelDataCollect.Append("   [●Used CellConfigProperties●] = ");
                                        foreach (var pro in usedPropertySet)
                                        {
                                            LevelDataCollect.Append(pro);
                                            if (pro.Equals("useGeneratorId"))
                                            {
                                                string tempStr = "";
                                                foreach (var ids in generateIDSet)
                                                {
                                                    tempStr += ids.ToString() + " ";
                                                }
                                                LevelDataCollect.Append(string.Format("(count:{0} ids: {1})", generateIDSet.Count, tempStr));
                                            }
                                            LevelDataCollect.Append(" ");
                                        }
                                        LevelDataCollect.Append("   [●GridConfig●] ");
                                        LevelDataCollect.AppendFormat("row {0} col {1}", row, col);
                                        if (useBorders)
                                        {
                                            LevelDataCollect.Append("useBorders ");
                                        }
                                        if (seedCount > 1)
                                        {
                                            LevelDataCollect.Append("mulitSeed ");
                                        }
                                        if (gridCount > 1)
                                        {
                                            LevelDataCollect.Append("mulitGrid ");
                                        }
                                        if (hadFireworkPillows)
                                        {
                                            LevelDataCollect.Append("hadFireworkPillows ");
                                        }

                                    }
                                }
                            }

                        }

                        if (seed.dict.ContainsKey("itemGeneratorManager"))
                        {
                            LevelDataCollect.Append(" ").Append("itemGeneratorManager111111");
                            var igmDic = seed.dict["itemGeneratorManager"];
                            if (igmDic.dict.ContainsKey("UseNewMethod"))
                            {
                                if (!igmDic.dict["UseNewMethod"].b)
                                {
                                    LevelDataCollect.Append(" ").Append("UseNewMethod-false");
                                }
                            }
                            if (igmDic.dict.ContainsKey("itemGenerators"))
                            {
                                LevelDataCollect.Append(" ").Append("itemGenerators");
                                List<CustomJSONObject> list = igmDic.dict["itemGenerators"].list;
                                for (int i = 0; i < list.Count; i++)
                                {
                                    if (list[i].dict.ContainsKey("generatorID"))
                                    {
                                        LevelDataCollect.Append(" ").Append("generatorID " + list[i].dict["generatorID"].i);
                                    }
                                    if (list[i].dict.ContainsKey("geneRedCarpetRolls"))
                                    {
                                        LevelDataCollect.Append(" ").Append("geneRedCarpetRolls " + list[i].dict["geneRedCarpetRolls"].b);
                                    }
                                    if (list[i].dict.ContainsKey("geneConditionDefault"))
                                    {
                                        LevelDataCollect.Append(" ").Append("geneConditionDefault");
                                    }
                                    if (list[i].dict.ContainsKey("geneConditionPresetQueue"))
                                    {
                                        LevelDataCollect.Append(" ").Append("geneConditionPresetQueue");
                                        var jsonList = list[i].dict["geneConditionPresetQueue"]["presetQueue"].list;
                                        if (jsonList != null)
                                        {
                                            for (int j = 0; j < jsonList.Count; ++j)
                                            {
                                                CustomJSONObject mTJSONObject = jsonList[j];
                                                ElementConfig key = new ElementConfig(mTJSONObject.dict);
                                                string typeName = key.type.ToString();
                                                if (!wholeGeneConditionPresetQueueDic.ContainsKey(typeName))
                                                {
                                                    wholeGeneConditionPresetQueueDic[typeName] = new HashSet<string>();
                                                }
                                                var checkStr = string.Format("type:{0} color:{1} subType:{2} hp:{3}", key.type, key.color, key.subType, key.hp);
                                                if (!wholeGeneConditionPresetQueueDic[typeName].Contains(checkStr))
                                                {
                                                    wholeGeneConditionPresetQueueDic[typeName].Add(checkStr);
                                                }
                                                if (!whichGeneConditionPresetQueueItem.ContainsKey(checkStr))
                                                {
                                                    whichGeneConditionPresetQueueItem[checkStr] = new HashSet<string>();
                                                }
                                                var levelIdStr = jsonName.Substring(6, jsonName.Length - 6);

                                                if (!whichGeneConditionPresetQueueItem[checkStr].Contains(levelIdStr))
                                                {
                                                    whichGeneConditionPresetQueueItem[checkStr].Add(levelIdStr);
                                                }
                                            }
                                        }
                                    }
                                    if (list[i].dict.ContainsKey("geneConditionElim"))
                                    {
                                        LevelDataCollect.Append(" ").Append("geneConditionElim");
                                        var jsonList = list[i].dict["geneConditionElim"].list;
                                        if (jsonList != null)
                                        {
                                            for (int j = 0; j < jsonList.Count; ++j)
                                            {
                                                CustomJSONObject mTJSONObject = jsonList[j];
                                                ElementConfig key = new ElementConfig(mTJSONObject["element"].dict);
                                                string typeName = key.type.ToString();
                                                if (!wholeGeneConditionElimDic.ContainsKey(typeName))
                                                {
                                                    wholeGeneConditionElimDic[typeName] = new HashSet<string>();
                                                }
                                                var checkStr = string.Format("type:{0} color:{1} subType:{2} hp:{3}", key.type, key.color, key.subType, key.hp);
                                                if (!wholeGeneConditionElimDic[typeName].Contains(checkStr))
                                                {
                                                    wholeGeneConditionElimDic[typeName].Add(checkStr);
                                                }
                                                if (!whichGeneConditionElimItem.ContainsKey(checkStr))
                                                {
                                                    whichGeneConditionElimItem[checkStr] = new HashSet<string>();
                                                }
                                                var levelIdStr = jsonName.Substring(6, jsonName.Length - 6);
                                                if (!whichGeneConditionElimItem[checkStr].Contains(levelIdStr))
                                                {
                                                    whichGeneConditionElimItem[checkStr].Add(levelIdStr);
                                                }
                                            }
                                        }
                                    }
                                    if (list[i].dict.ContainsKey("geneConditionRound"))
                                    {
                                        LevelDataCollect.Append(" ").Append("geneConditionRound");
                                        var jsonList = list[i].dict["geneConditionRound"].list;
                                        if (jsonList != null)
                                        {
                                            for (int j = 0; j < jsonList.Count; ++j)
                                            {
                                                CustomJSONObject mTJSONObject = jsonList[j];
                                                ElementConfig key = new ElementConfig(mTJSONObject["element"].dict);
                                                string typeName = key.type.ToString();
                                                if (!wholeGeneConditionRoundDic.ContainsKey(typeName))
                                                {
                                                    wholeGeneConditionRoundDic[typeName] = new HashSet<string>();
                                                }
                                                var checkStr = string.Format("type:{0} color:{1} subType:{2} hp:{3}", key.type, key.color, key.subType, key.hp);
                                                if (!wholeGeneConditionRoundDic[typeName].Contains(checkStr))
                                                {
                                                    wholeGeneConditionRoundDic[typeName].Add(checkStr);
                                                }
                                                if (!whichGeneConditionRoundItem.ContainsKey(checkStr))
                                                {
                                                    whichGeneConditionRoundItem[checkStr] = new HashSet<string>();
                                                }
                                                var levelIdStr = jsonName.Substring(6, jsonName.Length - 6);
                                                if (!whichGeneConditionRoundItem[checkStr].Contains(levelIdStr))
                                                {
                                                    whichGeneConditionRoundItem[checkStr].Add(levelIdStr);
                                                }
                                            }
                                        }
                                    }
                                    if (list[i].dict.ContainsKey("geneConditionKeep"))
                                    {
                                        LevelDataCollect.Append(" ").Append("geneConditionKeep");
                                    }
                                    if (list[i].dict.ContainsKey("generatorWeights"))
                                    {
                                        LevelDataCollect.Append(" ").Append("generatorWeights");
                                        var jsonList = list[i].dict["generatorWeights"]["elemWeights"].list;
                                        if (jsonList != null)
                                        {
                                            for (int j = 0; j < jsonList.Count; j++)
                                            {
                                                CustomJSONObject mTJSONObject = jsonList[j];
                                                ElementConfig key = new ElementConfig(mTJSONObject["element"].dict);
                                                string typeName = key.type.ToString();
                                                if (!wholeGeneratorWeightsDic.ContainsKey(typeName))
                                                {
                                                    wholeGeneratorWeightsDic[typeName] = new HashSet<string>();
                                                }
                                                var checkStr = string.Format("type:{0} color:{1} subType:{2} hp:{3}", key.type, key.color, key.subType, key.hp);
                                                if (!wholeGeneratorWeightsDic[typeName].Contains(checkStr))
                                                {
                                                    wholeGeneratorWeightsDic[typeName].Add(checkStr);
                                                }
                                                if (!whichGeneratorWeightsItem.ContainsKey(checkStr))
                                                {
                                                    whichGeneratorWeightsItem[checkStr] = new HashSet<string>();
                                                }
                                                var levelIdStr = jsonName.Substring(6, jsonName.Length - 6);
                                                if (!whichGeneratorWeightsItem[checkStr].Contains(levelIdStr))
                                                {
                                                    whichGeneratorWeightsItem[checkStr].Add(levelIdStr);
                                                }
                                            }
                                        }
                                    }
                                }

                            }
                            if (igmDic.dict.ContainsKey("weightedItems"))
                            {
                                LevelDataCollect.Append(" ").Append("weightedItems");
                                var jsonList = igmDic.dict["weightedItems"].list;
                                if (jsonList != null)
                                {
                                    for (int i = 0; i < jsonList.Count; i++)
                                    {
                                        CustomJSONObject mTJSONObject = jsonList[i];
                                        ElementConfig key = new ElementConfig(mTJSONObject["element"].dict);
                                        string typeName = key.type.ToString();
                                        if (!wholeUseWeightedItemDic.ContainsKey(typeName))
                                        {
                                            wholeUseWeightedItemDic[typeName] = new HashSet<string>();
                                        }
                                        var checkStr = string.Format("type:{0} color:{1} subType:{2} hp:{3}", key.type, key.color, key.subType, key.hp);
                                        if (!wholeUseWeightedItemDic[typeName].Contains(checkStr))
                                        {
                                            wholeUseWeightedItemDic[typeName].Add(checkStr);
                                        }
                                        if (!whichLevelUsedWeightedItem.ContainsKey(checkStr))
                                        {
                                            whichLevelUsedWeightedItem[checkStr] = new HashSet<string>();
                                        }
                                        var levelIdStr = jsonName.Substring(6, jsonName.Length - 6);

                                        if (!whichLevelUsedWeightedItem[checkStr].Contains(levelIdStr))
                                        {
                                            whichLevelUsedWeightedItem[checkStr].Add(levelIdStr);
                                        }

                                    }
                                }

                            }
                            if (igmDic.dict.ContainsKey("multiDrop"))
                            {
                                LevelDataCollect.Append(" ").Append("multiDrop");
                            }
                            if (igmDic.dict.ContainsKey("minRedCarpetOnBoard"))
                            {
                                LevelDataCollect.Append(" ").Append("minRedCarpetOnBoard");
                            }
                            if (igmDic.dict.ContainsKey("continuesRedCarpetRollsStop"))
                            {
                                LevelDataCollect.Append(" ").Append("continuesRedCarpetRollsStop");
                            }
                        }
                        if (seed.dict.ContainsKey("initUnmatch"))
                        {
                            LevelDataCollect.Append(" ").Append("initUnmatch");
                        }
                        if (seed.dict.ContainsKey("fishTargetFinder"))
                        {
                            LevelDataCollect.Append(" ").Append("fishTargetFinder");

                        }
                        if (seed.dict.ContainsKey("generator"))
                        {
                            LevelDataCollect.Append(" ").Append("generator");

                        }
                        if (seed.dict.ContainsKey("levelGenerator"))
                        {
                            LevelDataCollect.Append(" ").Append("levelGenerator");

                        }
                        if (seed.dict.ContainsKey("collR"))
                        {
                            LevelDataCollect.Append(" ").Append("collR");

                        }
                        if (seed.dict.ContainsKey("presetCollR"))
                        {
                            LevelDataCollect.Append(" ").Append("presetCollR");

                        }
                        LevelDataCollect.AppendLine();
                    }
                }
                levelDataCollectDic.Add(int.Parse(jsonName.Substring(6, jsonName.Length - 6)), LevelDataCollect);
            }
            sw.Write(jsonContent);
            sw.Close();
        }


        //var dicSort = from objDic in dic orderby objDic.Value descending select objDic
        var newSortDic = from kv in levelDataCollectDic orderby kv.Key ascending select kv;
        foreach (var kv in newSortDic)
        {
            elementCollect.Append(kv.Value);
        }

        elementCollect.Append("ProjectUsedAllElementTypes").Append(" = ");
        foreach (var type in wholeUseType)
        {
            elementCollect.Append(type.Key.ToString()).Append(" ");
        }
        elementCollect.AppendLine();

        elementCollect.Append("ProjectUsedAllCollectionTypes").Append(" = ");
        foreach (var type in wholeUseCollectionTypeDic)
        {
            elementCollect.Append(type.Key.ToString()).Append(" ");
        }
        elementCollect.AppendLine();

        elementCollect.Append("ProjectUsedAllAdditionTypes").Append(" = ");
        foreach (var type in wholeUseAdditionTypeDic)
        {
            elementCollect.Append(type.Key.ToString()).Append(" ");
        }
        elementCollect.AppendLine();

        elementCollect.Append("ProjectUsedAllObstacleTypes").Append(" = ");
        foreach (var type in wholeUseObstacleTypeDic)
        {
            elementCollect.Append(type.Key.ToString()).Append(" ");
        }
        elementCollect.AppendLine();

        elementCollect.Append("ProjectUsedAllWeightedItems").Append(" = ").AppendLine();
        foreach (var type in wholeUseWeightedItemDic)
        {
            elementCollect.Append("  ").Append(type.Key).AppendLine();
            foreach (var data in wholeUseWeightedItemDic[type.Key])
            {
                elementCollect.Append("      ").Append(data).Append(" ");
                if (whichLevelUsedWeightedItem.ContainsKey(data))
                {
                    foreach (var levelIDstr in whichLevelUsedWeightedItem[data])
                    {
                        elementCollect.Append(levelIDstr).Append(" ");
                    }
                }
                elementCollect.AppendLine();
            }
            elementCollect.AppendLine();
        }
        elementCollect.AppendLine();

        elementCollect.Append("ProjectGeneConditionPresetQueueItems").Append(" = ").AppendLine();
        foreach (var type in wholeGeneConditionPresetQueueDic)
        {
            elementCollect.Append("  ").Append(type.Key).AppendLine();
            foreach (var data in wholeGeneConditionPresetQueueDic[type.Key])
            {
                elementCollect.Append("     ").Append(data).Append(" ");
                if (whichGeneConditionPresetQueueItem.ContainsKey(data))
                {
                    foreach (var levelIDstr in whichGeneConditionPresetQueueItem[data])
                    {
                        elementCollect.Append(levelIDstr).Append(" ");
                    }
                }
                elementCollect.AppendLine();
            }
            elementCollect.AppendLine();
        }
        elementCollect.AppendLine();

        elementCollect.Append("ProjectGeneratorWeightsItems").Append(" = ").AppendLine();
        foreach (var type in wholeGeneratorWeightsDic)
        {
            elementCollect.Append("  ").Append(type.Key).AppendLine();
            foreach (var data in wholeGeneratorWeightsDic[type.Key])
            {
                elementCollect.Append("     ").Append(data).Append(" ");
                if (whichGeneratorWeightsItem.ContainsKey(data))
                {
                    foreach (var levelIDstr in whichGeneratorWeightsItem[data])
                    {
                        elementCollect.Append(levelIDstr).Append(" ");
                    }
                }
                elementCollect.AppendLine();
            }
            elementCollect.AppendLine();
        }
        elementCollect.AppendLine();

        elementCollect.Append("ProjectGeneConditionElimItems").Append(" = ").AppendLine();
        foreach (var type in wholeGeneConditionElimDic)
        {
            elementCollect.Append("  ").Append(type.Key).AppendLine();
            foreach (var data in wholeGeneConditionElimDic[type.Key])
            {
                elementCollect.Append("     ").Append(data).Append(" ");
                if (whichGeneConditionElimItem.ContainsKey(data))
                {
                    foreach (var levelIDstr in whichGeneConditionElimItem[data])
                    {
                        elementCollect.Append(levelIDstr).Append(" ");
                    }
                }
                elementCollect.AppendLine();
            }
            elementCollect.AppendLine();
        }
        elementCollect.AppendLine();

        elementCollect.Append("ProjectGeneConditionRoundItems").Append(" = ").AppendLine();
        foreach (var type in wholeGeneConditionRoundDic)
        {
            elementCollect.Append("  ").Append(type.Key).AppendLine();
            foreach (var data in wholeGeneConditionRoundDic[type.Key])
            {
                elementCollect.Append("     ").Append(data).Append(" ");
                if (whichGeneConditionRoundItem.ContainsKey(data))
                {
                    foreach (var levelIDstr in whichGeneConditionRoundItem[data])
                    {
                        elementCollect.Append(levelIDstr).Append(" ");
                    }
                }
                elementCollect.AppendLine();
            }
            elementCollect.AppendLine();
        }
        elementCollect.AppendLine();

        if (GenerateLevelAnalysisFile_)
        {
            if (Directory.Exists(LevelAnalysisFileSavePath_))
            {
                using (System.IO.TextWriter tw = new System.IO.StreamWriter(LevelAnalysisFileSavePath_ + "/LevelAnalysisData.txt"))
                {
                    tw.Write(elementCollect);
                    tw.Close();
                }
            }
        }
        UnityEditor.EditorUtility.ClearProgressBar();
    }

    [HorizontalGroup]
    [Button("重新生成ConfigSO配置文件", ButtonSizes.Gigantic)]
    public void GenerateNewConfigFiles()
    {
        AssetDatabase.Refresh();
        if (!System.IO.Directory.Exists(jsonConfigFileSavePath))
        {
            Directory.CreateDirectory(jsonConfigFileSavePath);
        }
        var di = new System.IO.DirectoryInfo(jsonConfigFileSavePath);
        var filesDIs = di.GetFiles();
        if (filesDIs.Length <= 0)
        {
            UnityEditor.EditorUtility.DisplayDialog("警告", "目标文件夹内容为空", "确定");
            return;
        }

        var index = 0;
        UnityEditor.EditorUtility.DisplayProgressBar("生成文件中...", "", index);


        ConfigSO configSO = AssetDatabase.LoadAssetAtPath<ConfigSO>(configSOPath);
        if (configSO == null)
        {
            configSO = ScriptableObject.CreateInstance<ConfigSO>();
            AssetDatabase.CreateAsset(configSO, configSOPath);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            configSO = AssetDatabase.LoadAssetAtPath<ConfigSO>(configSOPath);
        }
        configSO.configDict = new Dictionary<string, string>();
        foreach (var fileDI in filesDIs)
        {
            var filePath = fileDI.FullName;
            var fileName = fileDI.Name;
            var jsonName = fileName.Substring(0, fileName.LastIndexOf('.'));

            var sr = fileDI.OpenText();
            string jsonContent = sr.ReadToEnd();

            configSO.configDict.Add(jsonName, jsonContent);

            sr.Close();

            UnityEditor.EditorUtility.DisplayProgressBar("生成文件中...", jsonName, ((float)++index) / (float)filesDIs.Length);
        }

        EditorUtility.SetDirty(configSO);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        var setting = AssetDatabase.LoadAssetAtPath<AddressableAssetSettings>("Assets/AddressableAssetsData/AddressableAssetSettings.asset");
        var configSOGroup = setting.FindGroup("RemoteCore");
        var guid = AssetDatabase.AssetPathToGUID(configSOPath);
        var entry = setting.CreateOrMoveEntry(guid, configSOGroup);
        entry.SetLabel("preload", true);
        entry.SetAddress("ConfigSO", true);
        EditorUtility.SetDirty(configSOGroup);
        EditorUtility.SetDirty(setting);

        UnityEditor.EditorUtility.ClearProgressBar();
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }


}
