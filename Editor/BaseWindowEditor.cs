using UnityEditor;
using UnityEngine;
using System;
using UnityEngine.Events;

public abstract class BaseWindowEditor : EditorWindow
{
    protected abstract void OnGUI();

    protected void DrawLabel(string label)
    {
        EditorGUILayout.LabelField(label, EditorStyles.boldLabel);
    }

    protected void DrawFoldout(string label, ref bool isOpen)
    {
        isOpen = EditorGUILayout.Foldout(isOpen, label, true);
    }
    
    protected void DrawLine(int width = 1)
    {
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
    }
    
    protected void DrawSpace(int height = 1)
    {
        EditorGUILayout.Space(height);
    }
    
    protected void DrawButton(string label, float width, float height, UnityAction onClick)
    {
        if (GUILayout.Button(label, GUILayout.Width(width), GUILayout.Height(height)))
        {
            onClick?.Invoke();
        }
    }
    
    protected void DrawScrollView(ref Vector2 scrollPos, Action content)
    {
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
        content?.Invoke();
        EditorGUILayout.EndScrollView();
    }

    #region Draw Box

    protected void DrawInfoBox(string message)
    {
        EditorGUILayout.HelpBox(message, MessageType.Info);
    }
    
    protected void DrawErrorBox(string message)
    {
        EditorGUILayout.HelpBox(message, MessageType.Error);
    }

    protected void DrawWarningBox(string message)
    {
        EditorGUILayout.HelpBox(message, MessageType.Warning);
    }

    protected void DrawBox(string message)
    {
        EditorGUILayout.BeginVertical("box");
        EditorGUILayout.LabelField(message);
        EditorGUILayout.EndVertical();
    }

    #endregion
    
    #region Draw Fields

    protected void DrawField(string label, ref int value)
    {
        value = EditorGUILayout.IntField(label, value);
    }

    protected void DrawField(string label, ref float value)
    {
        value = EditorGUILayout.FloatField(label, value);
    }
    
    protected void DrawField(string label, ref string value)
    {
        value = EditorGUILayout.TextField(label, value);
    }

    protected void DrawField(string label, ref Vector2 value)
    {
        value = EditorGUILayout.Vector2Field(label, value);
    }

    protected void DrawField(string label, ref Vector3 value)
    {
        value = EditorGUILayout.Vector3Field(label, value);
    }

    protected void DrawField(string label, ref Vector4 value)
    {
        value = EditorGUILayout.Vector4Field(label, value);
    }

    protected void DrawField(string label, ref Rect value)
    {
        value = EditorGUILayout.RectField(label, value);
    }
    
    protected void DrawField(string label, ref RectInt value)
    {
        value = EditorGUILayout.RectIntField(label, value);
    }
    
    protected void DrawField(string label, ref Color value)
    {
        value = EditorGUILayout.ColorField(label, value);
    }

    protected void DrawField(string label, ref Bounds value)
    {
        value = EditorGUILayout.BoundsField(label, value);
    }

    protected void DrawField(string label, ref BoundsInt value)
    {
        value = EditorGUILayout.BoundsIntField(label, value);
    }
    
    protected void DrawEnumPopup<T>(string label, ref T value) where T : Enum
    {
        value = (T)EditorGUILayout.EnumPopup(label, value);
    }

    protected void DrawField<T>(string label, ref T obj) where T : UnityEngine.Object
    {
        obj = (T)EditorGUILayout.ObjectField(label, obj, typeof(T), true);
    }

    protected void DrawToggle(string label, ref bool value, UnityAction<bool> onValueChanged = null)
    {
        bool newValue = EditorGUILayout.Toggle(label, value);
        if (newValue != value)
        {
            value = newValue;
            onValueChanged?.Invoke(value);
        }
    }
    
    #endregion
}