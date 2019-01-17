using System;

namespace Alten.CarTracker.BackEndApi.Application.Exceptions
{
	public class CustomerIdException : ArgumentException
	{
		public CustomerIdException(string message) : base(message)
		{

		}
	}
}
