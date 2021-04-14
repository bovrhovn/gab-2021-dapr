using System.Threading.Tasks;
using State_Management_Models;

namespace State_Management_Web.Interfaces
{
    public interface IStateApi
    {
        Task<Person> GetPersonAsync(string email);
        Task<bool> SavePersonAsync(Person person);
    }
}