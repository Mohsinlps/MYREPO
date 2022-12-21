using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace BeajLearner.WebApi.Helpers
{
    public class ModelStateHelper
    {
        public static List<string> GetValidationErrorMessages(ActionContext context)
        {
            //var errorDictionary = new Dictionary<string, List<string>>();
            var errors = new List<string>();

            foreach (var modelStateKey in context.ModelState.Keys)
            {
                var modelStateVal = context.ModelState[modelStateKey];
                foreach (var error in modelStateVal.Errors)
                {
                    var propertyName = "";
                    if (modelStateKey.Contains('.'))
                        propertyName = Regex.Replace(modelStateKey, @"\[(.*?)\]", string.Empty);
                    else
                        propertyName = modelStateKey;

                    //if (!errorDictionary.ContainsKey(propertyName))
                    //    errorDictionary.Add(propertyName, new List<string>());

                    string errorMessage = "";
                    if (error.ErrorMessage.Contains('.'))
                        errorMessage = error.ErrorMessage; //.Substring(0, error.ErrorMessage.IndexOf(". Path"));
                    else
                        errorMessage = error.ErrorMessage;

                    errors.Add($"{propertyName}: {errorMessage}");
                }
            }
            return errors;
        }
    }
}
