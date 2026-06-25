using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Shubhdecoration.Data.Decoration
{
    public class DecorationModel
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public List<CardViewModel> CardList { get; set; } = new List<CardViewModel>();
    }

    public class AlldecorationModel
    {
        public List<DecorationModel> decorations { get; set; } = new List<DecorationModel>();
    }
    public class Category
    {
        public int CategoryId { get; set; }
        [Required]   
        public string CategoryName { get; set; } = string.Empty;
        [Required]
        public string? Description { get; set; }
        [Required]
        public IFormFile? ImgFile { get; set; }
        [Required]
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
    public class CreateCardModel
    {
        public int DecoTypeId { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]
        public string DecoName { get; set; } = string.Empty;
        [Required]
        public string? Description { get; set; }
        public IFormFile? Image { get; set; }
        public string? ImageUrl { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public bool IsActive { get; set; } = true;
    }
    public class CardViewModel
    {
        public int DecoTypeId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string DecoName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public decimal Price { get; set; }
    }
}
