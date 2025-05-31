using System;

namespace SAS.ScrapingManagementService.SharedKernel.DomainExceptions.Base
{
    public class DomainException : Exception
    {
        public DomainException(string message) : base(message)
        {

        }
    }
}
