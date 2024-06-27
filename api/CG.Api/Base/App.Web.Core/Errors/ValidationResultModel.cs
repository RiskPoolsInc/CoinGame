using App.Core.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace App.Web.Core.Errors;

public class ValidationResultModel : ErrorModel {
    public ValidationResultModel() : this((ModelStateDictionary)null) {
    }

    public ValidationResultModel(BadRequestException exception) {
        Message = exception.Message;
    }

    public ValidationResultModel(ValidationException exception) {
        Message = exception.Message;

        if (exception.Errors != null)
            Errors = exception.Errors
                              .Select(a => new ValidationError(a.PropertyName, a.ErrorMessage))
                              .ToList();
    }

    public ValidationResultModel(ModelStateDictionary modelState) {
        Message = @"Validation Failed";

        if (modelState != null) {
            Errors = new List<ValidationError>();

            modelState.Keys.ToList()
                      .ForEach(key => {
                           var messages = modelState[key]
                                         .Errors.ToList()
                                         .Select(x => {
                                              var message = x.ErrorMessage;

                                              if (string.IsNullOrWhiteSpace(message))
                                                  if (x.Exception is JsonException)
                                                      message = $"The data in {key} field is of incorrect format";
                                                  else
                                                      message = x.Exception.Message;
                                              return message;
                                          });

                           if (messages.Count() == 0 && modelState[key].ValidationState == ModelValidationState.Unvalidated)
                               messages = new[] { $"The data in {key} field is of incorrect format" };
                           messages.Distinct().ToList().ForEach(m => Errors.Add(new ValidationError(key, m)));
                       });
        }
    }

    public List<ValidationError> Errors { get; }
}