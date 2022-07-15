using KittyCare.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System;

namespace KittyCare.Repositories
{
    public class CatRepository : ICatRepository
    {
        private readonly IConfiguration _config;
        public CatRepository(IConfiguration config)
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

        public List<Cat> GetAllCats()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT Id, Name, OwnerId, Breed, ImageUrl
                        FROM Cat
                    ";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<Cat> cats = new List<Cat>();
                        while (reader.Read())
                        {
                            Cat cat = new Cat
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Name = reader.GetString(reader.GetOrdinal("Name")),
                                OwnerId = reader.GetInt32(reader.GetOrdinal("OwnerId")),
                                Breed = reader.GetString(reader.GetOrdinal("Breed")),
                                ImageUrl = !reader.IsDBNull(reader.GetOrdinal("ImageUrl")) ? reader.GetString(reader.GetOrdinal("ImageUrl")) : ""
                            };

                            cats.Add(cat);
                        }

                        return cats;
                    }
                }
            }
        }

        public Cat GetCatById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT Name, OwnerId, Breed, ImageUrl
                                        FROM Cat
                                        WHERE Id = @id";

                    cmd.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Cat cat = new Cat()
                            {
                                Id = id,
                                Name = reader.GetString(reader.GetOrdinal("Name")),
                                OwnerId = reader.GetInt32(reader.GetOrdinal("OwnerId")),
                                Breed = reader.GetString(reader.GetOrdinal("Breed")),
                                ImageUrl = !reader.IsDBNull(reader.GetOrdinal("ImageUrl")) ? reader.GetString(reader.GetOrdinal("ImageUrl")) : " "
                            };
                            return cat;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }

        public void AddCat(Cat cat)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                                        INSERT INTO Cat ([Name], OwnerId, Breed, Notes, ImageUrl)
                                        OUTPUT INSERTED.Id
                                        VALUES (@name, @ownerId, @breed, @notes, @imageUrl)";

                    cmd.Parameters.AddWithValue("@name", cat.Name);
                    cmd.Parameters.AddWithValue("@ownerId", cat.OwnerId);
                    cmd.Parameters.AddWithValue("@breed", cat.Breed);
                    cmd.Parameters.AddWithValue("@imageUrl", cat.ImageUrl == null ? DBNull.Value : cat.ImageUrl);

                    int id = (int)cmd.ExecuteScalar();

                    cat.Id = id;
                }
            }
        }

        public void UpdateCat(Cat cat)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                                        UPDATE Cat
                                        SET
                                            [Name] = @name,
                                            OwnerId = @ownerId,
                                            Breed = @breed,
                                            ImageUrl = @imageUrl
                                        WHERE Id = @id";

                    cmd.Parameters.AddWithValue("@id", cat.Id);
                    cmd.Parameters.AddWithValue("@name", cat.Name);
                    cmd.Parameters.AddWithValue("@ownerId", cat.OwnerId);
                    cmd.Parameters.AddWithValue("@breed", cat.Breed);
                    cmd.Parameters.AddWithValue("@imageUrl", cat.ImageUrl == null ? DBNull.Value : cat.ImageUrl);

                    cmd.ExecuteNonQuery();

                }
            }
        }

        public void DeleteCat(int catId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            DELETE FROM Cat
                            WHERE Id = @id
                        ";

                    cmd.Parameters.AddWithValue("@id", catId);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Cat> GetCatsByOwnerId(int ownerId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                SELECT Id, Name, Breed, ImageUrl, OwnerId 
                FROM Cat
                WHERE OwnerId = @ownerId
            ";

                    cmd.Parameters.AddWithValue("@ownerId", ownerId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                        List<Cat> cats = new List<Cat>();

                        while (reader.Read())
                        {
                            Cat cat = new Cat()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Name = reader.GetString(reader.GetOrdinal("Name")),
                                Breed = reader.GetString(reader.GetOrdinal("Breed")),
                                OwnerId = reader.GetInt32(reader.GetOrdinal("OwnerId"))
                            };

                            if (reader.IsDBNull(reader.GetOrdinal("ImageUrl")) == false)
                            {
                                cat.ImageUrl = reader.GetString(reader.GetOrdinal("ImageUrl"));
                            }

                            cats.Add(cat);
                        }

                        return cats;
                    }
                }
            }
        }
    }
}
