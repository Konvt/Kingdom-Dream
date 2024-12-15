using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    public int maxHp;

    public IntVariable hp;

    public IntVariable defense;

    public IntVariable strengthRound;

    public GameObject buffVFX;
    public GameObject debuffVFX;

    [Header("游戏阶段")]
    public IntVariable gameStage;

    [Header("阶段对应血量")]
    public List<int> perStageHp;

    private AudioSender audioSender;
    public int CurrentHp { get => hp.currentValue; set=>hp.SetValue(value);  }

    public int MaxHp { get => hp.maxValue; }

    public bool isDead;

    protected Animator animator;

    public float baseStrength = 1f;
    private float strengthEffect = 0.5f;

    [Header("广播")]
    public ObjectEventSO characterDeadEvnet;
    [Header("音效")]
    public List<AudioClip> audioClipList;
    protected virtual  void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        audioSender = GetComponentInChildren<AudioSender>();
    }

    protected virtual void Start()
    {
        isDead = false;
        if(gameObject.tag!="Player") maxHp = perStageHp[gameStage.currentValue-1]; //游戏阶段默认1开始
        hp.maxValue = maxHp;
        CurrentHp = maxHp;
        ResetDefense();
        strengthRound.SetValue(0);
    }

    protected virtual void Update()
    {
        animator.SetBool("isDead", isDead);
    }

    public virtual void TakeDamage(int damage)
    {
        var currentDamage = damage - defense.currentValue >= 0 ? damage - defense.currentValue : 0;
        var currentDefense = damage - defense.currentValue >= 0 ? 0:defense.currentValue- damage;
        defense.SetValue(currentDefense);
        if (CurrentHp > currentDamage)
        {
            CurrentHp -= currentDamage;
            animator.SetTrigger("hit");
            audioSender.Play(audioClipList[0]);
        }
        else
        {
            //死亡逻辑
            CurrentHp = 0;
            isDead = true;
            audioSender.Play(audioClipList[1]);
            characterDeadEvnet.RiseEvent(this, this);
        }
    }

    public void UpdateDefense(int amount)
    {
        var value = amount + defense.currentValue;
        defense.SetValue(value);
    }
    public void ResetDefense()
    {
        defense.SetValue(0);
    }
    public void Heal(int amount)
    {
        if (amount > 0)
        {
            var currentHP = Mathf.Min(CurrentHp + amount, maxHp);
            CurrentHp = currentHP;
            buffVFX.SetActive(true);
        }
        else
        {
            TakeDamage(-amount);
        }
    }

    public void SetUpStrength(int round , bool isPositive)
    {
        if (isPositive)
        {
            var newStrength = baseStrength + strengthEffect;
            baseStrength = MathF.Min(1.5f, newStrength);
            buffVFX.SetActive(true);
        }
        else
        {
            debuffVFX.SetActive(true);
            var newStrength = baseStrength - strengthEffect;
            baseStrength = MathF.Max(newStrength, 0.5f);
        }
        var currentRound = strengthRound.currentValue + round;
        if (baseStrength == 1) strengthRound.SetValue(0); //可以用一张牌抵消全部的负面回合
        else strengthRound.SetValue(currentRound);
        GetComponent<HealthBarController>().SetIntendElement();
    }

    public void UpdateStrength()
    {
        strengthRound.SetValue(strengthRound.currentValue-1);
        if (strengthRound.currentValue <= 0)
        {
            baseStrength = 1f;
            strengthRound.SetValue(0);
        }
    }
}
