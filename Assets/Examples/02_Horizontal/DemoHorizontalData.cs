using HowTungTung;
public class DemoHorizontalData : InfiniteCellData
{
    public override float Width
    {
        get
        {
            return width;
        }
    }

    private float width;

    public DemoHorizontalData(float width)
    {
        this.width = width;
    }
}
