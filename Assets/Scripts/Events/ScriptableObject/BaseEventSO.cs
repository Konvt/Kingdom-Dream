using UnityEngine;
using UnityEngine.Events;

//事件基类
public class BaseEventSO<T> : ScriptableObject //本地持久化场景事件
{
    [TextArea]
    public string description;//事件描述

    public UnityAction<T> OnEventRised; //事件变量

    public string lastSender; //发起者
    public void RiseEvent(T value,object sender) //发起事件，由事件发起者调用
    {
        OnEventRised?.Invoke(value);
        if(sender!=null)
        lastSender = sender.ToString();
    }

}

