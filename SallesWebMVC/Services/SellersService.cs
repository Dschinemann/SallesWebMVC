using SallesWebMVC.Data;
using SallesWebMVC.Models;

namespace SallesWebMVC.Services
{
    public class SellersService
    {
        private readonly SallesWebMVCContext _context;
        public SellersService(SallesWebMVCContext context)
        {
            _context = context;
        }
        public List<Seller> FindAll()
        {
            return _context.Seller.ToList();
        }
        public void Insert(Seller obj)
        {
            _context.Add(obj);
            _context.SaveChanges();
        }
    }
}
