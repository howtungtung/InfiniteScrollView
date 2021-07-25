using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HowTungTung;
public class ChatCell : InfiniteCell
{
    public Text speakerText;
    public Text messageText;

    public override void OnUpdate()
    {
        ChatCellData data = (ChatCellData)CellData.data;
        speakerText.text = data.speaker;
        messageText.text = data.message;
        speakerText.alignment = data.isSelf ? TextAnchor.UpperRight : TextAnchor.UpperLeft;
        messageText.alignment = data.isSelf ? TextAnchor.UpperRight : TextAnchor.UpperLeft;
        RectTransform.sizeDelta = CellData.cellSize;
    }
}
