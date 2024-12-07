using Forms.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forms.Repository.Analytics
{
    public interface IAnalyticsRepository
    {
        Task<List<Answer>> GetAnswersAsync();
        //Task<List<Answer>> GetAnswersAsync();
    }
}
