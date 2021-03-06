using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace coreDemo.Entity
{
    /// <summary>
    /// 班級
    /// </summary>
    [Table("ClassM")]
    public partial class ClassM
    {

        /// <summary>
        /// 班級代碼
        /// </summary>
        [Display(Name = "班級代碼")]
        [Key]
        public string Id{ get; set; }

        /// <summary>
        /// 班級名
        /// </summary>
        [Display(Name = "班級名")]
        public string Name{ get; set; }

        public virtual ICollection<Student> Students { get; set; }
    }
}
