namespace DatabaseLagCompensationPoC
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Database db = new Database();
            db.ResetTable();

            //get all messages from the database
            Console.WriteLine("Getting Messages from database");
            var messages = await db.GetMessages();


            //loop through each message and spit it out
            foreach (var msg in messages)
            {
                Console.WriteLine($"{msg.Name}: {msg.Content}");
            }
            Console.WriteLine();
        }
    }
}