using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RollsController : ControllerBase
    {
        private readonly DataContext _context;
        public RollsController(DataContext context)
        {
            _context = context;
        }

        //API Call to get all the previous Spins, and returns them in Decending order, Latest spin to oldest spin
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Roll>>> GetRolls()
        {
            var rolls = await _context.Rolls.OrderByDescending(x => x.Id).ToListAsync();
            return Ok(rolls);
        }

        //API Call  to get a previous spin via its ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Roll>> GetPreviousRoll(int id)
        {
            var roll = await _context.Rolls.Where(x => x.Id == id).SingleOrDefaultAsync();
            return Ok(roll);
        }

        //API Call  to Spin, also checks to see if there is a winning bet, if one is found then starts the function to check for winning bet
        [HttpPost("spin")]
        public async Task<ActionResult<Tuple<RollDto, string>>> Spin()
        {
            Random rand = new Random();
            String resultString = "No Winners were detected!";

            //C# Random Int value generator includes 0, and excludes 37, therefore making the range between 0 and 36
            int randomSpinValue = rand.Next(0, 37);

            var roll = new Roll
            {
                RollResult = randomSpinValue
            };

            _context.Rolls.Add(roll);
            await _context.SaveChangesAsync();

            if (await CheckForAnyWinners(roll))
                resultString = "We have detected a winner!";

            return Ok(Tuple.Create(new RollDto
            {
                RollResult = roll.RollResult
            }, resultString));
        }

        //Check function to see if the API Detects any winners, if found the functions starts the update winnig bet
        private async Task<bool> CheckForAnyWinners(Roll roll)
        {
            var bets = await _context.Bets.Where(x => x.RollId == roll.Id).ToListAsync();

            foreach (var bet in bets)
            {
                if (bet.BetNumber == roll.RollResult)
                {
                    bet.Winner = true;
                    if (await UpdateWinningBet(bet))
                        return true;
                }
            }
            return false;
        }

        //Function to Update a Winning bet to true
        private async Task<bool> UpdateWinningBet(Bet bet)
        {
            _context.Entry(bet).State = EntityState.Modified;

            if (await _context.SaveChangesAsync() > 0)
                return true;

            return false;
        }
    }
}