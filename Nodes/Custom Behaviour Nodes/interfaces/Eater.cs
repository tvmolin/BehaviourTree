public interface Eater
{
    bool WantsToEat();

    bool IsFull();

    void ConsumeFood(Food food);

    void StopEating();
}