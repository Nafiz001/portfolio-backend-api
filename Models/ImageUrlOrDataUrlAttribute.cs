using System.ComponentModel.DataAnnotations;

namespace PortfolioAPI.Models
{
    public class ImageUrlOrDataUrlAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value == null)
                return false;

            string? imageValue = value.ToString();
            
            if (string.IsNullOrWhiteSpace(imageValue))
                return false;

            // Check if it's a data URL (starts with data:image/)
            if (imageValue.StartsWith("data:image/", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            // Check if it's a valid HTTP/HTTPS URL
            if (Uri.TryCreate(imageValue, UriKind.Absolute, out Uri? uri))
            {
                return uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps;
            }

            return false;
        }

        public override string FormatErrorMessage(string name)
        {
            return $"The {name} field must be a valid HTTP/HTTPS URL or a data URL.";
        }
    }
}
