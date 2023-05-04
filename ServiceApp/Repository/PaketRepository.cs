using Microsoft.EntityFrameworkCore;
using ServiceApp.Models;
using ServiceApp.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceApp.Repository
{
    public class PaketRepository : IPaketRepository
    {
        private readonly AppDbContext _context;

        public PaketRepository (AppDbContext context)
        {
            _context = context;
        }

        public void Add(Paket paket)
        {
            _context.Add(paket);
            _context.SaveChanges();
        }

        public void Delete(Paket paket)
        {
            _context.Remove(paket);
            _context.SaveChanges();
        }

        public IQueryable<Paket> GetAll()
        {
            return _context.Paketi.Include(p => p.Kurir).OrderBy(p => p.Tezina);
        }

        public IQueryable<Paket> GetAllByPrimalac(string primalac)
        {
            return _context.Paketi.Include(p => p.Kurir).Where(p => p.Primalac == primalac).OrderBy(p => p.CenaPostarine);
        }

        public IEnumerable<DostavaDTO> GetAllByTezina(double granica)
        {
            return _context.Paketi.Include(p => p.Kurir).GroupBy(p => p.KurirId)
                .Select(r => new DostavaDTO
                {
                    KurirId = r.Key,
                    KurirIme = _context.Kuriri.Where(k => k.Id == r.Key).Select(k => k.Ime).Single(),
                    UkupnaTezinaPaketa = r.Sum(p => p.Tezina)
                }).Where(r => r.UkupnaTezinaPaketa < granica).OrderByDescending(r => r.KurirIme).ToList();
        }

        public Paket GetById(int id)
        {
            return _context.Paketi.Include(p => p.Kurir).FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<StanjeDTO> GetStanje()
        {
            return _context.Paketi.Include(p => p.Kurir).GroupBy(p => p.KurirId).
                Select(r => new StanjeDTO 
                {
                    KurirId = r.Key,
                    KurirIme = _context.Kuriri.Where(k => k.Id == r.Key).Select(k => k.Ime).Single(),
                    BrojPaketa = _context.Paketi.Where(p => p.KurirId == r.Key).Count()
                }).OrderByDescending(r => r.BrojPaketa).ToList();
        }

        public IQueryable<Paket> PretragaTezina(double najmanje, double najvise)
        {
            return _context.Paketi.Include(p => p.Kurir).Where(p => p.Tezina >= najmanje && p.Tezina <= najvise).OrderByDescending(p => p.Tezina);
        }

        public void Update(Paket paket)
        {
            _context.Paketi.Update(paket);

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {

                throw;
            }
        }
    }
}
