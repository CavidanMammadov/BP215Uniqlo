﻿namespace BP215Uniqlo.Models
{
    public class ProductComment
    {


        public int Id { get; set; }
        public int ProductId { get; set; }
        public string USerId { get; set; }
        public string Comment { get; set; }

    }
}
