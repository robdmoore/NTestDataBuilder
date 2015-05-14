using System;
using System.Linq;

namespace TestStack.Dossier.Factories
{
    /// <summary>
    /// Builds the object using the constructor with the most arguments.
    /// </summary>
    public class ConstructorFactory : IFactory
    {
        /// <inheritdoc />
        public virtual TObject BuildObject<TObject, TBuilder>(TestDataBuilder<TObject, TBuilder> builder)
            where TObject : class
            where TBuilder : TestDataBuilder<TObject, TBuilder>, new()
        {
            var longestConstructor = Reflector.GetLongestConstructor<TObject>();

            if (longestConstructor == null)
                throw new InvalidOperationException("Could not find callable constructor for class " + typeof(TObject).Name);

            var parameterValues = longestConstructor
                .GetParameters()
                .Select(x => Reflector.GetPropertyValueFromTestDataBuilder(x.Name, x.ParameterType, typeof(TObject), typeof(TBuilder), builder))
                .ToArray();

            return (TObject) longestConstructor.Invoke(parameterValues);
        }
    }
}