using Core1._0.Dtos.Request;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Core1._0.CoreAttribute
{
    /// <summary>
    /// 参数过滤
    /// </summary>
    public class TouristRouteTitleMustBeDifferentFromDescriptionAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var validateDto = (TouristRouteForCreateDto)validationContext.ObjectInstance;
            if (validateDto.Title == validateDto.Description)
            {

                return new ValidationResult(
                    "路綫名稱必須與路綫描述不同",
                    new[] { typeof(TouristRouteForCreateDto).Name }
                    );
            }
            return ValidationResult.Success;
        }
    }
}
