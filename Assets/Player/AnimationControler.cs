using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    Idle,
    Run,
    Jump,
    DoubleJump,
    Grounded
}
public class AnimationControler
{
    PlayerState state;
    Animator animator;


    public AnimationControler(Animator animator)
    {
        state = PlayerState.Idle;
        this.animator = animator;
    }

    public void ChangeState(PlayerState state)
    {
        switch (state)
        {
            case PlayerState.Idle:
                break;

        }
    }

}
