using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using OOPs_demo.Classes;
using Microsoft.Data.SqlClient;

namespace OOPs_demo.Services
{
    class Students
    {
        private const string CONNECTION_STRING = @"Data Source=(local);Initial Catalog=BAIS3150CodeSampleSsytem;Integrated Security=true";

        public bool AddStudent(Student acceptedStudent, string programCode)
        {
            bool success = false;

            SqlConnection NWConnection = new SqlConnection();
            NWConnection.ConnectionString = CONNECTION_STRING;

            NWConnection.Open();

            SqlCommand NWAddStudentCommand = CreateSqlCommandSP(NWConnection, "AddStudent");
            
            SqlParameter NWAddStudentParameter;

            NWAddStudentParameter = CreateSqlParameter("@StudentID", SqlDbType.VarChar, acceptedStudent.StudentID);
            NWAddStudentCommand.Parameters.Add(NWAddStudentParameter);

            NWAddStudentParameter = CreateSqlParameter("@FirstName", SqlDbType.VarChar, acceptedStudent.FirstName);
            NWAddStudentCommand.Parameters.Add(NWAddStudentParameter);

            NWAddStudentParameter = CreateSqlParameter("@LastName", SqlDbType.VarChar, acceptedStudent.LastName);
            NWAddStudentCommand.Parameters.Add(NWAddStudentParameter);

            NWAddStudentParameter = CreateSqlParameter("@Email", SqlDbType.VarChar, acceptedStudent.Email);
            NWAddStudentCommand.Parameters.Add(NWAddStudentParameter);

            NWAddStudentParameter = CreateSqlParameter("@ProgramCode", SqlDbType.VarChar, programCode);
            NWAddStudentCommand.Parameters.Add(NWAddStudentParameter);

            NWAddStudentCommand.ExecuteNonQuery();

            NWConnection.Close();

            success = true;

            return success; 
        }

        public Student GetStudent(string studentID)
        {
            SqlConnection NWConnection = new SqlConnection();
            NWConnection.ConnectionString = CONNECTION_STRING;

            NWConnection.Open();

            SqlCommand NWGetStudentCommand = CreateSqlCommandSP(NWConnection, "GetStudent");

            SqlParameter NWGetStudentParameter = CreateSqlParameter("@StudentID", SqlDbType.VarChar, studentID);
            NWGetStudentCommand.Parameters.Add(NWGetStudentParameter);

            SqlDataReader DataReader = NWGetStudentCommand.ExecuteReader();

            Student existingStudent = new Student();
            if (DataReader.HasRows)
            {
                DataReader.Read();
                existingStudent = new Student
                {
                    StudentID = DataReader["StudentID"].ToString(),
                    FirstName = DataReader["FirstName"].ToString(),
                    LastName = DataReader["LastName"].ToString(),
                    Email = DataReader["Email"].ToString(),
                    ProgramCode = DataReader["ProgramCode"].ToString() 
                };
            }
            NWConnection.Close();
            return existingStudent;
        }

        public bool UpdateStudent(Student enrolledStudent)
        {
            bool success = false;

            SqlConnection NWConnection = new SqlConnection();
            NWConnection.ConnectionString = CONNECTION_STRING;

            NWConnection.Open();

            SqlCommand NWUpdateStudentCommand = CreateSqlCommandSP(NWConnection, "UpdateStudent");

            SqlParameter NWUpdateStudentParameter;

            NWUpdateStudentParameter = CreateSqlParameter("@StudentID", SqlDbType.VarChar, enrolledStudent.StudentID);
            NWUpdateStudentCommand.Parameters.Add(NWUpdateStudentParameter);

            NWUpdateStudentParameter = CreateSqlParameter("@FirstName", SqlDbType.VarChar, enrolledStudent.FirstName);
            NWUpdateStudentCommand.Parameters.Add(NWUpdateStudentParameter);

            NWUpdateStudentParameter = CreateSqlParameter("@LastName", SqlDbType.VarChar, enrolledStudent.LastName);
            NWUpdateStudentCommand.Parameters.Add(NWUpdateStudentParameter);

            NWUpdateStudentParameter = CreateSqlParameter("@Email", SqlDbType.VarChar, enrolledStudent.Email);
            NWUpdateStudentCommand.Parameters.Add(NWUpdateStudentParameter);

            NWUpdateStudentParameter = CreateSqlParameter("@ProgramCode", SqlDbType.VarChar, enrolledStudent.ProgramCode);
            NWUpdateStudentCommand.Parameters.Add(NWUpdateStudentParameter);

            NWUpdateStudentCommand.ExecuteNonQuery();
            NWConnection.Close();

            success = true;

            return success;
        }

        public bool DeleteStudent(string studentID)
        {
            bool success = false;

            SqlConnection NWConnection = new SqlConnection();
            NWConnection.ConnectionString = CONNECTION_STRING;

            NWConnection.Open();

            SqlCommand NWDeleteStudentCommand = CreateSqlCommandSP(NWConnection, "DeleteStudent");

            SqlParameter NWDeleteStudentParameter = CreateSqlParameter("@StudentID", SqlDbType.VarChar, studentID);
            NWDeleteStudentCommand.Parameters.Add(NWDeleteStudentParameter);

            NWDeleteStudentCommand.ExecuteNonQuery();
            NWConnection.Close();

            success = true;

            return success;
        }

        public List<Student> GetStudents(string programCode)
        {

            SqlConnection NWConnection = new SqlConnection();
            NWConnection.ConnectionString = CONNECTION_STRING;

            NWConnection.Open();

            SqlCommand NWGetStudentsCommand = new SqlCommand
            {
                Connection = NWConnection,
                CommandType = CommandType.StoredProcedure,
                CommandText = "GetStudentsByProgram"
            };

            SqlParameter NWGetStudentsParameter;

            NWGetStudentsParameter = new SqlParameter
            {
                ParameterName = "@ProgramCode",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = programCode
            };
            NWGetStudentsCommand.Parameters.Add(NWGetStudentsParameter);

            SqlDataReader DataReader = NWGetStudentsCommand.ExecuteReader();

            List<Student> StudentList = new List<Student>();

            if (DataReader.HasRows)
            {
                while (DataReader.Read())
                {
                    Student existingStudent = new Student
                    {
                        StudentID = DataReader["StudentID"].ToString(),
                        FirstName = DataReader["FirstName"].ToString(),
                        LastName = DataReader["LastName"].ToString(),
                        Email = DataReader["Email"].ToString(),
                        ProgramCode = DataReader["ProgramCode"].ToString()
                    };
                    StudentList.Add(existingStudent);
                }
            }

            NWConnection.Close();

            return StudentList;
        }

        private static SqlCommand CreateSqlCommandSP(SqlConnection sqlConnection, string storedProcedureName)
        {
            return new SqlCommand
            {
                Connection = sqlConnection,
                CommandType = CommandType.StoredProcedure,
                CommandText = storedProcedureName
            };
        }

        private static SqlParameter CreateSqlParameter(string parameterName, SqlDbType sqlDbType, string sqlValue)
        {
            return new SqlParameter
            {
                ParameterName = parameterName,
                SqlDbType = sqlDbType,
                Direction = ParameterDirection.Input,
                SqlValue = sqlValue
            };
        }
    }
}
