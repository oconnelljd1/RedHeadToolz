using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private State currentState;
    private State previousState;

    public void ChangeState(State newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }

        previousState = currentState;
        currentState = newState;
        currentState.Enter();
    }

    public void RevertToPreviousState()
    {
        ChangeState(previousState);
    }

    void Update()
    {
        if (currentState != null)
        {
            currentState.Update();
        }
    }
}