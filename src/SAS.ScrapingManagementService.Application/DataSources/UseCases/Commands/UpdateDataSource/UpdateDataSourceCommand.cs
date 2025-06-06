﻿using Ardalis.Result;
using SAS.ScrapingManagementService.SharedKernel.CQRS.Commands;

namespace SAS.ScrapingManagementService.Application.DataSources.UseCases.Commands.UpdateDataSource
{
    public sealed record UpdateDataSourceCommand(
    Guid Id,
    string Name,
    string Target,
    Guid DomainId,
    Guid PlatformId) : ICommand<Result>;

}