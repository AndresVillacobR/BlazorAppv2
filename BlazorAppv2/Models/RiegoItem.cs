using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace BlazorAppv2.Models
{
    [Table("riegos")]
    public class RiegoItem : BaseModel
    {
        public Guid id { get; set; }
        public Guid dispositivo_id { get; set; }
        public DateTime inicio { get; set; }
        public DateTime fin { get; set; }
        public string tipo { get; set; } = string.Empty;
        public double humedad_inicio { get; set; }


    }
}