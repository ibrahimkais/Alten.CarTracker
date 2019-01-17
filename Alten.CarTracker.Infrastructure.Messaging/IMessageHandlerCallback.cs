using System.Threading.Tasks;

namespace Alten.CarTracker.Infrastructure.Messaging
{
	public interface IMessageHandlerCallback
	{
		Task<bool> HandleMessageAsync(string messageType, string message);
	}
}
