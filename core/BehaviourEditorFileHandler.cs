using UnityEngine;

public class BehaviourEditorFileHandler
{
    public static void SaveFile(object o)
    {
        string json = JsonUtility.ToJson(o);


    }

    public static void LoadFile()
    {

    }
}