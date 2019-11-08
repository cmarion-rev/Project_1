namespace Data_Layer
{
    public partial class Repository
    {
        protected readonly MainDbContext myContext = null;

        public Repository(MainDbContext newContext)
        {
            myContext = newContext;
        }
    }
}