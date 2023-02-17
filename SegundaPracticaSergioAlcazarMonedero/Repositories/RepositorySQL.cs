using SegundaPracticaSergioAlcazarMonedero.Models;
using System.Data;
using System.Data.SqlClient;

#region PROCEDIMIENTOS
//CREATE PROCEDURE SP_INSERT_COMIC
//(@NOMBRE NVARCHAR(150), @IMAGEN NVARCHAR(600), @DESCRIPCION NVARCHAR(500))
//AS
//    DECLARE @ID INT
//	SELECT @ID = MAX(IDCOMIC) FROM COMICS

//    INSERT INTO COMICS VALUES(@ID + 1, @NOMBRE, @IMAGEN, @DESCRIPCION)
//GO
#endregion

namespace SegundaPracticaSergioAlcazarMonedero.Repositories
{
    public class RepositorySQL : IRepository
    {

        private SqlConnection cn;
        private SqlCommand com;
        private SqlDataAdapter adapter;
        private DataTable tablaComic;
        public RepositorySQL()
        {
            string connectionName = @"Data Source=LOCALHOST\DESARROLLO;Initial Catalog=Hospital;User ID=sa;Password=MCSD2023";
            this.cn = new SqlConnection(connectionName);
            this.com = new SqlCommand();
            this.com.Connection = this.cn;
            string sql = "SELECT * FROM COMICS";
            this.adapter = new SqlDataAdapter(sql, this.cn);
            this.tablaComic = new DataTable();
            this.adapter.Fill(tablaComic);
        }

        public List<Comic> GetComics()
        {
            var consulta = from comic in this.tablaComic.AsEnumerable()
                           select new Comic
                           {
                               IdComic = comic.Field<int>("IDCOMIC"),
                               Nombre = comic.Field<string>("NOMBRE"),
                               Imagen = comic.Field<string>("IMAGEN"),
                               Descripcion = comic.Field<string>("DESCRIPCION")
                           };
            return consulta.ToList();
        }

        public void InsertarComic(string nombre, string imagen, string descripcion)
        {
            SqlParameter paranombre = new SqlParameter("@NOMBRE", nombre);
            this.com.Parameters.Add(paranombre);
            SqlParameter paraimagen = new SqlParameter("@IMAGEN", imagen);
            this.com.Parameters.Add(paraimagen);
            SqlParameter paradescripcion = new SqlParameter("@DESCRIPCION", descripcion);
            this.com.Parameters.Add(paradescripcion);
            this.com.CommandType = CommandType.StoredProcedure;
            this.com.CommandText = "SP_INSERT_COMIC";
            this.cn.Open();
            this.com.ExecuteNonQuery();
            this.cn.Close();
            this.com.Parameters.Clear();
        }
    }
}
