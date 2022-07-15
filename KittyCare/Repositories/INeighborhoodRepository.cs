using KittyCare.Models;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace KittyCare.Repositories
{
    public interface INeighborhoodRepository
    {
        List<Neighborhood> GetAll();
    }
}
