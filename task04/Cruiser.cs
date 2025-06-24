using task04;

public class Cruiser : ISpaceship
{
    public int Speed => 50;
    public int FirePower => 100;
    public int TotalDistance { get; private set; } = 0;
    public int CurrentAngle { get; private set; } = 0;
    public int TotalFires { get; private set; } = 0;

    public void MoveForward()
    {
        TotalDistance += Speed;
    }

    public void Rotate(int angle)
    {
        CurrentAngle = (CurrentAngle + angle) % 360;
    }

    public void Fire()
    {
        TotalFires++;
    }
}
