using FluentValidation.Results;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Integrador.Pix.Santander.Rest.Models
{
    public abstract class AbstractModel
    {
        public virtual string ToJson(bool idented = false)
        {
            var identedMode = idented ? Newtonsoft.Json.Formatting.Indented : Newtonsoft.Json.Formatting.None;
            string jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(this, identedMode);
            return jsonData;
        }

        [JsonIgnore]
        public ValidationResult ValidationResult { get; set; }
        public virtual bool EhValido()
        {
            throw new NotImplementedException();
        }

        public virtual IEnumerable<string> ObterErros()
        {
            var errors = ValidationResult.Errors.Select(e => e.ErrorMessage);
            return errors;
        }
    }
}
