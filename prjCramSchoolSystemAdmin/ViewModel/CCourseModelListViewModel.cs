using Microsoft.AspNetCore.Mvc.Rendering;
using prjCramSchoolSystemAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace prjCramSchoolSystemAdmin.ViewModel
{
    public class CCourseModelListViewModel
    {
        public string[] CategoryDDL = CourseData.c_name;
        public TCourseModel rowName { get; set; }
        public List<CCourseModel_List> course_model { get; set; }
    }

    public class CCourseModel_List
    {
        private string txtCategory = "";
        private string txtSchoolyear = "";    
        private decimal? _OriginalPrice;
        private decimal? _SpecialOffer;
        public string FCourseId { get; set; }
        public string FName { get; set; }
        public string FCategory
        {
            get { return changeCategory_name(txtCategory); }
            set { txtCategory = value; }
        }
        public string FTotalHours { get; set; }
        public string FTotalNumber { get; set; }
        public string FGrade { get; set; }
        public decimal? FOriginalPrice
        {
            get
            {
                if (_OriginalPrice==null)
                    return 0;
                else
                    return _OriginalPrice;
            }
            set { _OriginalPrice = value; }
        }
        public string txtOriginalPrice
        {
            get
            {
                if (FOriginalPrice != 0)
                {
                    string price = FOriginalPrice?.ToString("0");
                    return String.Format("{0:0,0}", Convert.ToDecimal(price));
                }
                else
                    return "";
            }
        }
        public decimal? FSpecialOffer {
            get
            {
                if (_SpecialOffer == null)
                    return 0;
                else
                    return _SpecialOffer;
            }

            set { _SpecialOffer = value; }
        }
        public string txtSpecialOffer
        {
            get
            {
                if (FOriginalPrice != 0)
                {
                    string money = _SpecialOffer?.ToString("0");
                    return String.Format("{0:0,0}", Convert.ToDecimal(money));
                }
                else
                    return "";
            }
        }
        public string FSchoolYear
        {
            get { return changeSchoolYear(txtSchoolyear); }
            set { txtSchoolyear = value; }
        }

        //課程類別代碼轉為文字
        public string changeCategory_name(string category)
        {
            if (!string.IsNullOrEmpty(category))
            {
                int p = Array.IndexOf(CourseData.c_number, category);
                return CourseData.c_name[p];
            }
            return "";
        }
        //學期代碼轉為文字
        public string changeSchoolYear(string schoolyear)
        {
            if (!string.IsNullOrEmpty(schoolyear))
            {
                CourseData c = new CourseData();
                int p = Array.IndexOf(c.schoolyear_number, schoolyear);
                return c.schoolyear_name[p];
            }
            return "";
        }

    }
}
