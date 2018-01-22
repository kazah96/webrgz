using System.ComponentModel.DataAnnotations;
namespace rgz.Models
{
    public class AddGoodModel
    {
        public int id{get;set;}
        [Required]
        public string Name{get;set;}
        [Required]
        public decimal Price{get;set;}
        [Required]
        public string Description{get;set;}
        [Required]
        public string Street{get;set;}
        [Required]
        public string StreetNumber{get;set;}
        public string ImgPath{get;set;}

    }
}