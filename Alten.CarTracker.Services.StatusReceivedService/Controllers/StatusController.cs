using Alten.CarTracker.Infrastructure.Common.Commands;
using Alten.CarTracker.Infrastructure.Common.DTOs;
using Alten.CarTracker.Services.StatusReceivedService.Application;
using Alten.CarTracker.Services.StatusReceivedService.DataAccess;
using Alten.CarTracker.Services.StatusReceivedService.Domain.Model;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Serilog;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Alten.CarTracker.Services.StatusReceivedService.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class StatusController : ControllerBase
	{
		private readonly EfDbContext _dbContext;
		private readonly IMapper _mapper;

		public StatusController(EfDbContext efDbContext, IMapper mapper)
		{
			_dbContext = efDbContext;
			_mapper = mapper;
		}

		[HttpPost]
		public async Task<dynamic> UpdateVehicleStatus([FromBody] JObject data)
		{
			try
			{
				if (ModelState.IsValid)
				{
					StatusReceivedDTO command = data.ToObject<StatusReceivedDTO>();

					UpdateStatus message = _mapper.Map<UpdateStatus>(command);
					StatusCheckList.Instance.ReceviedMessages.Enqueue(message);
					Log.Information($"Status message Received and enqueued for {message.VinCode}");


					Status status = Mapper.Map<UpdateStatus, Status>(message);
					_dbContext.Add(status);
					await _dbContext.SaveChangesAsync();
					Log.Information($"Status message saved in database for {message.VinCode}");
					return Ok();
				}
				return BadRequest();
			}
			catch (DbUpdateException)
			{
				ModelState.AddModelError("Database Error", "Unable to save changes.");
				Log.Error("Database Error", "Unable to save changes.");
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
		}

		[HttpGet]
		public async Task<ActionResult<IList<CarStatusLookup>>> GetCarStatusLookup()
		{
			Log.Information("Request for getting car status lookup");
			List<CarStatusLookup> carStatusLookups = await _dbContext.Set<CarStatusLookup>().ToListAsync();
			return carStatusLookups;
		}
	}
}
