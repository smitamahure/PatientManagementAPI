using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PatientManagementAPI.Data;
using PatientManagementAPI.DTO;
using PatientManagementAPI.Models; // <--- add the namespace where AppDbContext is declared

namespace PatientManagementAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PatientsController(AppDbContext context)
        {
            _context = context;
        }
        // GET: api/Patients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Patient>>> GetPatients()
        {
            var patients = await _context.Patients.ToListAsync();

            return Ok(patients);
        }
        // GET: api/Patients/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Patient>> GetPatient(int id)
        {
            var patient = await _context.Patients.FindAsync(id);

            if (patient == null)
            {
                return NotFound();
            }

            return Ok(patient);
        }
        // POST: api/Patients
        [Authorize(Roles ="Admin,Receptionist")]
        [HttpPost]
        public async Task<ActionResult<PatientResponseDto>> CreatePatient(PatientCreateDto dto)
        {
            var patient = new Patient
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                DateOfBirth = dto.DateOfBirth,
                Gender = dto.Gender,
                Phone = dto.Phone,
                Email = dto.Email,
                Address = dto.Address,
                BloodGroup = dto.BloodGroup,
                CreatedDate = DateTime.Now
            };
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();

            var response = new PatientResponseDto
            {
                PatientId = patient.PatientId,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                DateOfBirth = patient.DateOfBirth,
                Gender = patient.Gender,
                Phone = patient.Phone,
                Email = patient.Email,
                Address = patient.Address,
                BloodGroup = patient.BloodGroup,
                CreatedDate = patient.CreatedDate
            };

            return CreatedAtAction(
                    nameof(GetPatient),
                    new { id = patient.PatientId },
                    response);


            //without using DTOs, the code would look like this:
            //patient.CreatedDate = DateTime.Now;

            //_context.Patients.Add(patient);
            //await _context.SaveChangesAsync();

            //return CreatedAtAction(
            //    nameof(GetPatient),
            //    new { id = patient.PatientId },
            //    patient);
        }

        // PUT: api/Patients/1
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePatient(int id, PatientUpdateDto dto)
        {
            var existingPatient = await _context.Patients.FindAsync(id);

            if (existingPatient == null)
            {
                return NotFound();
            }

            existingPatient.FirstName = dto.FirstName;
            existingPatient.LastName = dto.LastName;
            existingPatient.DateOfBirth = dto.DateOfBirth;
            existingPatient.Gender = dto.Gender;
            existingPatient.Phone = dto.Phone;
            existingPatient.Email = dto.Email;
            existingPatient.Address = dto.Address;
            existingPatient.BloodGroup = dto.BloodGroup;

            await _context.SaveChangesAsync();
            return NoContent();
            //return Ok(existingPatient);



            //without using DTOs, the code would look like this:
            //if (id != patient.PatientId)
            //{
            //    return BadRequest("Patient ID mismatch.");
            //}

            //var existingPatient = await _context.Patients.FindAsync(id);

            //if (existingPatient == null)
            //{
            //    return NotFound();
            //}

            //existingPatient.FirstName = patient.FirstName;
            //existingPatient.LastName = patient.LastName;
            //existingPatient.DateOfBirth = patient.DateOfBirth;
            //existingPatient.Gender = patient.Gender;
            //existingPatient.Phone = patient.Phone;
            //existingPatient.Email = patient.Email;
            //existingPatient.Address = patient.Address;
            //existingPatient.BloodGroup = patient.BloodGroup;

            //await _context.SaveChangesAsync();

            //return NoContent();
        }

        // DELETE: api/Patients/1
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient(int id)
        {
            var patient = await _context.Patients.FindAsync(id);

            if (patient == null)
            {
                return NotFound();
            }

            _context.Patients.Remove(patient);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
