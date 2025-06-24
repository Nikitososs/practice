using Xunit;
using task04;

public class CruiserTests
{
    [Fact]
    public void Cruiser_ShouldCorrectMovingForward()
    {
        var cruiser = new Cruiser();
        cruiser.MoveForward();
        Assert.Equal(50, cruiser.TotalDistance);
        cruiser.MoveForward();
        Assert.Equal(100, cruiser.TotalDistance);
    }

    [Fact]
    public void Cruiser_ShouldCorrectRotating()
    {
        var cruiser = new Cruiser();
        cruiser.Rotate(40);
        Assert.Equal(40, cruiser.CurrentAngle);
        cruiser.Rotate(-50);
        Assert.Equal(-10, cruiser.CurrentAngle);
        cruiser.Rotate(540);
        Assert.Equal(170, cruiser.CurrentAngle);
    }

    [Fact]
    public void Cruiser_ShouldCorrectFiering()
    {
        var cruiser = new Cruiser();
        cruiser.Fire();
        Assert.Equal(1, cruiser.TotalFires);
        cruiser.Fire();
        cruiser.Fire();
        Assert.Equal(3, cruiser.TotalFires);
    }
}