﻿using bookingtaxi_backend.Model;
using bookingtaxi_backend.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace bookingtaxi_backend.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class DriverPropertyController : ControllerBase
    {
        private readonly DriverPropertiesService _driverPropertiesServices;

        public DriverPropertyController(DriverPropertiesService driverPropertiesServices)
        {
            _driverPropertiesServices = driverPropertiesServices;
        }

        [Authorize]       
        [HttpPost("DocumentationImage")]
        public async Task<IActionResult> PostDocumentationImage(DocumentationImage obj)
        {
            DocumentationImage createdObj = await _driverPropertiesServices.CreateDocumentationImage(obj);
            return CreatedAtAction("PostDocumentationImage", createdObj);
        }

        [Authorize]
        [HttpDelete("DocumentationImage")]
        public async Task<IActionResult> DeleteAccount(String id)
        {         
            await _driverPropertiesServices.DeleteDocumentation(id);
            return Ok();
        }

        [Authorize]       
        [HttpGet("GetAllDocumentationImages")]
        public async Task<List<DocumentationImage>> GetAllDocumentationImages(string driverID)
        {
            return await _driverPropertiesServices.GetAllDocumentationImages(driverID);
        }

        [Authorize]
        [HttpGet("GetDocumentationImage")]
        public async Task<DocumentationImage?> GetDocumentationImage(string id)
        {
            return await _driverPropertiesServices.GetDocumentationImage(id);
        }

        [Authorize]
        [HttpGet("GetDriverCar")]
        public async Task<DriverCar?> GetDriverCar(string driverID)
        {
            return await _driverPropertiesServices.GetDriverCar(driverID);
        }

        [Authorize]
        [HttpPost("DriverCar")]
        public async Task<IActionResult> PostDriverCar(DriverCar obj)
        {
            DriverCar createdObj = await _driverPropertiesServices.CreateDriverCar(obj);
            return CreatedAtAction("PostDriverCar", createdObj);
        }

        [Authorize]
        [HttpDelete("DriverCar")]
        public async Task<IActionResult> DeleteDriverCar(String id)
        {
            await _driverPropertiesServices.DeleteDriverCar(id);
            return Ok();
        }

        [Authorize]
        [HttpGet("DriverCar")]
        public async Task<IActionResult> GetDriverCar(DriverCar obj)
        {
            await _driverPropertiesServices.UpdateDriverCar(obj);
            return Ok();
        }
    }
}
