using SAS.ScrapingManagementService.SharedKernel.DomainExceptions.Base;

namespace SAS.ScrapingManagementService.SharedKernel.DomainExceptions
{
    public abstract class BadRequestException : DomainException
    {
        protected BadRequestException(string message)
            : base(message)
        {
        }
    }
}
