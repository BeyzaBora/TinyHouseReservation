using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using TinyHouseReservations.Models;

namespace TinyHouseReservations.DataAccess
{
    public class EvRepository
    {
        private readonly DatabaseHelper _db = new DatabaseHelper();

        // Ev sahibi ID'sine göre evleri getir
        public List<Ev> EvleriGetir(int evSahibiId)
        {
            var evler = new List<Ev>();

            using (SqlConnection conn = _db.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Ev WHERE EvSahibiID = @id", conn);
                cmd.Parameters.AddWithValue("@id", evSahibiId);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    evler.Add(new Ev
                    {
                        EvID = (int)reader["EvID"],
                        Baslik = reader["Baslik"].ToString(),
                        Aciklama = reader["Aciklama"].ToString(),
                        Konum = reader["Konum"].ToString(),
                        Fiyat = (decimal)reader["Fiyat"],
                        Durum = (bool)reader["Durum"],
                        GorselYolu = reader["GorselYolu"].ToString()
                    });
                }
            }

            return evler;
        }

        // ID'ye göre ev getir
        public Ev EvGetirById(int evID)
        {
            Ev ev = null;
            using (SqlConnection conn = _db.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Ev WHERE EvID = @id", conn);
                cmd.Parameters.AddWithValue("@id", evID);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    ev = new Ev
                    {
                        EvID = (int)reader["EvID"],
                        Baslik = reader["Baslik"].ToString(),
                        Aciklama = reader["Aciklama"].ToString(),
                        Konum = reader["Konum"].ToString(),
                        Fiyat = (decimal)reader["Fiyat"],
                        Durum = (bool)reader["Durum"],
                        GorselYolu = reader["GorselYolu"].ToString()
                    };
                }
            }
            return ev;
        }

        // Yeni ev ekle
        public void EvEkle(Ev ev)
        {
            using (SqlConnection conn = _db.GetConnection())
            {
                conn.Open();

                // SQL komutunu hazırlıyoruz
                SqlCommand cmd = new SqlCommand("INSERT INTO Ev (Baslik, Aciklama, Konum, Fiyat, Durum, GorselYolu, EvSahibiID) " +
                                                "VALUES (@Baslik, @Aciklama, @Konum, @Fiyat, @Durum, @GorselYolu, @EvSahibiID)", conn);

                // Parametreleri ekliyoruz, veri türlerini belirterek
                cmd.Parameters.Add("@Baslik", SqlDbType.NVarChar).Value = ev.Baslik;
                cmd.Parameters.Add("@Aciklama", SqlDbType.NVarChar).Value = ev.Aciklama;
                cmd.Parameters.Add("@Konum", SqlDbType.NVarChar).Value = ev.Konum;
                cmd.Parameters.Add("@Fiyat", SqlDbType.Decimal).Value = ev.Fiyat;
                cmd.Parameters.Add("@Durum", SqlDbType.Bit).Value = ev.Durum;
                cmd.Parameters.Add("@GorselYolu", SqlDbType.NVarChar).Value = ev.GorselYolu;
                cmd.Parameters.Add("@EvSahibiID", SqlDbType.Int).Value = ev.EvSahibiID;


                // SQL komutunu çalıştırıyoruz
                cmd.ExecuteNonQuery(); // Ev ekleme
            }
        }


        // Ev güncelleme
        public void EvGuncelle(Ev ev)
        {
            using (SqlConnection conn = _db.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Ev SET Baslik = @Baslik, Aciklama = @Aciklama, Konum = @Konum, Fiyat = @Fiyat, Durum = @Durum, GorselYolu = @GorselYolu WHERE EvID = @EvID", conn);

                cmd.Parameters.AddWithValue("@Baslik", ev.Baslik);
                cmd.Parameters.AddWithValue("@Aciklama", ev.Aciklama);
                cmd.Parameters.AddWithValue("@Konum", ev.Konum);
                cmd.Parameters.AddWithValue("@Fiyat", ev.Fiyat);
                cmd.Parameters.AddWithValue("@Durum", ev.Durum);
                cmd.Parameters.AddWithValue("@GorselYolu", ev.GorselYolu);
                cmd.Parameters.AddWithValue("@EvID", ev.EvID);

                cmd.ExecuteNonQuery(); // Ev güncelleme
            }
        }

        // Ev silme
        public void EvSil(int evID)
        {
            using (SqlConnection conn = _db.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM Ev WHERE EvID = @EvID", conn);
                cmd.Parameters.AddWithValue("@EvID", evID);

                cmd.ExecuteNonQuery(); // Ev silme
            }
        }
    }
}