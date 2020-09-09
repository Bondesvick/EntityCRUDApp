namespace CodeFirstModel.Data
{
    public static class GlobalConfig
    {
        private static readonly ICrudOperations Operations = new CrudOperations();

        public static ICrudOperations Inject()
        {
            return Operations;
        }
    }
}