using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Items with different priorities are added.
    // Expected Result: Items are removed in order of highest to lowest priority.
    // Defect(s) Found:
    // 1. Dequeue did not correctly return items in descending priority order.
    // 2. After removing the highest priority, the next highest was not selected correctly.

    public void TestPriorityQueue_1()
    {
        var queue = new PriorityQueue();

        queue.Enqueue("A", 1);
        queue.Enqueue("B", 5);
        queue.Enqueue("C", 3);

        // Highest priority should come first
        Assert.AreEqual("B", queue.Dequeue());
        Assert.AreEqual("C", queue.Dequeue());
        Assert.AreEqual("A", queue.Dequeue());
    }

    [TestMethod]
    // Scenario: Items with the same priority are added.
    // Expected Result: Items are removed in FIFO order.
    // Defect(s) Found:
    // 1. FIFO order was not preserved for items with equal priority.
    // 2. The implementation returned later elements instead of the earliest inserted.
    public void TestPriorityQueue_2()
    {
        var queue = new PriorityQueue();

        queue.Enqueue("A", 2);
        queue.Enqueue("B", 2);
        queue.Enqueue("C", 2);

        // Same priority → FIFO order
        Assert.AreEqual("A", queue.Dequeue());
        Assert.AreEqual("B", queue.Dequeue());
        Assert.AreEqual("C", queue.Dequeue());
    }

    // Add more test cases as needed below.
    [TestMethod]
    // Defect(s) Found:
    // No defect found. Exception handling works correctly.
    public void TestPriorityQueue_EmptyThrowsException()
    {
        var queue = new PriorityQueue();

        try
        {
            queue.Dequeue();
            Assert.Fail("Exception was not thrown");
        }
        catch (InvalidOperationException ex)
        {
            Assert.AreEqual("The queue is empty.", ex.Message);
        }
    }
}