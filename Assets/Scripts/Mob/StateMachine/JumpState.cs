using UnityEngine;

public class JumpState : IMobState
{
    private readonly Mob _mob;
    private float _timer;

    public JumpState(Mob mob)
    {
        _mob = mob;
    }

    public void ToAttackState()
    {
        _mob.CurrentState = _mob.AttackState;
        _timer = 0;

    }

    public void ToIdleState()
    {
        _mob.CurrentState = _mob.JumpState;
        _timer = 0;

    }

    public void ToJumpState()
    {

    }

    public void UpdateState()
    {
        _mob.mobColor.material = _mob.jumpColor;

        _mob.animator.SetBool("IsIdle", false);

        if (_mob.isGrounded)
        {
            _mob.rigidBody.AddForce(Vector3.up * _mob.jumpStrength, ForceMode.Impulse);
        }

        _timer += Time.deltaTime;

        if (_timer >= _mob.stayInState)
            ToAttackState();

    }
}
