using KittyCare.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace KittyCare.Repositories
{
    public class ProviderRepository : IProviderRepository
    {
        private readonly IConfiguration _config;

        // The constructor accepts an IConfiguration object as a parameter. This class comes from the ASP.NET framework and is useful for retrieving things out of the appsettings.json file like connection strings.
        public ProviderRepository(IConfiguration config)
        {
            _config = config;
        }

        public SqlConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            }
        }

        public List<Provider> GetAllProviders()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT p.Id, p.FirstName AS ProviderFirstName, p.LastName AS ProviderLastName, p.ImageUrl, p.NeighborhoodId, n.Name AS NeighborhoodName
                                        FROM Provider p
                                        LEFT JOIN Neighborhood n ON p.NeighborhoodId = n.Id";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<Provider> providers = new List<Provider>();
                        while (reader.Read())
                        {
                            Provider provider = new Provider
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                ImageUrl = reader.GetString(reader.GetOrdinal("ImageUrl")),
                                NeighborhoodId = reader.GetInt32(reader.GetOrdinal("NeighborhoodId")),
                                FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                                LastName = reader.GetString(reader.GetOrdinal("LastName")),
                                Neighborhood = new Neighborhood()
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("NeighborhoodId")),
                                    Name = reader.GetString(reader.GetOrdinal("NeighborhoodName"))
                                }
                            };

                            providers.Add(provider);
                        }

                        return providers;
                    }
                }
            }
        }

        public Provider GetProviderById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT p.Id, p.FirstName AS ProviderFirstName, p.LastName AS ProviderLastName, p.ImageUrl, p.NeighborhoodId, n.Name AS NeighborhoodName
                        FROM Provider p
                        LEFT JOIN Neighborhood n ON p.NeighborhoodId = n.Id
                        WHERE p.Id = @id
                    ";

                    cmd.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Provider provider = new Provider
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                                LastName = reader.GetString(reader.GetOrdinal("LastName")),
                                ImageUrl = reader.GetString(reader.GetOrdinal("ImageUrl")),
                                NeighborhoodId = reader.GetInt32(reader.GetOrdinal("NeighborhoodId")),
                                Neighborhood = new Neighborhood()
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("NeighborhoodId")),
                                    Name = reader.GetString(reader.GetOrdinal("NeighborhoodName"))
                                }
                            };

                            return provider;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }

        public List<Provider> GetProvidersInNeighborhood(int neighborhoodId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                SELECT Id, FirstName, LastName, ImageUrl, NeighborhoodId
                FROM Provider
                WHERE NeighborhoodId = @neighborhoodId
            ";

                    cmd.Parameters.AddWithValue("@neighborhoodId", neighborhoodId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                        List<Provider> providers = new List<Provider>();
                        while (reader.Read())
                        {
                            Provider provider = new Provider
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                                LastName = reader.GetString(reader.GetOrdinal("LastName")),
                                ImageUrl = reader.GetString(reader.GetOrdinal("ImageUrl")),
                                NeighborhoodId = reader.GetInt32(reader.GetOrdinal("NeighborhoodId"))
                            };

                            providers.Add(provider);
                        }

                        return providers;
                    }
                }
            }
        }

        public void AddProvider(Provider provider)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    INSERT INTO Provider (FirstName, LastName, ImageUrl, NeighborhoodId)
                    OUTPUT INSERTED.ID
                    VALUES (@name, @ImageUrl, @neighborhoodId);
                ";

                    cmd.Parameters.AddWithValue("@firstName", provider.FirstName);
                    cmd.Parameters.AddWithValue("@lastName", provider.LastName);
                    cmd.Parameters.AddWithValue("@ImageUrl", provider.ImageUrl);
                    cmd.Parameters.AddWithValue("@neighborhoodId", provider.NeighborhoodId);

                    int id = (int)cmd.ExecuteScalar();

                    provider.Id = id;
                }
            }
        }
    }
}
