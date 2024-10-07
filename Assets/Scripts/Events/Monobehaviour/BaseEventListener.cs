using System;
using UnityEngine;
using UnityEngine.Events;

//�����¼�����
public class BaseEventListener<T> : MonoBehaviour
{
    public BaseEventSO<T> eventSO; //�����¼�
    public UnityEvent<T> response; //���¼�

    private void OnEnable()
    {
        eventSO.OnEventRised += OnEventRised;
    }
    private void OnDisable()
    {
        eventSO.OnEventRised -= OnEventRised;
    }

    private void OnEventRised(T value) //�������¼��������������¼�
    {
        response?.Invoke(value);
    }
}
