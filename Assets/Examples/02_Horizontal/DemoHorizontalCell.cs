using UnityEngine;
using UnityEngine.UI;
using HowTungTung;
public class DemoHorizontalCell : InfiniteCell<DemoHorizontalData>
{
    public Text text;
    public override void OnUpdate()
    {
        var size = RectTransform.sizeDelta;
        size.x = CellData.Width;
        RectTransform.sizeDelta = size;
        text.text = CellData.Width.ToString();
    }
}
