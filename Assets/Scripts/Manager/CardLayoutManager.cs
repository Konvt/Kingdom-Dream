using System.Collections.Generic;
using UnityEngine;

public class CardLayoutManager : MonoBehaviour
{
    public bool isHorizontal; //�Ƿ��������
    public float maxWidth = 10f; //�����
    public float cardSpacing = 2f; //���Ƽ��

    public Vector3 centerPoint; //���������е�
    [SerializeField] //�������ɿ��Ƶ�����
    private List<Vector3> cardPositions= new();
    private List<Quaternion> cardRotations=new();

    //�ⲿ���ã���ȡ�������к������
    public CardTransform GetCardTransform(int index, int totalCards)
    {
        CalculatePositions(totalCards, isHorizontal);

        return new CardTransform(cardPositions[index], cardRotations[index]);
    }
    private void CalculatePositions(int numberOfCards , bool horizontal)
    {
        cardPositions.Clear();
        cardRotations.Clear();
        if (horizontal)
        {
            //���㵱ǰ�������ƵĿ��
            float currentWidth = (numberOfCards - 1) * cardSpacing;
            //�������ȡ��С
            float totalWidth = Mathf.Min(currentWidth, maxWidth);

            //һ�������Ͼ͸��¼��
            float currentSpacing = totalWidth > 0 ? totalWidth / (numberOfCards - 1) : 0;
            for (int i = 0; i < numberOfCards; i++)
            {
                float xPost = 0 - (totalWidth / 2) + (i * currentSpacing);
                var position = new Vector3(xPost, centerPoint.y, 0);
                var rotation = Quaternion.identity;
                cardPositions.Add(position);
                cardRotations.Add(rotation);
            }
        }
        else //��������
        { 
        }
    }
}
