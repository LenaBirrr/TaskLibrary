
using FluentValidation;

namespace TaskLibrary.Web.Pages.Languages.Models
{
    public class LanguageModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }

        public string Level { get; set; }

        public string Paradigm { get; set; }

        public string Realization { get; set; }
    }
    public class LanguageModelValidator : AbstractValidator<LanguageModel>
    {
        public LanguageModelValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(50).WithMessage("Name is long.");
            RuleFor(x => x.Level)
                .NotEmpty().WithMessage("Level is required.")
                .MaximumLength(50).WithMessage("Level is long.");
            RuleFor(x => x.Paradigm)
                .NotEmpty().WithMessage("Paradigm is required.")
                .MaximumLength(50).WithMessage("Paradigm is long.");
            RuleFor(x => x.Realization)
                .NotEmpty().WithMessage("Realization is required.")
                .MaximumLength(50).WithMessage("Realization is long.");
        }
        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<LanguageModel>.CreateWithOptions((LanguageModel)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };
    }

    
}
