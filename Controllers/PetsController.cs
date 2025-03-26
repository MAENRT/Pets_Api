using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetsAPI.Data;
using PetsAPI.Model;
using PetsAPI.Viewmodel;

namespace PetsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetsController : ControllerBase
    {
        private readonly Petsdb_context _context;


        public PetsController(Petsdb_context context)
        {
            _context = context;
        }


        [HttpPost("PetsForm")]
        public async Task<IActionResult> PetsForm([FromBody] PetsDetailm petsdetail)
        {

            if(petsdetail == null)
            {
                return BadRequest();
            }

            Pets_detail newpet = new Pets_detail();
            newpet.PetsName = petsdetail.PetsName;
            newpet.PetsType = petsdetail.PetsType;
            newpet.PetsAge = petsdetail.PetsAge;
            newpet.weight = petsdetail.weight;
            newpet.PetsGender = petsdetail.PetsGender;
            newpet.IsVaccinated = petsdetail.IsVaccinated;
            newpet.PetsBreed = petsdetail.PetsBreed;
            newpet.neutered = petsdetail.neutered;
            newpet.Status = true;
            newpet.CreateOn = DateTime.Now;
            newpet.PetsPhoto = petsdetail.PetsPhoto;

            _context.Pets_detail.Add(newpet);
            _context.SaveChanges();

            return Ok(new { success = true });
        }

        // GET api/Pets/Adoption?page=1&pageSize=5&state=Maharashtra&city=Mumbai
        [HttpGet("Adoption")]
        public async Task<IActionResult> GetPetsForAdoption(
          string petType = null,
          string breed = null,
          string gender = null,
           string vaccinated = null,
          string neutered = null,
          int page = 1,
          int pageSize = 5)
        {
            try
            {
                var query = _context.Pets_detail
                    .Where(p => p.Status == true);

                // Apply filters if they are provided
                if (!string.IsNullOrEmpty(petType))
                {
                    query = query.Where(p => p.PetsType == petType);
                }

                if (!string.IsNullOrEmpty(breed))
                {
                    query = query.Where(p => p.PetsBreed == breed);
                }

                if (!string.IsNullOrEmpty(gender))
                {
                    query = query.Where(p => p.PetsGender == gender);
                }
                if (!string.IsNullOrEmpty(vaccinated))
                {
                    bool isVaccinated = vaccinated.Equals("Yes", StringComparison.OrdinalIgnoreCase);
                    query = query.Where(p => p.IsVaccinated == isVaccinated);
                }

                // Handle neutered filter (assuming frontend sends "Yes"/"No")
                if (!string.IsNullOrEmpty(neutered))
                {
                    bool isNeutered = neutered.Equals("Yes", StringComparison.OrdinalIgnoreCase);
                    query = query.Where(p => p.neutered == isNeutered);
                }

                var totalCount = await query.CountAsync();
                var pets = await query
                    .OrderBy(p => p.PetsName)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .Select(p => new {
                        p.PetsID,
                        p.PetsName,
                        p.PetsType,
                        p.PetsBreed,
                        p.PetsGender,
                        p.PetsAge,
                        p.PetsPhoto,
                        neutered = p.neutered ? "Yes" : "No",
                        vaccinated = p.IsVaccinated ? "Yes" : "No",

                        p.Status
                    })
                    .ToListAsync();

                return Ok(new
                {
                    success = true,
                    data = pets,
                    totalCount,
                    currentPage = page,
                    totalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }
    }
}
