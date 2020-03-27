using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using test_app.Services;

namespace test_app.Core
{
    public class JsonSchemaValidator
    {
        private string validationSchema;

        public JsonSchemaValidator(string validationSchema)
        {
            this.validationSchema = validationSchema;
        }

        public bool Validate(string data)
        {
            bool result = false;

            return result;
        }
    }
}