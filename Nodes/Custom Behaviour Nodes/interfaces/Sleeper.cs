public interface Sleeper
{
    bool WantsToSleep();

    bool IsRested();

    void Sleep();

    void Awake();
}