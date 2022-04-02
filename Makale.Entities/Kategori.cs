using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace Makale.Entities
{
    [Table("Kategoriler")]
    public class Kategori:EntitiesBase
    {        
        [Required,StringLength(50),DisplayName("Kategori")]
        public string Title { get; set; }

        [StringLength(150), DisplayName("Açıklama")]
        public string Description { get; set; }
        public virtual List<Note> Notes { get; set; }

        public Kategori()
        {
            Notes = new List<Note>();
        }
    }
}
