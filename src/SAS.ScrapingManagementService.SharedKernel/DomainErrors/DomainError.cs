using Ardalis.Result;

namespace SAS.ScrapingManagementService.SharedKernel.DomainErrors
{
    public class DomainError : ValidationError
    {
        public DomainError(string errorCode, string errorMessage) : base(errorCode,errorMessage)
        {
            ErrorCode = errorCode;
        }


    }
}
