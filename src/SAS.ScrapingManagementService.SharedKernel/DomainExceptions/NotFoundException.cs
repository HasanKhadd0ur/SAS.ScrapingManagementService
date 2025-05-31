using SAS.ScrapingManagementService.SharedKernel.DomainExceptions.Base;

namespace SAS.ScrapingManagementService.SharedKernel.DomainExceptions
{
    public abstract class NotFoundException : DomainException
    {
        protected NotFoundException(string message)
            : base(message)
        {
        }
    }
}

