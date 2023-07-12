using System.Globalization;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ModelBinder.Binders
{
    public class ValueToListBinder<T> : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            //get the value from the query string
            var value = bindingContext.HttpContext.Request.Query[bindingContext.FieldName];

            try
            {
                var list = value.ToString()
                    .Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                    .Select(i => (T)Convert.ChangeType(i, typeof(T), CultureInfo.InvariantCulture))//convert to generic T
                    .Distinct()
                    .ToList();

                bindingContext.Result = ModelBindingResult.Success(list);
            }
            catch
            {
                throw new Exception($"{value} cannot be converted to a list of {nameof(T)}");
            }

            return Task.FromResult(0);
        }
    }
}

