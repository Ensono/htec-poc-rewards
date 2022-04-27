﻿using System.Threading.Tasks;
using Amido.Stacks.Application.CQRS.ApplicationEvents;
using Microsoft.Extensions.Logging;
using HTEC.Engagement.Application.CQRS.Events;

namespace HTEC.Engagement.BackgroundWorker.Handlers
{
    public class CategoryUpdatedEventHandler : IApplicationEventHandler<CategoryUpdatedEvent>
	{
		private readonly ILogger<CategoryUpdatedEventHandler> log;

		public CategoryUpdatedEventHandler(ILogger<CategoryUpdatedEventHandler> log)
		{
			this.log = log;
		}

		public Task HandleAsync(CategoryUpdatedEvent appEvent)
		{
			log.LogInformation($"Executing CategoryUpdatedEventHandler...");
			return Task.CompletedTask;
		}
	}
}
