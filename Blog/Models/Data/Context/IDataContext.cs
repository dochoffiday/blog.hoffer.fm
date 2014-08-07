namespace BC.Data.Context
{
    public interface IDataContextFactory
    {
        System.Data.Linq.DataContext Context { get; }

        void SaveAll();
    }

    public class DBDataContext : IDataContextFactory
    {
        #region IDataContextFactory Members

        private System.Data.Linq.DataContext dt;

        public System.Data.Linq.DataContext Context
        {
            get
            {
                if (dt == null)
                    dt = new BC.Data.Context.BCDataContext();

                return dt;
            }
        }

        public void SaveAll()
        {
            dt.SubmitChanges();
        }

        #endregion
    }
}