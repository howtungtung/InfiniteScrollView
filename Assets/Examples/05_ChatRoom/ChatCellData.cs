using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HowTungTung;
public class ChatCellData
{
    public string speaker;
    public string message;
    public bool isSelf;

    public ChatCellData(string speaker, string message, bool isSelf)
    {
        this.speaker = speaker;
        this.message = message;
        this.isSelf = isSelf;
    }
}
