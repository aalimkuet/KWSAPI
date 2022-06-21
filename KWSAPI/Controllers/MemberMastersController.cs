#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KWS.Models;
using Microsoft.AspNetCore.Authorization;

namespace KWSAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class MemberMastersController : ControllerBase
    {
        private readonly KWSDBContext _context;

        public MemberMastersController(KWSDBContext context)
        {
            _context = context;
        }

        [HttpGet("GetList")]
        public async Task<ActionResult<IEnumerable<MemberMaster>>> GetMemberMasters()
        {
            return await _context.MemberMasters.ToListAsync();
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<MemberMaster>> GetMemberMaster(int id)
        {
            var memberMaster = await _context.MemberMasters.FindAsync(id);

            if (memberMaster == null)
            {
                return NotFound();
            }

            return memberMaster;
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> PutMemberMaster(int id, MemberMaster memberMaster)
        {
            if (id != memberMaster.Id)
            {
                return BadRequest();
            }

            _context.Entry(memberMaster).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MemberMasterExists(id))
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
        [HttpPost("Create")]
        public async Task<ActionResult<MemberMaster>> PostMemberMaster(MemberMaster memberMaster)
        {
            _context.MemberMasters.Add(memberMaster);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMemberMaster", new { id = memberMaster.Id }, memberMaster);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteMemberMaster(int id)
        {
            var memberMaster = await _context.MemberMasters.FindAsync(id);
            if (memberMaster == null)
            {
                return NotFound();
            }

            _context.MemberMasters.Remove(memberMaster);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MemberMasterExists(int id)
        {
            return _context.MemberMasters.Any(e => e.Id == id);
        }
    }
}
