using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace netcore_phantomjs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<string> Get()
        {
            var escapedArgs = "dotnet --version";

            var process = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "/bin/bash",
                    Arguments = $"-c \"{escapedArgs}\"",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                }
            };
            process.Start();
            string result = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            System.Console.WriteLine(result);

            return result;
        }

        // POST api/values
        [HttpPost]
        public ActionResult<string> Post([FromBody] InputModel input)
        {            
            var folder_os = "linux";
            var size_heigh = "";

            if(System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(OSPlatform.Windows)){
                folder_os = "win";
            } else if(System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(OSPlatform.OSX)){
                folder_os = "osx";
            }

            if(!String.IsNullOrEmpty(input.size_heigh)) {
                size_heigh = "*"+input.size_heigh+"px";
            }

            var escapedArgs = "$PWD/phantomjs/"+folder_os+"/phantomjs $PWD/phantomjs/rasterize.js "+input.url+" $PWD/output/"+input.filename+" "+input.size_width+"px"+size_heigh;
            
            var process = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "/bin/bash",
                    Arguments = $"-c \"{escapedArgs}\"",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                }
            };
            process.Start();
            string result = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            System.Console.WriteLine(result);

            return result;
        }

    }

    public class InputModel {
        public string url { get; set;}
        public string filename { get; set;}
        public string size_width { get; set;}
        public string size_heigh { get; set;}

        public InputModel()
        { 
            long timenow = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

            filename = "screenshot-"+timenow+".png";
            size_width = "1280";
        }

    }
}
