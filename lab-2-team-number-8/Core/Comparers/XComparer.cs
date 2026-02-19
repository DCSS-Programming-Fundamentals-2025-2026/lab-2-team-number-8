using lab_2_team_number_8;
using System.Collections;

public class XComparer : IComparer
{
    public int Compare(object x, object y)
    {
        Delivery d1 = (Delivery)x;
        Delivery d2 = (Delivery)y;

        int priorityResult = d2.Priority.CompareTo(d1.Priority);

        if (priorityResult != 0)
        {
            return priorityResult;
        }

        return d1.Id.CompareTo(d2.Id);
    }
}

