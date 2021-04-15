using Microsoft.AspNetCore.Mvc;

namespace Confab.Shared.Infrastructure.Api
{
    public class ProducesDefaultContentTypeAttribute : ProducesAttribute
    {

        public ProducesDefaultContentTypeAttribute(params string[] additionalContentTypes) : base("application/json", additionalContentTypes)
        {
        }
    }
}
