using FluentValidation;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;

namespace API.ViewQuery
{
    public class UploadQueryValidator : AbstractValidator<UploadQuery>
    {
        public UploadQueryValidator()
        {
            RuleFor(o => o.Files)
                .Must(ExistFiles)
                    .WithMessage("The files parameter is mandatory.");

            RuleFor(o => o.Files)
               .Must(ValidateFiles)
                   .When(f => ExistFiles(f.Files))
                   .WithMessage("All files must be of type 'ofx'.");

            static bool ExistFiles(List<IFormFile> files)
            {
                return files != null && files.Any();
            }

            static bool ValidateFiles(List<IFormFile> files)
            {
                foreach (var file in files)
                {
                    if (!file.FileName.Contains(".ofx")) return false;
                }
                return true;
            }
        }
    }
}
