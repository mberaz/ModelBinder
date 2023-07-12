using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ModelBinder.Binders
{
    public class ToListOfIntBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            //get the value from the query string
            var value = bindingContext.HttpContext.Request.Query[bindingContext.FieldName];
             
            var list = value.ToString()
                .Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .Distinct()
                .ToList();

            bindingContext.Result = ModelBindingResult.Success(list);

            return Task.FromResult(0);
        }
    }
}
