using System;

public class TimerEventArgs : EventArgs
{
    public int Timer;

    public TimerEventArgs(int timer) => Timer = timer;
}
