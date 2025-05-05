using System.Reflection;

namespace SimpleStocker.Api.Util
{
    public class Mapper
    {
        public static TDestination Map<TDestination>(object source)
        where TDestination : new()
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            TDestination destination = new();

            PropertyInfo[] sourceProps = source.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
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
