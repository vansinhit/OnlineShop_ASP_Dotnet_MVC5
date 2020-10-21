namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User
    {
        public long ID { get; set; }

        [StringLength(50)]
        [DisplayName("Tên đăng nhập")]
        public string UserName { get; set; }

        [StringLength(32)]
        [DisplayName("Mật khẩu")]
        public string Password { get; set; }

        [StringLength(50)]
        [DisplayName("Tên người dùng")]
        public string Name { get; set; }

        [StringLength(50)]
        [DisplayName("Địa chỉ")]
        public string Address { get; set; }

        [StringLength(50)]
        [DisplayName("Email")]
        public string Email { get; set; }

        [StringLength(50)]
        [DisplayName("Sô điện thoại")]
        public string Phone { get; set; }

        public DateTime? CreateDate { get; set; }

        [StringLength(50)]
        public string CreateBy { get; set; }

        public DateTime? ModifieDate { get; set; }

        [StringLength(250)]
        public string ModifieBy { get; set; }
        [DisplayName("Quyền truy cập")]
        public bool Status { get; set; }
    }
}
