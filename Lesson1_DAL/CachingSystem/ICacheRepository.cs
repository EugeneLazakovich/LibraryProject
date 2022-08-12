using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lesson1_DAL.CachingSystem
{
    public interface ICacheRepository
    {
        Task SaveAsync(string key, string value);
        Task<string> GetAsync(string key);
    }
}
