using System.Collections.Concurrent;

namespace App.Common;

public static class CallContext<T> {
    private static readonly ConcurrentDictionary<string, AsyncLocal<T>> state = new();

    private static void ValueChanged(string name, AsyncLocalValueChangedArgs<T> args) {
        if (args.ThreadContextChanged && args.CurrentValue == null && args.PreviousValue != null)
            SetData(name, args.PreviousValue);
    }

    /// <summary>
    ///     Stores a given object and associates it with the specified name.
    /// </summary>
    /// <param name="name">The name with which to associate the new item in the call context.</param>
    /// <param name="data">The object to store in the call context.</param>
    public static void SetData(string name, T data) {
        state.GetOrAdd(name, _ => new AsyncLocal<T>(args => ValueChanged(name, args))).Value = data;
    }

    /// <summary>
    ///     Retrieves an object with the specified name from the <see cref="CallContext{T}" />.
    /// </summary>
    /// <typeparam name="T">
    ///     The type of the data being retrieved. Must match the type used when the <paramref name="name" /> was set via
    ///     <see cref="SetData{T}(string, T)" />.
    /// </typeparam>
    /// <param name="name">The name of the item in the call context.</param>
    /// <returns>
    ///     The object in the call context associated with the specified name, or a default value for
    ///     <typeparamref name="T" /> if none is found.
    /// </returns>
    public static T GetData(string name) {
        return state.TryGetValue(name, out var data) ? data.Value : default;
    }
}