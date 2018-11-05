﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aspit.StudentReg.Entities;
using System.Data.SqlClient;
using System.Data;


namespace Aspit.StudentReg.DataAccess
{
    /// <summary>
    /// The repository for AttendanceRegistrations
    /// </summary>
    public class AttendanceRegistrationsRepository : RepositoryBase
    {
        /// <summary>
        /// Intializes a new Repository for <see cref="AttendanceRegistration"/> using the given connection string.
        /// </summary>
        /// <param name="connectionString">the connection string used to connect to the database</param>
        public AttendanceRegistrationsRepository(string connectionString) : base(connectionString)
        {

        }

        /// <summary>
        /// Updates the given <see cref="AttendanceRegistration"/> in the database
        /// </summary>
        /// <param name="attendanceRegistration">The <see cref="AttendanceRegistration"/> to update</param>
        public void Update(AttendanceRegistration attendanceRegistration)
        {
            //TODO create the update method
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates the given <see cref="AttendanceRegistration"/> in the database
        /// </summary>
        /// <param name="attendanceRegistration">The <see cref="AttendanceRegistration"/> to create</param>
        public void Create(AttendanceRegistration attendanceRegistration)
        {
            //TODO create the create method
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets all <see cref="AttendanceRegistration"/>s from the database
        /// </summary>
        /// <returns>a list containing all the <see cref="AttendanceRegistration"/>s</returns>
        public List<AttendanceRegistration> GetAll()
        {
            SqlCommand getCommand = new SqlCommand("SELECT * FROM AttandanceRegistrations");
            DataSet getOutput = Execute(getCommand);

            if(getOutput.Tables.Count < 0)
            {
                throw new DataAccessException("Failed to get any tables from database");
            }
            else
            {
                return ConvertDateRowsIntoAttendanceRegistrations(getOutput.Tables[0].Rows);
            }
        }



        /// <summary>
        /// Gets the AttendanceRegistration with the given id
        /// </summary>
        /// <param name="id">the id</param>
        /// <returns>a AttendanceRegistration from the database</returns>
        public AttendanceRegistration GetFromId(int id)
        {
            SqlCommand getCommand = new SqlCommand("SELECT * FROM AttandanceRegistrations WHERE Id=@Id");
            getCommand.Parameters.AddWithValue("@Id",id);
            DataSet getOutput = Execute(getCommand);

            if(getOutput.Tables.Count < 0)
            {
                throw new DataAccessException("Failed to get any tables from database");
            }
            else
            {
                List<AttendanceRegistration> registrations = ConvertDateRowsIntoAttendanceRegistrations(getOutput.Tables[0].Rows);
                if(registrations.Count != 1)
                {
                    throw new DataAccessException("Failed to get an AttendanceRegistration with the given id");
                }
                else
                {
                    return registrations[0];
                }
            }
        }

        /// <summary>
        /// Gets the given <see cref="Student"/>'s <see cref="AttendanceRegistration"/>s from the database
        /// </summary>
        /// <param name="student">the <see cref="Student"/> to get the <see cref="AttendanceRegistration"/>s from</param>
        /// <returns>a lsit containing all the <see cref="AttendanceRegistration"/>s for the student</returns>
        public List<AttendanceRegistration> GetUsersRegistrations(Student student)
        {
            //TODO create the GetUsersRegistrations method
            throw new NotImplementedException();
        }

        /// <summary>
        /// Takes a <see cref="DataRowCollection"/> and converts all its rows into AttendanceRegistrations
        /// </summary>
        /// <param name="dataRows">The <see cref="DataRowCollection"/> to convert into AttendanceRegistration list</param>
        /// <returns>A list of all the AttendanceRegistrations made from the dataRows</returns>
        private static List<AttendanceRegistration> ConvertDateRowsIntoAttendanceRegistrations(DataRowCollection dataRows)
        {
            if(dataRows is null)
            {
                throw new ArgumentNullException("datarows cannot be null.");
            }

            if(dataRows.Count < 0)
            {
                return new List<AttendanceRegistration>();
            }
            else
            {
                List<AttendanceRegistration> returnList = new List<AttendanceRegistration>();
                foreach(DataRow row in dataRows)
                {
                    AttendanceRegistration registration = default;

                    try
                    {
                        registration.Id = row.Field<int>("MeetingTime");
                        registration.MeetingTime = row.Field<DateTime>("MeetingTime");
                        registration.LeavingTime = row.Field<DateTime>("MeetingTime");
                    }
                    catch(InvalidCastException)
                    {
                        throw new DataAccessException("Failed to convert table row into the needed AttendanceRegistration properties");
                    }

                    returnList.Add(registration);
                }

                return returnList;
            }
        }
    }
}