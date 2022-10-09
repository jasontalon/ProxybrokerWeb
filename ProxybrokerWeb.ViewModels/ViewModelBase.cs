using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Logging;

namespace ProxybrokerWeb.ViewModels;

internal abstract class ViewModelBase<T> where T : class
{
    public T Model { get; set; }
    private Action<object, PropertyChangedEventArgs>? _onPropertyChanged;
    private readonly ILogger _logger;

    protected ViewModelBase(ILogger logger)
    {
        _logger = logger;
    }

    public void Bind(T model, Action<object, PropertyChangedEventArgs>? onPropertyChanged)
    {
        Model = model ?? throw new ArgumentNullException(nameof(model));
        _onPropertyChanged = onPropertyChanged ?? throw new ArgumentNullException(nameof(onPropertyChanged));
    }

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null) =>
        _onPropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}