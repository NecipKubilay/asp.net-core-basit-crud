using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BasitAlisveris.Models
{
    public class Admin
    {
        [Key]
        public int AdminId { get; set; }


        [DisplayName("Kullanici Adi")]
        [Required(ErrorMessage = "Bu alani bos birakamazsiniz")]
        public string AdminName { get; set; }


        [DisplayName("Sifre")]
        [Required(ErrorMessage = "Bu alani bos birakamazsiniz")]
        [MinLength(8, ErrorMessage = "8 karakterden az olamaz")]
        public string AdminPassword { get; set; }
    }
}
