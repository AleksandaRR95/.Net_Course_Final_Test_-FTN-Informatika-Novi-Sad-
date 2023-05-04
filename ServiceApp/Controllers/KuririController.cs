using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceApp.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KuririController : ControllerBase
    {

        private readonly IKurirRepository _kurirRepository;


        public KuririController(IKurirRepository kurirRepository)
        {

            _kurirRepository = kurirRepository;
        }

        [HttpGet]
        [Route("/api/kuriri/nadji")]
        public IActionResult GetKuririByIme([FromQuery]string ime)
        {

            return Ok(_kurirRepository.GetAllByIme(ime).ToList());
        }


        [HttpGet]
        public IActionResult GetKuriri()
        {

            return Ok(_kurirRepository.GetAll().ToList());
        }

        [HttpGet("{id}")]
        public IActionResult GetKurir(int id)
        {
            var kurir = _kurirRepository.GetById(id);
            if (kurir == null)
            {
                return NotFound();
            }

            return Ok(kurir);
        }

      

    }
}
