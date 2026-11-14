using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Add three items with different priorities. The item with the highest priority is added last. 
    // Expected Result: The highest-priority item is dequeued first. 
    // Defect(s) Found: The loop does not check the last item in the queue, 
    // so it can return an item with a lower priority than the highest-priority item. 
    public void TestPriorityQueue_1()
    {
        var priorityQueue = new PriorityQueue();

        priorityQueue.Enqueue("Low", 1);
        priorityQueue.Enqueue("Medium", 5);
        priorityQueue.Enqueue("Highest", 10);

        Assert.AreEqual("Highest", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Add two items with equal priority and one item with lower priority.
    // Expected Result: Equal-priority items are dequeued in the order they were added,
    // followed by the lower-priority item.
    // Defect(s) Found: The >= comparison selects the later item when priorities 
    // are equal, instead of peserving FIFO order.
    public void TestPriorityQueue_2()
    {
        var priorityQueue = new PriorityQueue();

        priorityQueue.Enqueue("First", 5);
        priorityQueue.Enqueue("Second", 5);
        priorityQueue.Enqueue("Lower", 1);

        Assert.AreEqual("First", priorityQueue.Dequeue());
        Assert.AreEqual("Second", priorityQueue.Dequeue());
        Assert.AreEqual("Lower", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Attempt to dequeue from an empty priority queue.
    // Expected Result: InvalidOperationException with the message "The queue is empty."
    // Defect(s) Found: None. The correct exception and message were returned. 
    public void TestPriorityQueue_3()
    {
        var priorityQueue = new PriorityQueue();

        var exception = Assert.ThrowsException<InvalidOperationException>(
            () => priorityQueue.Dequeue()
        );

        Assert.AreEqual("The queue is empty.", exception.Message);
    }


[TestMethod]
// Scenario: Add one item and dequeue it twice.
// Expected Result: The first dequeue returns the item.
// The second dequeue throws InvalidOperationException.
// Defect(s) Found: Dequeue returns the item but does not remove it 
// from the queue. Calling Dequeue again returns the same item. 
public void TestPriorityQueue_RemovesDequeuedItems()
{
    var priorityQueue = new PriorityQueue();

    priorityQueue.Enqueue("Apple", 3);

    Assert.AreEqual("Apple", priorityQueue.Dequeue());

    var exception = Assert.ThrowsException<InvalidOperationException>(
        () => priorityQueue.Dequeue()
    );

    Assert.AreEqual("The queue is empty.", exception.Message);
}


}