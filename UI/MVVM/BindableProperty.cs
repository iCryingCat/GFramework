using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 可绑定属性
/// 当属性值发生改变时，调用委托
/// </summary>
/// <typeparam name="T"></typeparam>
public class BindableProperty<T>
{
    public delegate void OnValueChangeHandler(T old, T value);

    public OnValueChangeHandler OnValueChanged;

    T _value;
    public T Value
    {
        get => _value;
        set
        {
            if (Equals(_value, value))
                return;
            T oldValue = _value;
            _value = value;
            OnValueChanged?.Invoke(oldValue, _value);
        }
    }
}
