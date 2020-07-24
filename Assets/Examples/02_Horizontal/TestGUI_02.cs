using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using HowTungTung;
public class TestGUI_02 : MonoBehaviour
{
    private InfiniteScrollView<DemoHorizontalData> infiniteScrollView;

    private string dataWidth = "50";
    private string removeIndex = "0";
    private string snapIndex = "0";

    private void Awake()
    {
        infiniteScrollView = FindObjectOfType<InfiniteScrollView<DemoHorizontalData>>();
    }

    private void OnGUI()
    {
        if (GUILayout.Button("NextScene"))
        {
            SceneManager.LoadScene((int)Mathf.Repeat(SceneManager.GetActiveScene().buildIndex + 1, SceneManager.sceneCountInBuildSettings));
        }
        if (GUILayout.Button("Add 100 Random Width Cell"))
        {
            for(int i = 0; i < 100; i++)
            {
                var data = new DemoHorizontalData(Random.Range(50, 300));
                infiniteScrollView.Add(data);
            }
        }
        GUILayout.Label("Add New Cell Width");
        dataWidth = GUILayout.TextField(dataWidth);
        if (GUILayout.Button("Add"))
        {
            var data = new DemoHorizontalData(int.Parse(dataWidth));
            infiniteScrollView.Add(data);
        }
        GUILayout.Label("Remove Index");
        removeIndex = GUILayout.TextField(removeIndex);
        if (GUILayout.Button("Remove"))
        {
            infiniteScrollView.Remove(int.Parse(removeIndex));
        }
        GUILayout.Label("Snap Index");
        snapIndex = GUILayout.TextField(snapIndex);
        if (GUILayout.Button("Snap"))
        {
            infiniteScrollView.Snap(int.Parse(snapIndex), 0.1f);
        }
    }
}
