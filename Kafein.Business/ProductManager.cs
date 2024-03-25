using Kafein.Database;
using Kafein.Model;
using System.Linq.Expressions;

namespace Kafein.Business
{
    public class ProductManager : IProductManager
    {
        private readonly EticaretDbContext _context;
        public ProductManager(EticaretDbContext context )
        {
            _context = context;
        }

        public List<Urun> GetUrunList()
        {
            return _context.Urunler.ToList();
        }

        public List<Urun> GetUrunList(Expression<Func<Urun, bool>> expression)
        {
            return _context.Urunler.Where(expression).ToList();
        }
    }

    public interface IProductManager
    {
        List<Urun> GetUrunList();
        List<Urun> GetUrunList(Expression<Func<Urun, bool>> expression);
    }
}