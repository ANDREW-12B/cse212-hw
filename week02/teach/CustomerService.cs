/// <summary>
/// Maintain a Customer Service Queue.
/// </summary>
public class CustomerService
{
    public static void Run()
    {
        Console.WriteLine("Test 1: Invalid Queue Size");
        var cs = new CustomerService(0);
        Console.WriteLine(cs);

        Console.WriteLine("=================");

        Console.WriteLine("Test 2: Add New Customer");
        cs = new CustomerService(5);
        Console.SetIn(new StringReader(
            "Andrew\nA100\nAccount Problem\n"));
        cs.AddNewCustomer();
        Console.WriteLine(cs);

        Console.WriteLine("=================");

        Console.WriteLine("Test 3: Full Queue");
        cs = new CustomerService(1);
        Console.SetIn(new StringReader(
            "John\nA200\nLogin Problem\nPeter\nA300\nPassword Problem\n"));
        cs.AddNewCustomer();
        cs.AddNewCustomer();
        Console.WriteLine(cs);

        Console.WriteLine("=================");

        Console.WriteLine("Test 4: Serve Customer");
        cs = new CustomerService(5);
        Console.SetIn(new StringReader(
            "Peter\nA400\nPassword Problem\n"));
        cs.AddNewCustomer();
        cs.ServeCustomer();
        Console.WriteLine(cs);

        Console.WriteLine("=================");

        Console.WriteLine("Test 5: Empty Queue");
        cs = new CustomerService(5);
        cs.ServeCustomer();
        Console.WriteLine(cs);
    }

    private readonly List<Customer> _queue = new();
    private readonly int _maxSize;

    public CustomerService(int maxSize)
    {
        _maxSize = maxSize <= 0 ? 10 : maxSize;
    }

    private class Customer
    {
        public Customer(string name, string accountId, string problem)
        {
            Name = name;
            AccountId = accountId;
            Problem = problem;
        }

        private string Name { get; }
        private string AccountId { get; }
        private string Problem { get; }

        public override string ToString()
        {
            return $"{Name} ({AccountId}) : {Problem}";
        }
    }

    private void AddNewCustomer()
    {
        if (_queue.Count >= _maxSize)
        {
            Console.WriteLine("Maximum Number of Customers in Queue.");
            return;
        }

        Console.Write("Customer Name: ");
        var name = Console.ReadLine()!.Trim();

        Console.Write("Account Id: ");
        var accountId = Console.ReadLine()!.Trim();

        Console.Write("Problem: ");
        var problem = Console.ReadLine()!.Trim();

        var customer = new Customer(name, accountId, problem);
        _queue.Add(customer);
    }

    private void ServeCustomer()
    {
        if (_queue.Count == 0)
        {
            Console.WriteLine("No customers in the queue.");
            return;
        }

        var customer = _queue[0];
        _queue.RemoveAt(0);

        Console.WriteLine(customer);
    }

    public override string ToString()
    {
        return $"[size={_queue.Count} max_size={_maxSize} => "
            + string.Join(", ", _queue) + "]";
    }
}