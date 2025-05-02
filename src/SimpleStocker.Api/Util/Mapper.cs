using System.Reflection;

namespace SimpleStocker.Api.Util
{
    public static class Mapper
    {
        public static TDestination Map<TSource, TDestination>(TSource source)
        where TDestination : new()
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            TDestination destination = new TDestination();

            PropertyInfo[] sourceProps = typeof(TSource).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            PropertyInfo[] destProps = typeof(TDestination).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var destProp in destProps)
            {
                var sourceProp = sourceProps.FirstOrDefault(p =>
                    p.Name == destProp.Name &&
                    p.PropertyType == destProp.PropertyType &&
                    p.CanRead &&
                    destProp.CanWrite);

                if (sourceProp != null)
                {
                    var value = sourceProp.GetValue(source);
                    destProp.SetValue(destination, value);
                }
            }

            return destination;
        }
    }
}
