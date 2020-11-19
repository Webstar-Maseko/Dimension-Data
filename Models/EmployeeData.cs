using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dimension_Data.Models
{
    public partial class EmployeeData
    {
        public int Age { get; set; }
        public string Attrition { get; set; }
        [Display(Name = "Business Travel")]
        public string BusinessTravel { get; set; }
        [Display(Name = "Daily rate")]
        public int DailyRate { get; set; }
        public string Department { get; set; }
        [Display(Name = "Distance from home")]
        public int DistanceFromHome { get; set; }
        public int Education { get; set; }
        [Display(Name = "Education field")]
        public string EducationField { get; set; }
        [Display(Name = "Employee count")]
        public int EmployeeCount { get; set; }
        [Display(Name = "Employee number")]
        public int EmployeeNumber { get; set; }
        [Display(Name = "Environment satisfaction")]
        public int EnvironmentSatisfaction { get; set; }
        public string Gender { get; set; }
        [Display(Name = "Hourly rate")]
        public int HourlyRate { get; set; }
        [Display(Name = "Job involvement")]
        public int JobInvolvement { get; set; }
        [Display(Name = "Job level")]
        public int JobLevel { get; set; }
        [Display(Name = "Job role")]
        public string JobRole { get; set; }
        [Display(Name = "Job satisfaction")]
        public int JobSatisfaction { get; set; }
        [Display(Name = "Marital status")]
        public string MaritalStatus { get; set; }
        [Display(Name = "Monthly income")]
        [DataType(DataType.Currency)]

        public int MonthlyIncome { get; set; }
        [Display(Name = "Monthly rate")]
        [DataType(DataType.Currency)]
        public int MonthlyRate { get; set; }
        [Display(Name = "Number of companies worked for")]
        public int NumCompaniesWorked { get; set; }
        [Display(Name = "Over 18")]
        public string Over18 { get; set; }
        [Display(Name = "Over time")]
        public string OverTime { get; set; }
        [Display(Name = "Salary hike percent")]
        public int PercentSalaryHike { get; set; }
        [Display(Name = "Perfomance rating")]
        public int PerformanceRating { get; set; }
        [Display(Name = "Relationship satisfaction")]
        public int RelationshipSatisfaction { get; set; }
        [Display(Name = "Standard hours")]
        public int StandardHours { get; set; }
        [Display(Name = "Stock option level")]
        public int StockOptionLevel { get; set; }
        [Display(Name = "Total working years")]
        public int TotalWorkingYears { get; set; }
        [Display(Name = "Training times last year")]
        public int TrainingTimesLastYear { get; set; }
        [Display(Name = "Work life balance")]
        public int WorkLifeBalance { get; set; }
        [Display(Name = "Years at company")]
        public int YearsAtCompany { get; set; }
        [Display(Name = "Years in current role")]
        public int YearsInCurrentRole { get; set; }
        [Display(Name = "Years since last promotion")]
        public int YearsSinceLastPromotion { get; set; }
        [Display(Name = "Years with current manager")]

        public int YearsWithCurrManager { get; set; }
        public string UserId { get; set; }

        public virtual AspNetUsers User { get; set; }
    }
}
