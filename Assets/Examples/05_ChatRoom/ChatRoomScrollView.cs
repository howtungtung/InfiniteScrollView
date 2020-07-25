using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HowTungTung;
public class ChatRoomScrollView : VerticalInfiniteScrollView<ChatCellData>
{
    public float baseCellHeight = 20;
    public Text heightInstrument;
    public override void Add(ChatCellData data)
    {
        heightInstrument.text = data.message;
        data.cellSize.y = heightInstrument.preferredHeight + baseCellHeight;
        base.Add(data);
        Snap(dataList.Count - 1, 0.5f);
    }
}
