using MediatR;
using System;

namespace SAS.ScrapingManagementService.SharedKernel.DomainEvents
{
    public interface IDomainEvent : INotification
    {
        //public DateTime DateOccurred { get; set; };

    }
}
