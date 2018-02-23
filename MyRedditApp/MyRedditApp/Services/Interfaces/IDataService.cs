using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyRedditApp.Services.Interfaces
{
    public interface IDataService<T>
    {
        Task<T> GetItemAsync(string concreteSearch, string after = null);

        Task<IEnumerable<T>> GetItemListAsync(string concreteSearch);
    }
}
