using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;

namespace API.ViewQuery
{
    public class UploadQuery
    {
        [FromBody]
        public List<IFormFile> Files { get; set; }

        public List<StreamReader> GetStreamReader()
        {
            List<StreamReader> sr = new List<StreamReader>();
            foreach (var file in Files)
            {
                sr.Add(new StreamReader(file.OpenReadStream()));
            }
            return sr;
        }
    }

}
