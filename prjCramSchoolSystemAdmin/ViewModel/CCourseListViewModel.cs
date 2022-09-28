using prjCramSchoolSystemAdmin.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace prjCramSchoolSystemAdmin.ViewModel
{
    public class CCourseListViewModel
    {
        public string[] CategoryDDL = CourseData.c_name;
        public TCourseInformation rowName { get; set; }
        public List<CCourse_List> course { get; set; }

        //public List<TCourseInformation> course { get; set; }
        //public List<string> classstate { get; set; }
    }

    public class CCourse_List
    {
        public string FEchelonId { get; set; }
        public string FName { get; set; }
        public string FCourse_FName { get; set; }
        //[DataType(DataType.Date)]
        public DateTime? FStartTime { get; set; }
        public string txtStartTime
        {
            get
            {
                if (FStartTime == null)
                    return "";
                return Convert.ToDateTime(FStartTime).ToString("yyyy/MM/dd");
            }
        }
        //[DataType(DataType.Date)]
        public DateTime? FEndTime { get; set; }
        public string txtEndTime
        {
            get
            {
                if (FEndTime == null)
                    return "";
                return Convert.ToDateTime(FEndTime).ToString("yyyy/MM/dd");
            }
        }

        public int? FClassState { get; set; }
        public string txtClassState
        {
            get
            {
                return changeClassState_name(FClassState);
            }
        }
        public string FTeacher { get; set; }
        public DateTime? FDiscountDate { get; set; }
        public decimal? FOriginalPrice { get; set; }
        public decimal? FSpecialOffer { get; set; }
        public string txtPrice
        {
            get
            {
                decimal? money = checkPrice(FOriginalPrice, FSpecialOffer, FDiscountDate);
                return String.Format("{0:0,0}", (decimal)money);
            }
        }

        //確認價錢
        public decimal? checkPrice(decimal? fOriginalPrice, decimal? fSpecialOffer, DateTime? fDiscountDate)
        {
            if (fOriginalPrice == null)
                fOriginalPrice = 0;
            if (fSpecialOffer == null)
                fSpecialOffer = 0;
            DateTime now = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            if (fDiscountDate >= now)
                return fSpecialOffer;
            else
                return fOriginalPrice;
        }
        
        //課程狀態代碼轉為文字
        public string changeClassState_name(int? classstate)
        {
            if (classstate == null)
                return "";
            CourseData c = new CourseData();
            int p = Array.IndexOf(c.classstate_number, classstate.ToString());
            if (p == -1)
                return "";
            return c.classstate_name[p];
        }
    }
}
