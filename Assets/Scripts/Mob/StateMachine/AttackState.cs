using UnityEngine;

public class AttackState : IMobState
{
    private readonly Mob _mob;
    private float _timer;

    public AttackState(Mob mob)
    {
        _mob = mob;
    }

    public void ToAttackState()
    {

    }

    public void ToIdleState()
    {
        _mob.CurrentState = _mob.JumpState;
    }

    public void ToJumpState()
    {
        _mob.CurrentState = _mob.JumpState;
    }

    public void UpdateState()
    {
        _mob.mobColor.material = _mob.attackColor;

        _mob.animator.SetBool("IsIdle", false);

        _mob.transform.Rotate(0, _mob.rotateSpeed * Time.deltaTime, 0);

        if (Time.time > _timer)
        {
            _timer = Time.time + _mob.Attack.FireRate;
            _mob.Shoot();
        }

    }

}
