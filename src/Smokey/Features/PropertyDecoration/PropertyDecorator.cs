using System;

namespace Smokey.Features.PropertyDecoration
{
    public class PropertyDecorator<T> : IPropertyDecorator<T>
    {
        private readonly Func<T> _valueProvider;
        private readonly Action<T> _getterDecorator;
        private readonly Action<T> _setterDecorator;
        private T _value;

        protected PropertyDecorator(
            Func<T> valueProvider = null, 
            Action<T> getterDecorator = null, 
            Action<T> setterDecorator = null)
        {
            _valueProvider = valueProvider;
            _getterDecorator = getterDecorator;
            _setterDecorator = setterDecorator;
        }
        
        public T Value
        {
            get
            {
                if (_valueProvider is not null)
                {
                    _value = _valueProvider.Invoke();
                }
                
                _getterDecorator?.Invoke(_value);
                
                return _value;
            }
            set
            {
                _setterDecorator?.Invoke(value);
                _value = value;
            }
        }
        
        public void Invoke() => _ = Value;
    }
}
