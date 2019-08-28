using System.ComponentModel.DataAnnotations;

namespace ChatApp.Web.Server
{
    public class SettingsDataModel
    {
        /// <summary>
        /// The unique Id for this entry
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// The settings name
        /// </summary>
        /// <remarks>This column is indexed</remarks>
        [Required]
        [MaxLength(2048)]
        public string Name { get; set; }

        /// <summary>
        /// The settings value
        /// </summary>
        [Required]
        [MaxLength(2048)]
        public string Value { get; set; }
    }
}
