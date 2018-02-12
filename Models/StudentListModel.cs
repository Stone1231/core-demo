using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using coreDemo.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using ReflectionIT.Mvc.Paging;

namespace coreDemo.Models
{
    public class StudentListModel
    {
        [Display(Name = "名稱")]
        public string Name { get; set; }

        [Display(Name = "班級")]
        public string ClassId { get; set; }
        public IEnumerable<SelectListItem> ClassIdSelect { get; set; }

        public PagingList<Student> List { get; set; }
        //public IList<StudentInfo> List { get; set; }        
    }

}
