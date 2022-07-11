﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OOPs_demo.Classes;
using Microsoft.Data.SqlClient;
using System.Data;
using OOPs_demo.Services;

namespace OOPs_demo
{
    class Programs
    {
        private const string CONNECTION_STRING = @"Data Source=(local);Initial Catalog=BAIS3150CodeSampleSsytem;Integrated Security=true";
        public bool AddProgram (string programCode, string description)
        {
            bool success = false;

            SqlConnection NWConnection = new SqlConnection();
            NWConnection.ConnectionString = CONNECTION_STRING;

            NWConnection.Open();

            SqlCommand NWAddProgramCommand = CreateSqlCommandSP(NWConnection, "AddProgram");

            SqlParameter NWAddProgramParameter;

            NWAddProgramParameter = CreateSqlParameter("@ProgramCode", SqlDbType.VarChar, programCode);
            NWAddProgramCommand.Parameters.Add(NWAddProgramParameter);

            NWAddProgramParameter = CreateSqlParameter("@Description", SqlDbType.VarChar, description);
            NWAddProgramCommand.Parameters.Add(NWAddProgramParameter);

            NWAddProgramCommand.ExecuteNonQuery();

            NWConnection.Close();

            success = true;

            return success;
        }

        public Classes.Program GetProgram(string programCode)
        {
            SqlConnection NWConnection = new SqlConnection();
            NWConnection.ConnectionString = CONNECTION_STRING;

            NWConnection.Open();

            SqlCommand NWGetProgramCommand = CreateSqlCommandSP(NWConnection, "GetProgram");

            SqlParameter NWGetProgramParameter = CreateSqlParameter("@ProgramCode", SqlDbType.VarChar, programCode);
            NWGetProgramCommand.Parameters.Add(NWGetProgramParameter);

            SqlDataReader DataReader = NWGetProgramCommand.ExecuteReader();

            Classes.Program existingProgram = new Classes.Program();
            if (DataReader.HasRows)
            {
                DataReader.Read();
                existingProgram = new Classes.Program
                {
                    ProgramCode = DataReader["ProgramCode"].ToString(),
                    Description = DataReader["Description"].ToString()
                };
                Students StudentManager = new Students();
                existingProgram.Students = StudentManager.GetStudents(existingProgram.ProgramCode);
            }

            NWConnection.Close();

            return existingProgram;
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
