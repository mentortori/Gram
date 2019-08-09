using Gram.Application.Interfaces;

namespace Gram.Application.Abstraction
{
    public abstract class BaseHandler
    {
        protected IDataContext DataContext { get; }

        protected BaseHandler(IDataContext dataContext)
        {
            DataContext = dataContext;
        }
    }
}