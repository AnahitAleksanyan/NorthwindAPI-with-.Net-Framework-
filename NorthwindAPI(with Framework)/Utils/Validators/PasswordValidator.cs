using NorthwindAPI_with_Framework_.DTO.Models;
using System;
using System.Text.RegularExpressions;

namespace NorthwindAPI_with_Framework_.Utils.Validators
{
    public static class PasswordValidator
    {
        public static ValidatorResult ValidatePassword(UserRegisterDTO user) {

            ValidatorResult validatorResult = new ValidatorResult(true);

            bool passwordContainsUsername = user.Password.IndexOf(user.Username, StringComparison.OrdinalIgnoreCase) >= 0;

            if (passwordContainsUsername && user.Username.Length >= 3) {
                validatorResult.IsValid = false;
                validatorResult.ValidationMessage = "Password could not contains the username.";
                return validatorResult;
            }

            validatorResult = PasswordContainsFullName(user.Password, user.FullName);

            if (!validatorResult.IsValid) {
                return validatorResult;
            }

            Regex regex = new Regex(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$");
            Match match = regex.Match(user.Password);
            if (!match.Success) {
                validatorResult.IsValid = false;
                validatorResult.ValidationMessage = "Password must contain at less one Upper case letter, one lower case letter, one digit number and one special symbol.";
                return validatorResult;
            }

            return validatorResult;
        }

        private static ValidatorResult PasswordContainsFullName(string password,string fullName) {

            ValidatorResult validatorResult = new ValidatorResult(true);

            bool passwordContainsFullName = password.IndexOf(fullName, StringComparison.OrdinalIgnoreCase) >= 0;

            if (passwordContainsFullName && fullName.Length >= 3)
            {
                validatorResult.IsValid = false;
                validatorResult.ValidationMessage = "Password could not contains the full Name.";
                return validatorResult;
            }

            var splitWithComma = fullName.Split(',');
            var splitWithPeriod = fullName.Split('.');
            var splitWithDashes = fullName.Split('-');
            var splitWithunderscore = fullName.Split('_');
            var splitWithSpase= fullName.Split(' ');

            for (int i = 0; i < splitWithComma.Length; i++) {
                bool containsStr = password.IndexOf(splitWithComma[i], StringComparison.OrdinalIgnoreCase) >= 0;
                if (containsStr && splitWithComma[i].Length>=3) {
                    validatorResult.IsValid = false;
                    validatorResult.ValidationMessage = "Password could not contains part of the Full name";
                    return validatorResult;
                }
            }

            for (int i = 0; i < splitWithPeriod.Length; i++)
            {
                bool containsStr = password.IndexOf(splitWithPeriod[i].Trim(), StringComparison.OrdinalIgnoreCase) >= 0;
                if (containsStr && splitWithPeriod[i].Trim().Length >= 3)
                {
                    validatorResult.IsValid = false;
                    validatorResult.ValidationMessage = "Password could not contains part of the Full name";
                    return validatorResult;
                }
            }

            for (int i = 0; i < splitWithDashes.Length; i++)
            {
                bool containsStr = password.IndexOf(splitWithDashes[i].Trim(), StringComparison.OrdinalIgnoreCase) >= 0;
                if (containsStr && splitWithDashes[i].Trim().Length >= 3)
                {
                    validatorResult.IsValid = false;
                    validatorResult.ValidationMessage = "Password could not contains part of the Full name";
                    return validatorResult;
                }
            }


            for (int i = 0; i < splitWithunderscore.Length; i++)
            {
                bool containsStr = password.IndexOf(splitWithunderscore[i].Trim(), StringComparison.OrdinalIgnoreCase) >= 0;
                if (containsStr && splitWithunderscore[i].Trim().Length >= 3)
                {
                    validatorResult.IsValid = false;
                    validatorResult.ValidationMessage = "Password could not contains part of the Full name";
                    return validatorResult;
                }
            }

            for (int i = 0; i < splitWithSpase.Length; i++)
            {
                bool containsStr = password.IndexOf(splitWithSpase[i].Trim(), StringComparison.OrdinalIgnoreCase) >= 0;
                if (containsStr && splitWithSpase[i].Trim().Length >= 3)
                {
                    validatorResult.IsValid = false;
                    validatorResult.ValidationMessage = "Password could not contains part of the Full name";
                    return validatorResult;
                }
            }

            return validatorResult;
        }
       
    }
}