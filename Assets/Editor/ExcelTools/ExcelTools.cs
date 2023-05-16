using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;

public class ExcelTools : OdinEditorWindow
{
    private List<string> excelList = new List<string>();

    private static ExcelTools window;

    public static void ShowExcelTools()
    {
        if (window != null)
        {
            window.Close();
            window = null;
        }
        window = OdinEditorWindow.GetWindow<ExcelTools>();
        window.titleContent = new GUIContent("Excel转Json工具");
        window.Show();
    }

    protected override void OnGUI()
    {
        DrawExport();
        base.OnGUI();
    }

    Vector2 scrollPosition = Vector2.zero;
    /// <summary>
    /// 绘制插件界面输出项
    /// </summary>
    private void DrawExport()
    {
        if (excelList.Count < 1)
        {
            EditorGUILayout.LabelField("请选择所有需要转换的Excel");
        }
        else
        {
            EditorGUILayout.LabelField("下列excel将被转换为json");
            scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Height(500));
            GUILayout.BeginVertical();
            foreach (string excel in excelList)
            {
                GUILayout.BeginHorizontal();
                string str = Path.GetFileName(excel);
                GUILayout.Toggle(true, str);
                if (GUILayout.Button("删除", GUILayout.Width(60), GUILayout.Height(20)))
                {
                    excelList.Remove(excel);
                    return;
                }
                GUILayout.EndHorizontal();
            }
            GUILayout.EndVertical();
            GUILayout.EndScrollView();
        }
    }


    /// 转换Excel文件
    /// </summary>
    [HorizontalGroup]
    [GUIColor(0.95f, 1, 0.95f)]
    [Button("添加需要转换的Excel", ButtonSizes.Large)]
    public void LoadExcel()
    {
        DirectoryInfo info = new DirectoryInfo(Application.dataPath);
        string fileName = EditorUtility.OpenFilePanel("选择要转换的excel", info.Parent + "/Configs/ExcelConfig/", "xlsx");
        if (string.IsNullOrEmpty(fileName))
        {
            return;
        }
        excelList.Add(fileName);
    }

    [HorizontalGroup]
    [GUIColor(0.95f, 1, 0.95f)]
    [Button("添加ExcelConfig路径下所有的Excel", ButtonSizes.Large)]
    public void ConvertAllExcelToJson()
    {
        excelList.Clear();

        DirectoryInfo info = new DirectoryInfo(Application.dataPath);
        string rootPath = Path.Combine(info.Parent.FullName, "Configs/ExcelConfig");
        DirectoryInfo rootDir = new DirectoryInfo(rootPath);
        FileInfo[] fileInfos = rootDir.GetFiles();
        foreach (var item in fileInfos)
        {
            excelList.Add(item.FullName);
        }
    }

    [GUIColor(0.9f, 1, 0.9f)]
    [Button("将上述选中的Excel转换成json", ButtonSizes.Gigantic)]
    public void ConvertExcelToJson()
    {
        DirectoryInfo info = new DirectoryInfo(Application.dataPath);
        foreach (string assetsPath in excelList)
        {
            string excelPath = assetsPath;
            UnityEditor.EditorUtility.DisplayProgressBar("转换中...", Path.GetFileName(excelPath), 0);
            ExcelUtility excel = new ExcelUtility(excelPath);
            string outputFilePath = Path.Combine(info.Parent.FullName, "Configs", "Json", Path.GetFileNameWithoutExtension(excelPath) + ".json");
            excel.ConvertToJson(outputFilePath);
            UnityEditor.EditorUtility.DisplayProgressBar("转换完成", Path.GetFileName(excelPath), 1);
        }
        UnityEditor.EditorUtility.ClearProgressBar();
        AssetDatabase.Refresh();
        excelList.Clear();
        EditorUtility.DisplayDialog("excel转json", "转换成功", "ok");

        if (window != null)
        {
            window.Close();
            window = null;
        }
    }

    [GUIColor(1, 0.9f, 0.9f)]
    [Button("清除所有选择")]
    public void CleanAll()
    {
        excelList.Clear();
    }

}