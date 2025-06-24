using task04;
public class Fighter : ISpaceship
{
    public int Speed => 100;
    public int FirePower => 25;

    private int _totalDistance = 0;
    private int _currentAngle = 0;
    private int _totalFires = 0;

    public int TotalDistance => _totalDistance;
    public int CurrentAngle => _currentAngle;
    public int TotalFires => _totalFires;

    public void MoveForward()
    {
        _totalDistance += Speed;
    }

    public void Rotate(int angle)
    {
        _currentAngle = (_currentAngle + angle) % 360;
    }

    public void Fire()
    {
        _totalFires++;
    }
}
