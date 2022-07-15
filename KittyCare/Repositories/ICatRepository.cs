using KittyCare.Models;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace KittyCare.Repositories
{
    public interface ICatRepository
    {
        List<Cat> GetAllCats();
        Cat GetCatById(int id);
        List<Cat> GetCatsByOwnerId(int ownerId);
        void AddCat(Cat owner);
        void UpdateCat(Cat owner);
        void DeleteCat(int id);
    }
}
