using ServiceApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceApp.Repository.Interfaces
{
    public interface IPaketRepository
    {
        IEnumerable<DostavaDTO> GetAllByTezina(double granica);


        IEnumerable<StanjeDTO> GetStanje();

        IQueryable<Paket> GetAllByPrimalac(string primalac);

        IQueryable<Paket> PretragaTezina(double najmanje, double najvise);

        IQueryable<Paket> GetAll();
        Paket GetById(int id);
        void Add(Paket paket);

        void Update(Paket paket);

        void Delete(Paket paket);

    }
}
