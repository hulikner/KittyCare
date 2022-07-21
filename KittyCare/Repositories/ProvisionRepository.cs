using KittyCare.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace KittyCare.Repositories
{
    public class ProvisionRepository : IProvisionRepository
    {
        private readonly IConfiguration _config;

        public ProvisionRepository(IConfiguration config)
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

        public List<Provision> GetAllProvisions()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT p.Id, p.Date, p.Duration, p.ProviderId, p.CatId, 
                                               d.Id, d.FirstName, d.LastName, d.ImageUrl, d.NeighborhoodId, 
                                               n.Id, n.Name AS NeighborhoodName, 
                                               c.Id, c.Name AS CatName
                                        FROM Provision p
                                        LEFT JOIN Cat c ON p.CatId = c.Id
                                        LEFT JOIN Provider d ON p.ProviderId = d.Id
                                        LEFT JOIN Neighborhood n ON d.NeighborhoodId = n.Id";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<Provision> provisions = new List<Provision>();
                        while (reader.Read())
                        {
                            Provision provision = new Provision
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Date = reader.GetDateTime(reader.GetOrdinal("Date")),
                                Duration = reader.GetInt32(reader.GetOrdinal("Duration")),
                                ProviderId = reader.GetInt32(reader.GetOrdinal("ProviderId")),
                                CatId = reader.GetInt32(reader.GetOrdinal("CatId")),
                                Provider = new Provider()
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("ProviderId")),
                                    FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                                    LastName = reader.GetString(reader.GetOrdinal("LastName")),
                                    NeighborhoodId = reader.GetInt32(reader.GetOrdinal("NeighborhoodId")),
                                },
                                Neighborhood = new Neighborhood()
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("NeighborhoodId")),
                                    Name = reader.GetString(reader.GetOrdinal("NeighborhoodName")),
                                },
                                Cat = new Cat()
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("CatId")),
                                    Name = reader.GetString(reader.GetOrdinal("CatName")),
                                }
                            };
                            provisions.Add(provision);
                        }
                        return provisions;
                    }
                }
            }
        }

        public void AddProvision(Provision provision)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Provision (Date, Duration, ProviderId, CatId)
                                        OUTPUT INSERTED.ID
                                        VALUES (@date, @duration, @providerId, @catId)";

                    cmd.Parameters.AddWithValue("@date", provision.Date);
                    cmd.Parameters.AddWithValue("@duration", provision.Duration);
                    cmd.Parameters.AddWithValue("@providerId", provision.ProviderId);
                    cmd.Parameters.AddWithValue("@catId", provision.CatId);

                    int id = (int)cmd.ExecuteScalar();

                    provision.Id = id;
                }
            }
        }

        public void DeleteProvision(int provisionId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            DELETE FROM Provision
                            WHERE Id = @id
                        ";

                    cmd.Parameters.AddWithValue("@id", provisionId);

                    cmd.ExecuteNonQuery();
                }
            }
        }
        public List<Provision> GetProvisionsByProviderId(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT Id, Date, Duration, ProviderId, CatId
                                        FROM Provision
                                        WHERE ProviderId = @id";
                    cmd.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<Provision> provisions = new List<Provision>();
                        while (reader.Read())
                        {
                            Provision provision = new Provision()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Date = reader.GetDateTime(reader.GetOrdinal("Date")),
                                Duration = reader.GetInt32(reader.GetOrdinal("Duration")),
                                ProviderId = reader.GetInt32(reader.GetOrdinal("ProviderId")),
                                CatId = reader.GetInt32(reader.GetOrdinal("CatId"))
                            };
                            provisions.Add(provision);
                        }
                        return provisions;
                    }
                }
            }
        }
    }
}
