namespace ExternalAssembly;

/// <summary>
/// Progress event arguments.
/// </summary>
public class ProgressEventArgs : EventArgs
{
    #region Properties

    public string ProgressMessage { get; set; }

    #endregion

    #region Constructor

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="progressMessage">The progress message</param>
    public ProgressEventArgs(string progressMessage)
    {
        ProgressMessage = progressMessage;
    }

    #endregion
}