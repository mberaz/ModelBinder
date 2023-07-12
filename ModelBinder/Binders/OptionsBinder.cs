using Microsoft.AspNetCore.Mvc.ModelBinding;
using ModelBinder.Helper;

namespace ModelBinder.Binders
{
    public class OptionsBinder : IModelBinder
    {
        private readonly IDbHelper _dbHelper;

        public OptionsBinder(IDbHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var value = bindingContext.HttpContext.Request.Query[bindingContext.FieldName];

            var options = await _dbHelper.GetOptions();

            var hasInfo = options.TryGetValue(value.ToString().ToLower(), out var result);

            bindingContext.Result = hasInfo ?
                ModelBindingResult.Success(result) :
                ModelBindingResult.Failed();
        }
    }
}
