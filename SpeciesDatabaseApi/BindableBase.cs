using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SpeciesDatabaseApi;

public class BindableBase : INotifyPropertyChanged
{
    private event PropertyChangedEventHandler? _propertyChanged;

    public event PropertyChangedEventHandler? PropertyChanged
    {
        add
        {
            _propertyChanged -= value;
            _propertyChanged += value;
        }
        remove => _propertyChanged -= value;
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        _propertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetAndRaisePropertyIfChanged<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}