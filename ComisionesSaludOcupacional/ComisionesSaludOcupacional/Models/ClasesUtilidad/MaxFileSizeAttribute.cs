using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace ComisionesSaludOcupacional.Models.ViewModels
{
    /*Esta clase fue implementada con la finalidad de atajar el error que ocurre cuando
     * un archivo es demasiado grande para el sistema. No se está utilizando, ya que se 
     * abordó la situación mediante otro enfoque, ya que este no era posible en la máquina
     * actual. Pero si se pudiese llegar a implementar este enfoque, es más dinámico y no
     * necesita de una vista adicional, sino que el mensaje de error se mostraría directamente
     * en la vista, debajo del botón de "Añadir archivo".*/ 
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

            if(file != null)
            {
                if (file.ContentLength > _maxFileSize)
                {
                    return new ValidationResult("Excede el tamaño máximo del archivo, debe ser menor a 10MB");
                }
            }
            

            return ValidationResult.Success;
        }
    }
}