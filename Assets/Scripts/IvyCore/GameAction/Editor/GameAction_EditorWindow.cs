using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace IvyCore{
    public class GameAction_EditorWindow : OdinMenuEditorWindow
    {
        [MenuItem("IvyCore/GameActions管理器")]
        static void ShowGameAction_EditorWindow()
        {
            if(GameAction_EditorWindow.instance_==null)
            {
                var window = UnityEditor.EditorWindow.GetWindow<GameAction_EditorWindow>(false, "GameAction管理器", true);
                window.position = Sirenix.Utilities.Editor.GUIHelper.GetEditorWindowRect().AlignCenter(800, 600);
            }
        }

        public static GameAction_EditorWindow instance_ = null;
        public GameActions m_GameActions;
        int m_GameActionShowTypeValue = 0;
        string m_SerchString = "";
        string m_TargetDescription = "";
        public GameAction_EditorWindow()
        {
            instance_ = this;
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            IvyCoreEditorUtilities.CreateAndGetGameActionsDataInEditorMode();
            m_GameActions = GameActionManager.Instance.GameActionsData;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            instance_ = null;
        }

        void SaveData()
        {
            if (m_GameActions!=null)
                EditorUtility.SetDirty(m_GameActions);
            AssetDatabase.SaveAssets();
        }
        string[] GameActionShowTypeStrings = { "名称","描述"}; 
        protected override void OnGUI()
        {
            GUILayout.BeginVertical();
            GUILayout.BeginHorizontal();
            
            if (GUILayout.Button("添加", GUILayout.MaxWidth(50), GUILayout.MaxHeight(25)))
            {
                var window = UnityEditor.EditorWindow.GetWindow<GameActionCreateWizard>(false, "添加GameAction", true);
                window.position = Sirenix.Utilities.Editor.GUIHelper.GetEditorWindowRect().AlignCenter(400, 300);
                window.init();
                m_SerchString = string.Empty;
                Refresh();
            }

            if (GUILayout.Button("删除", GUILayout.MaxWidth(50), GUILayout.MaxHeight(25)))
            {
                if(MenuTree.Selection.Count>0)
                {
                    var seq = MenuTree.Selection[0].Value as ActionSequence;
                    if(m_GameActions!=null)
                    {
                        var findedGA = m_GameActions.m_Actions.Find((GameAction ga)=> ga.m_Action == seq);
                        if(findedGA!=null)
                        {
                            if(EditorUtility.DisplayDialog("警告",string.Format("GameAction 名称 = {0} 描述 = {1} 将被删除,是否确定?",findedGA.m_Name,findedGA.m_Description),"确定","取消"))
                            {
                                
                                GameActionManager.Instance.DeleteGameActionByName(findedGA.m_Name);
                                Refresh();
                                SaveData();
                            }
                        }
                    }
                }
                m_SerchString = string.Empty;
                Refresh();
            }
            if (GUILayout.Button("保存", GUILayout.MaxWidth(50), GUILayout.MaxHeight(25)))
            {
                SaveData();
            }

            if (GUILayout.Button("从选中项创建", GUILayout.MaxWidth(150), GUILayout.MaxHeight(25)))
            {
                if (MenuTree != null && MenuTree.Selection.Count == 1)
                {
                    var seq1 = MenuTree.Selection[0].Value as ActionSequence;
                    if (m_GameActions != null)
                    {
                        var findedGA1 = m_GameActions.m_Actions.Find((GameAction ga) => ga.m_Action == seq1);
                        if (findedGA1 != null)
                        {
                            var window1 = UnityEditor.EditorWindow.GetWindow<GameActionCreateWizard>(false, "添加GameAction", true);
                            window1.position = Sirenix.Utilities.Editor.GUIHelper.GetEditorWindowRect().AlignCenter(400, 300);
                            window1.init(findedGA1.m_Name, findedGA1.m_Description, findedGA1.m_Action);
                            m_SerchString = string.Empty;
                            Refresh();
                        }
                    }
                }
            }

            GUILayout.EndHorizontal();

            EditorGUI.BeginChangeCheck();
            m_GameActionShowTypeValue = EditorGUILayout.Popup("显示方式:", m_GameActionShowTypeValue, GameActionShowTypeStrings, GUILayout.MaxWidth(290), GUILayout.MaxHeight(25));
            if(EditorGUI.EndChangeCheck())
            {
                Refresh();
            }
            EditorGUI.BeginChangeCheck();
            m_SerchString=Sirenix.Utilities.Editor.SirenixEditorGUI.SearchField(GUILayoutUtility.GetRect(300,20, GUILayout.MaxWidth(300), GUILayout.MaxHeight(25)), m_SerchString);
            if (EditorGUI.EndChangeCheck())
            {
                Refresh();
            }
            GUILayout.EndVertical();
            base.OnGUI();
        }

        public void JumpToLastMenuItem(object obj)
        {
            this.TrySelectMenuItemWithObject(obj);
        }

        public void JumpToTargetDescriptionMenuItem(string description)
        {
            m_SerchString = string.Empty;
            m_TargetDescription = description;
            m_GameActionShowTypeValue = 1;
            Refresh();
        }

        public void RefreshMenuTree(OdinMenuTree tree,bool rebuild)
        {
            tree.MenuItems.Clear();
            ActionSequence targetJumpAction = null;
            if (m_GameActions != null)
            {
                foreach (var action in m_GameActions.m_Actions)
                {
                    switch(m_GameActionShowTypeValue)
                    {
                        case 0:
                            if(m_SerchString!=string.Empty)
                            {
                                if(action.m_Name.Contains(m_SerchString))
                                    tree.Add(action.m_Name, action.m_Action, Sirenix.Utilities.Editor.EditorIcons.Flag);
                            }
                            else
                                tree.Add(action.m_Name, action.m_Action, Sirenix.Utilities.Editor.EditorIcons.Flag);
                            break;
                        case 1:
                            if (m_SerchString != string.Empty)
                            {
                                if (action.m_Description.Contains(m_SerchString))
                                    tree.Add(action.m_Description, action.m_Action, Sirenix.Utilities.Editor.EditorIcons.Tag);
                            }
                            else
                            {
                                if(m_TargetDescription!=string.Empty&& m_TargetDescription.Equals(action.m_Description))
                                {
                                    targetJumpAction = action.m_Action;
                                }
                                tree.Add(action.m_Description, action.m_Action, Sirenix.Utilities.Editor.EditorIcons.Tag);
                            }
                            break;
                    }
                }
            }
            if(rebuild)
                this.ForceMenuTreeRebuild();
            m_TargetDescription = string.Empty;
            if(targetJumpAction!=null)
            {
                TrySelectMenuItemWithObject(targetJumpAction);
                Repaint();
            }
        }

        public void Refresh()
        {
            if(MenuTree!=null)
                RefreshMenuTree(MenuTree,true);
        }

        protected override OdinMenuTree BuildMenuTree()
        {
            OdinMenuTree tree = new OdinMenuTree(supportsMultiSelect: false);
            RefreshMenuTree(tree,false);
            return tree;
        }
    }

    public class GameActionCreateWizard : Sirenix.OdinInspector.Editor.OdinEditorWindow
    {
        [LabelText("注册名称")]
        public string registName_ = string.Empty;

        [LabelText("备注")]
        public string description_ = string.Empty;

        bool vaildData = false;

        [LabelText("错误信息"), GUIColor(1, 0, 0), HideIf("vaildData")]
        public string errorInfo = "";

        ActionSequence willCloneAction_ = null;

        public void init()
        {
        }

        public void init(string name,string des , ActionSequence ga)
        {
            if(ga!=null)
            {
                willCloneAction_ = ga;
                registName_ = name;
                description_ = des;
            }
        }

        [Button("添加"), EnableIf("vaildData")]
        void addData()
        {
            var gam = GameActionManager.Instance;
            if (!gam.IsActionNameNotRegisted(registName_))
            {
                ShowNotification(new GUIContent("已注册同名GameAction"));
                return;
            }

            if (!StringTools.IsStringOnlyContainEnglishNumber(registName_))
            {
                ShowNotification(new GUIContent("注册名称仅允许26个英文字母,数字和下划线"));
                return;
            }

            if (!gam.IsActionDescriptionNotRegisted(description_))
            {
                ShowNotification(new GUIContent("GameAction详细描述重复"));
                return;
            }

            var newGameAction = gam.AddGameAction(registName_,description_, willCloneAction_);
            if(GameAction_EditorWindow.instance_!=null)
            {
                GameAction_EditorWindow.instance_.Refresh();
                GameAction_EditorWindow.instance_.JumpToLastMenuItem(newGameAction.m_Action);
            }


            EditorCoroutineRunner.StartEditorCoroutine(CloseWindow());
        }

        IEnumerator CloseWindow()
        {
            yield return null;
            this.Close();
        }

        [Button("取消")]
        void cancelAddData()
        {
            EditorCoroutineRunner.StartEditorCoroutine(CloseWindow());
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }

        public void Update()
        {
            if (registName_ == string.Empty)
            {
                errorInfo = "注册名称为空";
                vaildData = false;
                return;
            }

            if (registName_ != null && registName_.Contains("/"))
            {
                errorInfo = "注册名称中不允许出现字符 / ";
                vaildData = false;
                return;
            }
            
            vaildData = true;
        }
    }
}