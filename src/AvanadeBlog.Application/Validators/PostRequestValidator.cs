using AvanadeBlog.Application.Models.Post;
using FluentValidation;

namespace AvanadeBlog.Application.Validators
{
    public class PostRequestValidator : AbstractValidator<PostRequest>
    {
        public PostRequestValidator()     
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(255)
                .WithMessage("Title is required and the maximum length is 255 characters.");

            RuleFor(x => x.Content)
                .NotEmpty()
                .MaximumLength(4000)
                .WithMessage("Content is required and the maximum length is 4000 characters.");
        }
    }
}
    