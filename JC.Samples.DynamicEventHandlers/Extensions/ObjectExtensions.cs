using System.Reflection;

namespace JC.Samples.DynamicEventHandlers.Extensions;

/// <summary>
/// Contains extension methods that deal with objects.
/// </summary>
public static class ObjectExtensions
{
    #region Methods

    /// <summary>
    /// Adds an event handler to an object dynamically.
    /// </summary>
    /// <typeparam name="T1">The target object type</typeparam>
    /// <typeparam name="T2">The handler source object type</typeparam>
    /// <param name="target">The target object</param>
    /// <param name="eventName">The name of the event to subcribe to</param>
    /// <param name="handlerName">The name of the method that will handle the event</param>
    /// <param name="handlerSource">The source object containing the handler method</param>
    /// <returns>A reference to the event handler delegate that was added</returns>
    /// <exception cref="MissingMemberException"></exception>
    /// <exception cref="MissingMethodException"></exception>
    public static Delegate AddDynamicEventHandler<T1, T2>(
        this T1 target,
        string eventName,
        string handlerName,
        T2 handlerSource) where T1 : class where T2 : class
    {
        EventInfo? eventInfo = target.GetType().GetEvent(eventName);

        if (eventInfo == null || eventInfo.EventHandlerType == null)
        {
            throw new MissingMemberException($"Could not get event named '{eventName}'");
        }

        MethodInfo? methodInfo = handlerSource.GetType().GetMethod(
            handlerName,
            BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

        if (methodInfo == null)
        {
            throw new MissingMethodException($"Could not get method named '{eventName}'");
        }

        Delegate handler = Delegate.CreateDelegate(
            eventInfo.EventHandlerType, 
            handlerSource,
            methodInfo);

        eventInfo.AddEventHandler(target, handler);

        return handler;
    }

    /// <summary>
    /// Removes an event handler from an object dynamically.
    /// </summary>
    /// <typeparam name="T">The target object type</typeparam>
    /// <param name="target">The target object</param>
    /// <param name="eventName">The name of the event to unsubcribe from</param>
    /// <param name="handler">The event handler delegate to remove</param>
    /// <exception cref="MissingMemberException"></exception>
    public static void RemoveDynamicEventHandler<T>(
        this T target, 
        string eventName, 
        Delegate handler) where T : class
    {
        EventInfo? eventInfo = target.GetType().GetEvent(eventName);

        if (eventInfo == null)
        {
            throw new MissingMemberException($"Could not get event named '{eventName}'");
        }

        eventInfo.RemoveEventHandler(target, handler);
    }

    #endregion
}