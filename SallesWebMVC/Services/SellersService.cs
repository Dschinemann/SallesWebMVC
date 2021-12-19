using SallesWebMVC.Data;
using SallesWebMVC.Models;
using SallesWebMVC.Services.Exceptions;
using Microsoft.EntityFrameworkCore;

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

        public Seller FindById(int id)
        {
            return _context.Seller.Include(obj => obj.Department).FirstOrDefault(x => x.Id == id);
        }

        public void Remove(int id)
        {
            var obj = _context.Seller.Find(id);
            _context.Seller.Remove(obj);
            _context.SaveChanges();
        }

        public void update(Seller obj)
        {
            if (obj == null) return;
            if (!_context.Seller.Any(x => x.Id == obj.Id))
            {
                throw new NotFoundExceptions("Id not Found");
            }
            try
            {
                _context.Update(obj);
                _context.SaveChanges();
            }catch(DbUpdateConcurrencyException ex)
            {
                throw new DbConcurrencyException(ex.Message);
            }

        }
    }
}
