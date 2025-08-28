using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pustok.Data;
using Pustok.Models;

namespace Pustok.Areas.Manage.Controllers;

[Area("Manage")]
public class SettingController(AppDbContext context) : Controller
{
    public async Task<IActionResult> Index()
    {
        var settings = await context.Settings.ToListAsync();
        return View(settings);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Setting setting)
    {
        if (!ModelState.IsValid)
        {
            return View(setting);
        }

        if (context.Settings.Any(s => s.Key == setting.Key))
        {
            ModelState.AddModelError("Key", "This setting key already exists");
            return View(setting);
        }

        context.Settings.Add(setting);
        await context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var setting = await context.Settings.FindAsync(id);
        if (setting == null) return NotFound();
        return View(setting);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Setting setting)
    {
        if (id != setting.Id) return NotFound();

        if (!ModelState.IsValid)
        {
            return View(setting);
        }

        if (context.Settings.Any(s => s.Key == setting.Key && s.Id != id))
        {
            ModelState.AddModelError("Key", "This setting key already exists");
            return View(setting);
        }

        try
        {
            context.Update(setting);
            await context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!SettingExists(setting.Id))
            {
                return NotFound();
            }
            throw;
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var setting = await context.Settings.FindAsync(id);
        if (setting == null) return NotFound();

        context.Settings.Remove(setting);
        await context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool SettingExists(int id)
    {
        return context.Settings.Any(e => e.Id == id);
    }
}
