using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator animator;
    private Player player;
    private void Awake()
    {
        player = GetComponent<Player>();    
        animator = GetComponentInChildren<Animator>();
    }

    private void OnEnable()
    {
        animator.Play("sleep");
        animator.SetBool("isSleep", true);

    }
    public void PlayerTurnAnimation()
    {
        animator.SetBool("isSleep",false);
        animator.SetBool("isParry", false);
    }
    public void PlayerEndTurnAnimation()
    {

        if (player.defense.currentValue > 0)
        {
            animator.SetBool("isSleep", false);
            animator.SetBool("isParry", true);
        }
        else
        {
            animator.SetBool("isParry", false);
            animator.SetBool("isSleep", true);
        }
    }
    public void PlayerExecuteCardAnimation(object obj)
    {
        Card card = obj as Card;
        if (card != null) 
        {
            switch (card.cardData.cardType)
            {
                case CardType.Attack:
                    animator.SetTrigger("attack");
                    break;
                case CardType.Defense:
                case CardType.Abilities:
                    animator.SetTrigger("skill");
                    break;
            }
        }
    }
    public void PlayerDieAnimation()
    {
        animator.SetBool("isDie", true);
    }

    public void SleepAnimation()
    {
        animator.Play("death");
    }
}
