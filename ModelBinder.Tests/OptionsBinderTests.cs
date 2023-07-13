using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Primitives;
using ModelBinder.Binders;
using ModelBinder.Helper;
using Moq;

namespace ModelBinder.Tests
{
    [TestClass]
    public class OptionsBinderTests
    {
        [TestMethod]
        [DataRow("True", true)]
        [DataRow("true", true)]
        [DataRow("T", true)]
        [DataRow("t", true)]
        [DataRow("1", true)]
        [DataRow("False", false)]
        [DataRow("false", false)]
        [DataRow("F", false)]
        [DataRow("f", false)]
        [DataRow("0", false)]
        public async Task OptionsBinder_ValidValue(string value, bool expectedBool)
        {
            var dbHelper = new DbHelper();
            var binder = new OptionsBinder(dbHelper);

            var context = new Mock<ModelBindingContext>();
            context.Setup(c => c.FieldName).Returns("isValid");
            context.SetupProperty(c => c.Result);

            context.Setup(c => c.HttpContext.Request.Query)
                .Returns(new QueryCollection(new Dictionary<string, StringValues> { { "isValid", value } }));
            var bindingContext = context.Object;

            await binder.BindModelAsync(bindingContext);

            Assert.AreEqual(bindingContext.Result.IsModelSet, true);
            Assert.AreEqual(bindingContext.Result.Model, expectedBool);
        }

        [TestMethod]
        public async Task OptionsBinder_NotValidValue()
        {
            var dbHelper = new DbHelper();
            var binder = new OptionsBinder(dbHelper);

            var context = new Mock<ModelBindingContext>();
            context.Setup(c => c.FieldName).Returns("isValid");
            context.SetupProperty(c => c.Result);

            context.Setup(c => c.HttpContext.Request.Query)
                .Returns(new QueryCollection(new Dictionary<string, StringValues> { { "isValid", "ff" } }));
            var bindingContext = context.Object;

            await binder.BindModelAsync(bindingContext);

            Assert.AreEqual(bindingContext.Result.IsModelSet, false);
            Assert.IsNull(bindingContext.Result.Model);
        }

        [TestMethod]
        public async Task OptionsBinder_NoValueInQueryString()
        {
            var dbHelper = new DbHelper();
            var binder = new OptionsBinder(dbHelper);

            var context = new Mock<ModelBindingContext>();
            context.Setup(c => c.FieldName).Returns("isValid");
            context.SetupProperty(c => c.Result);

            context.Setup(c => c.HttpContext.Request.Query)
                .Returns(new QueryCollection(new Dictionary<string, StringValues>()));
            var bindingContext = context.Object;

            await binder.BindModelAsync(bindingContext);

            Assert.AreEqual(bindingContext.Result.IsModelSet, false);
            Assert.IsNull(bindingContext.Result.Model);
        }
    }
}