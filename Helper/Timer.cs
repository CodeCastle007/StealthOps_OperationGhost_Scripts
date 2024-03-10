using System;

public class Timer 
{
    private float currentTimer;
    public Timer(float _duration) {
        currentTimer = _duration;
    }

    public void ResetTimer(float _duration) {
        currentTimer = _duration;
    }

    public void TickTimer(float delta) {
        currentTimer -= delta;
    }

    public bool IsComplete() {
        return currentTimer <= 0;
    }
}
