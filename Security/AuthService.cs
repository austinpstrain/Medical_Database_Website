using System;
using System.Linq;
using System.Security.Authentication;

using Microsoft.AspNetCore.Http;
using MySql.Data.MySqlClient;
using ClinicWeb.Model;
using ClinicWeb.Util;

namespace ClinicWeb.Security
{
    public class AuthService
    {
        public void Login(HttpContext context, string username, string password)
        {
            var account = GetAccountByUsername(username);
            if (account == null)
                throw new ArgumentException("Invalid username");

            if (account.Password != password)
                throw new AuthenticationException("Invalid password");

            context.Response.Cookies.Append("username", account.Username);
            context.Response.Cookies.Append("access", ((int) account.GetAccessLevel()).ToString());
        }

        public void Logout(HttpContext context)
        {
            context.Response.Cookies.Delete("username");
            context.Response.Cookies.Delete("access");
        }

        public Account GetSessionAccount(HttpContext context)
        {
            var username = GetSessionUsername(context);
            if (username == null)
                return null;

            return GetAccountByUsername(username);
        }

        private string GetSessionUsername(HttpContext context)
        {
            return context.Request.Cookies["username"];
        }

        private Account GetAccountByUsername(string username)
        {
            using (var conn = new MySqlConnection(ConnectionStrings.Default))
            {
                conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandText = @"SELECT *
                                    FROM `account`
                                    WHERE username=@username;";
                cmd.Parameters.AddWithValue("@username", username);
                cmd.ExecuteNonQuery();

                using (var reader = cmd.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        return null;
                    }

                    return new EntityMapper().Map<Account>(reader);
                }
            }
        }
    }
}