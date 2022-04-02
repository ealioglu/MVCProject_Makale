using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makale.Entities
{
    public class EntitiesBase
    {
        [Key]
        public int ID { get; set; }

        [Required,DisplayName("Oluşturma Tarihi")]
        public DateTime CreateDate { get; set; }

        [Required, DisplayName("Değiştirme Tarihi")]
        public DateTime ModifiedDate { get; set; }

        [Required,StringLength(30), DisplayName("Değiştiren Kullanıcı")]
        public string ModifiedUsername { get; set; }
    }
}
