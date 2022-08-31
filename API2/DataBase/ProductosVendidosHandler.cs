﻿using API2.Mappers;
using System.Data;
using System.Data.SqlClient;

namespace API2.DataBase
{
    public class ProductosVendidosHandler : DBHandler
    {
        public static List<ProductoVendidoyProducto> GetProductoVendido(int IdUsuario)
        {
            List<ProductoVendidoyProducto> ListaProductoVendidoyProductos = new List<ProductoVendidoyProducto>();
            var query = "SELECT P.Id as 'PID', P.Descripciones, P.Costo, P.PrecioVenta, p.Stock AS 'StockProductos'," +
                        "p.IdUsuario, PV.Id, Pv.Stock, Pv.IdProducto, Pv.IdVenta FROM Producto AS P " +
                        "INNER JOIN ProductoVendido AS PV on P.Id = PV.IdProducto " +
                        "WHERE P.IdUsuario = @IdUsuario";

            var sqlParameter = new SqlParameter("IdUsuario", SqlDbType.BigInt) { Value = IdUsuario };
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    sqlConnection.Open();
                    sqlCommand.Parameters.Add(sqlParameter);
                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                ProductoVendidoyProducto ProductoVendidoyProductos = new ProductoVendidoyProducto();
                                ProductoVendidoyProductos.Producto.Id = Convert.ToInt32(dataReader["PID"]);
                                ProductoVendidoyProductos.Producto.Descripciones = dataReader["Descripciones"].ToString();
                                ProductoVendidoyProductos.Producto.Costo = Convert.ToDouble(dataReader["Costo"]);
                                ProductoVendidoyProductos.Producto.PrecioVenta = Convert.ToDouble(dataReader["PrecioVenta"]);
                                ProductoVendidoyProductos.Producto.Stock = Convert.ToInt32(dataReader["StockProductos"]);
                                ProductoVendidoyProductos.Producto.IdUsuario = Convert.ToInt32(dataReader["IdUsuario"]);
                                ProductoVendidoyProductos.ProductoVendido.Id = Convert.ToInt32(dataReader["ID"]);
                                ProductoVendidoyProductos.ProductoVendido.Stock = Convert.ToInt32(dataReader["Stock"]);
                                ProductoVendidoyProductos.ProductoVendido.IdProducto = Convert.ToInt32(dataReader["IdProducto"]);
                                ProductoVendidoyProductos.ProductoVendido.IdVenta = Convert.ToInt32(dataReader["IdVenta"]);
                                ListaProductoVendidoyProductos.Add(ProductoVendidoyProductos);
                            }
                        }
                        sqlConnection.Close();
                    }
                }
            }
            return ListaProductoVendidoyProductos;


            // ******* Esta es la manera que más se adecúa a la consigna, pero me pareció mejor realizar sólo una conexion a la baes

            //List<ProductoVendidoyProducto> ListaProductoVendidoyProductos = new List<ProductoVendidoyProducto>();
            //var ListaProductos = ProductoHandler.GetProductos();

            //foreach (var Producto in ListaProductos)
            //{
            //    if (Producto.IdUsuario == IdUsuario)
            //    {
            //        var query = "SELECT * FROM ProductoVendido Where IdProducto = @IdProducto";
            //        var sqlParameter = new SqlParameter("IdProducto", SqlDbType.BigInt) { Value = Producto.Id };
            //        using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            //        {
            //            using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
            //            {
            //                sqlConnection.Open();
            //                sqlCommand.Parameters.Add(sqlParameter);
            //                using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
            //                {
            //                    if (dataReader.HasRows)
            //                    {
            //                        while (dataReader.Read())
            //                        {
            //                            ProductoVendidoyProducto ProductoVendidoyProductos = new ProductoVendidoyProducto();
            //                            ProductoVendidoyProductos.Producto = Producto;
            //                            ProductoVendidoyProductos.ProductoVendido.Id = Convert.ToInt32(dataReader["ID"]);
            //                            ProductoVendidoyProductos.ProductoVendido.Stock = Convert.ToInt32(dataReader["Stock"]);
            //                            ProductoVendidoyProductos.ProductoVendido.IdProducto = Convert.ToInt32(dataReader["IdProducto"]);
            //                            ProductoVendidoyProductos.ProductoVendido.IdVenta = Convert.ToInt32(dataReader["IdVenta"]);
            //                            ListaProductoVendidoyProductos.Add(ProductoVendidoyProductos);
            //                        }
            //                    }
            //                    sqlConnection.Close();
            //                }
            //            }
            //        }
            //    }
            //}
            //return ListaProductoVendidoyProductos;
        }
    }
}

