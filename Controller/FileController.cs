
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using bookingtaxi_backend.Model;

using static System.Net.Mime.MediaTypeNames;
using Image = bookingtaxi_backend.Model.Image;

namespace bookingtaxi_backend.Controller;


[ApiController]
[Route("api/[controller]")]
public class FileController : ControllerBase
{
    // string path = "..\\Files\\Images";
    string path =  String.Format("Files{0}Images", Path.DirectorySeparatorChar);

    [HttpPost("ImageUpload")]
    public async Task<IActionResult> PostImage([FromForm] Image image) {
        try
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string filePath = path + Path.DirectorySeparatorChar + image.Name;
            using (FileStream filestream = System.IO.File.Create(filePath))
            {
                image.File!.CopyTo(filestream);
                filestream.Flush();
            }

            return Ok();
        }
        catch (Exception ex)
        {
        }
        return BadRequest(new ErrorResponse("Issue uploading image"));
    }

    [HttpGet("ImageDownload/{fileName}")]
    public async Task<IActionResult> GetImage(String fileName)
    {

        try
        {
            string filePath = path + Path.DirectorySeparatorChar + fileName;
            if (System.IO.File.Exists(filePath))
            {
                var bytes = await System.IO.File.ReadAllBytesAsync(filePath);
                return File(bytes, "text/plain", Path.GetFileName(filePath));
            }

            return NotFound();
        }
        catch (Exception)
        {
        }
        return BadRequest(new ErrorResponse("Issue downloading image"));
    }

    [HttpGet("logo")]
    public async Task<IActionResult> GetLogo()
    {
        try
        {
            string filePath = path + Path.DirectorySeparatorChar + "booking_taxi_logo_full_large.png";

            if (System.IO.File.Exists(filePath))
            {
                var bytes = await System.IO.File.ReadAllBytesAsync(filePath);
                return File(bytes, "text/plain", Path.GetFileName(filePath));
            }

            return NotFound();
        }
        catch (Exception)
        {
        }
        return BadRequest(new ErrorResponse("Issue downloading image"));
    }
}