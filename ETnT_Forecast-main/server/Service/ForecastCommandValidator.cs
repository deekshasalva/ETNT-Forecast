using System.Linq;
using Common.Commands;
using DataAccess;
using DataAccess.DbSets;
using FluentValidation;

namespace Service
{
    public class ForecastCommandValidator : AbstractValidator<ForecastCommand>
    {
        private const string DECIMAL_REGEX = @"^[+-]?((\d+(\.\d*)?)|(\.\d+))$";
        private const string NOT_FOUND = "{PropertyName} not found";
        private const string NEEDS_DECIMAL = "{PropertyName} has to be number";
        public ForecastCommandValidator()
        {
            
            RuleFor(x => x.Org)
                .NotEmpty()
                .Must(Exists<Org>).WithMessage(NOT_FOUND);

            RuleFor(x => x.SkillGroup)
                .NotEmpty()
                .Must(Exists<Skill>).WithMessage(NOT_FOUND);

            RuleFor(x => x.Business)
                .NotEmpty()
                .Must(Exists<Business>).WithMessage(NOT_FOUND);

            RuleFor(x => x.Capability)
                .NotEmpty()
                .Must(Exists<Capability>).WithMessage(NOT_FOUND);

            RuleFor(x => x.ForecastConfidence)
                .NotEmpty()
                .Must(Exists<Category>).WithMessage(NOT_FOUND);

            RuleFor(x => x.Year)
                .NotEmpty()
                .Matches(DECIMAL_REGEX).WithMessage(NEEDS_DECIMAL);
            
            RuleFor(x => x.Jan)
                .NotEmpty()
                .Matches(DECIMAL_REGEX).WithMessage(NEEDS_DECIMAL);
            
            RuleFor(x => x.Feb)
                .NotEmpty()
                .Matches(DECIMAL_REGEX).WithMessage(NEEDS_DECIMAL);
            
            RuleFor(x => x.Mar)
                .NotEmpty()
                .Matches(DECIMAL_REGEX).WithMessage(NEEDS_DECIMAL);
            
            RuleFor(x => x.Apr)
                .NotEmpty()
                .Matches(DECIMAL_REGEX).WithMessage(NEEDS_DECIMAL);
            
            RuleFor(x => x.May)
                .NotEmpty()
                .Matches(DECIMAL_REGEX).WithMessage(NEEDS_DECIMAL);
            
            RuleFor(x => x.June)
                .NotEmpty()
                .Matches(DECIMAL_REGEX).WithMessage(NEEDS_DECIMAL);
            
            RuleFor(x => x.July)
                .NotEmpty()
                .Matches(DECIMAL_REGEX).WithMessage(NEEDS_DECIMAL);
            
            RuleFor(x => x.Aug)
                .NotEmpty()
                .Matches(DECIMAL_REGEX).WithMessage(NEEDS_DECIMAL);
            
            RuleFor(x => x.Sep)
                .NotEmpty()
                .Matches(DECIMAL_REGEX).WithMessage(NEEDS_DECIMAL);
            
            RuleFor(x => x.Oct)
                .NotEmpty()
                .Matches(DECIMAL_REGEX).WithMessage(NEEDS_DECIMAL);
            
            RuleFor(x => x.Nov)
                .NotEmpty()
                .Matches(DECIMAL_REGEX).WithMessage(NEEDS_DECIMAL);
            
            RuleFor(x => x.Dec)
                .NotEmpty()
                .Matches(DECIMAL_REGEX).WithMessage(NEEDS_DECIMAL);

            RuleFor(x => x.Project)
                .NotEmpty()
                .Must(Exists<Project>).WithMessage(NOT_FOUND);

            RuleFor(x => x.Manager)
                .NotEmpty()
                .Must(Exists).WithMessage(NOT_FOUND);

            RuleFor(x => x.USFocal)
                .NotEmpty()
                .Must(Exists).WithMessage(NOT_FOUND);
        }

        private static bool Exists<T>(string name) where T : Lookup
        {
            using var context = new ForecastContext();
            return context.Set<T>().Any(x => x.Value == name);
        }

        private static bool Exists(string name)
        {
            using var context = new ForecastContext();
            return context.Set<User>().ToList().Any(x => x.FullName == name);
        }
    }
}