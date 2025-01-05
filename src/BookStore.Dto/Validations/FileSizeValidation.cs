using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Dto.Validations
{
    public class FileSizeValidation : ValidationAttribute
    {
        private readonly int maxSizeInMegabytes;

        public FileSizeValidation(int maxSizeInMegabytes)
        {
            this.maxSizeInMegabytes = maxSizeInMegabytes;
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if(value is null)
                return ValidationResult.Success;

            IFormFile? formFile = value as IFormFile;

            if(formFile is null)
                return ValidationResult.Success;

            if (formFile.Length > maxSizeInMegabytes * 1024 * 1024)
                return new ValidationResult($"El tamaño del archivo no puede exceder {maxSizeInMegabytes} mb.");

            return ValidationResult.Success;

        }
    }
}
