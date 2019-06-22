﻿using System;
using System.Linq.Expressions;
using Model.Model;
using ViewModel;

namespace RequestModel.Employees
{
    public class EmployeeInfoRequestModel : RequestModel<Employee>
    {
        public EmployeeInfoRequestModel(string keyword, string orderBy = "Modified", string isAscending = "False") :
            base(keyword, orderBy, isAscending)
        {
        }

        public bool IsEmployeeActive { get; set; }

        public string RoleId { get; set; }

        public string WarehouseId { get; set; }

        protected override Expression<Func<Employee, bool>> GetExpression()
        {
            if (!string.IsNullOrWhiteSpace(Keyword))
            {
                ExpressionObj = x =>
                    x.Name.ToLower().Contains(Keyword) || x.Phone.ToLower().Contains(Keyword) ||
                    x.Email.ToLower().Contains(Keyword);
            }

            if (RoleId.IdIsOk())
            {
                this.ExpressionObj = this.ExpressionObj.And(x => x.RoleId == RoleId);
            }

            //if (WarehouseId.IdIsOk())
            //{
            //    this.ExpressionObj = this.ExpressionObj.And(x => x.WarehouseId == WarehouseId);
            //}

            if (IsEmployeeActive)
            {
                this.ExpressionObj = this.ExpressionObj.And(x => x.IsActive == IsEmployeeActive);
            }

            ExpressionObj = ExpressionObj.And(x => x.ShopId == ShopId);
            ExpressionObj = ExpressionObj.And(GenerateBaseEntityExpression());
            return ExpressionObj;
        }

        public override Expression<Func<Employee, DropdownViewModel>> Dropdown()
        {
            return x => new DropdownViewModel() { Id = x.Id, Text = x.Name };
        }
    }
}