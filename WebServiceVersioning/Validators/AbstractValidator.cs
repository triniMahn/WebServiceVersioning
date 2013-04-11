using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebServiceVersioning.Validators
{
    public abstract class AbstractValidator
    {
        protected AbstractValidator validator = null;

        protected abstract List<int> validateFields(WebServiceVersioning.Service.WSCustomerInfo custInfo);
        
        
        public List<int> isValid(WebServiceVersioning.Service.WSCustomerInfo custInfo)
        {
            if (null != validator)
            {
                List<int> errors = validateFields(custInfo);
                errors.AddRange(validator.isValid(custInfo));
                return errors;
            }

            return validateFields(custInfo);
        }
    }

    public class WSCustomerInfoValidatorV1 : AbstractValidator
    {
        
        public static System.Text.RegularExpressions.Regex postalExCDN = new System.Text.RegularExpressions.Regex(@"[a-zA-Z]\d[a-zA-Z][ ]?\d[a-zA-Z]\d");
        public static System.Text.RegularExpressions.Regex postalExUS = new System.Text.RegularExpressions.Regex(@"\d{5}(-\d{4})?");

        public WSCustomerInfoValidatorV1() { }
        
        protected override List<int> validateFields(WebServiceVersioning.Service.WSCustomerInfo custInfo)
        {
            List<int> errors = new List<int>(5);

            if (custInfo.Country.Trim().ToUpper().Equals("CA") && false == postalExCDN.Match(custInfo.PostalCode.Trim()).Success)
                errors.Add(2); //Postal code invalid for Country

            if (custInfo.Country.Trim().ToUpper().Equals("US") && false == postalExUS.Match(custInfo.PostalCode.Trim()).Success)
                errors.Add(2); //Postal code invalid for Country

            //validate donor name if not corporate donation
            if (true == string.IsNullOrEmpty(custInfo.CompanyName))
            {
                if (true == string.IsNullOrEmpty(custInfo.FirstName) || true == string.IsNullOrEmpty(custInfo.LastName))
                    errors.Add(4);
            }

            if (true == string.IsNullOrEmpty(custInfo.FirstName) && true == string.IsNullOrEmpty(custInfo.LastName) && true == string.IsNullOrEmpty(custInfo.CompanyName))
                errors.Add(5);

            /*
            //validate donorID
            //return 6 if invalid account assigning donorID
            if (this.DonorID != 0 && accountType!=(int)APIAccountType.CANADAHELPS)
                errorCode = 6;
            */

            return errors;

        }
    }

    public class WSCusomterInfoValidatorV2 : AbstractValidator
    {
        public static System.Text.RegularExpressions.Regex emailEx = new System.Text.RegularExpressions.Regex(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");

        
        public WSCusomterInfoValidatorV2(AbstractValidator validator)
        {
            this.validator = validator;
        }
        
        protected override List<int> validateFields(WebServiceVersioning.Service.WSCustomerInfo custInfo)
        {
            List<int> errors = new List<int>(5);
            WebServiceVersioning.v2.Service.WSCustomerInfo info = (WebServiceVersioning.v2.Service.WSCustomerInfo)custInfo;

            if (emailEx.Match(info.EmailAddress).Success == false)
                errors.Add(1); //Email address format invalid

            return errors;
        }
    }

    public class WSCusomterInfoValidatorV3 : AbstractValidator
    {
        
        public WSCusomterInfoValidatorV3(AbstractValidator validator)
        {
            this.validator = validator;
        }

        protected override List<int> validateFields(WebServiceVersioning.Service.WSCustomerInfo custInfo)
        {
            List<int> errors = new List<int>(5);
            WebServiceVersioning.v3.Service.WSCustomerInfo info = (WebServiceVersioning.v3.Service.WSCustomerInfo)custInfo;

            if (true == string.IsNullOrEmpty(info.SIN))
                errors.Add(6); //Email address format invalid

            return errors;
        }
    }



}