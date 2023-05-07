using FormsProject.Data.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormsProject.Data
{
    public class FormsRepository
    {
        private readonly string _connectionString;

        public FormsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Query forms from database
        /// </summary>
        /// <returns>A list of <see cref="FormModel"/></returns>
        public List<FormModel> GetForms()
        {
            var forms = new List<FormModel>();
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("dbo.Forms_Get", connection))
                {
                    connection.Open();
                    var reader = command.ExecuteReader();

                    if (!reader.HasRows)
                        return forms;

                    
                    while (reader.Read())
                    {
                        var form = new FormModel();

                        form.Id = reader.GetInt32("Id");
                        form.Title = reader.GetString("Title");
                        form.CreatedAt = reader.GetDateTime("CreatedAt");
                        if (!reader.IsDBNull("UpdatedAt"))
                            form.UpdatedAt = reader.GetDateTime("UpdatedAt");

                        forms.Add(form);
                    }
                }
            }

            return forms;
        }

        /// <summary>
        /// Query the database for a form with given id, if the form does not exist a <see langword="null"/> value is returned.
        /// </summary>
        /// <param name="id">The form id to query the database</param>
        /// <returns>A <see cref="FormModel"/> if the form exists or <see langword="null"/> if not.</returns>
        public FormModel? GetForm(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("dbo.Forms_Get_ById", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("id", id);

                    connection.Open();
                    var reader = command.ExecuteReader();

                    if (!reader.HasRows)
                        return null;


                    var form = new FormModel();
                    while (reader.Read())
                    {

                        form.Id = reader.GetInt32("Id");
                        form.Title = reader.GetString("Title");
                        form.CreatedAt = reader.GetDateTime("CreatedAt");
                        if (!reader.IsDBNull("UpdatedAt"))
                            form.UpdatedAt = reader.GetDateTime("UpdatedAt");

                        break;
                    }

                    return form;
                }
            }
        }

        /// <summary>
        /// Create a new form with the given title. 
        /// Note that the method does NOT check for title uniqueness and a <see cref="SqlException"/> might throwned.
        /// </summary>
        /// <param name="title">The title of the form</param>
        /// <returns>The id of the created form</returns>
        public int CreateForm(string title)
        {
            var result = 0;
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("dbo.Forms_Create", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@title", title);

                    connection.Open();
                    var reader = command.ExecuteReader();

                    if (!reader.HasRows)
                        return result;
                    while (reader.Read())
                    {

                        result = reader.GetInt32("Id");

                        break;
                    }

                    return result;
                }
            }
        }

        /// <summary>
        /// Update the title of the form with given form id.
        /// Note that the method does NOT check for title uniqueness and a <see cref="SqlException"/> might throwned.
        /// </summary>
        /// <param name="formId">The form id to update</param>
        /// <param name="title">the new title of the form</param>
        public bool UpdateForm(int formId, string title)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("dbo.Forms_Update", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("id", formId);
                    command.Parameters.AddWithValue("title", title);

                    connection.Open();
                    var rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected == 1;
                }
            }
        }

        /// <summary>
        /// Delete the form with the given id. 
        /// Note that this method does not check for relationships in other tables and a <see cref="SqlException"/> might throwned.
        /// </summary>
        /// <param name="formId">the form id to delete</param>
        public bool DeleteForm(int formId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("dbo.Forms_Delete", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("id", formId);

                    connection.Open();
                    var rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected == 1;
                }
            }
        }

        /// <summary>
        /// Get field of a form of the given id. If form doesn't exist an empty list returned
        /// </summary>
        /// <param name="formId">The form id</param>
        /// <returns>List of field properties</returns>
        public IEnumerable<FormFieldModel> GetFieldsByFormId(int formId)
        {
            var fields = new List<FormFieldModel>();
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("dbo.Fields_Get_ByFormId", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("formId", formId);

                    connection.Open();
                    var reader = command.ExecuteReader();

                    if(!reader.HasRows)
                        return fields;

                    while (reader.Read())
                    {
                        var field = new FormFieldModel 
                        { 
                            Id = reader.GetInt32("Id"),
                            Name = reader.GetString("Name"),
                            FormId = reader.GetInt32("FormId"),
                            Value = Convert.ToDouble(reader["Value"])
                        };

                        fields.Add(field);
                    }
                }
            }

            return fields;
        }

        /// <summary>
        /// Find in database the field with the given id. If field doesn't exist <see langword="null"/> is returned.
        /// </summary>
        /// <param name="formId"></param>
        /// <returns></returns>
        public FormFieldModel? GetFieldById(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("dbo.Fields_Get_ById", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("id", id);

                    connection.Open();
                    var reader = command.ExecuteReader();

                    if (!reader.HasRows)
                        return null;

                    while (reader.Read())
                    {
                        return new FormFieldModel
                        {
                            Id = reader.GetInt32("Id"),
                            Name = reader.GetString("Name"),
                            FormId = reader.GetInt32("FormId"),
                            Value = Convert.ToDouble(reader["Value"])
                        };
                    }

                    return null;
                }
            }
        }

        /// <summary>
        /// Create a new field in database and return the field id. Note that this method does not check unique constraints.
        /// </summary>
        /// <param name="name">the name of the field</param>
        /// <param name="value">the value of the field</param>
        /// <param name="formId">the form id that field belongs</param>
        /// <returns>The field id</returns>
        public int CreateField(string name, double value, int formId)
        {
            var result = 0;
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("dbo.Fields_Create", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@value", value);
                    command.Parameters.AddWithValue("@formId", formId);

                    connection.Open();
                    var reader = command.ExecuteReader();

                    if (!reader.HasRows)
                        return result;
                    while (reader.Read())
                    {
                        result = reader.GetInt32("Id");
                        break;
                    }

                    return result;
                }
            }
        }

        /// <summary>
        /// Update a field in database. Note that this method does not check unique constraints.
        /// </summary>
        /// <param name="id">the id of the field</param>
        /// <param name="name">the new name of the field</param>
        /// <param name="value">the new value of the field</param>
        /// <returns><see langword="true"/> if the operation succeeded otherwise <see langword="false"/></returns>
        public bool UpdateField(int id, string name, double value)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("dbo.Fields_Update", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@value", value);
                    command.Parameters.AddWithValue("@id", id);

                    connection.Open();
                    var rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected == 1;
                }
            }
        }

        /// <summary>
        /// Delete a field from database. Note that this method does not check foreign key constraints.
        /// </summary>
        /// <param name="id">the id of the field to delete</param>
        /// <returns><see langword="true"/> if the operation succeeded otherwise <see langword="false"/></returns>
        public bool DeleteField(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("dbo.Fields_Delete", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@id", id);

                    connection.Open();
                    var rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected == 1;
                }
            }
        }
    }
}
