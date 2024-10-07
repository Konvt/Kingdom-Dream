using System;
using UnityEngine;
using UnityEngine.Events;

//监听事件基类
public class BaseEventListener<T> : MonoBehaviour
{
    public BaseEventSO<T> eventSO; //监听事件
    public UnityEvent<T> response; //绑定事件

    private void OnEnable()
    {
        eventSO.OnEventRised += OnEventRised;
    }
    private void OnDisable()
    {
        eventSO.OnEventRised -= OnEventRised;
    }

    private void OnEventRised(T value) //监听的事件发生后，启动绑定事件
    {
        response?.Invoke(value);
    }
}
