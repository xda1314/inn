using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace IvyCore
{
    public class UI_TutorAtomDataCreateWizard : Sirenix.OdinInspector.Editor.OdinEditorWindow
    {
        public static void ShowAddTutorAtomDataWizard(UI_TutorData tutorData, UITutor_EditorWindows tutorEditorWindow)
        {
            var window = UnityEditor.EditorWindow.GetWindow<UI_TutorAtomDataCreateWizard>(false, "添加数据", true);
            window.position = Sirenix.Utilities.Editor.GUIHelper.GetEditorWindowRect().AlignCenter(400, 300);
            window.init(tutorData, tutorEditorWindow);
        }
        UITutor_EditorWindows tutorEditorWindow_;
        UI_TutorData data_;
        bool vaildData = false;

        [LabelText("注册名称")]
        public string registName_ = string.Empty;

        [LabelText("备注"), MultiLineProperty]
        public string description_ = string.Empty;

        [LabelText("错误信息"), GUIColor(1, 0, 0), HideIf("vaildData")]
        public string errorInfo = "";

        public void init(UI_TutorData data, UITutor_EditorWindows w)
        {
            data_ = data;
            tutorEditorWindow_ = w;
            if (vaildData) { }
        }

        bool willClose_ = false;
        public void Update()
        {
            if (willClose_)
            {
                this.Close();
                return;
            }
            if (registName_ == string.Empty)
            {
                errorInfo = "教学名称为空";
                vaildData = false;
                return;
            }
            if (registName_ != null && registName_.Contains("/"))
            {
                errorInfo = "教学名称中不允许出现字符 / ";
                vaildData = false;
                return;
            }
            if (data_ == null)
            {
                errorInfo = "注册对象为空";
                vaildData = false;
                return;
            }
            if (data_.IsDataNameExist(registName_))
            {
                errorInfo = "已存在同名教学";
                vaildData = false;
                return;
            }
            vaildData = true;
        }

        [Button("添加"), EnableIf("vaildData")]
        void AddData()
        {
            if (data_.IsDataNameExist(registName_))
            {
                ShowNotification(new GUIContent("已存在同名教学"));
                return;
            }

            if (!StringTools.IsStringOnlyContainEnglishNumber(registName_))
            {
                ShowNotification(new GUIContent("教学名称仅允许26个英文字母,数字和下划线"));
                return;
            }

            data_.AddTutorAtomData(registName_, description_);
            tutorEditorWindow_.RefreshMenuTree();

            willClose_ = true;
        }

        [Button("取消")]
        void CancelAddData()
        {
            willClose_ = true;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }
    }

    public class UI_TutorAtomDataRenameWizard : Sirenix.OdinInspector.Editor.OdinEditorWindow
    {
        public static void ShowRenameTutorAtomDataWizard(UI_TutorAtomData renameData, UI_TutorData tutorData, UITutor_EditorWindows tutorEditorWindow)
        {
            var window = UnityEditor.EditorWindow.GetWindow<UI_TutorAtomDataRenameWizard>(false, "重命名", true);
            window.position = Sirenix.Utilities.Editor.GUIHelper.GetEditorWindowRect().AlignCenter(400, 300);
            window.init(renameData, tutorData, tutorEditorWindow);
        }
        UI_TutorAtomData renameData_;
        UITutor_EditorWindows tutorEditorWindow_;
        UI_TutorData data_;
        string srcName_;
        bool vaildData = false;

        [LabelText("注册名称")]
        public string registName_ = string.Empty;

        [LabelText("备注"), MultiLineProperty]
        public string description_ = string.Empty;

        [LabelText("错误信息"), GUIColor(1, 0, 0), HideIf("vaildData")]
        public string errorInfo = "";

        public void init(UI_TutorAtomData renameData, UI_TutorData data, UITutor_EditorWindows w)
        {
            renameData_ = renameData;
            registName_ = renameData_.Name;
            srcName_ = renameData_.Name;
            description_ = renameData_.Description;
            data_ = data;
            tutorEditorWindow_ = w;
            if (vaildData) { }
        }

        bool willClose_ = false;
        public void Update()
        {
            if (willClose_)
            {
                this.Close();
                return;
            }
            if (registName_ == string.Empty)
            {
                errorInfo = "教学名称为空";
                vaildData = false;
                return;
            }
            if (registName_ != null && registName_.Contains("/"))
            {
                errorInfo = "教学名称中不允许出现字符 / ";
                vaildData = false;
                return;
            }
            if (data_ == null)
            {
                errorInfo = "注册对象为空";
                vaildData = false;
                return;
            }
            if (data_.IsDataNameExist(registName_))
            {
                errorInfo = "已存在同名教学";
                vaildData = false;
                return;
            }
            vaildData = true;
        }

        [Button("添加"), EnableIf("vaildData")]
        void AddData()
        {
            if (data_.IsDataNameExist(registName_))
            {
                ShowNotification(new GUIContent("已存在同名教学"));
                return;
            }

            if (!StringTools.IsStringOnlyContainEnglishNumber(registName_))
            {
                ShowNotification(new GUIContent("教学名称仅允许26个英文字母,数字和下划线"));
                return;
            }

            data_.RenameTutorAtomData(srcName_, registName_, description_);
            tutorEditorWindow_.RefreshMenuTree();

            willClose_ = true;
        }

        [Button("取消")]
        void CancelAddData()
        {
            willClose_ = true;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }
    }

    public class UI_TutorAtomDataCloneWizard : Sirenix.OdinInspector.Editor.OdinEditorWindow
    {
        public static void ShowCloneTutorAtomDataWizard(UI_TutorAtomData renameData, UI_TutorData tutorData, UITutor_EditorWindows tutorEditorWindow)
        {
            var window = UnityEditor.EditorWindow.GetWindow<UI_TutorAtomDataCloneWizard>(false, "克隆", true);
            window.position = Sirenix.Utilities.Editor.GUIHelper.GetEditorWindowRect().AlignCenter(400, 300);
            window.init(renameData, tutorData, tutorEditorWindow);
        }
        UI_TutorAtomData renameData_;
        UITutor_EditorWindows tutorEditorWindow_;
        UI_TutorData data_;
        string srcName_;
        bool vaildData = false;

        [LabelText("注册名称")]
        public string registName_ = string.Empty;

        [LabelText("备注"), MultiLineProperty]
        public string description_ = string.Empty;

        [LabelText("错误信息"), GUIColor(1, 0, 0), HideIf("vaildData")]
        public string errorInfo = "";

        public void init(UI_TutorAtomData renameData, UI_TutorData data, UITutor_EditorWindows w)
        {
            renameData_ = renameData;
            registName_ = renameData_.Name;
            srcName_ = renameData_.Name;
            description_ = renameData_.Description;
            data_ = data;
            tutorEditorWindow_ = w;
            if (vaildData) { }
        }

        bool willClose_ = false;
        public void Update()
        {
            if (willClose_)
            {
                this.Close();
                return;
            }
            if (registName_ == string.Empty)
            {
                errorInfo = "教学名称为空";
                vaildData = false;
                return;
            }
            if (registName_ != null && registName_.Contains("/"))
            {
                errorInfo = "教学名称中不允许出现字符 / ";
                vaildData = false;
                return;
            }
            if (data_ == null)
            {
                errorInfo = "注册对象为空";
                vaildData = false;
                return;
            }
            if (data_.IsDataNameExist(registName_))
            {
                errorInfo = "已存在同名教学";
                vaildData = false;
                return;
            }
            vaildData = true;
        }

        [Button("添加"), EnableIf("vaildData")]
        void AddData()
        {
            if (data_.IsDataNameExist(registName_))
            {
                ShowNotification(new GUIContent("已存在同名教学"));
                return;
            }

            if (!StringTools.IsStringOnlyContainEnglishNumber(registName_))
            {
                ShowNotification(new GUIContent("教学名称仅允许26个英文字母,数字和下划线"));
                return;
            }

            data_.CloneAndAddTutorAtomDataByName(srcName_, registName_, description_);
            tutorEditorWindow_.RefreshMenuTree();

            willClose_ = true;
        }

        [Button("取消")]
        void CancelAddData()
        {
            willClose_ = true;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }
    }

    public partial class UITutor_EditorWindows : OdinMenuEditorWindow
    {
        UI_TutorData UI_TD_;
        public static UITutor_EditorWindows instance_ = null;
        [MenuItem("IvyCore/UI教学编辑器")]
        static void ShowGameAction_EditorWindow()
        {
            if (UITutor_EditorWindows.instance_ == null)
            {
                var window = UnityEditor.EditorWindow.GetWindow<UITutor_EditorWindows>(false, "UI教学编辑器", true);
                instance_ = window;
                window.position = Sirenix.Utilities.Editor.GUIHelper.GetEditorWindowRect().AlignCenter(900, 600);
            }
        }
        GUIStyle gs;
        Color globleColor;
        Color globleContentColor;
        Color globleBackColor;
        UITutor_EditorWindows()
        {
            this.DrawMenuSearchBar = true;

        }

        protected override void OnEnable()
        {
            base.OnEnable();
            globleColor = GUI.color;
            globleContentColor = GUI.contentColor;
            globleBackColor = GUI.backgroundColor;
            gs = new GUIStyle(EditorGUIUtility.GetBuiltinSkin(EditorSkin.Inspector).button);
            gs.normal.textColor = Color.green;
        }

        void SaveData()
        {
            if (UI_TD_ != null)
                EditorUtility.SetDirty(UI_TD_);
            AssetDatabase.SaveAssets();
        }

        protected override void OnDestroy()
        {
            SaveData();
            instance_ = null;
            base.OnDestroy();
        }
        protected override void DrawMenu()
        {
            GUI.color = globleColor;
            GUI.contentColor = globleContentColor;
            GUI.backgroundColor = globleBackColor;
            if (UI_TD_ != null)
            {
                GUILayout.BeginVertical();
                GUILayout.Space(3);
                GUILayout.BeginHorizontal();
                //GUIHelper.PushColor(Color.green);
                GUI.backgroundColor = Color.gray;
                if (GUILayout.Button(EditorIcons.Plus.Raw, gs,/*GUILayout.MaxWidth(100),*/ GUILayout.MaxHeight(25)))
                {
                    UI_TutorAtomDataCreateWizard.ShowAddTutorAtomDataWizard(UI_TD_, this);
                }
                GUI.backgroundColor = globleBackColor;
                //GUIHelper.PopColor();
                if (MenuTree.Selection != null && MenuTree.Selection.Count > 0)
                {
                    GUI.backgroundColor = Color.gray;
                    if (GUILayout.Button(EditorIcons.Minus.Raw, /*GUILayout.MaxWidth(100),*/ GUILayout.MaxHeight(25)))
                    {
                        if (EditorUtility.DisplayDialog("提示", "是否删除当前选择教学数据?", "确定", "取消"))
                        {
                            UI_TD_.DeleteTutorData(MenuTree.Selection[0].Name);
                            RefreshMenuTree();
                            GUI.backgroundColor = globleBackColor;
                            return;
                        }
                    }
                    GUI.backgroundColor = globleBackColor;
                }
                //GUI.backgroundColor = Color.gray;

                if (GUILayout.Button(UnityEditor.EditorGUIUtility.FindTexture("SaveActive"), /*GUILayout.MaxWidth(50),*/ GUILayout.MaxHeight(25)))
                {
                    SaveData();
                }
                //GUI.backgroundColor = globleBackColor;
                bool testButtonCanUse = Application.isPlaying && MenuTree.Selection != null && !UI_TutorManager.Instance.IsTutoring();

                EditorGUI.BeginDisabledGroup(!testButtonCanUse);
                {
                    if (testButtonCanUse)
                        GUIHelper.PushColor(Color.green);
                    if (GUILayout.Button("测试教学", GUILayout.MaxHeight(25)))
                    {
                        var name = MenuTree.Selection[0].Name;
                        UI_TutorManager.Instance.RunTutorWithName(name);
                    }
                    if (testButtonCanUse)
                        GUIHelper.PopColor();
                }
                EditorGUI.EndDisabledGroup();
                var isTutoring = Application.isPlaying && UI_TutorManager.Instance.IsTutoring();
                EditorGUI.BeginDisabledGroup(!isTutoring);
                {
                    if (isTutoring)
                        GUIHelper.PushColor(Color.green);
                    if (GUILayout.Button("强制停止教学", GUILayout.MaxHeight(26)))
                    {
                        UI_TutorManager.Instance.ForceEndTutor();
                    }
                    if (isTutoring)
                        GUIHelper.PopColor();
                }
                EditorGUI.EndDisabledGroup();
                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal();
                if (MenuTree.Selection.Count > 0)
                {
                    if (GUILayout.Button("重命名"))
                    {
                        UI_TutorAtomDataRenameWizard.ShowRenameTutorAtomDataWizard(MenuTree.Selection[0].Value as UI_TutorAtomData, UI_TD_, this);
                    }
                    if (GUILayout.Button("复制"))
                    {
                        UI_TutorAtomDataCloneWizard.ShowCloneTutorAtomDataWizard(MenuTree.Selection[0].Value as UI_TutorAtomData, UI_TD_, this);
                    }
                }
                GUILayout.EndHorizontal();
                GUILayout.EndVertical();
            }
            base.DrawMenu();
        }

        private void Update()
        {
            if (Application.isPlaying)
            {
                if (UI_TutorManager.Instance.IsTutoring())
                {
                    this.Repaint();
                }
            }
        }
        protected override void OnGUI()
        {
            GUILayout.BeginHorizontal();
            base.OnGUI();
            GUILayout.BeginVertical(GUILayout.MaxWidth(30));
            GUILayout.Space(5);
            bool AddButtonCanUse = MenuTree.Selection != null;
            EditorGUI.BeginDisabledGroup(!AddButtonCanUse);
            GUI.backgroundColor = Color.gray;
            if (GUILayout.Button(EditorIcons.Plus.Raw))
            {
                (MenuTree.Selection[0].Value as UI_TutorAtomData).ShowAddMenu();
            }
            GUI.backgroundColor = globleBackColor;
            EditorGUI.EndDisabledGroup();
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
        }

        public void RefreshMenuTree()
        {
            if (UI_TD_ != null && MenuTree != null)
            {
                MenuTree.Selection.Clear();
                MenuTree.MenuItems.Clear();
                var itr = UI_TD_.dataList_.GetEnumerator();
                while (itr.MoveNext())
                {
                    MenuTree.Add(itr.Current.Name, itr.Current);
                }
                if (MenuTree.MenuItems.Count > 0)
                    MenuTree.Selection.Add(MenuTree.MenuItems[0]);
            }
        }

        protected override OdinMenuTree BuildMenuTree()
        {
            ResizableMenuWidth = true;
            DrawMenuSearchBar = true;
            UI_TD_ = CreateAndGetTutorData();
            OdinMenuTree omt = new OdinMenuTree(supportsMultiSelect: false);
            omt.Config.DrawSearchToolbar = true;
            var itr = UI_TD_.dataList_.GetEnumerator();
            while (itr.MoveNext())
            {
                if (!string.IsNullOrEmpty(itr.Current.Name))
                    omt.Add(itr.Current.Name, itr.Current);
            }
            return omt;
        }

        public static UI_TutorData CreateAndGetTutorData()
        {
            var filePathSB = new StringBuilder();
            filePathSB.Append(Application.dataPath).Append("/").Append(UI_TutorData.DataFilePath).Append("/").Append(UI_TutorData.DataFileName);
            if (!File.Exists(filePathSB.ToString()))
            {
                ScriptableObjectUtility.CreateAsset<UI_TutorData>(UI_TutorData.DataFileName, "Assets/" + UI_TutorData.DataFilePath);
            }
            var data = AssetDatabase.LoadAssetAtPath<UI_TutorData>("Assets/" + UI_TutorData.DataFilePath + "/" + UI_TutorData.DataFileName);
            return data;
        }
    }
}
