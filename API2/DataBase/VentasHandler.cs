﻿using System.Data;
using API2.Mappers;
using System.Data.SqlClient;

namespace API2.DataBase
{
    public class VentasHandler : DBHandler
    {
        public List<Venta> GetVentas(int IdUsuario)
        {
            var ventasMapper = new VentasMapper();
            var query = "SELECT V.Id, V.Comentarios FROM Venta as V " +
                "inner join ProductoVendido as PV on PV.IdVenta = V.Id " +
                "inner join Producto as P on PV.IdProducto = P.Id " +
                "WHERE P.IdUsuario = @IdUsuario";
            var sqlParamenter = new SqlParameter("IdUsuario", SqlDbType.BigInt) { Value = IdUsuario };
            var V = Execute(query, sqlParamenter, ventasMapper);
            return V;
        }
        public static bool CreateNewSale(List<Producto> producto, Venta venta)
        {
            bool result = false;
            string queryInsert = "INSERT INTO Venta (Comentarios, IdUsuario) " +
                                    "VALUES (@comentarios, @IdUsuario)";
            SqlParameter comentarios = new SqlParameter("Comentarios", System.Data.SqlDbType.VarChar) { Value = venta.Comentarios };
            SqlParameter IdUsuario = new SqlParameter("IdUsuario", System.Data.SqlDbType.VarChar) { Value = venta.IdUsuario };
            List<SqlParameter> ParametrosVentas = new List<SqlParameter>();
            ParametrosVentas.Add(comentarios);
            ParametrosVentas.Add(IdUsuario);

            result = DBHandler.InsertUpdate(queryInsert, ParametrosVentas);

            string query = "SELECT TOP (1) [Id] FROM [Venta] ORDER BY Id Desc";
            var idVentas = DBHandler.GetId(query);
            foreach (var prod in producto)
            {
                string queryInsertProductoVendido = "INSERT INTO ProductoVendido (Stock, IdProducto, IdVenta) " +
                                       "VALUES (@Stock, @IdProductom, @IdVenta)";
                SqlParameter Stock = new SqlParameter("Stock", System.Data.SqlDbType.Int) { Value = prod.Stock};
                SqlParameter IdProducto = new SqlParameter("IdProducto", System.Data.SqlDbType.Int) { Value = prod.Id};
                SqlParameter IdVenta = new SqlParameter("IdVenta", System.Data.SqlDbType.Int) { Value = idVentas };
                List<SqlParameter> ParametrosProductosVendidos = new List<SqlParameter>();
                ParametrosProductosVendidos.Add(Stock);
                ParametrosProductosVendidos.Add(IdProducto);
                ParametrosProductosVendidos.Add(IdVenta);
                var Pv = DBHandler.InsertUpdate(queryInsertProductoVendido, ParametrosProductosVendidos);

                // ---- FALTA DESCONTAR EL STOCK DE TABA PRODUCTOS ---- //
            }
                
            return result;
        }
    }
}
