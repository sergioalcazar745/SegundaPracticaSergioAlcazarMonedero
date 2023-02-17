using Oracle.ManagedDataAccess.Client;
using SegundaPracticaSergioAlcazarMonedero.Models;
using System.Data;

#region PROCEDIMIENTOS
//CREATE OR REPLACE PROCEDURE SP_INSERT_COMIC
//(P_NOMBRE COMICS.NOMBRE%TYPE, P_IMAGEN COMICS.IMAGEN%TYPE, P_DESCRIPCION COMICS.DESCRIPCION%TYPE)
//AS
//      P_ID COMICS.IDCOMIC%TYPE;
//BEGIN
//      SELECT MAX(IDCOMIC) INTO P_ID FROM COMICS;
//INSERT INTO COMICS VALUES(P_ID + 1, P_NOMBRE, P_IMAGEN, P_DESCRIPCION);

//COMMIT;
//END;
#endregion

namespace SegundaPracticaSergioAlcazarMonedero.Repositories
{
    public class RepositoryOracle : IRepository
    {
        private OracleConnection cn;
        private OracleCommand com;
        private OracleDataAdapter adapter;
        private DataTable tablaComic;
        public RepositoryOracle()
        {
            string connectionName = @"USER ID=SYSTEM;PASSWORD=oracle;PERSIST SECURITY INFO=True;DATA SOURCE=LOCALHOST:1521/XE;CONNECTION TIMEOUT=250";
            this.cn = new OracleConnection(connectionName);
            this.com = new OracleCommand();
            this.com.Connection = this.cn;
            string sql = "SELECT * FROM COMICS";
            this.adapter = new OracleDataAdapter(sql, this.cn);
            this.tablaComic = new DataTable();
            adapter.Fill(this.tablaComic);
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
            OracleParameter paranombre = new OracleParameter(":P_NOMBRE", nombre);
            this.com.Parameters.Add(paranombre);
            OracleParameter paraimagen = new OracleParameter(":P_IMAGEN", imagen);
            this.com.Parameters.Add(paraimagen);
            OracleParameter paradescripcion = new OracleParameter(":P_DESCRIPCION", descripcion);
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
