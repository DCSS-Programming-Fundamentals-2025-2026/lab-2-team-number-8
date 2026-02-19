namespace lab_2_team_number_8
{
    public class DeliveryManager
    {
        private Delivery[] deliveries = new Delivery[200];
        private int deliveryCount = 0;
        private int nextId = 1;

        public bool Add(string title, string address, PriorityLevel p, out int id)
        {
            if (deliveryCount >= deliveries.Length) { id = -1; return false; }
            id = nextId++;
            deliveries[deliveryCount++] = new Delivery(id, title, address, p);
            return true;
        }

        public bool Edit(int id, string title, string address, PriorityLevel p)
        {
            int index = FindIndexById(id);
            if (index == -1) return false;
            deliveries[index].Update(title, address, p);
            return true;
        }

        public bool MarkDone(int id)
        {
            int index = FindIndexById(id);
            if (index == -1) return false;
            deliveries[index].UpdateStatus(Status.Delivered);
            return true;
        }

        public bool Cancel(int id)
        {
            int index = FindIndexById(id);
            if (index == -1) return false;
            deliveries[index].UpdateStatus(Status.Cancelled);
            return true;
        }

        public Delivery? FindById(int id)
        {
            int index = FindIndexById(id);
            return index != -1 ? deliveries[index] : null;
        }

        public int FindIndexById(int id)
        {
            for (int i = 0; i < deliveryCount; i++)
                if (deliveries[i].Id == id) return i;
            return -1;
        }

        public void SortByPriority()
        {
            for (int i = 0; i < deliveryCount - 1; i++)
                for (int j = 0; j < deliveryCount - i - 1; j++)
                    if (deliveries[j].GetPriorityKey() < deliveries[j + 1].GetPriorityKey())
                    {
                        var temp = deliveries[j];
                        deliveries[j] = deliveries[j + 1];
                        deliveries[j + 1] = temp;
                    }
        }

        public void Summary(out int total, out int done, out int pending, out int cancelled)
        {
            total = deliveryCount; done = 0; pending = 0; cancelled = 0;
            for (int i = 0; i < deliveryCount; i++)
            {
                if (deliveries[i].Status == Status.Delivered) done++;
                else if (deliveries[i].Status == Status.Pending) pending++;
                else if (deliveries[i].Status == Status.Cancelled) cancelled++;
            }
        }

        public int GetCount() => deliveryCount;

        public int CopyAll(Delivery[] buffer)
        {
            int count = 0;
            for (int i = 0; i < deliveryCount && count < buffer.Length; i++)
                buffer[count++] = deliveries[i];
            return count;
        }

        public int CopyPending(Delivery[] buffer)
        {
            int count = 0;
            for (int i = 0; i < deliveryCount && count < buffer.Length; i++)
                if (deliveries[i].Status == Status.Pending)
                    buffer[count++] = deliveries[i];
            return count;
        }

        public void ResetDay(bool resetIds)
        {
            deliveryCount = 0; 
            deliveries = new Delivery[200];
            if (resetIds) nextId = 1;
        }
    }
}