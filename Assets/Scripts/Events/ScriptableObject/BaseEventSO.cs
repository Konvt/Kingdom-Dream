using UnityEngine;
using UnityEngine.Events;

//�¼�����
public class BaseEventSO<T> : ScriptableObject //���س־û������¼�
{
    [TextArea]
    public string description;//�¼�����

    public UnityAction<T> OnEventRised; //�¼�����

    public string lastSender; //������
    public void RiseEvent(T value,object sender) //�����¼������¼������ߵ���
    {
        OnEventRised?.Invoke(value);
        if(sender!=null)
        lastSender = sender.ToString();
    }

}

