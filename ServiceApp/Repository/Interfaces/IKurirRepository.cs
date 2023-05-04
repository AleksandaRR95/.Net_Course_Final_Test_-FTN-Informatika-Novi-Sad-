using ServiceApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceApp.Repository.Interfaces
{
    public interface IKurirRepository
    {
        IEnumerable<Kurir> GetAll();

        IEnumerable<Kurir> GetAllByIme(string ime);

        Kurir GetById(int id);

    }
}
