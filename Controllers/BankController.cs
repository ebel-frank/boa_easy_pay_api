using BoaEasyPay.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Extensions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BoaEasyPay.Controllers
{
    [Route("bank")]
    [ApiController]
    public class BankController : ControllerBase
    {
        private readonly DataContext _context;

        public BankController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("/banks")]
        public async Task<ActionResult<IEnumerable<Bank>>> GetBanks()
        {
            return await _context.Banks.ToListAsync();
        }

        [HttpGet("/all_banks_name")]
        public async Task<ActionResult<IEnumerable<String>>> GetBanksName()
        {
            return await _context.Banks
                         .Select(b => b.Name)
                         .Distinct()
                         .OrderBy(name => name)
                         .ToListAsync();
        }

        [HttpGet("/banks/{name}")]
        public async Task<ActionResult<IEnumerable<Bank>>> GetBanksByName(String name)
        {
            return await _context.Banks
                         .Where(b => b.Name.ToLower() == name.ToLower())
                         .ToListAsync();
        }

        // GET api/<BankController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Bank>> GetBank(int id)
        {
            var bank = await _context.Banks.FindAsync(id);

            if (bank == null)
            {
                return NotFound();
            }

            return bank;
        }

        [HttpPost]
        [Route("add")]
        public async Task<ActionResult<Bank>> AddBank(Bank bank)
        {
            _context.Banks.Add(bank);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBank", new { id = bank.Id }, bank);
        }

        // PUT api/<BankController>/5
        [HttpPut("edit/{id}")]
        public async Task<IActionResult> EditBank(int id, Bank bank)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != bank.Id)
            {
                return BadRequest("ID mismatch");
            }

            var existingBank = await _context.Banks.FindAsync(id);
            if (existingBank == null)
            {
                return NotFound();
            }

            existingBank.Name = bank.Name;
            existingBank.SortCode = bank.SortCode;
            existingBank.Address = bank.Address;
            existingBank.BranchCode = bank.BranchCode;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BankExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE api/<BankController>/5
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var bank = await _context.Banks.FindAsync(id);
            if (bank == null)
            {
                return NotFound();
            }

            _context.Banks.Remove(bank);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BankExists(int Id)
        {
            return _context.Banks.Any(e => e.Id == Id);
        }
    }
}
