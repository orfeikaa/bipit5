﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Service
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "Service1" в коде и файле конфигурации.
    public class Service1 : IService1
    {
        string connectionString = @"workstation id=BIPIT3BIPIT3.mssql.somee.com;packet size=4096;user id=orfeikaa_SQLLogin_1;pwd=o1fnc3stay;data source=BIPIT3BIPIT3.mssql.somee.com;persist security info=False;initial catalog=BIPIT3BIPIT3";

        public DataTable GetData()
        {
            string query = "SELECT ID_ARENDA as 'Код аренды', FIO.FIO as 'ФИО', format(FIO.Data_rochden, 'dd.MM.yyyy') as 'Дата рождения'," +
             " Avto.Firma_avto as 'Фирма автомобиля', Avto.Model_avto as 'Модель автомобиля'," +
             " format(Data_vzitia, 'dd.MM.yyyy') as 'Дата взятия', format(Data_zdachi, 'dd.MM.yyyy') as 'Дата сдачи'," +
             " Avto.Cost_avto as 'Цена аренды за день', DATEDIFF(day, Data_vzitia, Data_zdachi) as 'Кол-во дней аренды'," +
             " Avto.Cost_avto* DATEDIFF(day, Data_vzitia , Data_zdachi) as 'Итогавая цена'" +
             " FROM Arenda, Avto, FIO" +
             " Where FIO.ID_FIO = Arenda.ID_fio AND Avto.ID_AVTO = Arenda.ID_avto";

           

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            dt.TableName = "Arenda";
                            sda.Fill(dt);
                            return dt;
                        }
                    }
                }
            }
        }

        public DataTable FillFIO()
        {
            string query = "SELECT * FROM FIO";
            using (SqlConnection path = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        command.Connection = path;
                        sda.SelectCommand = command;
                        using (DataTable dt = new DataTable())
                        {
                            dt.TableName = "FIO";
                            sda.Fill(dt);
                            return dt;
                        }
                    }
                }
            }
        }
        
        public DataTable FillAvto()
        {
            string query = "SELECT * FROM Avto";
            using (SqlConnection path = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        command.Connection = path;
                        sda.SelectCommand = command;
                        using (DataTable dt = new DataTable())
                        {
                            dt.TableName = "Avto";
                            sda.Fill(dt);
                            return dt;
                        }
                    }
                }
            }
        }



        public void NewRec(string ID_FIO, string ID_AVTO, string Data_vzitia, string Data_zdachi)
        {
            var query = $"INSERT INTO Arenda VALUES ('{ID_FIO}','{ID_AVTO}','{Data_vzitia}','{Data_zdachi}')";
            var connect = new SqlConnection(connectionString);
            connect.Open();
            SqlCommand command = connect.CreateCommand();
            command.CommandText = query;
            command.ExecuteNonQuery();
            connect.Close();
        }



    }
}
