using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace IvyCore.UI
{
    [CustomEditor(typeof(UI_CheckBox))]
    public class UICheckBoxEditor : Editor
    {
        string[] ShowTypeInfos = { "精灵展示", "Spine动画展示" };
        string[] skinName;
        string[] animationName;

        Dictionary<string, int> skinFindIndexByNameDic;
        Dictionary<int, string> skinFindNameByIndexDic;

        Dictionary<string, int> animationFindIndexByNameDic;
        Dictionary<int, string> animationFindNameByIndexDic;

        public void OnEnable()
        {
            
            var obj = target as UI_CheckBox;
            skinFindIndexByNameDic = new Dictionary<string, int>();
            skinFindNameByIndexDic = new Dictionary<int, string>();
            animationFindIndexByNameDic = new Dictionary<string, int>();
            animationFindNameByIndexDic = new Dictionary<int, string>();
            if(obj.checkBoxShowType== UI_CheckBox.CheckBoxShowType.eSpine)
            {
                if(obj.skeletonDataAsset!=null)
                {
                    InitSpineInfo(obj.skeletonDataAsset);
                }
            }
        }

        public void OnDisable()
        {
            skinName = null;
            animationName = null;
            skinFindIndexByNameDic.Clear();
            skinFindNameByIndexDic.Clear();
            animationFindIndexByNameDic.Clear();
            animationFindNameByIndexDic.Clear();
        }

        public override void OnInspectorGUI()
        {
            bool needRefresh = false;
            var obj = target as UI_CheckBox;
            bool isChecked = EditorGUILayout.Toggle("是否选中", obj.isChecked);
            if (obj.isChecked != isChecked)
            {
                obj.Set(isChecked, false, false);
                needRefresh = true;
            }

            var showType = (UI_CheckBox.CheckBoxShowType)EditorGUILayout.Popup("展示类型:", (int)obj.checkBoxShowType, ShowTypeInfos);
            if (showType != obj.checkBoxShowType)
            {
                obj.checkBoxShowType = showType;
                obj.GenerateByShowType();
            }
            switch (showType)
            {
                case UI_CheckBox.CheckBoxShowType.eSprite:
                    if (OnSpriteShowTypeInspectorGUI(obj))
                        needRefresh = true;
                    break;
                case UI_CheckBox.CheckBoxShowType.eSpine:
                    if (OnSpineShowTypeInspectorGUI(obj))
                        needRefresh = true;
                    break;
            }
            var group = EditorGUILayout.ObjectField("UICheckBox组", obj.group,typeof(UI_CheckBoxGroup),true) as UI_CheckBoxGroup;
            if(group!=obj.group)
            {
                obj.group = group;
            }
            obj.prototypeObjectSizeSynchronousChange = EditorGUILayout.Toggle("原型对象尺寸同步", obj.prototypeObjectSizeSynchronousChange);
            if (GUILayout.Button("同步RectTransForm区域"))
            {
                obj.SynchronousRectTransformToChild();
            }
            if (needRefresh)
            {
                obj.Set(obj.isChecked, false, false);
                EditorUtility.SetDirty(obj);
            }
        }
        static Vector3[] fourCorners = new Vector3[4];
        private void OnSceneGUI()
        {
            var obj = target as UI_CheckBox;
            {
                RectTransform rectTransform = obj.prototypeObjectTransform.GetComponent<RectTransform>();
                rectTransform.GetWorldCorners(fourCorners);
                Handles.color = Color.blue;
                for (int i = 0; i < 4; i++)
                    Handles.DrawLine(fourCorners[i], fourCorners[(i + 1) % 4]);
            }

            if (obj.group != null)
            {
                RectTransform rectTransform = obj.GetComponent<RectTransform>();
                rectTransform.GetWorldCorners(fourCorners);
                Vector3 thisPos = new Vector3(fourCorners[0].x + (fourCorners[2].x - fourCorners[0].x) * 0.5f, fourCorners[0].y + (fourCorners[2].y - fourCorners[0].y) * 0.5f, fourCorners[0].z + (fourCorners[2].z - fourCorners[0].z) * 0.5f);
                RectTransform rectTransform1 = obj.group.GetComponent<RectTransform>();
                rectTransform1.GetWorldCorners(fourCorners);
                Vector3 thatPos = new Vector3(fourCorners[0].x + (fourCorners[2].x - fourCorners[0].x) * 0.5f, fourCorners[0].y + (fourCorners[2].y - fourCorners[0].y) * 0.5f, fourCorners[0].z + (fourCorners[2].z - fourCorners[0].z) * 0.5f);

                Handles.color = Color.green;
                Handles.DrawLine(thisPos, thatPos);
            }
        }

        bool OnSpriteShowTypeInspectorGUI(UI_CheckBox obj)
        {
            bool needRefresh = false;
            Sprite s1 = EditorGUILayout.ObjectField("选中状态精灵", obj.checkedSprite, typeof(Sprite),false) as Sprite;
            if (s1 != obj.checkedSprite)
            {
                obj.checkedSprite = s1;
                needRefresh = true;
            }
            Sprite s2 = EditorGUILayout.ObjectField("未选中状态精灵", obj.unCheckedSprite, typeof(Sprite), false) as Sprite;
            if (s2 != obj.unCheckedSprite)
            {
                obj.unCheckedSprite = s2;
                needRefresh = true;
            }
            
            var UseAdvancedOptions = EditorGUILayout.BeginToggleGroup("进阶设置", obj.useSpriteAdvanceOptions);
            if(UseAdvancedOptions!= obj.useSpriteAdvanceOptions)
            {
                obj.useSpriteAdvanceOptions = UseAdvancedOptions;
            }
            EditorGUI.indentLevel++;
            if(UseAdvancedOptions)
            {
                if (OnSpriteGUI(obj.checkSpriteOptions, 0))
                {
                    needRefresh = true;
                }
                if (OnSpriteGUI(obj.unCheckSpriteOptions, 1))
                {
                    needRefresh = true;
                }
            }
            EditorGUI.indentLevel--;
            EditorGUILayout.EndToggleGroup();
            return needRefresh;
        }

        public bool OnSpriteGUI(ImageOptions iao, int type)
        {
            switch (type)
            {
                case 0:
                    EditorGUILayout.LabelField("选中状态:");
                    break;
                case 1:
                    EditorGUILayout.LabelField("未选中状态:");
                    break;
            }

            bool needRefresh = false;
            {
                EditorGUI.indentLevel++;
                var SpriteColor = EditorGUILayout.ColorField("颜色", iao.m_Color);
                if (iao.m_Color != SpriteColor)
                {
                    iao.m_Color = SpriteColor;
                    needRefresh = true;
                }
                var SpriteMaterial = EditorGUILayout.ObjectField("材质", iao.m_Material, typeof(Material), false) as Material;
                if (SpriteMaterial != iao.m_Material)
                {
                    iao.m_Material = SpriteMaterial;
                    needRefresh = true;
                }
                var it = (Image.Type)EditorGUILayout.EnumPopup("类型", iao.m_ImageType);
                if (iao.m_ImageType != it)
                {
                    iao.m_ImageType = it;
                    needRefresh = true;
                }
                switch (it)
                {
                    case Image.Type.Filled:
                        {
                            EditorGUI.indentLevel++;
                            var fillMethod = (Image.FillMethod)EditorGUILayout.EnumPopup("填充方式", iao.m_FillMethod);
                            if (fillMethod != iao.m_FillMethod)
                            {
                                iao.m_FillMethod = fillMethod;
                                iao.m_FillOrigin = 0;
                                needRefresh = true;
                            }
                            switch (iao.m_FillMethod)
                            {
                                case Image.FillMethod.Horizontal:
                                    {
                                        var fillOrigin = (int)(object)EditorGUILayout.EnumPopup("填充起点", (Image.OriginHorizontal)iao.m_FillOrigin);
                                        if (fillOrigin != iao.m_FillOrigin)
                                        {
                                            iao.m_FillOrigin = fillOrigin;
                                            needRefresh = true;
                                        }
                                    }
                                    break;
                                case Image.FillMethod.Vertical:
                                    {
                                        var fillOrigin = (int)(object)EditorGUILayout.EnumPopup("填充起点", (Image.OriginVertical)iao.m_FillOrigin);
                                        if (fillOrigin != iao.m_FillOrigin)
                                        {
                                            iao.m_FillOrigin = fillOrigin;
                                            needRefresh = true;
                                        }
                                    }
                                    break;
                                case Image.FillMethod.Radial90:
                                    {
                                        var fillOrigin = (int)(object)EditorGUILayout.EnumPopup("填充起点", (Image.Origin90)iao.m_FillOrigin);
                                        if (fillOrigin != iao.m_FillOrigin)
                                        {
                                            iao.m_FillOrigin = fillOrigin;
                                            needRefresh = true;
                                        }
                                    }
                                    break;
                                case Image.FillMethod.Radial180:
                                    {
                                        var fillOrigin = (int)(object)EditorGUILayout.EnumPopup("填充起点", (Image.Origin180)iao.m_FillOrigin);
                                        if (fillOrigin != iao.m_FillOrigin)
                                        {
                                            iao.m_FillOrigin = fillOrigin;
                                            needRefresh = true;
                                        }
                                    }
                                    break;
                                case Image.FillMethod.Radial360:
                                    {
                                        var fillOrigin = (int)(object)EditorGUILayout.EnumPopup("填充起点", (Image.Origin360)iao.m_FillOrigin);
                                        if (fillOrigin != iao.m_FillOrigin)
                                        {
                                            iao.m_FillOrigin = fillOrigin;
                                            needRefresh = true;
                                        }
                                    }
                                    break;
                            }
                            var fillAmount = EditorGUILayout.FloatField("填充数值", iao.m_FillAmount);
                            if (iao.m_FillAmount != fillAmount)
                            {
                                iao.m_FillAmount = fillAmount;
                                needRefresh = true;
                            }
                            if (fillMethod != Image.FillMethod.Horizontal && fillMethod != Image.FillMethod.Vertical)
                            {
                                var clockWise = EditorGUILayout.Toggle("顺时钟填充", iao.m_Clockwise);
                                if (clockWise != iao.m_Clockwise)
                                {
                                    iao.m_Clockwise = clockWise;
                                    needRefresh = true;
                                }
                            }
                            var SpritePreserveAspect = EditorGUILayout.Toggle("保持长宽比", iao.m_PreserveAspect);
                            if (SpritePreserveAspect != iao.m_PreserveAspect)
                            {
                                iao.m_PreserveAspect = SpritePreserveAspect;
                                needRefresh = true;
                            }
                            EditorGUI.indentLevel--;
                        }
                        break;
                    case Image.Type.Simple:
                        {
                            EditorGUI.indentLevel++;
                            var SpritePreserveAspect = EditorGUILayout.Toggle("保持长宽比", iao.m_PreserveAspect);
                            if (SpritePreserveAspect != iao.m_PreserveAspect)
                            {
                                iao.m_PreserveAspect = SpritePreserveAspect;
                                needRefresh = true;
                            }
                            EditorGUI.indentLevel--;
                        }
                        break;
                    case Image.Type.Sliced:
                    case Image.Type.Tiled:
                    default:
                        break;
                }

                EditorGUI.indentLevel--;
            }
            return needRefresh;
        }

        void InitSpineInfo(Spine.Unity.SkeletonDataAsset sda)
        {
            skinFindIndexByNameDic.Clear();
            skinFindNameByIndexDic.Clear();
            animationFindIndexByNameDic.Clear();
            animationFindNameByIndexDic.Clear();
            skinName = null;
            animationName = null;
            if (sda == null)
                return;
            var skeletonData = sda.GetSkeletonData(false);
            skinName = new string[skeletonData.Skins.Count];
            int index = 0;
            foreach (var skin in skeletonData.Skins)
            {
                skinName[index] = skin.Name;
                skinFindIndexByNameDic[skin.Name] = index;
                skinFindNameByIndexDic[index] = skin.Name;
                ++index;
            }
            animationName = new string[skeletonData.Animations.Count];
            index = 0;
            foreach (var ani in skeletonData.Animations)
            {
                animationName[index] = ani.Name;
                animationFindIndexByNameDic[ani.Name] = index;
                animationFindNameByIndexDic[index] = ani.Name;
                ++index;
            }
        }
        
        bool OnSpineShowTypeInspectorGUI(UI_CheckBox obj)
        {
            bool needRefresh = false;
            var skeletonAssetData = EditorGUILayout.ObjectField("SkeletonDataAsset:", obj.skeletonDataAsset,typeof(Spine.Unity.SkeletonDataAsset), false) as Spine.Unity.SkeletonDataAsset;
            if(skeletonAssetData!= obj.skeletonDataAsset)
            {
                obj.skeletonDataAsset = skeletonAssetData;
                if(obj.skeletonDataAsset.atlasAssets!=null&&obj.skeletonDataAsset.atlasAssets.Length>0&& obj.skeletonDataAsset.atlasAssets[0].Materials!=null&& obj.skeletonDataAsset.atlasAssets[0].MaterialCount>0)
                    obj.spineMaterial = (Material)obj.skeletonDataAsset.atlasAssets[0].Materials;
                else
                {
                    EditorUtility.DisplayDialog("错误","Spine资源存在问题,未查找到材质","确定");
                }
                InitSpineInfo(obj.skeletonDataAsset);
            }
            if(skeletonAssetData!=null)
            {
                //EditorGUILayout.ColorField("选中状态颜色:",obj.spineColor);
                var mer = EditorGUILayout.ObjectField("材质:", obj.spineMaterial, typeof(Material), false) as Material;
                if(mer!=obj.spineMaterial)
                {
                    obj.spineMaterial = mer;
                    needRefresh = true;
                }
                EditorGUILayout.Space();
                ++EditorGUI.indentLevel;
                var checkSkinIndex = skinFindIndexByNameDic.ContainsKey(obj.spineCheckedSkinName) ? skinFindIndexByNameDic[obj.spineCheckedSkinName] : -1;
                var newCheckSkinIndex = EditorGUILayout.Popup("选中状态Skin:", checkSkinIndex==-1?0:checkSkinIndex, skinName);
                if(checkSkinIndex!= newCheckSkinIndex)
                {
                    obj.spineCheckedSkinName = skinFindNameByIndexDic[newCheckSkinIndex];
                    needRefresh = true;
                }
                var checkAnimationIndex = animationFindIndexByNameDic.ContainsKey(obj.spineCheckedAnimationName) ? animationFindIndexByNameDic[obj.spineCheckedAnimationName] : -1;
                var newCheckAnimationIndex = EditorGUILayout.Popup("选中状态Animation:", checkAnimationIndex==-1?0: checkAnimationIndex, animationName);
                if(checkAnimationIndex!= newCheckAnimationIndex)
                {
                    obj.spineCheckedAnimationName = animationFindNameByIndexDic[newCheckAnimationIndex];
                    needRefresh = true;
                }
                EditorGUILayout.Space();
                var uncheckSkinIndex = skinFindIndexByNameDic.ContainsKey(obj.spineUnCheckedSkinName) ? skinFindIndexByNameDic[obj.spineUnCheckedSkinName] : -1;
                var newunCheckSkinIndex = EditorGUILayout.Popup("未选中状态Skin:", uncheckSkinIndex==-1?0: uncheckSkinIndex, skinName);
                if (uncheckSkinIndex != newunCheckSkinIndex)
                {
                    obj.spineUnCheckedSkinName = skinFindNameByIndexDic[newunCheckSkinIndex];
                    needRefresh = true;
                }
                var uncheckAnimationIndex = animationFindIndexByNameDic.ContainsKey(obj.spineUnCheckedAnimationName) ? animationFindIndexByNameDic[obj.spineUnCheckedAnimationName] : -1;
                var newunCheckAnimationIndex = EditorGUILayout.Popup("未选中状态Animation:", uncheckAnimationIndex==-1?0: uncheckAnimationIndex, animationName);
                if (uncheckAnimationIndex != newunCheckAnimationIndex)
                {
                    obj.spineUnCheckedAnimationName = animationFindNameByIndexDic[newunCheckAnimationIndex];
                    needRefresh = true;
                }
                --EditorGUI.indentLevel;
                EditorGUILayout.Space();
                bool useSpineAdvanceOptions = EditorGUILayout.BeginToggleGroup("进阶设置:",obj.useSpineAdvanceOptions);
                if(useSpineAdvanceOptions!=obj.useSpineAdvanceOptions)
                {
                    obj.useSpineAdvanceOptions = useSpineAdvanceOptions;
                }
                if(obj.useSpineAdvanceOptions)
                {
                    if (OnSpineGUI(obj.checkedSpineOptions, 0))
                        needRefresh = true;
                    if (OnSpineGUI(obj.unCheckedSpineOptions, 1))
                        needRefresh = true;
                }
                EditorGUILayout.EndToggleGroup();
            }
            return needRefresh;
        }
        SerializedProperty test;
        public bool OnSpineGUI(SpineOptions so, int type)
        {
            switch (type)
            {
                case 0:
                    EditorGUILayout.LabelField("选中状态:");
                    break;
                case 1:
                    EditorGUILayout.LabelField("未选中状态:");
                    break;
            }
            ++EditorGUI.indentLevel;
            bool needRefresh = false;
            var color = EditorGUILayout.ColorField("颜色:", so.m_Color);
            if(color!=so.m_Color)
            {
                so.m_Color = color;
                needRefresh = true;
            }
            var loop = EditorGUILayout.Toggle("循环播放:", so.m_Loop);
            if(loop!=so.m_Loop)
            {
                so.m_Loop = loop;
                needRefresh = true;
            }
            var timeScale = EditorGUILayout.FloatField("播放速率:", so.m_TimeScale);
            if(timeScale!=so.m_TimeScale)
            {
                so.m_TimeScale = timeScale;
                needRefresh = true;
            }
            var unScaleTime = EditorGUILayout.Toggle("无视全局播放速率:", so.m_UnScaleTime);
            if(unScaleTime!=so.m_UnScaleTime)
            {
                so.m_UnScaleTime = unScaleTime;
                needRefresh = true;
            }
            var freeze = EditorGUILayout.Toggle("冻结",so.m_Freeze);
            if(freeze!=so.m_Freeze)
            {
                so.m_Freeze = freeze;
                needRefresh = true;
            }
            --EditorGUI.indentLevel;
            return needRefresh;
        }
    }
}