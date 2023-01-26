using JC.Samples.DynamicEventHandlers.Extensions;
using System.Reflection;

namespace JC.Samples.DynamicEventHandlers;

/// <summary>
/// Interacts with a Worker class from an external assembly.
/// </summary>
public class WorkerClient
{
    #region Methods

    #region Public

    /// <summary>
    /// Runs Worker code from an external assembly.
    /// </summary>
    /// <exception cref="TypeLoadException"></exception>
    public void RunWorker()
    {
        // Load the external assembly.
        Assembly externalAssembly = Assembly.LoadFrom(
            "../../../../ExternalAssembly/bin/Debug/net6.0/ExternalAssembly.dll");

        // Get the Worker class type.
        Type? workerType = externalAssembly.GetType("ExternalAssembly.Worker");

        if (workerType == null)
        {
            throw new TypeLoadException("Could not get Worker type.");
        }

        // Create an instance of the Worker class.
        var worker = Activator.CreateInstance(workerType);
        
        if (worker == null)
        {
            throw new TypeLoadException("Could not create Worker instance.");
        }

        Delegate handler = null!;

        try
        {
            // Wire up the 'ProgressUpdate' event handler.
            handler = worker.AddDynamicEventHandler("ProgressUpdate", nameof(OnProgressUpdate), this);

            // Call the 'DoWork' method dynamically.
            ((dynamic)worker).DoWork();
        }
        finally
        {
            // Unwire the 'ProgressUpdate' event handler.
            worker.RemoveDynamicEventHandler("ProgressUpdate", handler);
        }
    }

    #endregion

    #region Private

    /// <summary>
    /// Handles the 'ProgressUpdate' event.
    /// </summary>
    /// <param name="sender">The object that fired the event</param>
    /// <param name="e">The event arguments</param>
    private void OnProgressUpdate(object sender, EventArgs e)
    {
        dynamic args = e;
        Console.WriteLine("Progress: {0}", args.ProgressMessage);
    }

    #endregion

    #endregion
}