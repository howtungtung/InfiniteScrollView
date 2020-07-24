using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HowTungTung;
public class DemoTabPageCell : InfiniteCell<DemoTabPageData>
{
    public Text text;

    public override void OnUpdate()
    {
        RectTransform.sizeDelta = new Vector2(CellData.Width, CellData.Height);
        text.text = CellData.content;
    }
}
