using System;
using System.Threading.Tasks;

namespace TechCalendar.Web.Handler
{
    public interface ICommandHandler<TInput>
    {
        Task HandleAsync(TInput query);
    }

    public interface ICommandHandler<TInput, TOutput>
    {
        Task<TOutput> HandleAsync(TInput query);
    }
}