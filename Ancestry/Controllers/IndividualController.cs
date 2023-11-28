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

            // Assigning unique IDs to individuals
            for (int i = 0; i < individuals.Count; i++)
            {
                individuals[i].Id = i + 1; // Assigning IDs sequentially starting from 1
            }

            return View(individuals);
        }

        public IActionResult Details(string gedcomId)
        {
            GEDCOMIndividual individual = _dataRepository.GetIndividualByGEDCOMId(gedcomId);
            if (individual == null)
            {
                return NotFound();
            }

            return View(individual);
        }


    }
}
