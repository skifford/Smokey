using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Smokey
{
    public record BrowserConfiguration
    {
        [Required]
        public BrowserType BrowserType { get; init; }
        
        [Required]
        [NotNull]
        public string DriverSource { get; init; }
        
        [Required]
        [NotNull]
        public IReadOnlyCollection<string> Arguments { get; init; }
        
        [Required]
        [NotNull]
        public string RemoteHost { get; init; }
    }
}
