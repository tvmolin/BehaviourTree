using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;

public class BooleanCheckNodeWindow : AbstractActionNodeWindow
{
    private string dropDownText = "Select a method for the boolean check";

    public BooleanCheckNodeWindow(Vector3 position, AnimalAI animalAI) : base(animalAI)
    {
        windowTitle = "Boolean Check Node";
        windowBounds = new Rect(position.x, position.y, width, height);
        this.animalAI = animalAI;
    }

    protected override void DrawNodeWindow()
    {
        if (!EditorGUILayout.DropdownButton(new GUIContent(dropDownText), FocusType.Passive))
        {
            return;
        }

        //TODO place this code elsewhere
        List<MethodInfo> methods = typeof(AnimalAI)
                                    .GetMethods()
                                    .Where(method => method.GetCustomAttributes(typeof(BooleanCheckMethod), false).Length > 0)
                                    .ToList();

        GenericMenu menu = new GenericMenu();
        foreach (MethodInfo method in methods)
        {
            menu.AddItem(new GUIContent(method.Name), false, handleItemClicked, method);
        }
        menu.DropDown(new Rect(0, 0, 0, 0));
    }

    void handleItemClicked(object method)
    {
        MethodInfo methodInfo = (MethodInfo)method;
        dropDownText = methodInfo.Name;
        logicNode = new BooleanCheck(() => (bool)methodInfo.Invoke(animalAI, null));
        ((CompositeNode)parent.logicNode).childNodes.Add(logicNode);
    }
}