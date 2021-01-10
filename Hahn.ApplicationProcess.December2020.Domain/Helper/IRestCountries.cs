using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.December2020.Domain.Helper
{
    public interface ICountryService
    {
        Task<string> QueryWebService(string name); 
    }
}