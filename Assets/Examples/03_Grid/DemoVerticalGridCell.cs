using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HowTungTung;

public class DemoVerticalGridCell : InfiniteCell<DemoVerticalGridData>
{
    public Text text;

    public override void OnUpdate()
    {
        RectTransform.sizeDelta = new Vector2(CellData.Width, CellData.Height);
        text.text = CellData.index.ToString();
    }

    public void OnClicked()
    {
        InvokeSelected();
    }
}
