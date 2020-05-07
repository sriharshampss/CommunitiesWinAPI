# CommunitiesWinAPI

* [GetVendor]
  ### Usage
    https://localhost:44355/api/VendorDetails/GetVendor/9999999999
    Type: GET
    Returns: JSON
  
* [Vendor registration add or update]
  ### Usage
    https://localhost:44355/api/VendorDetails/RegisterVendor
    Type: POST
    Parameters: 
       * phoneNumber: 10 digits 
       * vendorName:  string
       * country: string/null
       * state: string/null
       * city: string/null
       * pin: string/null
       * latitude: decimal/null
       * longitude:decimal/null
   Note: if you want to update the existing one, just pass phone number and updated parameters
 
 ### Vendor corona precautions update Usage
    https://localhost:44355/api/VendorDetails/UpdateVendorCorona
    Type: POST
    Parameters: 
       * phoneNumber: 10 digits 
       * isFeverScreening:  bool/null
       * isSanitizerUsed: bool/null
       * isStampCheckDone: bool/null
       * isSocialDistanced: bool/null     
   Note: if you want to update the existing one, just pass phone number and updated parameters
    

 ### Vendor product prices update Usage
    https://localhost:44355/api/VendorDetails/VendorProductPrices
    Type: POST
    Parameters: 
       * phoneNumber: 10 digits 
       * productName:  string not null
       * price: decimal/null
       * minimumOrderQuantity: decimal/null
       * units: string/null     
   Note: if you want to update the existing one, just pass phone number and updated parameters
   
 
  ### Vendor category update Usage
    https://localhost:44355/api/VendorDetails/VendorCategory
    Type: POST
    Parameters: 
       * phoneNumber: 10 digits 
       * category:  string not null(new/old category)
       * enable:   true/false
   Note: if you want to update the existing one, just pass phone number and updated parameters
