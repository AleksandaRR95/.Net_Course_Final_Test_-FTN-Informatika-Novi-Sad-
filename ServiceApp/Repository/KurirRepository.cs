using ServiceApp.Models;
using ServiceApp.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceApp.Repository
{
    public class KurirRepository : IKurirRepository
    {
        private readonly AppDbContext _context;

        public KurirRepository (AppDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Kurir> GetAll()
        {
            return _context.Kuriri.OrderBy(k => k.Ime);
        }

        public IEnumerable<Kurir> GetAllByIme(string ime)
        {
            return _context.Kuriri.Where(k => k.Ime.Contains(ime)).OrderByDescending(k => k.GodinaRodjenja);
        }

        public Kurir GetById(int id)
        {
            return _context.Kuriri.FirstOrDefault(s => s.Id == id);
        }
    }
}
