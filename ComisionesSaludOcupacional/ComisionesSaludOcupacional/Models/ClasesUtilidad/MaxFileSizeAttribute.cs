using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace ComisionesSaludOcupacional.Models.ViewModels
{
    public class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly int _maxFileSize;
        public MaxFileSizeAttribute(int maxFileSize)
        {
            _maxFileSize = maxFileSize;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var file = value as HttpPostedFileBase;

            if (file.ContentLength > _maxFileSize)
            {
                return new ValidationResult("Excede el tamaño máximo del archivo, debe ser menor a 10MB");
            }

            return ValidationResult.Success;
        }
    }
}