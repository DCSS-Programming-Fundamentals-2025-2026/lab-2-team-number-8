using NUnit.Framework;
using lab_2_team_number_8;

namespace lab_2_team_number_8.Tests
{
    [TestFixture]
    public class DeliveryManagerTests
    {
        private DeliveryManager manager;

        [SetUp]
        public void Setup()
        {
            manager = new DeliveryManager();
        }

        [Test]
        public void Add_ValidDelivery_ReturnsTrueAndIncrementsCount()
        {
            bool result = manager.Add("Pizza", "Kyiv", PriorityLevel.High, out int id);
            Assert.IsTrue(result);
            Assert.Greater(id, 0);
            Assert.AreEqual(1, manager.GetCount());
        }

        [Test]
        public void FindById_ExistingId_ReturnsCorrectDelivery()
        {
            manager.Add("Sushi", "Lviv", PriorityLevel.Low, out int id);
            var delivery = manager.FindById(id);
            Assert.IsNotNull(delivery);
            Assert.AreEqual("Sushi", delivery.Title);
            Assert.AreEqual("Lviv", delivery.Address);
        }

        [Test]
        public void FindById_NonExistingId_ReturnsNull()
        {
            var delivery = manager.FindById(999);
            Assert.IsNull(delivery);
        }

        [Test]
        public void Edit_ExistingDelivery_UpdatesData()
        {
            manager.Add("Old Title", "Old Addr", PriorityLevel.Low, out int id);
            bool result = manager.Edit(id, "New Title", "New Addr", PriorityLevel.High);
            var updatedItem = manager.FindById(id);
            Assert.IsTrue(result);
            Assert.AreEqual("New Title", updatedItem.Title);
            Assert.AreEqual(PriorityLevel.High, updatedItem.Priority);
        }

        [Test]
        public void MarkDone_ChangesStatusToDelivered()
        {
            manager.Add("Test", "Addr", PriorityLevel.Medium, out int id);
            bool result = manager.MarkDone(id);
            var item = manager.FindById(id);
            Assert.IsTrue(result);
            Assert.AreEqual(Status.Delivered, item.Status);
        }

        [Test]
        public void SortByPriority_OrdersHighToLow()
        {
            manager.Add("Low Item", "A", PriorityLevel.Low, out int id1);
            manager.Add("High Item", "B", PriorityLevel.High, out int id2);
            manager.Add("Medium Item", "C", PriorityLevel.Medium, out int id3);

            manager.SortByPriority();
            Delivery[] buffer = new Delivery[10];
            manager.CopyAll(buffer);

            Assert.AreEqual(PriorityLevel.High, buffer[0].Priority);
            Assert.AreEqual(PriorityLevel.Medium, buffer[1].Priority);
            Assert.AreEqual(PriorityLevel.Low, buffer[2].Priority);
        }

        [Test]
        public void Summary_CalculatesCorrectly()
        {
            manager.Add("1", "A", PriorityLevel.Low, out int i1);
            manager.Add("2", "B", PriorityLevel.Low, out int i2);
            manager.MarkDone(i2);
            manager.Add("3", "C", PriorityLevel.Low, out int i3);
            manager.Cancel(i3);

            manager.Summary(out int total, out int done, out int pending, out int cancelled);

            Assert.AreEqual(3, total);
            Assert.AreEqual(1, done);
            Assert.AreEqual(1, pending);
            Assert.AreEqual(1, cancelled);
        }

        [Test]
        public void ResetDay_ClearsAllData()
        {
            manager.Add("Test", "Addr", PriorityLevel.High, out int id);
            manager.ResetDay(false);
            Assert.AreEqual(0, manager.GetCount());
            Assert.IsNull(manager.FindById(id));
        }
    }
}