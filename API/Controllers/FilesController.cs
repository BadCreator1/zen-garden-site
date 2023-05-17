using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using static System.Net.Mime.MediaTypeNames;
namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FilesController : ControllerBase
    {
        private readonly ILogger<FilesController> _logger;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment;
        private readonly IConfiguration _config;

        public FilesController(ILogger<FilesController> logger,
        Microsoft.AspNetCore.Hosting.IHostingEnvironment environment,
        IConfiguration config)
        {
            _config = config;
            _environment = environment;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult> UploadImage()
        {
            var file = Request.Form.Files[0];
            var folderName = "images";
            var pathToSave = Path.Combine(_environment.WebRootPath, folderName);
            if (file.Length > 0)
            {

                var fileName =ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                var fullPath = Path.Combine(pathToSave, fileName);
                var resultPath = _config["ApiUrl"] + Path.Combine(folderName, fileName).Replace(@"\\", @"/");
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                return Ok(new {  resultPath });
            }
            else
            {
                return BadRequest();
            }
        }


    }
}