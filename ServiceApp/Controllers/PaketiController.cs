using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceApp.Models;
using ServiceApp.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaketiController : ControllerBase
    {
        private readonly IPaketRepository _paketRepository;

        private readonly IMapper _mapper;


        public PaketiController(IPaketRepository paketRepository, IMapper mapper)
        {
            _paketRepository = paketRepository;
            _mapper = mapper;

        }

        [HttpGet]
        public IActionResult GetPaketi()
        {

            return Ok(_paketRepository.GetAll().ProjectTo<PaketDTO>(_mapper.ConfigurationProvider).ToList());
        }

        [HttpGet("{id}")]
        public IActionResult GetPaket(int id)
        {
            var paket = _paketRepository.GetById(id);
            if (paket == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<PaketDTO>(paket));
        }
        [HttpPost]
        [Authorize]
        public IActionResult PostPaket(Paket paket)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _paketRepository.Add(paket);
            return CreatedAtAction("GetPaket", new { id = paket.Id }, _mapper.Map<PaketDTO>(paket));
        }

        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult DeletePaket(int id)
        {
            var paket = _paketRepository.GetById(id);
            if (paket == null)
            {
                return NotFound();
            }

            _paketRepository.Delete(paket);
            return NoContent();
        }

        [HttpPut("{id}")]

        public IActionResult PutPaket(int id, Paket paket)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != paket.Id)
            {
                return BadRequest();
            }

            try
            {
                _paketRepository.Update(paket);
            }
            catch
            {
                return BadRequest();
            }

            return Ok(_mapper.Map<PaketDTO>(paket));
        }

        [HttpGet]
        [Route("/api/stanje")]
        public IActionResult Stanje()
        {
            return Ok(_paketRepository.GetStanje().ToList());
        }
        [HttpGet]
        [Route("/api/dostave")]
        public IActionResult Granica([FromQuery]double granica)
        {
            return Ok(_paketRepository.GetAllByTezina(granica).ToList());
        }

   

        [HttpPost]
        [Route("/api/pretraga")]
        [Authorize]
        public IActionResult Pretraga(TezinaPretraga tezina)
        {
            return Ok(_paketRepository.PretragaTezina(tezina.Najmanje, tezina.Najvise).ProjectTo<PaketDTO>(_mapper.ConfigurationProvider).ToList());
        }

        [HttpGet]
        [Route("/api/Paketi/trazi")]
        public IActionResult Granica([FromQuery] string prijem)
        {
            return Ok(_paketRepository.GetAllByPrimalac(prijem).ToList());
        }

    }
}
