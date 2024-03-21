using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace RMS.Utility
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ValidateFileAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value,
      ValidationContext validationContext)
        {
            HttpPostedFileBase file = value as HttpPostedFileBase;

            // The file is required.
            if (file == null)
            {
                return new ValidationResult("Please upload a file!");
            }

            // The meximum allowed file size is 10MB.
            if (file.ContentLength > 10 * 1024 * 1024)
            {
                return new ValidationResult("This file is too big!");
            }

            // Only PDF can be uploaded.
            string ext = Path.GetExtension(file.FileName);
            if (String.IsNullOrEmpty(ext) ||
               !ext.Equals(".pdf", StringComparison.OrdinalIgnoreCase))
            {
                return new ValidationResult("This file is not a PDF!");
            }

            // Everything OK.
            return ValidationResult.Success;
        }
    }
}
