﻿using EmployeeRegistrationService.Interface;
using EmployeeRegistrationService.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace EmployeeRegistrationService.Service
{
    public class PlaceInfoService : IPlaceInfoService
    {
        //public string sConStr = "Data Source=PEFLBELH3T;Initial Catalog=EmployeesDB;Integrated Security=True";
        public string sConStr = "Data Source=tcp:sqlpocbyashish.database.windows.net,1433;Initial Catalog=EmployeesDB;Persist Security Info=False;User ID=ashish;Password=Radha@0786;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";
        public int Add(PlaceInfo placeInfo)
        {
            string sQry = "INSERT INTO [PlaceInfo] ([Place],[About],[City],[State],[Country]) " +
                "VALUES('" + placeInfo.Place + "','" + placeInfo.About + "','" + placeInfo.City + "','" +
                placeInfo.State + "','" + placeInfo.Country + "')";
            int retVal = ExecuteCRUDByQuery(sQry);
            return retVal;
        }

        public int AddRange(IEnumerable<PlaceInfo> places)
        {
            string sQry = "INSERT INTO [PlaceInfo] ([Place],[About],[City],[State],[Country]) VALUES";
            string sVal = "";
            foreach (var place in places)
                sVal += "('" + place.Place + "','" + place.About + "','" + place.City + "','" + place.State + "','" + place.Country + "'),";
            sVal = sVal.TrimEnd(',');
            sQry = sQry + sVal;
            int retVal = ExecuteCRUDByQuery(sQry);
            return retVal;
        }

        public PlaceInfo Find(int id)
        {
            PlaceInfo placeInfo = null;
            string sQry = "SELECT * FROM [PlaceInfo] WHERE [Id]=" + id;
            DataTable dtPlaceInfo = ExecuteQuery(sQry);
            if (dtPlaceInfo != null)
            {
                DataRow dr = dtPlaceInfo.Rows[0];
                placeInfo = GetPlaceInfoByRow(dr);
            }
            return placeInfo;
        }

        public IEnumerable<PlaceInfo> GetAll()
        {
            List<PlaceInfo> placeInfos = null;
            string sQry = "SELECT * FROM [PlaceInfo]";
            DataTable dtPlaceInfo = ExecuteQuery(sQry);
            if (dtPlaceInfo != null)
            {
                placeInfos = new List<PlaceInfo>();
                foreach (DataRow dr in dtPlaceInfo.Rows)
                    placeInfos.Add(GetPlaceInfoByRow(dr));
            }
            return placeInfos;
        }

        public int Remove(int id)
        {
            string sQry = "DELETE FROM [PlaceInfo] WHERE [Id]=" + id;
            int retVal = ExecuteCRUDByQuery(sQry);
            return retVal;
        }

        public int Update(PlaceInfo placeInfo)
        {
            string sQry = "UPDATE [PlaceInfo] SET [Place]='" + placeInfo.Place + "',[About]='" + placeInfo.About + "',[City]='" + placeInfo.City + "',[State]='" + placeInfo.State + "',[Country]='" + placeInfo.Country + "' WHERE [Id]=" + placeInfo.Id;
            int retVal = ExecuteCRUDByQuery(sQry);
            return retVal;
        }

        private int ExecuteCRUDByQuery(string strSql)
        {
            //string sConStr = "Data Source=sqlpocbyashish.database.windows.net;Initial Catalog=EmployeesDB;Persist Security Info=True;User ID=ashish;Password=Radha@0786";
            SqlConnection conn = null;
            int iR = 0;
            try
            {
                conn = new SqlConnection(sConStr);
                SqlCommand cmd = new SqlCommand(strSql, conn);
                cmd.CommandType = CommandType.Text;
                conn.Open();
                //Execute the command
                iR = cmd.ExecuteNonQuery();
            }
            catch { iR = 0; }
            finally { if (conn.State != 0) conn.Close(); }
            return iR;
        }

        private DataTable ExecuteQuery(string strSql)
        {
            //string sConStr = "Data Source=.\\SQLExpress;Initial Catalog=BillGatesMoney;Integrated Security=True";
            SqlConnection conn = null;
            DataTable dt = null;
            try
            {
                conn = new SqlConnection(sConStr);
                SqlCommand cmd = new SqlCommand(strSql, conn);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                conn.Open();
                dt = new DataTable();
                //Fill the dataset
                da.Fill(dt);
                if (!(dt.Rows.Count > 0)) dt = null;
            }
            catch { dt = null; }
            finally { if (conn.State != 0) conn.Close(); }
            return dt;
        }

        private PlaceInfo GetPlaceInfoByRow(DataRow dr)
        {
            PlaceInfo placeInfo = new PlaceInfo();
            placeInfo.Id = Convert.ToInt32(dr["Id"]);
            placeInfo.Place = dr["Place"].ToString();
            placeInfo.About = dr["About"].ToString();
            placeInfo.City = dr["City"].ToString();
            placeInfo.State = dr["State"].ToString();
            placeInfo.Country = dr["Country"].ToString();
            return placeInfo;
        }
    }



}


