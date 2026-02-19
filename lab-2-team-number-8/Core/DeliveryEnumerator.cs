using lab_2_team_number_8;
using System.Collections;

public class DeliveryEnumerator : IEnumerator
{
    private Delivery[] _deliveries;
    private int _position = -1;

    public DeliveryEnumerator(Delivery[] deliveries)
    {
        _deliveries = deliveries;
    }

    public bool MoveNext()
    {
        _position++;
        
        if (_position < _deliveries.Length)
        {
            return true;
        }
        
        return false;
    }

    public object Current
    {
        get { return _deliveries[_position]; }
    }

    public void Reset()
    {
        _position = -1;
    }
}

