using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCatachSwordState : PlayerState
{
    private Transform sword;
    public PlayerCatachSwordState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        sword = player.sword.transform;

        /*
         * 根据剑的位置翻转角色朝向
         */
        if (sword.position.x < player.transform.position.x && player.facingDir == 1)
        {
            player.Flip();
        }
        else if (sword.position.x > player.transform.position.x && player.facingDir == -1)
        {
            player.Flip();
        }

        rb.velocity = new Vector2(rb.velocity.x * 0.7f + player.swordReturnImpact * -player.facingDir,rb.velocity.y);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
    
        if(triggerCalled)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
