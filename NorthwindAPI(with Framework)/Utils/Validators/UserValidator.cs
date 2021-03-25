using NorthwindAPI_with_Framework_.DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NorthwindAPI_with_Framework_.Utils.Validators
{
    public class UserValidator
    {
        public static ValidatorResult IsValidUser(UserRegisterDTO user)
        {
            ValidatorResult validatorResult = new ValidatorResult(true);
            if (user == null)
            {
                validatorResult.IsValid = false;
                validatorResult.ValidationMessage = "The user is null.";
                return validatorResult;
            }
            if (user.FullName.Trim().Equals(""))
            {
                validatorResult.IsValid = false;
                validatorResult.ValidationMessage = "The user Full name is required.";
                return validatorResult;
            }
            if (user.Username.Trim().Equals(""))
            {
                validatorResult.IsValid = false;
                validatorResult.ValidationMessage = "The user username is required.";
                return validatorResult;
            }
            if (user.Password.Trim().Equals(""))
            {
                validatorResult.IsValid = false;
                validatorResult.ValidationMessage = "The user password is required.";
                return validatorResult;
            }
            if (user.CustomerId.Trim().Equals(""))
            {
                validatorResult.IsValid = false;
                validatorResult.ValidationMessage = "The CustomerId is required.";
                return validatorResult;
            }
            else if (user.Password.Trim().Length < 8)
            {
                validatorResult.IsValid = false;
                validatorResult.ValidationMessage = "The user Password length must be at less 8 characters.";
                return validatorResult;
            }
            else
            {
                var passwordValidatorResult = PasswordValidator.ValidatePassword(user);
                if (!passwordValidatorResult.IsValid)
                {
                    return passwordValidatorResult;
                }
            }
            return validatorResult;
        }
    }
}