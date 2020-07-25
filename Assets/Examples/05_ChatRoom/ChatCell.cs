using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HowTungTung;
public class ChatCell : InfiniteCell<ChatCellData>
{
    public Text speakerText;
    public Text messageText;

    public override void OnUpdate()
    {
        speakerText.text = CellData.speaker;
        messageText.text = CellData.message;
        speakerText.alignment = CellData.isSelf ? TextAnchor.UpperRight : TextAnchor.UpperLeft;
        messageText.alignment = CellData.isSelf ? TextAnchor.UpperRight : TextAnchor.UpperLeft;
        RectTransform.sizeDelta = CellData.cellSize;
    }
}
