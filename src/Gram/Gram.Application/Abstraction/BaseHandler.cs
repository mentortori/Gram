using Gram.Application.Interfaces;

namespace Gram.Application.Abstraction
{
    public abstract class BaseHandler
    {
        public IDataContext DataContext { get; set; }

        protected BaseHandler(IDataContext dataContext)
        {
            DataContext = dataContext;
        }
    }
}