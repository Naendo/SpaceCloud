using System.Collections.Generic;
using Coworking.Domain;

namespace Coworking.Application.Interfaces
{
    public interface ICalculator<T> where T : BaseEntity
    {
        decimal Sum(List<T> collection);
        decimal Single(T obj);
        decimal Sum(params T[] obj);
    }
}