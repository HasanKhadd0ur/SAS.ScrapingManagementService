using Ardalis.Result;
using MediatR;
using SAS.ScrapingManagementService.Domain.Scrapers.DomainErrors;
using SAS.ScrapingManagementService.Domain.Scrapers.Entities;
using SAS.ScrapingManagementService.SharedKernel.Repositories;
using SAS.ScrapingManagementService.Application.Contracts.Providers;

namespace SAS.ScrapingManagementService.Application.Scrapers.UseCases.Commands.CompleteScrapingTask
{
    public class CompleteScrapingTaskCommandHandler : IRequestHandler<CompleteScrapingTaskCommand, Result>
    {
        private readonly IRepository<ScrapingTask, Guid> _taskRepository;
        private readonly IDateTimeProvider _dateTimeProvider;

        public CompleteScrapingTaskCommandHandler(
            IRepository<ScrapingTask, Guid> taskRepository,
            IDateTimeProvider dateTimeProvider)
        {
            _taskRepository = taskRepository;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<Result> Handle(CompleteScrapingTaskCommand request, CancellationToken cancellationToken)
        {
            var task = await _taskRepository.GetByIdAsync(request.ScrapingTaskId);
            if (task == null)
                return Result.Invalid(ScrapingTaskErrors.UnExistTask);

            if (task.CompletedAt != null)
                return Result.Invalid(ScrapingTaskErrors.TaskAlreadyCompleted);

            task.CompletedAt = _dateTimeProvider.UtcNow;

            await _taskRepository.UpdateAsync(task);

            return Result.Success();
        }
    }
}
