using FluentValidation.Results;

namespace SimpleStocker.ProductApi.Util
{
    public class ErrorFormater
    {
        public static List<Dictionary<string, string>> FulentValidationResultToDictionaryList(ValidationResult validation)
        {
            return validation.Errors.GroupBy(x => x.PropertyName)
                                 .Select(group => new Dictionary<string, string>
                                 {
                                    { group.Key, group.First().ErrorMessage }
                                 })
                                 .ToList();
        }
    }
}
