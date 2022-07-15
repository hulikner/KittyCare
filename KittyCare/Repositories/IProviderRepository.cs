using KittyCare.Models;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace KittyCare.Repositories
{
    public interface IProviderRepository
    {
        List<Provider> GetAllProviders();
        Provider GetProviderById(int id);
        List<Provider> GetProvidersInNeighborhood(int neighborhoodId);
        void AddProvider(Provider provider);
    }
}
