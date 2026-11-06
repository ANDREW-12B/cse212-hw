using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Add items with different priorities, with the highest priority
    // item added last.
    // Expected Result: Items should be removed from highest to lowest priority.
    // Defect(s) Found: The original loop condition excluded the last queue item,
    // potentially missing the item with the highest priority.
    public void TestPriorityQueue_1()
    {
        var priorityQueue = new PriorityQueue();

        priorityQueue.Enqueue("Low", 1);
        priorityQueue.Enqueue("Medium", 3);
        priorityQueue.Enqueue("High", 5);

        Assert.AreEqual("High", priorityQueue.Dequeue());
        Assert.AreEqual("Medium", priorityQueue.Dequeue());
        Assert.AreEqual("Low", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Add multiple items with the same priority.
    // Expected Result: Items should be removed in the order they were added (FIFO).
    // Defect(s) Found: The original comparison used >=, causing later items
    // with equal priorities to be selected before earlier items.
    public void TestPriorityQueue_2()
    {
        var priorityQueue = new PriorityQueue();

        priorityQueue.Enqueue("First", 5);
        priorityQueue.Enqueue("Second", 5);
        priorityQueue.Enqueue("Third", 5);

        Assert.AreEqual("First", priorityQueue.Dequeue());
        Assert.AreEqual("Second", priorityQueue.Dequeue());
        Assert.AreEqual("Third", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Attempt to dequeue from an empty queue.
    // Expected Result: An InvalidOperationException with the message
    // "The queue is empty." should be thrown.
    // Defect(s) Found: None. The original implementation handled this requirement correctly.
    public void TestPriorityQueue_EmptyQueue()
    {
        var priorityQueue = new PriorityQueue();

        var exception = Assert.ThrowsException<InvalidOperationException>(
            () => priorityQueue.Dequeue());

        Assert.AreEqual("The queue is empty.", exception.Message);
    }

    [TestMethod]
    // Scenario: Add two items and dequeue them one after another.
    // Expected Result: Each item should be removed after being dequeued.
    // The queue should throw an exception when all items have been removed.
    // Defect(s) Found: The original Dequeue method returned the selected
    // value without removing the item from the queue.
    public void TestPriorityQueue_RemovesItems()
    {
        var priorityQueue = new PriorityQueue();

        priorityQueue.Enqueue("First", 3);
        priorityQueue.Enqueue("Second", 2);

        Assert.AreEqual("First", priorityQueue.Dequeue());
        Assert.AreEqual("Second", priorityQueue.Dequeue());

        Assert.ThrowsException<InvalidOperationException>(
            () => priorityQueue.Dequeue());
    }
}