using System.ComponentModel.DataAnnotations;

namespace ExploreCalifornia.Models
{
    public class MonthlySpecial
    {
        public long Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5)]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        private string _key;

        public string Key
        {
            get
            {
                if (_key == null)
                {
                    _key = Name.ToLower().Replace(" ", "").Substring(0, Math.Min(4, Name.Length));
                }
                return _key;
            } 
            set
            {
              _key = value;
            }
        }
        
        public string? Type { get; set; }
        [Required]
        public Decimal Price { get; set; }

    }
}
