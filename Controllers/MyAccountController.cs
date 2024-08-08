﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProductApp.Dto;
using ProductApp.Models;

[Authorize]
public class MyAccountController : Controller
{
    private readonly UserManager<AppUser> _userManager;

    public MyAccountController(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var values = await _userManager.FindByNameAsync(User.Identity.Name);
        AppUserEditDto appUserEditDto = new AppUserEditDto
        {
            FirstName = values.FirstName,
            LastName = values.LastName,
            PhoneNumber = values.PhoneNumber,
            Email = values.Email,
            City = values.City
        };
        return View(appUserEditDto);
    }

    [HttpPost]
    public async Task<IActionResult> Index(AppUserEditDto appUserEditDto)
    {
        if (appUserEditDto.Password == appUserEditDto.ConfirmPassword)
        {
            var user = await _userManager.FindByEmailAsync(appUserEditDto.Email);
            user.FirstName = appUserEditDto.FirstName;
            user.LastName = appUserEditDto.LastName;
            user.City = appUserEditDto.City;
            user.PhoneNumber = appUserEditDto.PhoneNumber;
            user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, appUserEditDto.Password);
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
        }
        return View(appUserEditDto);
    }

}
