using System.Collections;
using UnityEngine;

public class Enemy :CharacterBase
{
    public EnemyActionDataSO actionDataSO;

    public EnemyAction currentAction;

    protected Player player;

    protected override void Awake()
    {
        base.Awake();
    }

    public virtual void OnPlayerTurnBegin()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        int randomActionIndex = Random.Range(0, actionDataSO.actions.Count);
        currentAction = actionDataSO.actions[randomActionIndex];
    }
    public virtual void OnEnemyTurnBegin()
    {
        ResetDefense();
        switch (currentAction.effect.targetType)
        {
            case EffectTargetType.Self:
                Skill();
                break;
            case EffectTargetType.Target:
                Attack();
                break;
            case EffectTargetType.All: break;
        }
    }
    public virtual void Skill()
    {
        StartCoroutine(ProcessDelayActioin("skill"));
    }
    public virtual void Attack()
    {
        StartCoroutine(ProcessDelayActioin("attack"));
    }

    IEnumerator ProcessDelayActioin(string actionName)
    {
        animator.SetTrigger(actionName);

        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime % 1.0f > 0.6f 
        &&!animator.IsInTransition(0)
        &&animator.GetCurrentAnimatorStateInfo(0).IsName(actionName)
        );

        if(actionName=="attack") currentAction.effect.Excute(this, player);
        else  currentAction.effect.Excute(this, this);

    }
}
