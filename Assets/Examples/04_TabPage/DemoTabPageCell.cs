using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HowTungTung;
public class DemoTabPageCell : InfiniteCell
{
    public Text text;

    public override void OnUpdate()
    {
        DemoTabPageData data = (DemoTabPageData)CellData.data;
        RectTransform.sizeDelta = CellData.cellSize;
        text.text = data.content;
    }
}
