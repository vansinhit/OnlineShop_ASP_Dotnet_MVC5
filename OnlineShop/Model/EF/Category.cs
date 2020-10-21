namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Category")]
    public partial class Category
    {
        public long ID { get; set; }

        [StringLength(250)]
        [Display(Name ="Category_Name", ResourceType =typeof(StaticResouce.Resources))]
        [Required(ErrorMessageResourceName = "Category_RequiredName",ErrorMessageResourceType =typeof(StaticResouce.Resources))]
        public string Name { get; set; }
        [Display(Name = "Category_Metatitle", ResourceType = typeof(StaticResouce.Resources))]
        [StringLength(250)]
        public string Metatile { get; set; }
        [Display(Name = "Category_ParenID", ResourceType = typeof(StaticResouce.Resources))]
        public long? ParenID { get; set; }
        [Display(Name = "Category_DisplayOrder", ResourceType = typeof(StaticResouce.Resources))]
        public int? DisplayOrder { get; set; }
        [Display(Name = "Category_SeoTitle", ResourceType = typeof(StaticResouce.Resources))]
        [StringLength(250)]
        public string SeoTitle { get; set; }

        public DateTime? CreateDate { get; set; }

        [StringLength(50)]
        public string CreateBy { get; set; }

        public DateTime? ModifieDate { get; set; }

        [StringLength(250)]
        public string ModifieBy { get; set; }

        [StringLength(250)]
        [Display(Name = "Category_Metakeywords", ResourceType = typeof(StaticResouce.Resources))]
        public string Metakeywords { get; set; }

        [StringLength(250)]
        [Display(Name = "Category_MetaDecriptions", ResourceType = typeof(StaticResouce.Resources))]
        public string MetaDecriptions { get; set; }
        [Display(Name = "Category_Status", ResourceType = typeof(StaticResouce.Resources))]
        [Required(ErrorMessageResourceName = "Category_RequiredStatus", ErrorMessageResourceType = typeof(StaticResouce.Resources))]
        public bool? Status { get; set; }
        [Display(Name = "Category_ShowOnHome", ResourceType = typeof(StaticResouce.Resources))]
        public bool? ShowOnHome { get; set; }
        public string Language { get; set; }
    }
}
