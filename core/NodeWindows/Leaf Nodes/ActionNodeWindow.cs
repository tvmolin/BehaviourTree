using UnityEngine;
using UnityEditor;
using System.Reflection;

public class ActionNodeWindow : AbstractActionNodeWindow
{
    private Node currentCustomNode;
    private Node customNode;

    public ActionNodeWindow(Vector3 position, AnimalAI animalAI) : base(animalAI)
    {
        windowTitle = "Action Node";
        windowBounds = new Rect(position.x, position.y, width, height);
        this.animalAI = animalAI;
    }

    protected override void DrawNodeWindow()
    {
        customNode = (ActionNode)EditorGUILayout.ObjectField("Action script:", customNode, typeof(ActionNode), false);

        if (currentCustomNode != customNode)
        {
            currentCustomNode = customNode;

            //Reflect to get the constructor that takes something that AnimalAI inherits from
            //For some reason Activator.CreateInstance couldn't find the correct constructor
            ConstructorInfo constructorInfo = customNode.GetType().GetConstructor(new[] { typeof(AnimalAI) });
            logicNode = (ActionNode)constructorInfo.Invoke(new[] { animalAI });

            ((CompositeNode)parent.logicNode).childNodes.Add(logicNode);
        }
    }
}