using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using HaripayTechnicalAssessmentData.Model;
using HaripayTechnicalAssessmentData.Interface;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using HaripayTechnicalAssessmentData;

namespace HaripayTechnicalAssessmentServices
{
    public class DiscountService : IDiscountService
    {
        private readonly HaripayDataContext _context;

        public DiscountService(HaripayDataContext context)
        {
            _context = context;
        }

        public Discount CreateDiscount(string type, decimal value, int discountType, int status)
        {
            Discount discount = null;

            try
            {
                if (String.IsNullOrWhiteSpace(type))
                {
                    throw new Exception("Type Is Required");
                }

                if (!Enum.IsDefined(typeof(DiscountType), discountType))
                {
                    throw new InvalidCastException("Invalid Discount Type");
                }
                DiscountType discountTypeSelected = (DiscountType)discountType;

                if(discountTypeSelected == DiscountType.Percentage)
                {
                    if (value < 0 || value > 100)
                    {
                        throw new InvalidOperationException("Percentage must be between 1 to 100");
                    }
                }
                else
                {
                    if(value < 100)
                    {
                        throw new Exception("Minimum amount that can be set is 100.");
                    }
                }

                if (!Enum.IsDefined(typeof(DiscountStatus), status))
                {
                    throw new InvalidCastException("Invalid Discount Status");
                }
                DiscountStatus discountStatus = (DiscountStatus)status;

                discount = new Discount()
                {
                    Status = discountStatus,
                    DateCreated = DateTime.Now,
                    Value = value,
                    Type = type,
                    DiscountType = discountTypeSelected,
                };

                _context.Discounts.Add(discount);
                _context.SaveChanges();
            }
            catch(Exception err)
            {
                throw err;
            }

            return discount;
        }

        public List<Discount> GetAllDiscounts()
        {
            return _context.Discounts.ToList();
        }

        public Discount GetDiscountByType(string type)
        {
            if (String.IsNullOrWhiteSpace(type))
            {
                throw new Exception("Type is not passed");
            }

            return _context.Discounts.FirstOrDefault(x => x.Type == type);
        }

        public decimal GetTotalInvoiceAmount(long customerId, decimal billAmount, bool isGrocery, long discountId = 0)
        {
            decimal totalInvoiceAmount = 0;

            try
            {
                if (billAmount <= 0)
                {
                    throw new Exception("Invalid Bill Amount Passed");
                }

                if (customerId <= 0)
                {
                    throw new Exception("Invalid Customer ID");
                }

                Customer customer = _context.Customers.FirstOrDefault(x => x.Id == customerId);
                if (customer == null)
                {
                    throw new Exception("No customer with this ID exists.");
                }

                bool isApplyingSpecificDiscount = false;
                Discount specificDiscountToApply = null;
                if (discountId > 0)
                {
                    Discount discount = _context.Discounts.FirstOrDefault(x => x.Id == discountId);
                    if (discount == null)
                    {
                        throw new Exception("Invalid Discount Applied");
                    }

                    if (discount.Status == DiscountStatus.Inactive)
                    {
                        throw new Exception("Discount is Inactive");
                    }

                    isApplyingSpecificDiscount = true;
                    specificDiscountToApply = discount;
                }

                decimal discountToApply = 0;
                decimal percentageDiscountToApply = 0;
                decimal variableDiscountToApply = 0;

                //Set Percentage Discount
                if (isApplyingSpecificDiscount)
                {
                    if(specificDiscountToApply.DiscountType == DiscountType.Percentage)
                    {
                        percentageDiscountToApply = specificDiscountToApply.Value;
                    }
                }
                else
                {
                    percentageDiscountToApply = (customer.CustomerType == CustomerType.Affiliate) ? 10 : (customer.CustomerType == CustomerType.Employee) ? 30 : ((DateTime.Now - customer.DateCreated).TotalDays >= 730) ? 5 : 0;
                }

                //Set Variable Discounts
                if (isApplyingSpecificDiscount)
                {
                    if(specificDiscountToApply.DiscountType == DiscountType.Fixed)
                    {
                        variableDiscountToApply = specificDiscountToApply.Value;
                    }
                }

                decimal calculate100billDiscount = Math.Floor(billAmount / 100);
                variableDiscountToApply = variableDiscountToApply + (calculate100billDiscount * 5);

                decimal percentageDiscountAmount = (percentageDiscountToApply / 100) * billAmount;
                totalInvoiceAmount = billAmount - (percentageDiscountAmount + variableDiscountToApply);
            }
            catch (Exception err)
            {
                throw err;
            }

            return totalInvoiceAmount;
        }
    }
}
