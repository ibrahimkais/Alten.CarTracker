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
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Alten.CarTracker.Services.StatusReceivedService.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class StatusController : ControllerBase
	{
		//private readonly IMessagePublisher _messagePublisher;
		private readonly EfDbContext _dbContext;
		//private readonly IMapper _mapper;

		private readonly IMapper _mapper;

		//public StatusController(EfDbContext efDbContext, IMessagePublisher messagePublisher, IMapper mapper)
		public StatusController(EfDbContext efDbContext, IMapper mapper)
		{
			_dbContext = efDbContext;
			//_messagePublisher = messagePublisher;
			//_mapper = mapper;

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

					Status status = Mapper.Map<UpdateStatus, Status>(message);
					_dbContext.Add(status);
					await _dbContext.SaveChangesAsync();

					return Ok();
				}
				return BadRequest();
			}
			catch (DbUpdateException)
			{
				ModelState.AddModelError("", "Unable to save changes. " +
					"Try again, and if the problem persists " +
					"see your system administrator.");
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
		}

		[HttpGet]
		public async Task<ActionResult<IList<CarStatusLookup>>> GetCarStatusLookup()
		{
			List<CarStatusLookup> carStatusLookups = await _dbContext.Set<CarStatusLookup>().ToListAsync();
			return carStatusLookups;
		}
	}
}
