using KittyCare.Models;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace KittyCare.Repositories
{
    public interface IProvisionRepository
    {
        public List<Provision> GetAllProvisions();
        List<Provision> GetProvisionsByProviderId(int id);
        public void AddProvision(Provision provision);
        public void DeleteProvision(int provisionId);
    }
}
