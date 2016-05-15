namespace cryingpants
{
    public interface FiniteStateHandler<StateEnum>
    {
        void OnStateEnter(StateEnum newState);
        void OnStateExit(StateEnum oldState);
        StateEnum OnStateUpdate(StateEnum curState);
    }
}
