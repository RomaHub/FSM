[System.Serializable]
public class StateSerializer
{

    public enum MobState
    {
        Idle,
        Jump,
        Attack
    }

    public MobState state;

    public void LoadState(Mob mob)
    {

        if (state == MobState.Idle)
        {
            mob.CurrentState = mob.IdleState;
        }
        else if (state == MobState.Jump)
        {
            mob.CurrentState = mob.JumpState;
        }
        else if (state == MobState.Attack)
        {
            mob.CurrentState = mob.AttackState;
        }

    }

    public void SaveState(Mob mob)
    {

        if (mob.CurrentState == mob.IdleState)
        {
            state = MobState.Idle;
        }
        else
            if (mob.CurrentState == mob.JumpState)
        {
            state = MobState.Jump;
        }
        else if (mob.CurrentState == mob.AttackState)
        {
            state = MobState.Attack;
        }

    }
}
