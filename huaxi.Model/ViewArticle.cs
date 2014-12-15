using System;

namespace huaxi
{
    public class ViewArticle
    {
        public int ID { get; set; }
        public int CateID { get; set; }
        public string Contents { get; set; }
        public string BeFrom { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string UserName { get; set; }
        public string DocUrl { get; set; }
        public string UserUrl { get; set; }
        public int Status { get; set; }
        public string StatusName { get; set; }
        public string Cate { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public int Comment { get; set; }
        public int Views { get; set; }
    }
}
