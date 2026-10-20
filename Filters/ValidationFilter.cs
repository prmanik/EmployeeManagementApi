using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementApi.Filters
{

    public class ValidationFilter<T> : IEndpointFilter where T : class
    {
        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            var model = context.Arguments.OfType<T>().FirstOrDefault();
            if (model is null)
            {
                return Results.BadRequest("Invalid request payload.");
            }

            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(model);
            if (!Validator.TryValidateObject(model, validationContext, validationResults, true))
            {
                var errors = validationResults.Select(vr => vr.ErrorMessage).ToList();
                return Results.BadRequest(errors);
            }

            return await next(context);
        }
    }
}