namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Slide")]
    public partial class Slide
    {
        public long ID { get; set; }

        [StringLength(50)]
        public string Image { get; set; }

        public int? DisplayOrder { get; set; }

        [StringLength(50)]
        public string Link { get; set; }

        [StringLength(10)]
        public string Decriptions { get; set; }

        public DateTime? CreateDate { get; set; }

        [StringLength(50)]
        public string CreateBy { get; set; }

        public DateTime? ModifieDate { get; set; }

        [StringLength(250)]
        public string ModifieBy { get; set; }

        public bool? Status { get; set; }
    }
}
