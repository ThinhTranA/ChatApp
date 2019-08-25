using System.ComponentModel.DataAnnotations;

namespace ChatApp.Web.Server
{
    public class SettingsDataModel
    {
        [Key]
        public string Id { get; set; }

        [Required]
        [MaxLength(2048)]
        public string Name { get; set; }

        [Required]
        [MaxLength(2048)]
        public string Value { get; set; }
    }
}
