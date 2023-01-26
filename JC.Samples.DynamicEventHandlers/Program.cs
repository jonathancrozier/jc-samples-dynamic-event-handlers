using JC.Samples.DynamicEventHandlers;

Console.WriteLine("Running Worker Client...");

var client = new WorkerClient();
client.RunWorker();

Console.WriteLine("Finished running Worker Client");