using UnityEngine;
using UnityEngine.UI;
using HowTungTung;
public class DemoVerticalCell : InfiniteCell<DemoVerticalData>
{
    public Text text;

    public override void OnUpdate()
    {
        RectTransform.sizeDelta = new Vector2(RectTransform.sizeDelta.x, CellData.cellSize.y);
        text.text = CellData.cellSize.y.ToString();
    }
}
