using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SortingApp.Models.Dtos;
using SortingApp.Services;
using System.Linq.Expressions;

namespace SortingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SortingController : ControllerBase
    {
        private readonly ISortingService _sortingService;
        public SortingController(ISortingService sortingService)
        {
            _sortingService = sortingService;
        }

        [HttpGet]
        [Route("bubble")]
        public IActionResult Bubble()
        {
          var sortingList = _sortingService.SortingList();

          if (!sortingList.Any())
            {
                return NotFound();
            }

            return Ok(new { sortingList });
        }

    }
}
