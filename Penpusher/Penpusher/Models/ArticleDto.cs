using System;

namespace Penpusher.Models
{
    public class ArticleDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public DateTime Date { get; set; }
        public string Image { get; set; }
        public bool IsInFavorite { get; set; }
        public bool IsInReadLater{ get; set; }
    }
}