using Alten.CarTracker.Infrastructure.Common.Commands;
using System;
using System.Collections.Generic;

//http://csharpindepth.com/articles/general/singleton.aspx
namespace Alten.CarTracker.Services.StatusReceivedService.Application
{
	public sealed class StatusCheckList
	{
		private static readonly Lazy<StatusCheckList> lazy = new Lazy<StatusCheckList>(() => new StatusCheckList());
		public static StatusCheckList Instance => lazy.Value;

		public Queue<UpdateStatus> ReceviedMessages { get; set; } = new Queue<UpdateStatus>();

		private StatusCheckList()
		{

		}

		public Dictionary<string, UpdateStatus> StartCheck()
		{
			Dictionary<string, UpdateStatus> checkList = new Dictionary<string, UpdateStatus>();
			while (Instance.ReceviedMessages.Count > 0)
			{
				UpdateStatus command = Instance.ReceviedMessages.Dequeue();
				checkList.Add(command.VinCode, command);
			};

			return checkList;
		}
	}
}
