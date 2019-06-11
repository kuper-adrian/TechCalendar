using System;
using System.Threading.Tasks;

namespace TechCalendar.Web.Handler
{
    public interface IQueryHandler<TQuery, TResult>
    {
        Task<TResult> HandleAsync(TQuery query);
    }
}