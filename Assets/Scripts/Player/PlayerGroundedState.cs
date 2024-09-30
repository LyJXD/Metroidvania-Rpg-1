using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    public PlayerGroundedState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        /*
         * ��������ͨ����
         */
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            stateMachine.ChangeState(player.primaryAttack);
        }

        /*
         * ����Ҽ�Ͷ����׼
         */
        if (Input.GetKeyDown(KeyCode.Mouse1) && HasNoSwordOutside())
        {
            stateMachine.ChangeState(player.aimSword);
        }

        if (!player.IsGroundDetected())
        {
            stateMachine.ChangeState(player.airState);
        }

        /*
         * �ո���Ծ
         */
        if (Input.GetKeyDown(KeyCode.Space) && player.IsGroundDetected())
        {
            stateMachine.ChangeState(player.jumpState);
        }

        /*
         * Q������
         */
        if (Input.GetKeyDown(KeyCode.Q))
        {
            stateMachine.ChangeState(player.counterAttack);
        }

        /*
         * R���ڶ�����
         */
        if(Input.GetKeyDown(KeyCode.R))
        {
            stateMachine.ChangeState(player.blackHole);
        }
    }

    /*
     * �жϽ��Ƿ�Ϊ���ӳ�״̬
     */
    private bool HasNoSwordOutside()
    {
        if (!player.sword)
        {
            return true;
        }

        player.sword.GetComponent<Sword_Skill_Controller>().ReturnSword();
        return false;
    }
}
