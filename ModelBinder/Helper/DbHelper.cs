namespace ModelBinder.Helper
{
    public class DbHelper : IDbHelper
    {
        public Task<Dictionary<string, bool>> GetOptions()
        {
            var options = new Dictionary<string, bool>
            {
                {"true", true},
                {"false", false},
                {"t", true},
                {"f", false},
                {"1", true},
                {"0", false},
            };
            return Task.FromResult(options);
        }

    }
}
