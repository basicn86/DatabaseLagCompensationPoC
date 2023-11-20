namespace DatabaseLagCompensationPoC
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            CacheDatabase cacheDatabase = new CacheDatabase();

            cacheDatabase.ResetTable();

            //get all messages
            Console.WriteLine("Getting all messages once...");
            var messages = await cacheDatabase.GetMessages();

            Console.WriteLine("Getting all messages twice...");
            messages = await cacheDatabase.GetMessages();

            Console.WriteLine("Getting all messages three times...");
            messages = await cacheDatabase.GetMessages();

            //print out all messages
            Console.WriteLine("All messages:");
            foreach (var msg in messages)
            {
                Console.WriteLine($"Id: {msg.Id}, Name: {msg.Name}, Content: {msg.Content}");
            }
            Console.WriteLine();

            //Update ALL messages
            Console.WriteLine("Updating all messages...");
            foreach (var msg in messages)
            {
                await cacheDatabase.UpdateMessage(msg.Id);
            }

            //get all messages again and print them out
            Console.WriteLine("Getting all messages again...");
            messages = await cacheDatabase.GetMessages();
            Console.WriteLine("All messages on client:");
            foreach (var msg in messages)
            {
                Console.WriteLine($"Id: {msg.Id}, Name: {msg.Name}, Content: {msg.Content}");
            }
            messages = cacheDatabase.GetCurrentServerMessages();
            Console.WriteLine("All messages on server:");
            foreach (var msg in messages)
            {
                Console.WriteLine($"Id: {msg.Id}, Name: {msg.Name}, Content: {msg.Content}");
            }
            Console.WriteLine();

            //wait for updates to complete
            Console.WriteLine("Waiting for updates to complete");
            await cacheDatabase.WaitForUpdates();
            Console.WriteLine("Updates are completed!");
            Console.WriteLine("All messages on server:");
            messages = cacheDatabase.GetCurrentServerMessages();
            foreach (var msg in messages)
            {
                Console.WriteLine($"Id: {msg.Id}, Name: {msg.Name}, Content: {msg.Content}");
            }
        }
    }
}