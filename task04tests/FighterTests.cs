using Xunit;
using task04;

public class FighterTests
{
    [Fact]
    public void Fighter_ShouldCorrectMovingForward()
    {
        var fighter = new Fighter();
        fighter.MoveForward();
        Assert.Equal(100, fighter.TotalDistance);
        fighter.MoveForward();
        Assert.Equal(200, fighter.TotalDistance);
    }

    [Fact]
    public void Fighter_ShouldCorrectRotating()
    {
        var fighter = new Fighter();
        fighter.Rotate(40);
        Assert.Equal(40, fighter.CurrentAngle);
        fighter.Rotate(-50);
        Assert.Equal(-10, fighter.CurrentAngle);
        fighter.Rotate(540);
        Assert.Equal(170, fighter.CurrentAngle);
    }

    [Fact]
    public void Fighter_ShouldCorrectFiering()
    {
        var fighter = new Fighter();
        fighter.Fire();
        Assert.Equal(1, fighter.TotalFires);
        fighter.Fire();
        fighter.Fire();
        Assert.Equal(3, fighter.TotalFires);
    }
}