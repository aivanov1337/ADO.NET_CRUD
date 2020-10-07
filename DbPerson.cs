using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace ADOdotNET_CRUD
{
    public sealed class DbPerson
{
        public void AddPerson(Person person)
        {
            using (var connection = new SqlConnection(new DatabaseConnections().GetConnection1()))
            using (var cmd = connection.CreateCommand())
            {
                connection.Open();
                cmd.CommandText = "INSERT INTO People (FirstName, LastName) VALUES(@fname, @lname)";
                cmd.Parameters.AddWithValue("fname", person.FirstName);
                cmd.Parameters.AddWithValue("lname", person.LastName);
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }


        public void RemovePerson(int id)
        {
            using (var connection = new SqlConnection(new DatabaseConnections().GetConnection1()))
            using (var cmd = connection.CreateCommand())
            {
                connection.Open();
                cmd.CommandText = "DELETE FROM People WHERE id = @id";
                cmd.Parameters.AddWithValue("id", id);
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }


        public Person GetPersonById(int id)
        {
            Person person = new Person();
            using (var connection = new SqlConnection(new DatabaseConnections().GetConnection1()))
            using (var cmd = connection.CreateCommand())
            {
                connection.Open();
                cmd.CommandText = "SELECT * FROM People p WHERE p.id = @id";
                cmd.Parameters.AddWithValue("id", id);

                var reader = cmd.ExecuteReader();
              
                reader.Read();
                person.ID = reader.GetInt32(reader.GetOrdinal("id"));
                person.FirstName = reader.GetString(reader.GetOrdinal("FirstName"));
                person.LastName = reader.GetString(reader.GetOrdinal("LastName"));

                connection.Close();
            }
            return person;
        }


        public List<Person> GetPersonByFirstName(string fname)
        {
            List<Person> people = new List<Person>();

            using (var connection = new SqlConnection(new DatabaseConnections().GetConnection1()))
            using (var cmd = connection.CreateCommand())
            {
                connection.Open();
                cmd.CommandText = "SELECT * FROM People p WHERE p.FirstName = @name";
                cmd.Parameters.AddWithValue("name", fname);

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Person person = new Person();
                    person.ID = reader.GetInt32(reader.GetOrdinal("id"));
                    person.FirstName = reader.GetString(reader.GetOrdinal("FirstName"));
                    person.LastName = reader.GetString(reader.GetOrdinal("LastName"));
                    people.Add(person);
                }
                connection.Close();
            }
            return people;
        }

        public void EditPerson(Person person)
        {
            using (var connection = new SqlConnection(new DatabaseConnections().GetConnection1()))
            using (var cmd = connection.CreateCommand())
            {
                connection.Open();
                cmd.CommandText = "UPDATE People SET FirstName = @firstname, LastName = @lastname WHERE id = @id";
                cmd.Parameters.AddWithValue("firstname", person.FirstName);
                cmd.Parameters.AddWithValue("lastname", person.LastName);
                cmd.Parameters.AddWithValue("id", person.ID);
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}
