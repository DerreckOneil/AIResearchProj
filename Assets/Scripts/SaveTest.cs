using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Assertions;
public class SaveTest : MonoBehaviour
{
    [SerializeField] private ThoughtProcess thoughtProcess;
    public ThoughtProcess ThoughtProcess
    {
        get { return thoughtProcess; }
    }
    private void Start()
    {
        Load();
    }
    private void OnDestroy()
    {
        Save();
    }
    public void Save()
    {
        string text = JsonUtility.ToJson(thoughtProcess, true);
        // keep in mind this goes here C:\Users\user\AppData\LocalLow\Unity Technologies\UnityEnvironment
        string fileName = Path.Combine(Application.persistentDataPath, "Example.json").Replace('\\', '/');
        File.WriteAllText(fileName, text);
    }

    public void Load()
    {
        string fileName = Path.Combine(Application.persistentDataPath,"Example.json").Replace('\\', '/');
        if (!File.Exists(fileName))
            return;
        string text = File.ReadAllText(fileName);
        JsonUtility.FromJsonOverwrite(text, thoughtProcess);
    }

    public void Delete()
    {
        string fileName = Path.Combine(Application.persistentDataPath, "Example.json").Replace('\\', '/');

        if (File.Exists(fileName))
        {
            Debug.Log("Deleting: " + fileName);
            File.Delete(fileName);
            Assert.IsFalse(File.Exists(fileName));
        }
        else
        {
            Debug.Log("file DNE");
        }
    }
}
