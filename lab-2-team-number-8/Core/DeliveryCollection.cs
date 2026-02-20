using lab_2_team_number_8;
using System.Collections;

public class DeliveryCollection : IEnumerable
{
    public Delivery[] Deliveries;

    public int DeliveryCount;

    public DeliveryCollection()
    {
        Deliveries = new Delivery[5];
        DeliveryCount = 0;
    }

    public DeliveryCollection(Delivery[] deliveries, int deliveryCount)
    {
        Deliveries = deliveries;
        DeliveryCount = deliveryCount;
    }

    public void IncreaseCollection()
    {
        Delivery[] newDelieveries = new Delivery[Deliveries.Length + 10];
        
        for (int i = 0; i < Deliveries.Length; i++)
        {
            newDelieveries[i] = Deliveries[i];
        }

        Deliveries = newDelieveries;
    }

    public bool IsThisId(int id)
    {
        for (int i = 0; i < DeliveryCount; i++)
        {
            if (Deliveries[i].Id == id)
            {
                return true;
            }
        }

        return false;
    }

    public bool Add(Delivery delivery)
    {
        if (DeliveryCount >= Deliveries.Length)
        {
            IncreaseCollection();
        }

        if (IsThisId(delivery.Id))
        {
            return false;
        }

        Deliveries[DeliveryCount] = delivery;
        DeliveryCount++;
        return true;
    }

    public bool RemoveAt(int index)
    {
        if (index < DeliveryCount && index > -1)
        {
            for (int i = index; i < DeliveryCount - 1; i++)
            {
                Deliveries[i] = Deliveries[i + 1];
            }

            DeliveryCount--;
            Deliveries[DeliveryCount] = null;

            return true;
        }
        
        return false;
    }

    public Delivery? GetAt(int index)
    {
        if (index < DeliveryCount && index > -1)
        {
            return Deliveries[index];
        }
        
        return null;
    }

    public bool SetAt(int index, Delivery delivery)
    {
        if (index <= DeliveryCount && index > -1 && !IsThisId(delivery.Id))
        {
            if (DeliveryCount >= Deliveries.Length)
            {
                IncreaseCollection();
            }

            for (int i = DeliveryCount; i > index; i--)
            {
                Deliveries[i] = Deliveries[i - 1];
            }

            DeliveryCount++;
            Deliveries[index] = delivery;

            return true;
        }

        return false;
    }

    public int Count()
    {
        return DeliveryCount;
    }

    public IEnumerator GetEnumerator()
    {
        return new DeliveryEnumerator(Deliveries);
    }
}

