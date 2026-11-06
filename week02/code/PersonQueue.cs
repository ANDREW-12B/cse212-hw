/// <summary>
/// A basic implementation of a FIFO Queue.
/// </summary>
public class PersonQueue
{
    private readonly List<Person> _queue = new();

    public int Length => _queue.Count;

    /// <summary>
    /// Add a person to the back of the queue.
    /// </summary>
    public void Enqueue(Person person)
    {
        _queue.Add(person);
    }

    /// <summary>
    /// Remove and return the person at the front of the queue.
    /// </summary>
    public Person Dequeue()
    {
        if (_queue.Count == 0)
        {
            throw new InvalidOperationException("The queue is empty.");
        }

        Person person = _queue[0];
        _queue.RemoveAt(0);

        return person;
    }

    public bool IsEmpty()
    {
        return _queue.Count == 0;
    }

    public override string ToString()
    {
        return $"[{string.Join(", ", _queue)}]";
    }
}