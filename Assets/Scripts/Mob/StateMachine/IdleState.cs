public class IdleState : IMobState
{
    private readonly Mob _mob;

    public IdleState(Mob mob)
    {
        _mob = mob;
    }

    public void ToAttackState()
    {
        _mob.CurrentState = _mob.AttackState;
    }

    public void ToIdleState()
    {

    }

    public void ToJumpState()
    {
        _mob.CurrentState = _mob.JumpState;
    }

    public void UpdateState()
    {
        _mob.mobColor.material = _mob.idleColor;

        _mob.animator.SetBool("IsIdle", true);
    }

}
