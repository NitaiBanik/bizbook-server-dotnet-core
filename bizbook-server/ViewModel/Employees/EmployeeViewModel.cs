﻿using Model.Model;

namespace ViewModel.Employees
{
    public class EmployeeViewModel : BaseViewModel<Employee>
    {
        public EmployeeViewModel(Employee x) : base(x)
        {
            Name = x.Name;
            Phone = x.Phone;
            Email = x.Email;
            RoleId = x.RoleId;
            Salary = x.Salary;
            SaleTargetAmount = x.SaleTargetAmount;
            SaleAchivedAmount = x.SaleAchivedAmount;
            ShopId = x.ShopId;
        }

        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string RoleId { get; set; }
        public double Salary { get; set; }
        public double SaleTargetAmount { get; set; }
        public double SaleAchivedAmount { get; set; }
        public string ShopId { get; set; }
    }
}