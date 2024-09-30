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
         * Êó±ê×ó¼üÆÕÍ¨¹¥»÷
         */
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            stateMachine.ChangeState(player.primaryAttack);
        }

        /*
         * Êó±êÓÒ¼üÍ¶½£Ãé×¼
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
         * ¿Õ¸ñÌøÔ¾
         */
        if (Input.GetKeyDown(KeyCode.Space) && player.IsGroundDetected())
        {
            stateMachine.ChangeState(player.jumpState);
        }

        /*
         * Q¼ü·´»÷
         */
        if (Input.GetKeyDown(KeyCode.Q))
        {
            stateMachine.ChangeState(player.counterAttack);
        }

        /*
         * R¼üºÚ¶´¼¼ÄÜ
         */
        if(Input.GetKeyDown(KeyCode.R))
        {
            stateMachine.ChangeState(player.blackHole);
        }
    }

    /*
     * ÅÐ¶Ï½£ÊÇ·ñÎª±»ÈÓ³ö×´Ì¬
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
