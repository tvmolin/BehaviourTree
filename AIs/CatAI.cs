using System.Collections.Generic;
using UnityEngine;


public class CatAI : AnimalAI
{

    void Start()
    {
        fullness = Random.Range(25, 99);
        energy = Random.Range(60, 99);
        // ConstructBehaviourTree();
    }

    void Update()
    {
        if (topNode != null)
        {
            topNode.evaluate();
        }
    }

    private void ConstructBehaviourTree()
    {
        BooleanCheck isHungry = new BooleanCheck(WantsToEat);
        ActionNode eat = new EatAction(this);
        SequenceNode eatSequence = new SequenceNode(new List<Node>() { isHungry, eat });

        BooleanCheck isTired = new BooleanCheck(WantsToSleep);
        ActionNode sleep = new SleepAction(this);
        SequenceNode sleepSequence = new SequenceNode(new List<Node>() { isTired, sleep });

        ActionNode wander = new WanderAction(this, agent);

        topNode = new SelectorNode(new List<Node>() { eatSequence, sleepSequence, wander });
    }
}
