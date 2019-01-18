using Alten.CarTracker.Infrastructure.Common.Commands;
using Alten.CarTracker.Infrastructure.Common.Events;
using Alten.CarTracker.Infrastructure.Messaging;
using Alten.CarTracker.Services.StatusReceivedService.DataAccess;
using Alten.CarTracker.Services.StatusReceivedService.Domain.Model;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Alten.CarTracker.Services.StatusReceivedService.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class StatusController : ControllerBase
	{
		private readonly IMessagePublisher _messagePublisher;
		private readonly EfDbContext _dbContext;
		private readonly IMapper _mapper;

		public StatusController(EfDbContext efDbContext, IMessagePublisher messagePublisher, IMapper mapper)
		{
			_dbContext = efDbContext;
			_messagePublisher = messagePublisher;
			_mapper = mapper;
		}

		[HttpPost]
		public async Task<dynamic> Post([FromBody] UpdateStatus command)
		{
			try
			{
				if (ModelState.IsValid)
				{
					Status status = _mapper.Map<UpdateStatus, Status>(command);
					_dbContext.Add(status);
					int result = await _dbContext.SaveChangesAsync();

					// send event
					StatusReceived message = Mapper.Map<StatusReceived>(command);
					await _messagePublisher.PublishMessageAsync(message.MessageType, message, "StatusReceived");

					//return result
					return result > 0 ? new
					{
						Saved = true,
					}
					: new
					{
						Saved = false,
					};
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
			List<CarStatusLookup> carStatusLookups = await _dbContext.Query<CarStatusLookup>().ToListAsync();
			return carStatusLookups;
		}
	}
}
