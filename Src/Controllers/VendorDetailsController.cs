using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using CommunitiesWinApi.Context;
using CommunitiesWinApi.Models;

namespace CommunitiesWinApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendorDetailsController : ControllerBase
    {
        private readonly CommunitiesDBContext _context;

        private const string InvalidPhoneNumberMessage = "Invalid new phone number passed. Expecting phone number to be greater than zero and 10 digits in length.";
        
        public VendorDetailsController(CommunitiesDBContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Invoke the method based on the URL below:
        /// https://localhost:44355/api/VendorDetails/GetVendor/9999999999
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        [HttpGet("GetVendor/{phoneNumber}", Name = "GetVendorDetailsByPhone")]
        public JsonResult GetVendorDetailsByPhone(long phoneNumber)
        {
            var vendorDetail = _context.VendorDetails.FirstOrDefault(x => x.Phone == phoneNumber);
            if(vendorDetail == null)
            {
                throw new ArgumentException("Couldn't find a vendor profile with phone number: " + phoneNumber);
            }
            var VendorCategory = _context.VendorCategory.Where(x => x.VendorId == vendorDetail.VendorId).ToList();
            List<string> Category = new List<string>();
            foreach(var vendorCategory in VendorCategory)
            {
                var category = _context.Category.FirstOrDefault(x => x.CategoryId == vendorCategory.CategoryId);
                if (category != null)
                {
                    Category.Add(category.CategoryName);
                }
            }
            var vendorProducts = _context.VendorProduct.Where(x => x.VendorId == vendorDetail.VendorId).ToList();
            List<object> vendorProductsData = new List<object>();
            foreach (var vendorProduct in vendorProducts)
            {
                var product = _context.Product.FirstOrDefault(x => x.ProductId == vendorProduct.ProductId);
                if (product != null)
                {
                    vendorProductsData.Add(new
                    {
                        ProductName = product.ProductName,
                        MinOrderQuantity = vendorProduct.MinOrderQuantity,
                        Units = vendorProduct.Units,
                        Price = vendorProduct.Price
                    });
                }
            }
            var data = new 
            {
                VendorName = vendorDetail.VendorName,
                VendorPin = vendorDetail.Pin,
                VendorCity = vendorDetail.City,
                VendorCountry = vendorDetail.Country,
                VendorState = vendorDetail.State,
                VendorSantizier = vendorDetail.IsSanitizerUsed,
                VendorFeverScreening = vendorDetail.IsFeverScreen,
                VendorStampCheck = vendorDetail.IsStampCheck,
                VendorSocialDistanced = vendorDetail.IsSocialDistance,
                VendorLatitude = vendorDetail.Latitude,
                VendorLongitude = vendorDetail.Longitude,
                Category = Category,
                VendorProducts = vendorProductsData
            };
            return new JsonResult(data);
        }

        /// <summary>
        /// This method is used for new vendor registration. If we can't find phoneNumber in the database, create a new record. 
        /// else update the existing record with the new phone number.
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <param name="vendorName"></param>
        /// <param name="country"></param>
        /// <param name="state"></param>
        /// <param name="city"></param>
        /// <param name="pin"></param>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <param name="newPhoneNumber"></param>
        /// <returns></returns>
        [HttpPost("RegisterVendor", Name = "VendorRegistrationAddUpdate")]
        public ActionResult VendorRegistrationAddUpdate(long phoneNumber, string vendorName, string country, string state, string city, string pin, decimal? latitude, decimal? longitude)
        {
            if(phoneNumber <= 0 || phoneNumber.ToString().Length != 10)
            {
                throw new ArgumentException(InvalidPhoneNumberMessage);
            }
            if (string.IsNullOrEmpty(vendorName))
            {
                throw new ArgumentException("Invalid vendor name");
            }

            var existingVendorDetail = _context.VendorDetails.FirstOrDefault(x => x.Phone == phoneNumber);
            if(existingVendorDetail == null)
            {
                _context.VendorDetails.Add(new VendorDetails
                {
                    Phone = phoneNumber,
                    VendorName = vendorName,
                    Country = country,
                    City = city,
                    State = state,
                    Pin = pin,
                    Latitude = latitude,
                    Longitude = longitude
                });
            }
            else
            {
                existingVendorDetail.VendorName = vendorName;
                existingVendorDetail.Latitude = latitude;
                existingVendorDetail.Longitude = longitude;
                existingVendorDetail.City = city;
                existingVendorDetail.State = state;
                existingVendorDetail.Pin = pin;
                existingVendorDetail.Country = country;
            }
            _context.SaveChanges();
            return Ok();
        }

        [HttpPost("UpdateVendorCorona", Name = "UpdateVendorCoronaPrecautions")]
        public ActionResult UpdateVendorCoronaPrecautions(long phoneNumber, bool? isFeverScreening, bool? isSanitizerUsed, bool? isStampCheckDone, bool? isSocialDistanced)
        {
            if (phoneNumber <= 0 || phoneNumber.ToString().Length != 10)
            {
                throw new ArgumentException(InvalidPhoneNumberMessage);
            }

            var existingVendorDetail = _context.VendorDetails.FirstOrDefault(x => x.Phone == phoneNumber);
            if (existingVendorDetail == null)
            {
                throw new ArgumentException("Couldn't find an existing vendor detail entry. Please add the vendor details first.");
            }
            else
            {
                existingVendorDetail.IsFeverScreen = isFeverScreening;
                existingVendorDetail.IsSanitizerUsed = isSanitizerUsed;
                existingVendorDetail.IsStampCheck = isStampCheckDone;
                existingVendorDetail.IsSocialDistance = isSocialDistanced;
            }
            _context.SaveChanges();
            return Ok();
        }

        [HttpPost("VendorProductPrices", Name = "VendorProductPrices")]
        public ActionResult VendorProductPrices(long phoneNumber, string productName, decimal? price, decimal? minimumOrderQuantity, string units)
        {
            Product currentProduct;
            if (string.IsNullOrEmpty(productName))
            {
                throw new ArgumentException("Product name cannot be empty");
            }
            var vendorDetail = _context.VendorDetails.FirstOrDefault(x => x.Phone == phoneNumber);
            if (vendorDetail == null)
            {
                throw new ArgumentException("Couldn't find a vendor profile with phone number: " + phoneNumber);
            }

            var product = _context.Product.FirstOrDefault(x => x.ProductName.ToLower() == productName.ToLower());
            if (product == null)
            {
                currentProduct = new Product()
                {
                    ProductName = productName
                };
                _context.Product.Add(currentProduct);
                
            }
            else
            {
                currentProduct = product;
                currentProduct.ProductName = productName;
            }
            _context.SaveChanges();

            var vendorProductDetail = _context.VendorProduct.FirstOrDefault(x => x.VendorId == vendorDetail.VendorId);
            if (vendorProductDetail == null)
            {
                _context.VendorProduct.Add(new VendorProduct()
                {
                    VendorId = vendorDetail.VendorId,
                    ProductId = currentProduct.ProductId,
                    MinOrderQuantity = minimumOrderQuantity,
                    Price = price,
                    Units = units
                });
            }
            else
            {
                vendorProductDetail.ProductId = currentProduct.ProductId;
                vendorProductDetail.MinOrderQuantity = minimumOrderQuantity;
                vendorProductDetail.Price = price;
                vendorProductDetail.Units = units;
            }
            _context.SaveChanges();
            return Ok();
        }

        [HttpPost("VendorCategory", Name = "VendorCategory")]
        public ActionResult VendorCategory(long phoneNumber, string categoryName, bool? enable)
        {
            if (string.IsNullOrEmpty(categoryName))
            {
                throw new ArgumentException("Category name cannot be empty.");
            }
            var vendorDetail = _context.VendorDetails.FirstOrDefault(x => x.Phone == phoneNumber);
            if (vendorDetail == null)
            {
                throw new ArgumentException("Couldn't find a vendor profile with phone number: " + phoneNumber);
            }

            var category = _context.Category.FirstOrDefault(x => x.CategoryName.ToLower() == categoryName.ToLower());
            if (category == null)
            {
                category = new Category()
                {
                    CategoryName = categoryName
                };
                _context.Category.Add(category);
            }            
            _context.SaveChanges();

            var vendorCategory = _context.VendorCategory.FirstOrDefault(x => x.VendorId == vendorDetail.VendorId && x.CategoryId == category.CategoryId);
            if (vendorCategory == null)
            {
                _context.VendorCategory.Add(new VendorCategory()
                {
                    VendorId = vendorDetail.VendorId,
                    CategoryId = category.CategoryId,
                    IsActive = true
                });
            }
            else
            {
                if(enable.GetValueOrDefault())
                    throw new ArgumentException("There is an existing relation to category and vendor with category name: " + categoryName + " and its enabled.");
                else
                {
                    vendorCategory.IsActive = false;                    
                }
            }
            _context.SaveChanges();
            return Ok();
        }        
    }
}
