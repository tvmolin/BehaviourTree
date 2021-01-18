using UnityEngine;
using UnityEngine.AI;

public abstract class AnimalAI : MonoBehaviour, Eater, Sleeper, Wanderer
{
    protected float hungerThreshold = 30;
    protected float energyThreshold = 30;

    public NavMeshAgent agent;
    public GameObject sleepingText;
    public GameObject eatingText;

    public float fullness;
    public float energy;
    protected bool isSleeping = false;
    protected bool isEating = false;

    public Node topNode;

    [BooleanCheckMethod]
    public bool WantsToEat()
    {
        return (fullness < hungerThreshold) || (fullness < 100 && isEating);
    }

    public void ConsumeFood(Food food)
    {
        eatingText.SetActive(true);
        isEating = true;
        fullness = Mathf.Clamp(food.sustenance + fullness, 0, 100);
    }

    [BooleanCheckMethod]
    public bool IsFull()
    {
        return fullness >= 100;
    }

    public void StopEating()
    {
        eatingText.SetActive(false);
        isEating = false;
    }

    [BooleanCheckMethod]
    public bool WantsToSleep()
    {
        return (energy < energyThreshold) || (energy < 100 && isSleeping);
    }

    public void Sleep()
    {
        sleepingText.SetActive(true);
        isSleeping = true;
        energy = Mathf.Clamp(5 + energy, 0, 100);
    }

    public void Awake()
    {
        sleepingText.SetActive(false);
        isSleeping = false;
    }

    public Transform GetTransform()
    {
        return this.transform;
    }

    public void SpendEnergyWandering()
    {
        energy -= 1;
        fullness -= 1;
    }

    public bool IsRested()
    {
        return energy >= 100;
    }

}