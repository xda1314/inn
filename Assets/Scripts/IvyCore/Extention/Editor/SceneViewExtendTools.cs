using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace IvyCore
{
    public class SceneViewExtendTools
    {
        public static void Layout_AlignToLeft()
        {
            float x = Mathf.Min(Selection.gameObjects.Select(obj => obj.transform.localPosition.x).ToArray());

            foreach (GameObject gameObject in Selection.gameObjects)
            {
                Undo.RegisterCompleteObjectUndo(gameObject.transform, "AlignToLeft");
                gameObject.transform.localPosition = new Vector2(x,
                    gameObject.transform.localPosition.y);
            }
        }

        public static void Layout_AlignToRight()
        {
            float x = Mathf.Max(Selection.gameObjects.Select(obj => obj.transform.localPosition.x + ((RectTransform)obj.transform).sizeDelta.x).ToArray());
            foreach (GameObject gameObject in Selection.gameObjects)
            {
                Undo.RegisterCompleteObjectUndo(gameObject.transform, "AlignToRight");
                gameObject.transform.localPosition = new Vector3(x -
            ((RectTransform)gameObject.transform).sizeDelta.x, gameObject.transform.localPosition.y);
            }
        }

        public static void Layout_AlignToTop()
        {
            float y = Mathf.Max(Selection.gameObjects.Select(obj => obj.transform.localPosition.y + ((RectTransform)obj.transform).sizeDelta.y * 0.5f).ToArray());
            foreach (GameObject gameObject in Selection.gameObjects)
            {
                Undo.RegisterCompleteObjectUndo(gameObject.transform as RectTransform, "布局相关");
                gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, y - ((RectTransform)gameObject.transform).sizeDelta.y * 0.5f);
            }
        }

        public static void Layout_AlignToBottom()
        {
            float y = Mathf.Min(Selection.gameObjects.Select(obj => obj.transform.localPosition.y - ((RectTransform)obj.transform).sizeDelta.y*0.5f).ToArray());

            foreach (GameObject gameObject in Selection.gameObjects)
            {
                Undo.RegisterCompleteObjectUndo(gameObject.transform as RectTransform, "布局相关");
                gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, y + ((RectTransform)gameObject.transform).sizeDelta.y*0.5f);
            }
        }

        public static void Layout_UniformDistributionInHorziontal()
        {
            int count = Selection.gameObjects.Length;
            float firstX = Mathf.Min(Selection.gameObjects.Select(obj => obj.transform.localPosition.x).ToArray());
            float lastX = Mathf.Max(Selection.gameObjects.Select(obj => obj.transform.localPosition.x).ToArray());
            float distance = (lastX - firstX) / (count - 1);
            var objects = Selection.gameObjects.ToList();
            objects.Sort((x, y) => (int)(x.transform.localPosition.x - y.transform.localPosition.x));
            for (int i = 0; i < count; i++)
            {
                Undo.RegisterCompleteObjectUndo(objects[i].transform as RectTransform, "布局相关-水平分布");
                objects[i].transform.localPosition = new Vector3(firstX + i * distance, objects[i].transform.localPosition.y);
            }
        }

        public static void Layout_UniformDistributionInVertical()
        {
            int count = Selection.gameObjects.Length;
            float firstY = Mathf.Min(Selection.gameObjects.Select(obj => obj.transform.localPosition.y).ToArray());
            float lastY = Mathf.Max(Selection.gameObjects.Select(obj => obj.transform.localPosition.y).ToArray());
            float distance = (lastY - firstY) / (count - 1);
            var objects = Selection.gameObjects.ToList();
            objects.Sort((x, y) => (int)(x.transform.localPosition.y - y.transform.localPosition.y));
            for (int i = 0; i < count; i++)
            {
                Undo.RegisterCompleteObjectUndo(objects[i].transform as RectTransform, "布局相关-垂直分布");
                objects[i].transform.localPosition = new Vector3(objects[i].transform.localPosition.x, firstY + i * distance);
            }
        }



        public static void Size_ResizeToMax()
        {
            var height = Mathf.Max(Selection.gameObjects.Select(obj => ((RectTransform)obj.transform).sizeDelta.y).ToArray());
            var width = Mathf.Max(Selection.gameObjects.Select(obj => ((RectTransform)obj.transform).sizeDelta.x).ToArray());
            foreach (GameObject gameObject in Selection.gameObjects)
            {
                Undo.RecordObject((RectTransform)gameObject.transform, "尺寸相关");
                ((RectTransform)gameObject.transform).sizeDelta = new Vector2(width, height);
            }
        }

        public static void Size_ResizeToMin()
        {
            var height = Mathf.Min(Selection.gameObjects.Select(obj => ((RectTransform)obj.transform).sizeDelta.y).ToArray());
            var width = Mathf.Min(Selection.gameObjects.Select(obj => ((RectTransform)obj.transform).sizeDelta.x).ToArray());
            foreach (GameObject gameObject in Selection.gameObjects)
            {
                Undo.RecordObject((RectTransform)gameObject.transform, "尺寸相关");
                ((RectTransform)gameObject.transform).sizeDelta = new Vector2(width, height);
            }
        }

        public static void Priority_MoveToTop()
        {
            Transform curSelect = Selection.activeTransform;
            if (curSelect != null)
            {
                curSelect.SetAsFirstSibling();
            }
        }

        public static void Priority_MoveToBottom()
        {
            Transform curSelect = Selection.activeTransform;
            if (curSelect != null)
            {
                curSelect.SetAsLastSibling();
            }
        }

        public static void Priority_MoveUp()
        {
            Transform curSelect = Selection.activeTransform;
            if (curSelect != null)
            {
                int curIndex = curSelect.GetSiblingIndex();
                if (curIndex > 0)
                {
                    curSelect.SetSiblingIndex(curIndex - 1);
                }
            }
        }

        public static void Priority_MoveDown()
        {
            Transform curSelect = Selection.activeTransform;
            if (curSelect != null)
            {
                int curIndex = curSelect.GetSiblingIndex();
                int child_num = curSelect.parent.childCount;
                if (curIndex < child_num - 1)
                {
                    curSelect.SetSiblingIndex(curIndex + 1);
                }
            }
        }

        public static void Active_ShowAll()
        {
            foreach (var item in Selection.gameObjects)
            {
                Undo.RecordObject(item, "显示");
                item.SetActive(true);
            }
        }

        public static void Active_HideAll()
        {
            foreach (var item in Selection.gameObjects)
            {
                Undo.RecordObject(item, "隐藏");
                item.SetActive(false);
            }
        }
    }
}