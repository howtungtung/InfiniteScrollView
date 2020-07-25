using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using HowTungTung;
public class TestGUI_05 : MonoBehaviour
{
    private ChatRoomScrollView chatScrollView;
    public string myName = "HowTungTung";
    private string speaker = "Tester";
    private string message = "In a recent blog post we introduced the concept of Scriptable Render Pipelines. In short, SRP allow developers to control how Unity renders a frame in C#. We will release two built-in render pipelines with Unity 2018.1: the Lightweight Pipeline and HD Pipeline. In this article we’re going to focus on the Lightweight Pipeline or LWRP.";

    private void Awake()
    {
        chatScrollView = FindObjectOfType<ChatRoomScrollView>();
    }

    private void OnGUI()
    {
        if (GUILayout.Button("NextScene"))
        {
            SceneManager.LoadScene((int)Mathf.Repeat(SceneManager.GetActiveScene().buildIndex + 1, SceneManager.sceneCountInBuildSettings));
        }
        GUILayout.Label("Speaker");
        speaker = GUILayout.TextField(speaker);
        GUILayout.Label("Message");
        message = GUILayout.TextArea(message, GUILayout.MaxWidth(300),GUILayout.MaxHeight(100));
        if (GUILayout.Button("Add"))
        {
            var data = new ChatCellData(speaker, message, false);
            chatScrollView.Add(data);
        }
    }

    public void OnSubmit(string input)
    {

        chatScrollView.Add(new ChatCellData(myName, input, true));
    }
}
