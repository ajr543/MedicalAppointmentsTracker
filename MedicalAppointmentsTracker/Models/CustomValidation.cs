using System.ComponentModel.DataAnnotations;

namespace MedicalAppointmentsTracker.Models
{
    public class CustomValidation
    {
    }

    public class FutureDateRangeAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime dateValue)
            {
                DateTime today = DateTime.Now.Date;
                DateTime maxDate = today.AddYears(1); // Allow only up to 1 year ahead

                if (dateValue < today || dateValue > maxDate)
                {
                    return new ValidationResult($"Date must be between today ({today.ToShortDateString()}) and {maxDate.ToShortDateString()}.");
                }
            }

            return ValidationResult.Success;
        }
    }


}
