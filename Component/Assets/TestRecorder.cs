using System.IO;
using UnityEditor;
using UnityEngine;

public class TestRecorder : MonoBehaviour
{
  
    [Tooltip("Check if the component was created programmatically. Leave unchecked if done through the Editor.")]
   
    public bool isProgrammatic = false;
   
    
    private float startTime;
    private bool isTiming = false;

    // right click on the component and press this functio to start test
    [ContextMenu("Start Test")]
    public void StartTest()
    {
        startTime = Time.realtimeSinceStartup;
        isTiming = true;
        Debug.Log("Test started...");
    }

    // right click on the component and press this functio to end test
    [ContextMenu("End Test")]
    public void EndTest()
    {
        if (!isTiming) return;

        float elapsed = Time.realtimeSinceStartup - startTime;
        isTiming = false;

        string method = isProgrammatic ? "Programmatic" : "Editor";
        WriteToCSV(elapsed, method);
        Debug.Log($"Test ended. Method: {method}, Time: {elapsed:F2} seconds");
    }

    void WriteToCSV(float timeTaken, string method)
    {
        string filePath = Application.dataPath + "/ComponentTestResults.csv";
        bool fileExists = File.Exists(filePath);

        using (StreamWriter writer = new StreamWriter(filePath, true))
        {
            if (!fileExists)
                writer.WriteLine("Method,TimeTaken");

            writer.WriteLine($"{method},{timeTaken:F2}");
        }

#if UNITY_EDITOR
        AssetDatabase.Refresh(); // Makes the file show up in the editor right away
#endif
    }
}
