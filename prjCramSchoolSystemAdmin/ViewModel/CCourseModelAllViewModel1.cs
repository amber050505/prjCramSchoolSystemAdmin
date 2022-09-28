using Microsoft.AspNetCore.Mvc.Rendering;
using prjCramSchoolSystemAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace prjCramSchoolSystemAdmin.ViewModel
{
    public class CCourseModelAllViewModel
    {
        public TCourseModel c_model { get; set; }
        //欄位名稱用
        public TCourseModelDetail c_model_detail { get; set; }
        public List<TCourseModelDetail> c_model_detail_list { get; set; }

        public List<SelectListItem> CategoryDDL = new CourseModelMenu().CategoryDropDownList;
        public List<SelectListItem> SchoolYearDDL
        {
            get { return new CourseModelMenu().SchoolYearDropDownList; }
            set { new CourseModelMenu().SchoolYearDropDownList = value; }
        }
    }

    //前端給後端Josn格式轉換
    public class arrDetail
    {
        public int _Count { get; set; }
        public int? FCourseNumber { get; set; }
        public string FSchedule { get; set; }
        public string FScheduleDetail { get; set; }
        public string FTeachingMethod { get; set; }
        public string FRemark { get; set; }

    }
}
