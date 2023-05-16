using UnityEngine;
using System.Collections.Generic;
using Excel;
using System.Data;
using System.IO;
using Newtonsoft.Json;
using System.Reflection;
using System;

public class ExcelUtility
{
    /// <summary>
    /// 表格数据集合
    /// </summary>
    private DataSet mResultSet;

    /// <summary>
    /// 构造函数
    /// </summary>
    public ExcelUtility(string excelFile)
    {
        using (FileStream mStream = File.Open(excelFile, FileMode.Open, FileAccess.Read))
        {
            IExcelDataReader mExcelReader = ExcelReaderFactory.CreateOpenXmlReader(mStream);
            mResultSet = mExcelReader.AsDataSet();
        }
    }

    /// <summary>
    /// 转换为实体类列表
    /// </summary>
    public List<T> ConvertToList<T>()
    {
        //判断Excel文件中是否存在数据表
        if (mResultSet.Tables.Count < 1)
            return null;
        //默认读取第一个数据表
        DataTable mSheet = mResultSet.Tables[0];

        //判断数据表内是否存在数据
        if (mSheet.Rows.Count < 1)
            return null;

        //读取数据表行数和列数
        int rowCount = mSheet.Rows.Count;
        int colCount = mSheet.Columns.Count;

        //准备一个列表以保存全部数据
        List<T> list = new List<T>();

        //读取数据
        for (int i = 1; i < rowCount; i++)
        {
            //创建实例
            Type t = typeof(T);
            ConstructorInfo ct = t.GetConstructor(System.Type.EmptyTypes);
            T target = (T)ct.Invoke(null);
            for (int j = 0; j < colCount; j++)
            {
                //读取第1行数据作为表头字段
                string field = mSheet.Rows[0][j].ToString();
                object value = mSheet.Rows[i][j];
                //设置属性值
                SetTargetProperty(target, field, value);
            }

            //添加至列表
            list.Add(target);
        }

        return list;
    }

    /// <summary>
    /// 转换为Json
    /// </summary>
    /// <param name="Header">表头行数</param>
    public void ConvertToJson(string JsonPath)
    {
        //判断Excel文件中是否存在数据表
        if (mResultSet.Tables.Count < 1)
            return;

        //默认读取第一个数据表
        DataTable mSheet = mResultSet.Tables[0];

        //判断数据表内是否存在数据
        if (mSheet.Rows.Count < 1)
            return;

        //读取数据表行数和列数
        int rowCount = mSheet.Rows.Count;
        int colCount = mSheet.Columns.Count;

        //先判断表头行数
        int Header;
        if (mSheet.Rows[1][0].ToString() == "string" || mSheet.Rows[1][0].ToString() == "int")
        {
            Header = 3;
        }
        else if (mSheet.Rows[0][0].ToString() == "string" || mSheet.Rows[0][0].ToString() == "int")
        {
            Header = 2;
        }
        else
        {
            Header = 1;
        }
        //准备一个列表存储整个表的数据
        Dictionary<string, object> jsonValueDict = new Dictionary<string, object>();
        for (int i = Header; i < rowCount; i++)
        {
            //准备一个字典存储每一行的数据
            Dictionary<string, object> rowData = new Dictionary<string, object>();
            for (int j = 0; j < colCount; j++)
            {
                //读取表头字段
                string title = mSheet.Rows[Header - 1][j].ToString();
                if (Header == 1)
                {
                    rowData[title] = mSheet.Rows[i][j].ToString();
                }
                else
                {
                    //读取数据类型数据类型
                    switch (mSheet.Rows[Header - 2][j])
                    {
                        case "string":
                            rowData[title] = mSheet.Rows[i][j].ToString();
                            break;
                        case "string[]":
                            List<string> activeList = new List<string>();
                            string[] array = mSheet.Rows[i][j].ToString().Split(';');
                            for (int k = 0; k < array.Length; k++)
                            {
                                if (!string.IsNullOrEmpty(array[k]))
                                    activeList.Add(array[k]);
                            }
                            rowData[title] = activeList;
                            break;
                        case "int":
                            if (int.TryParse(mSheet.Rows[i][j].ToString(), out int num))
                            {
                                rowData[title] = num;
                            }
                            else
                            {
                                rowData[title] = 0;
                            }
                            break;
                        default:
                            rowData[title] = mSheet.Rows[i][j].ToString();
                            break;
                    }
                }
            }

            if (!string.IsNullOrEmpty(mSheet.Rows[i][0].ToString()))
            {
                if (jsonValueDict.TryGetValue(mSheet.Rows[i][0].ToString(), out _))
                {
                    Debug.LogError("配表有相同的值！配表：" + Path.GetFileName(JsonPath) + ",相同的key：" + mSheet.Rows[i][0].ToString());
                }
                else
                {
                    jsonValueDict.Add(mSheet.Rows[i][0].ToString(), rowData);
                }
            }
        }

        Dictionary<string, Dictionary<string, object>> jsonDataDict = new Dictionary<string, Dictionary<string, object>>();
        jsonDataDict.Add(Path.GetFileNameWithoutExtension(JsonPath), jsonValueDict);

        //生成Json字符串
        string json = JsonConvert.SerializeObject(jsonDataDict);

        //写入文件
        if (File.Exists(JsonPath))
        {
            File.Delete(JsonPath);
        }
        File.WriteAllText(JsonPath, json);
    }


    /// <summary>
    /// 设置目标实例的属性
    /// </summary>
    private void SetTargetProperty(object target, string propertyName, object propertyValue)
    {
        //获取类型
        Type mType = target.GetType();
        //获取属性集合
        PropertyInfo[] mPropertys = mType.GetProperties();
        foreach (PropertyInfo property in mPropertys)
        {
            if (property.Name == propertyName)
            {
                property.SetValue(target, Convert.ChangeType(propertyValue, property.PropertyType), null);
            }
        }
    }
}
