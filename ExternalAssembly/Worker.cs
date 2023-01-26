namespace ExternalAssembly;

/// <summary>
/// Class which simulates work in progress.
/// </summary>
public class Worker
{
    #region Events

    public event EventHandler<ProgressEventArgs>? ProgressUpdate;

    #endregion

    #region Methods

    /// <summary>
    /// Simulates work.
    /// </summary>
    public void DoWork()
    {
        for (int i = 1; i <= 100; i++)
        {
            // Simulate an expensive operation.
            Thread.Sleep(10);

            // Report a progress update.
            ProgressUpdate?.Invoke(this, new ProgressEventArgs($"Working ({i}%)"));
        }
    }

    #endregion
}