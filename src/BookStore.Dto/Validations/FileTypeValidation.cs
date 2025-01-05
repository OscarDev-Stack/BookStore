using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace MusicStore.Dto.Validations
{
    public class FileTypeValidation : ValidationAttribute
    {
        private readonly string[]? validTypes;

        public FileTypeValidation(string[] validTypes)
        {
            this.validTypes = validTypes;
        }

        public FileTypeValidation(FileTypeGroup fileTypeGroup)
        {
            if (fileTypeGroup is FileTypeGroup.Image)
            {
                validTypes = ["image/jpeg", "image/png", "image/jpg"];
            }
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is null)
                return ValidationResult.Success;

            if (validTypes is null || validTypes.Length == 0)
                return new ValidationResult("No se encontro un tipo valido.");

            IFormFile? formfile = value as IFormFile;

            if (formfile is null)
                return ValidationResult.Success;

            if (!validTypes.Contains(formfile.ContentType))
                return new ValidationResult($"Tipo de archivo no valido, solo se permite: {string.Join(",", validTypes)}");

            return ValidationResult.Success;
        }
    }
    public enum FileTypeGroup
    {
        Image
    }
}

