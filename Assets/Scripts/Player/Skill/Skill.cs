using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    [SerializeField] protected float cooldown;
    protected float cooldownTimer;

    protected virtual void Update()
    {
        cooldownTimer -= Time.deltaTime;    // ���û��ϴ�ʹ�ü���ʱ��
    }

    public virtual bool CanUseSkill()
    {
        if (cooldownTimer < 0)
        {
            UseSkill();
            cooldownTimer = cooldown;       // ����Ϊ�趨��ȴʱ��
            return true;
        }

        Debug.Log("Skill is on cooldown.");
        return false;
    }

    public virtual void UseSkill()
    {

    }
}
