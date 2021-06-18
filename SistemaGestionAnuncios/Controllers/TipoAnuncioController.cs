using Npgsql;
using SistemaGestionAnuncios.Models.Response;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SistemaGestionAnuncios.Controllers
{
    public class TipoAnuncioController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Get()
        {
            List<TipoAnuncioResponse> anuncios = new List<TipoAnuncioResponse>();

            var dato = ConfigurationManager.AppSettings["ConexionBD"];//"Host=saea98.com;Port=5433;Username=ca_12;Password=4i5r25u8ca12;Database=ca_12";
            var sql = @"SELECT ""Id"", ""Descripcion"" FROM public.""TipoAnuncio"";";
            using (var conn = new NpgsqlConnection(dato))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    using (NpgsqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            TipoAnuncioResponse anun = new TipoAnuncioResponse();
                            anun.Id = dr.GetInt32(0);
                            anun.Descripcion = dr.GetString(1);
                            anuncios.Add(anun);
                        }
                    }
                }
            }


            return  Ok(anuncios);
        }
    }
}
