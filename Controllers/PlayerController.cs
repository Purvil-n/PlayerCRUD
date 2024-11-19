using PlayerCRUD.Context;
using PlayerCRUD.Models;
using PlayerCRUD.RequestResponse;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace PlayerCRUD.Controllers
{
    [RoutePrefix("api/player")]
    public class PlayerController : ApiController
    {
        private readonly PlayerContext _context;
        public PlayerController()
        {
            _context = new PlayerContext();
        }

        [HttpPost]
        [Route("addplayer")]
        public async Task<string> AddPlayer(Response player)
        {
            PlayerEntity obj = new PlayerEntity()
            {
                Id = player.PlayerId,
                Name = player.PlayerName,
                Mobile = player.PlayerMobile,
            };
            _context.Players.Add(obj);
            await _context.SaveChangesAsync();
            return "Added";
        }

        [HttpGet]
        [Route("getplayers")]
        public async Task<List<Response>> GetPlayers()
        {
            var res = await _context.Players.Select(item => new Response
            {
                PlayerId = item.Id,
                PlayerName = item.Name,
                PlayerMobile = item.Mobile
            }).ToListAsync();
            return res;
        }

        [HttpGet]
        [Route("getplayerbyid")]
        public async Task<Response> GetPlayerById(int id)
        {
            var res = await _context.Players.Where(item => item.Id == id).Select(item => new Response
            {
                PlayerId = item.Id,
                PlayerName = item.Name,
                PlayerMobile = item.Mobile
            }).FirstOrDefaultAsync();
            return res;
        }

        [HttpDelete]
        [Route("deleteplayerbyid")]
        public async Task<string> DeletePlayerById(int id)
        {
            var res = await _context.Players.Where(item => item.Id == id).FirstOrDefaultAsync();
            if (res != null)
            {
                _context.Players.Remove(res);
                await _context.SaveChangesAsync();
                return "Deleted";
            }
            return null;
        }

        [HttpPut]
        [Route("updateplayer")]
        public async Task<string> UpdatePlayer(int id, Response player)
        {
            var res = await _context.Players.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (res != null)
            {
                res.Name = player.PlayerName;
                res.Mobile = player.PlayerMobile;

                //_context.Players.AddOrUpdate(res);
                await _context.SaveChangesAsync();
                return "Updated";
            }
            return null;
        }
    }
}
