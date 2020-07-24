using HowTungTung;
public class DemoVerticalData : InfiniteCellData
{
    public override float Height
    {
        get
        {
            return height;
        }
    }

    private float height;

    public DemoVerticalData(float height)
    {
        this.height = height;
    }
}