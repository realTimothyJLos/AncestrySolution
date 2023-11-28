using Ancestry.Models;
using Ancestry.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Ancestry.Controllers
{
    public class IndividualController : Controller
    {
        private readonly GEDCOMDataRepository _dataRepository;

        public IndividualController(GEDCOMDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }

        public IActionResult Index()
        {
            List<GEDCOMIndividual> individuals = _dataRepository.GetAllIndividuals();
            return View(individuals);
        }

        public IActionResult Details(string individualId)
        {
            GEDCOMIndividual individual = _dataRepository.GetIndividualByIndividualId(individualId);
            if (individual == null)
            {
                return NotFound();
            }

            return View(individual);
        }


    }
}
