using UnityEngine;
using UnityEngine.UI;
using HowTungTung;
public class DemoHorizontalCell : InfiniteCell<DemoHorizontalData>
{
    public Text text;
    public override void OnUpdate()
    {
        RectTransform.sizeDelta = new Vector2(CellData.cellSize.x, RectTransform.sizeDelta.y);
        text.text = CellData.cellSize.x.ToString();
    }
}
