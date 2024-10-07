using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.TerrainTools;
using UnityEngine;

[CustomEditor(typeof(BaseEventSO<>))]

//实现在inspector  显示 事件的订阅物体与数量 ，复制粘贴的看不懂
public class BaseEventSOEditor<T>:Editor
{
    private BaseEventSO<T> baseEventSO;

    private void OnEnable()
    {
        if (baseEventSO == null)
        {
            baseEventSO = target as BaseEventSO<T>;
        }
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.LabelField("订阅数量: "+ GetListener().Count);

        foreach (var listner in GetListener())
        {
            EditorGUILayout.LabelField(listner.ToString());
        }
    }

    private List<MonoBehaviour> GetListener()
    {
        List<MonoBehaviour> Listeners = new List<MonoBehaviour>();

        if (baseEventSO == null || baseEventSO.OnEventRised == null)
        {
            return Listeners;
        }

        var subscribers = baseEventSO.OnEventRised.GetInvocationList();

        foreach (var subscriber in subscribers)
        {
            var obj = subscriber.Target as MonoBehaviour;
            if(!Listeners.Contains(obj)) Listeners.Add(obj);

        }

        return Listeners;
    }
}