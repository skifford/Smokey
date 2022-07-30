using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Smokey.Models
{
    public record BrowserConfiguration
    {
        /// <summary>
        /// Type of browser: Chrome, Firefox, Edge, etc.
        /// </summary>
        [Required]
        public BrowserType BrowserType { get; init; }
        
        /// <summary>
        /// Source of Selenium WebDriver on the drive
        /// </summary>
        [Required]
        [NotNull]
        public string DriverSource { get; init; }
        
        /// <summary>
        /// Arguments of Selenium WebDriver, for example: "--window-size=1366,768", "--headless"
        /// </summary>
        [Required]
        [NotNull]
        public IReadOnlyCollection<string> Arguments { get; init; }
        
        /// <summary>
        /// Uri of host for remote running tests in docker containers
        /// </summary>
        [Required]
        [NotNull]
        public string RemoteHost { get; init; }
    }
}
