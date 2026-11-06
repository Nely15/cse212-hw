using System.IO;

/// <summary>
/// Maintain a Customer Service Queue.  Allows new customers to be 
/// added and allows customers to be serviced.
/// </summary>
public class CustomerService {
    public static void Run() {
        // Example code to see what's in the customer service queue:
        // var cs = new CustomerService(10);
        // Console.WriteLine(cs);

        TextReader originalInput = Console.In;

        // Test Cases
        try
        {
            // Test 1
            // Scenario: Create queues with valid, zero, and negative sizes.
            // Expected Result: Valid size is accepted. Invalid sizes default to 10.
            Console.WriteLine("Test 1: Maximum Queue Size");

            var cs1 = new CustomerService(5);
            var cs2 = new CustomerService(0);
            var cs3 = new CustomerService(-3);

            Console.WriteLine("Valid size: " + cs1);
            Console.WriteLine("Zero size: " + cs2);
            Console.WriteLine("Negative size: " + cs3);

            // Defect(s) Found: The constructor did not correctly handle 
            // zero or negative maximum sizes. These should default to 10.

            Console.WriteLine("=================");


            // Test 2
            // Scenario: Add a new customer to the queue.
            // Expected Result: The customer is added successfully.  
            Console.WriteLine("Test 2: Add New Customer");

            var cs4 = new CustomerService(5);

            Console.SetIn(new StringReader(
                "Selena Gomez\n12345\nBilling question\n"));

            cs4.AddNewCustomer();

            Console.WriteLine(cs4);
            
            // Defect(s) Found: The customer was not being added correctly to 
            //the queue. The customer object needed to be created and added. 

            Console.WriteLine("=================");


            // Add more Test Cases As Needed Below
            // Test 3
            // Scenario: Try to add a third customer to a queue with a maximum size of 2.
            // Expected Result: An error message appears and the que stays at 2 customers.  
            Console.WriteLine("Test 3: Add Customer to Full Queue");

            var cs5 = new CustomerService(2);

            Console.SetIn(new StringReader(
                "Selena Gomez\n12345\nBilling question\n" +
                "Taylor Swift\n15515\nAccount question\n" +
                "William Levi\n54321\nTechnical question\n"));

            cs5.AddNewCustomer();
            cs5.AddNewCustomer();
            cs5.AddNewCustomer();

            Console.WriteLine(cs5);
            
            // Defect(s) Found: The program did not properly check 
            // whether the queue was full before adding another customer.

            Console.WriteLine("=================");

            
            
            // Test 4
            // Scenario: Add 2 customers and serve the next customer.
            // Expected Result: Selena is displayed and removed. taylor remains.  
            Console.WriteLine("Test 4: Serve Next Customer");

            var cs6 = new CustomerService(5);
            // Add 2 customers directly to the queue for this test.
            cs6._queue.Add(new Customer("Selena Gomez", "12345" , "Billing question"));
            cs6._queue.Add(new Customer("Taylor Swift", "15515" , "Account question"));
            
            Console.WriteLine("Queue before serving:");
            Console.WriteLine(cs6);

            Console.WriteLine("Customer Served:");
            cs6.ServeCustomer();

            Console.WriteLine("Queue after serving:");
            Console.WriteLine(cs6);

            // Defect(s) Found: The program did not correctly serve the first 
            // customer in the queue and remove them after being served.

            Console.WriteLine("=================");

            
            // Test 5
            // Scenario: Try to serve a customer from an empty queue.
            // Expected Result: An errosmessage appears without crashing.  
            Console.WriteLine("Test 5:Serve Customer from Empty Queue");

            var cs7 = new CustomerService(5);

            cs7.ServeCustomer();

            Console.WriteLine(cs7);

            // Defect(s) Found: The program did not properly check 
            // whether the queue was empty before trying to serve a customer

            Console.WriteLine("=================");

        }

        finally

        {
            Console.SetIn(originalInput);
        }

    }

    private readonly List<Customer> _queue = new();
    private readonly int _maxSize;

    public CustomerService(int maxSize) {
        if (maxSize <= 0)
            _maxSize = 10;
        else
            _maxSize = maxSize;
    }

    /// <summary>
    /// Defines a Customer record for the service queue.
    /// This is an inner class.  Its real name is CustomerService.Customer
    /// </summary>
    private class Customer {
        public Customer(string name, string accountId, string problem) {
            Name = name;
            AccountId = accountId;
            Problem = problem;
        }

        private string Name { get; }
        private string AccountId { get; }
        private string Problem { get; }

        public override string ToString() {
            return $"{Name} ({AccountId})  : {Problem}";
        }
    }

    /// <summary>
    /// Prompt the user for the customer and problem information.  Put the 
    /// new record into the queue.
    /// </summary>
    private void AddNewCustomer() {
        // Verify there is room in the service queue
        if (_queue.Count >= _maxSize) {
            Console.WriteLine("Maximum Number of Customers in Queue.");
            return;
        }

        Console.Write("Customer Name: ");
        var name = Console.ReadLine()!.Trim();
        Console.Write("Account Id: ");
        var accountId = Console.ReadLine()!.Trim();
        Console.Write("Problem: ");
        var problem = Console.ReadLine()!.Trim();

        // Create the customer object and add it to the queue
        var customer = new Customer(name, accountId, problem);
        _queue.Add(customer);
    }

    /// <summary>
    /// Dequeue the next customer and display the information.
    /// </summary>
    private void ServeCustomer() {

        if (_queue.Count == 0) {
        
            Console.WriteLine("No customers in queue.");
            return;
        }

        var customer = _queue[0];
        Console.WriteLine(customer);

        _queue.RemoveAt(0);
       
    }

    /// <summary>
    /// Support the WriteLine function to provide a string representation of the
    /// customer service queue object. This is useful for debugging. If you have a 
    /// CustomerService object called cs, then you run Console.WriteLine(cs) to
    /// see the contents.
    /// </summary>
    /// <returns>A string representation of the queue</returns>
    public override string ToString() {
        return $"[size={_queue.Count} max_size={_maxSize} => " + string.Join(", ", _queue) + "]";
    }
}