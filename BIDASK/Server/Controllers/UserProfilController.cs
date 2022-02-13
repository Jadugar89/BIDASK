using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BIDASK.Server.Data;
using BIDASK.Shared;
using BIDASK.Server.Services;
using Microsoft.AspNetCore.Authorization;

namespace BIDASK.Server.Controllers
{
    
    [Route("[controller]")]
    [ApiController]

    public class UserProfilController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IUtilityService _utilityService;

        public UserProfilController(DataContext context, IUtilityService utilityService)
        {
            _context = context;
            _utilityService = utilityService;
        }


        [HttpGet]
        public async Task<ActionResult<UserProfil>> GetUserProfil()
        {
            var user = await _utilityService.GetUser();
            
            if (user == null)
            {
                return NotFound();
            }
            var test = await _context.userProfils.FindAsync(user.Id);
            return test;
        }

        // PUT: api/UserProfil/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<ActionResult<UserProfil>> PutUserProfil(UserProfil userProfil)
        {
            var user = await _utilityService.GetUser();

            if (user.Id != userProfil.Id)
            {
                return BadRequest();
            }

            _context.Entry(userProfil).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserProfilExists(user.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return userProfil;
        }

        // POST: api/UserProfil
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserProfil>> PostUserProfil(UserProfil userProfil)
        {
            

            _context.userProfils.Add(userProfil);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserProfil", new { id = userProfil.Id }, userProfil);
        }

        // DELETE: api/UserProfil/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserProfil(int id)
        {
            var userProfil = await _context.userProfils.FindAsync(id);
            if (userProfil == null)
            {
                return NotFound();
            }

            _context.userProfils.Remove(userProfil);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserProfilExists(int id)
        {
            return _context.userProfils.Any(e => e.Id == id);
        }
    }
}
