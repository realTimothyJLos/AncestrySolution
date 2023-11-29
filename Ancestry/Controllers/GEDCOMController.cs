using Ancestry.Models;
using Ancestry.Parsing;
using Ancestry.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Ancestry.Controllers
{
    public class GEDCOMController : Controller
    {
        private readonly GEDCOMDataRepository _dataRepository;
        private readonly GEDCOMParser _gedcomParser;
        private readonly string _filePath; 

        public GEDCOMController(GEDCOMDataRepository dataRepository, IConfiguration configuration, GEDCOMParser gedcomParser)
        {
            _dataRepository = dataRepository;
            _filePath = configuration["FilePaths:GEDCOMFilePath"];
            _gedcomParser = gedcomParser;

        }

        public IActionResult Index()
        {
            List<GEDCOMIndividual> gedcomData = _gedcomParser.ParseGEDCOM(_filePath);
            return View(gedcomData);
        }

    }
}
