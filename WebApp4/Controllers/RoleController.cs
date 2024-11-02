﻿using Microsoft.AspNetCore.Mvc;
using WebApp4.Models;
using WebApp4.Services;

namespace WebApp4.Controllers
{
    public class RoleController : Controller
    {
        private readonly IRoleService roleService;

        public RoleController(IRoleService roleService)
        {
            this.roleService = roleService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await roleService.GetAll();
            return View(result);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RoleDto roleDto)
        {
            var success = await roleService.Add(roleDto);

            if (!success)
            {
                TempData["message"] = "ไม่สำเร็จ";
                return View();
            }

            return RedirectToAction(nameof(Index));
        }


    }
}
