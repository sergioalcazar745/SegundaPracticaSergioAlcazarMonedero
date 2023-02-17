using SegundaPracticaSergioAlcazarMonedero.Models;

namespace SegundaPracticaSergioAlcazarMonedero.Repositories
{
    public interface IRepository
    {
        List<Comic> GetComics();
        void InsertarComic(string nombre, string imagen, string descripcion);
    }
}
