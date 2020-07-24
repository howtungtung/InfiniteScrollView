using HowTungTung;
public class DemoHorizontalData : InfiniteCellData
{
    public override float Width => width;
    private float width;

    public DemoHorizontalData(float width)
    {
        this.width = width;
    }
}
