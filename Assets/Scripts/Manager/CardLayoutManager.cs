using System.Collections.Generic;
using UnityEngine;

public class CardLayoutManager : MonoBehaviour
{
    public bool isHorizontal; //是否横向排列
    public float maxWidth = 10f; //最大宽度
    public float cardSpacing = 2f; //卡牌间距

    public Vector3 centerPoint; //卡牌生成中点
    [SerializeField] //保存生成卡牌的坐标
    private List<Vector3> cardPositions= new();
    private List<Quaternion> cardRotations=new();

    //外部调用，获取卡牌排列后的坐标
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
            //计算当前数量卡牌的宽度
            float currentWidth = (numberOfCards - 1) * cardSpacing;
            //与最大宽度取最小
            float totalWidth = Mathf.Min(currentWidth, maxWidth);

            //一张牌以上就更新间距
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
        else //扇形排列
        { 
        }
    }
}
