using NorthwindAPI_with_Framework_.DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NorthwindAPI_with_Framework_.Utils.Validators
{
    public class CourseCustomerValidator
    {
        public static ValidatorResult IsValidCourseCustomer(CourseCustomerCastDTO courseCustomerCast)
        {
            ValidatorResult validatorResult = new ValidatorResult(true);
            if (courseCustomerCast == null)
            {
                validatorResult.IsValid = false;
                validatorResult.ValidationMessage = "The courseCustomerCast is null.";
                return validatorResult;
            }

            if (courseCustomerCast.CourseId == 0)
            {
                validatorResult.IsValid = false;
                validatorResult.ValidationMessage = "The courseId is required.";
                return validatorResult;
            }
            if (courseCustomerCast.CustomerId.Trim().Equals(""))
            {
                validatorResult.IsValid = false;
                validatorResult.ValidationMessage = "The customerId is required.";
                return validatorResult;
            }
            return validatorResult;
        }
    }
}