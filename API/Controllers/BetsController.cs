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
    public class BetsController : ControllerBase
    {
        private readonly DataContext _context;
        public BetsController(DataContext context)
        {
            _context = context;
        }

        //API Call to get all bets in the Bets table
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bet>>> GetBets()
        {
            var bets = await _context.Bets.ToListAsync();
            return Ok(bets);
        }

        //API Call to get all the bets via a RollID
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Bet>>> GetBetsForRoll(int id)
        {
            var bets = await _context.Bets.Where(x => x.RollId == id).ToListAsync();
            return Ok(bets);
        }

        //API Call to create a bet.
        [HttpPost("placeBet")]
        public async Task<ActionResult<BetDto>> PlaceBet(BetDto bet)
        {
            if (bet.BetNumber > 36 || bet.BetNumber < 0)
                return BadRequest("Invalid Bet, Bets must be between 0 and 36");

            var newRollId = 1;
            var latestRoll = await _context.Rolls.OrderByDescending(x => x.Id).FirstOrDefaultAsync();
            if (latestRoll == null)
                latestRoll = new Roll { Id = 0, RollResult = 0};


            var newBet = new Bet
            {
                BetNumber = bet.BetNumber,
                BetAmount = bet.BetAmount,
                RollId = newRollId + latestRoll.Id,
                Winner = false,
                Payout = false
            };

            _context.Bets.Add(newBet);
            await _context.SaveChangesAsync();

            return Ok(bet);
        }

        //API Call to facilitate a payout and display the total winnings to the output
        [HttpGet("payout")]
        public async Task<ActionResult<Tuple<string, Bet, string>>> PayOut()
        {
            var bet = await _context.Bets.Where(x => x.Winner == true && x.Payout == false).SingleOrDefaultAsync();

            if (bet == null)
                return Ok(Tuple.Create("Unfortunately no winning bets are found", bet, "Try again next time."));
            else
            {
                bet.Payout = true;
                if (await UpdateWinningBet(bet))
                {
                    return Ok(Tuple.Create("Congratulations to the Winning Bet!", bet, "You've won R" + bet.BetAmount * 35));
                }
            }

            return BadRequest("An Error Occured");
        }

        //Function to update the winning bet and change the payout identifier to true, therefore not allowing for multiple payouts to occur for a single winning bet
        private async Task<bool> UpdateWinningBet(Bet bet)
        {
            _context.Entry(bet).State = EntityState.Modified;

            if (await _context.SaveChangesAsync() > 0)
                return true;

            return false;
        }
    }
}