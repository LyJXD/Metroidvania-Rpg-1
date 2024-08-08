using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : PlayerState
{
    public PlayerAirState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
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
         * ��ײ���-ǽ��
         */
        if (player.IsWallDetected() && rb.velocity.y < 0)
        {
            stateMachine.ChangeState(player.wallSlide);
        }

        /*
         * ��ײ���-����
         */
        if (player.IsGroundDetected())
        {
            stateMachine.ChangeState(player.idleState);
        }

        /*
         * �����ƶ�
         */
        if (xInput != 0)
        {
            player.SetVelocity(player.moveSpeed * .8f * xInput, rb.velocity.y);
        }
    }
}
